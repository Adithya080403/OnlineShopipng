using User_Order.Model;
using User_Order.Repository.IRepository;
using User_Order.Service.IService;

namespace User_Order.Service
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLogService(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }
        public async Task AddAuditLogAsync(string entityName, string entityId, string operation, string userId, string userName, string details = " ")
        {
            var auditLog = new AuditLog
            {
                EntityName = entityName,
                EntityId = entityId,
                Operation = operation,
                UserId = userId,
                UserName = userName,
                OperationDate = DateTime.Now,
                Details = details,
            };
            await _auditLogRepository.AddAsync(auditLog);
        }

        public async Task<IEnumerable<AuditLog>> GetAllAuditLogAsync()
        {
            var auditLogs = await _auditLogRepository.GetAllAsync();
            return auditLogs ?? [];
        }
    }
}
