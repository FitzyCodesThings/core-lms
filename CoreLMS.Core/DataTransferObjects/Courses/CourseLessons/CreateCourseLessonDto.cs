using CoreLMS.Core.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreLMS.Core.DataTransferObjects
{
    public class CreateCourseLessonDto
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public LessonType LessonType { get; set; }
        public VideoSourceType VideoSourceType { get; set; }
        public string VideoSourceCode { get; set; }

        // TODO Add Authors to CourseLessonDtos
        //public ICollection<AuthorDto> Authors { get; set; }
    }
}
