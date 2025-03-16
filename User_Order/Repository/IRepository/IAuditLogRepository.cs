using User_Order.Model;

namespace User_Order.Repository.IRepository
{
    public interface IAuditLogRepository
    {
        Task AddAsync(AuditLog auditLog);

        Task<IEnumerable<AuditLog>> GetAllAsync();
    }
}
