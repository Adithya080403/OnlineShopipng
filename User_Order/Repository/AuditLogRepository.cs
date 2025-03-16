using Microsoft.EntityFrameworkCore;
using User_Order.Data;
using User_Order.Model;
using User_Order.Repository.IRepository;

namespace User_Order.Repository
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly ApplicationDbContext _context;

        public AuditLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(AuditLog auditLog)
        {
            await _context.AuditLogs.AddAsync(auditLog);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetAllAsync()
        {
            var auditLogs = await _context.AuditLogs.ToListAsync();
            return auditLogs;
        }
    }
}
