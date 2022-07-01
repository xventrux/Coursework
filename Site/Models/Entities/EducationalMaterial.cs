using Site.Models.Entities.Base;
using System;

namespace Site.Models.Entities
{
    public class EducationalMaterial : EntityBase
    {
        public int Num { get; set; }
        public string Name { get; set; }
        public string HTML { get; set; }

        public Course Course { get; set; }
        public Guid CourseId { get; set; }
    }
}
