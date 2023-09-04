using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorManagement.Domain.Common;
using VisitorManagement.Infrastructure.Data;

namespace VisitorManagement.Infrastructure.Repositories
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly VisitorManagementApplicationContext _context;

        public VisitorRepository(VisitorManagementApplicationContext context)
        {
            _context = context;
        }

        public async Task<Visitor> CreateAsync(Visitor visitor)
        {
            _context.Visitors.Add(visitor);
            await _context.SaveChangesAsync();
            return visitor;
        }

        public async Task<Visitor> GetVisitorByIdAsync(int visitorId)
        {
            return await _context.Visitors.FindAsync(visitorId);
        }

        public async Task<IEnumerable<Visitor>> GetAllVisitorsByHostIdOnSpecificCheckInDateAsync(int hostId, DateTime checkInDate)
        {
            return await _context.Visitors
                .Where(v => v.HostId == hostId && v.ExpectedArrivalTime.Date == checkInDate.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Visitor>> GetAllVisitorsAsync()
        {
            return await _context.Visitors.ToListAsync();
        }

        public async Task<IEnumerable<Visitor>> GetAllPreRegisterVisitorsAsync()
        {
            return await _context.Visitors
                .Where(v => v.PreRegisterVisitor)
                .ToListAsync();
        }

        public async Task<IEnumerable<Visitor>> GetAllOnSiteRegisterVisitorsAsync()
        {
            return await _context.Visitors
                .Where(v => v.OnSiteRegisterVisitor)
                .ToListAsync();
        }

        public async Task UpdateAsync(Visitor visitor)
        {
            _context.Entry(visitor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Visitor visitor)
        {
            _context.Visitors.Remove(visitor);
            await _context.SaveChangesAsync();
        }
    }
}
