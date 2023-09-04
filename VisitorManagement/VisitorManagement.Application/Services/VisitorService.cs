using AutoMapper;
using VisitorManagement.Application.DTOs;
using VisitorManagement.Domain.Common;
using VisitorManagement.Domain.Exceptions;
using VisitorManagement.Infrastructure.Repositories;

namespace VisitorManagement.Application.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IVisitorRepository _visitorRepository;
        private readonly IMapper _mapper;

        public VisitorService(IVisitorRepository visitorRepository, IMapper mapper)
        {
            _visitorRepository = visitorRepository;
            _mapper = mapper;
        }

        public async Task<VisitorDTO> CreateVisitorAsync(VisitorDTO visitorDTO)
        {
            var visitor = _mapper.Map<Visitor>(visitorDTO);

            // Calculate VisitDuration
            visitor.VisitDuration = visitorDTO.ExpectedCheckOut - visitorDTO.ExpectedArrival;
            var createdVisitor = await _visitorRepository.CreateAsync(visitor);

            // Map the calculated VisitDuration back to the DTO -- To return immediately in json
            visitorDTO.VisitDuration = createdVisitor.VisitDuration;

            // Update the visitorDTO with the generated visitorId
            visitorDTO.Id = createdVisitor.VisitorId;

            return visitorDTO;
        }

        public async Task<VisitorDTO> GetVisitorByVisitorIdAsync(int visitorId)
        {
            var visitor = await _visitorRepository.GetVisitorByIdAsync(visitorId);

            if (visitor == null)
            {
                throw new VisitorNotFoundException($"Visitor with ID {visitorId} not found");
            }

            return _mapper.Map<VisitorDTO>(visitor);
        }

        public async Task<IEnumerable<VisitorDTO>> GetAllVisitorsByHostIdOnSpecificCheckInDateAsync(int hostId, DateTime checkInDate)
        {
            var visitors = await _visitorRepository.GetAllVisitorsByHostIdOnSpecificCheckInDateAsync(hostId, checkInDate);
            return _mapper.Map<IEnumerable<VisitorDTO>>(visitors);
        }

        public async Task<IEnumerable<VisitorDTO>> GetAllVisitorsAsync()
        {
            var visitors = await _visitorRepository.GetAllVisitorsAsync();
            return _mapper.Map<IEnumerable<VisitorDTO>>(visitors);
        }

        public async Task<IEnumerable<VisitorDTO>> GetAllPreRegisterVisitorsAsync()
        {
            var preRegisterVisitors = await _visitorRepository.GetAllPreRegisterVisitorsAsync();
            return _mapper.Map<IEnumerable<VisitorDTO>>(preRegisterVisitors);
        }

        public async Task<IEnumerable<VisitorDTO>> GetAllOnSiteRegisterVisitorsAsync()
        {
            var onSiteRegisterVisitors = await _visitorRepository.GetAllOnSiteRegisterVisitorsAsync();
            return _mapper.Map<IEnumerable<VisitorDTO>>(onSiteRegisterVisitors);
        }

        public async Task UpdateVisitorAsync(int visitorId, VisitorDTO visitorDTO)
        {
            var existingVisitor = await _visitorRepository.GetVisitorByIdAsync(visitorId);

            if (existingVisitor == null)
            {
                throw new VisitorNotFoundException($"Visitor with ID {visitorId} not found");
            }

            // Update properties based on DTO
            var visitorType = typeof(Visitor);
            var visitorDTOType = typeof(VisitorDTO);

            foreach (var property in visitorDTOType.GetProperties())
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(visitorDTO, null);

                if (propertyName != "Id" && propertyValue != null)
                {
                    var visitorProperty = visitorType.GetProperty(propertyName);

                    if (visitorProperty != null)
                    {
                        visitorProperty.SetValue(existingVisitor, propertyValue);
                    }
                }
            }

            await _visitorRepository.UpdateAsync(existingVisitor);
        }


        public async Task DeleteVisitorAsync(int visitorId)
        {
            var existingVisitor = await _visitorRepository.GetVisitorByIdAsync(visitorId);

            if (existingVisitor == null)
            {
                throw new VisitorNotFoundException($"Visitor with ID {visitorId} not found");
            }

            await _visitorRepository.DeleteAsync(existingVisitor);
        }

        public async Task UpdateVisitorImageAsync(int visitorId, VisitorDTO visitorDTO)
        {
            var existingVisitor = await _visitorRepository.GetVisitorByIdAsync(visitorId);

            if (existingVisitor == null)
            {
                throw new VisitorNotFoundException($"Visitor with ID {visitorId} not found");
            }

            existingVisitor.VisitorImage = visitorDTO.VisitorImage;

            await _visitorRepository.UpdateAsync(existingVisitor);
        }

    }
}
