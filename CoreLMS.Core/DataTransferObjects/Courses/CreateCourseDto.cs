using CoreLMS.Core.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreLMS.Core.DataTransferObjects
{
    public class CreateCourseDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public CourseType CourseType { get; set; }
        public string CourseImageURL { get; set; }
    }
}
