using CoreLMS.Core.Interfaces;
using CoreLMS.Core.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLMS.Core.Entities
{
    public class Course : IAuditableEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsDeleted { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public CourseType CourseType { get; set; }
        public string CourseImageURL { get; set; }

    }
}
