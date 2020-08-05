using CoreLMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLMS.Core.Entities
{
    public class Author : IAuditableEntity
    {
        public Author()
        {
            this.CourseLessons = new HashSet<AuthorCourseLesson>();
        }

        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }        

        public int PersonID { get; set; }
        public Person Person { get; set; }

        public string Description { get; set; }
        public string WebsiteURL { get; set; }

        public ICollection<AuthorCourseLesson> CourseLessons { get; set; }
    }
}
