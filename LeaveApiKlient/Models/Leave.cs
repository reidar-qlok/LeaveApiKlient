using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveApiClient.Models
{
    public enum LeaveStatus
    {
        //0,1,2
        Pending, Approved, Denied
    }
    public class Leave
    {
        [Key]
        public int LeaveId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
        [ForeignKey("Employee")]
        public int FkEmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
