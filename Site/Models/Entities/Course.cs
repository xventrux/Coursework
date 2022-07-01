using Site.Models.Entities.Base;
using System;
using System.Collections.Generic;

namespace Site.Models.Entities
{
    public class Course : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Hours { get; set; }
        
        public User Teacher { get; set; }
        public string TeacherId { get; set; }

        public Group Group { get; set; }
        public Guid GroupId { get; set; }

        public List<StudentStatistic> Students { get; set; }

        public double MaxScore { get; set; }

        public List<EducationalMaterial> Materials { get; set; }

    }
}
