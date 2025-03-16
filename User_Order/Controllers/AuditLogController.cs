//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using User_Order.Repository.IRepository;
using User_Order.Service.IService;

namespace User_Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuditLogs()
        {
            var auditLogs = await _auditLogService.GetAllAuditLogAsync();
            if(auditLogs==null || !auditLogs.Any())
                return NotFound(new {error="No AuditLog Data"});
            return Ok(auditLogs);
        }
    }
}
