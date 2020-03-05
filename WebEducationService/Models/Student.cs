using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebEducationService.Models {
    public class Student {
        
        public int Id { get; set; }
        [StringLength (30)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(30)]
        [Required]
        public string LastName { get; set; }
        public decimal GPA { get; set; }
        public int SAT { get; set; }
        public int ? MajorId { get; set; }

        public virtual Major major { get; set; } //study this and why

        public Student() { }
    }
}
