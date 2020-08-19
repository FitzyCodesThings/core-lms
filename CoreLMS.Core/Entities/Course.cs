using CoreLMS.Core.Interfaces;
using CoreLMS.Core.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreLMS.Core.Entities
{
    public class Course : IAuditableEntity
    {
        public Course()
        {
            this.CourseLessons = new HashSet<CourseLesson>();
        }

        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }        
        
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public CourseType CourseType { get; set; }
        public string CourseImageURL { get; set; }

        public ICollection<CourseLesson> CourseLessons { get; set; }

    }
}
