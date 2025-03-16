using System.ComponentModel.DataAnnotations;

namespace User_Order.Model
{
    public class AuditLog
    {
        [Key]
        public int AuditId { get; set; }

        public string? EntityName { get; set; }

        public string? EntityId { get; set; }
        public string? Operation { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime OperationDate { get; set; }
        public string? Details { get; set; }
        
    }
}
