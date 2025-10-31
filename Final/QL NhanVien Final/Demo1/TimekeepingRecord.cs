using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo1
{
    public class TimekeepingRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public DateTime Date { get; set; }  // Ngày chấm công

        public DateTime? CheckInTime { get; set; }

        public DateTime? CheckOutTime { get; set; }
    }
}
