using CoreLMS.Core.Interfaces;
using CoreLMS.Core.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLMS.Core.Entities
{
    public class CourseLesson : IAuditableEntity
    {
        public CourseLesson()
        {
            this.CourseLessonAttachments = new HashSet<CourseLessonAttachment>();
            this.Authors = new HashSet<AuthorCourseLesson>();
        }

        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }        

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public LessonType LessonType { get; set; }
        public VideoSourceType VideoSourceType { get; set; }
        public string VideoSourceCode { get; set; }

        public ICollection<CourseLessonAttachment> CourseLessonAttachments { get; set; }

        public ICollection<AuthorCourseLesson> Authors { get; set; }
    }
}
