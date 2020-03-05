using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebEducationService.Models {
    public class StudentClassRel {

        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        // public int ClassGradeValueId { get; set; }

        public virtual Student student { get; set; }

        public virtual Class Class { get; set; }


        public StudentClassRel() { }
        
    }
}   
