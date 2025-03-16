using User_Order.Model;

namespace User_Order.Service.IService
{
    public interface IAuditLogService
    {
        Task AddAuditLogAsync(string entityName, string entityId, string operation, string userId, string userName, string details = " ");

        Task<IEnumerable<AuditLog>> GetAllAuditLogAsync();
    }
}
