using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorManagement.Domain.Common;

namespace VisitorManagement.Infrastructure.Repositories
{
    public interface IVisitorRepository
    {
        Task<Visitor> CreateAsync(Visitor visitor);
        Task<Visitor> GetVisitorByIdAsync(int visitorId);
        Task<IEnumerable<Visitor>> GetAllVisitorsByHostIdOnSpecificCheckInDateAsync(int hostId, DateTime checkInDate);
        Task<IEnumerable<Visitor>> GetAllVisitorsAsync();
        Task<IEnumerable<Visitor>> GetAllPreRegisterVisitorsAsync();
        Task<IEnumerable<Visitor>> GetAllOnSiteRegisterVisitorsAsync();
        Task UpdateAsync(Visitor visitor);
        Task DeleteAsync(Visitor visitor);
    }
}
