using VisitorManagement.Application.DTOs;

namespace VisitorManagement.Application.Services
{
    public interface IVisitorService
    {
        Task<VisitorDTO> CreateVisitorAsync(VisitorDTO visitorDTO);
        Task<VisitorDTO> GetVisitorByVisitorIdAsync(int visitorId);
        Task<IEnumerable<VisitorDTO>> GetAllVisitorsByHostIdOnSpecificCheckInDateAsync(int hostId, DateTime checkInDate);
        Task<IEnumerable<VisitorDTO>> GetAllVisitorsAsync();
        Task<IEnumerable<VisitorDTO>> GetAllPreRegisterVisitorsAsync();
        Task<IEnumerable<VisitorDTO>> GetAllOnSiteRegisterVisitorsAsync();
        Task UpdateVisitorAsync(int visitorId, VisitorDTO visitorDTO);
        Task DeleteVisitorAsync(int visitorId);
        Task UpdateVisitorImageAsync(int visitorId, VisitorDTO visitorDTO);

    }
}