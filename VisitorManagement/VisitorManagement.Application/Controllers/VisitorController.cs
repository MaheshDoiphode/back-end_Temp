using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorManagement.Application.DTOs;
using VisitorManagement.Application.Services;
using VisitorManagement.Domain.Common;
using VisitorManagement.Domain.Exceptions;

namespace VisitorManagement.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorService _visitorService;

        public VisitorController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        [HttpPost]
        public async Task<ActionResult<VisitorDTO>> CreateVisitorAsync(VisitorDTO visitorDTO)
        {
            try
            {
                var createdVisitorDTO = await _visitorService.CreateVisitorAsync(visitorDTO);
                return Ok(createdVisitorDTO);
            }
            catch (VisitorServiceException ex)
            {
                return BadRequest($"Failed to create visitor: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while creating the visitor: {ex.Message}");
            }
        }

        [HttpGet("{visitorId}")]
        public async Task<ActionResult<VisitorDTO>> GetVisitorByIdAsync(int visitorId)
        {
            try
            {
                var visitorDTO = await _visitorService.GetVisitorByVisitorIdAsync(visitorId);
                return Ok(visitorDTO);
            }
            catch (VisitorNotFoundException ex)
            {
                return NotFound($"Visitor with ID {visitorId} not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while retrieving the visitor: {ex.Message}");
            }
        }

        [HttpGet("host/{hostId}/checkin/{checkInDate}")]
        public async Task<ActionResult<IEnumerable<VisitorDTO>>> GetAllVisitorsByHostIdOnSpecificCheckInDateAsync(int hostId, DateTime checkInDate)
        {
            try
            {
                var visitors = await _visitorService.GetAllVisitorsByHostIdOnSpecificCheckInDateAsync(hostId, checkInDate);
                return Ok(visitors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while retrieving visitors: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitorDTO>>> GetAllVisitorsAsync()
        {
            try
            {
                var visitors = await _visitorService.GetAllVisitorsAsync();
                return Ok(visitors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while retrieving visitors: {ex.Message}");
            }
        }

        [HttpGet("preregister")]
        public async Task<ActionResult<IEnumerable<VisitorDTO>>> GetAllPreRegisterVisitorsAsync()
        {
            try
            {
                var preRegisterVisitors = await _visitorService.GetAllPreRegisterVisitorsAsync();
                return Ok(preRegisterVisitors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while retrieving pre-registered visitors: {ex.Message}");
            }
        }

        [HttpGet("onsiteregister")]
        public async Task<ActionResult<IEnumerable<VisitorDTO>>> GetAllOnSiteRegisterVisitorsAsync()
        {
            try
            {
                var onSiteRegisterVisitors = await _visitorService.GetAllOnSiteRegisterVisitorsAsync();
                return Ok(onSiteRegisterVisitors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while retrieving on-site registered visitors: {ex.Message}");
            }
        }

        [HttpPut("{visitorId}")]
        public async Task<ActionResult> UpdateVisitorAsync(int visitorId, VisitorDTO visitorDTO)
        {
            try
            {
                await _visitorService.UpdateVisitorAsync(visitorId, visitorDTO);
                return NoContent();
            }
            catch (VisitorNotFoundException ex)
            {
                return NotFound($"Visitor with ID {visitorId} not found: {ex.Message}");
            }
            catch (VisitorServiceException ex)
            {
                return BadRequest($"Failed to update visitor: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while updating the visitor: {ex.Message}");
            }
        }

        [HttpDelete("{visitorId}")]
        public async Task<ActionResult> DeleteVisitorAsync(int visitorId)
        {
            try
            {
                await _visitorService.DeleteVisitorAsync(visitorId);
                return NoContent();
            }
            catch (VisitorNotFoundException ex)
            {
                return NotFound($"Visitor with ID {visitorId} not found: {ex.Message}");
            }
            catch (VisitorServiceException ex)
            {
                return BadRequest($"Failed to delete visitor: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while deleting the visitor: {ex.Message}");
            }
        }


        [HttpPost("upload-image")]
        public async Task<ActionResult> UploadImage(int visitorId, [FromForm] IFormFile imageFile)
        {
            try
            {
                if (imageFile == null || imageFile.Length == 0)
                {
                    return BadRequest("No image file provided.");
                }

                // Convert the uploaded image to a byte array
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();

                    // Update the VisitorDTO with the image data
                    var visitorDTO = new VisitorDTO
                    {
                        VisitorImage = imageBytes
                    };

                    // Save the image data in the database by calling your service method
                    await _visitorService.UpdateVisitorImageAsync(visitorId, visitorDTO);

                    return Ok("Image uploaded and saved successfully.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An unexpected error occurred while uploading the image: {ex.Message}");
            }
        }


}
}
