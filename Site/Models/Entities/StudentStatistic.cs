using Site.Models.Entities.Base;
using System;

namespace Site.Models.Entities
{
    public class StudentStatistic : EntityBase
    {
        public User Student { get; set; }
        public string StudentId { get; set; }

        public double Score { get; set; }

        public Course Course { get; set; }
        public Guid? CourseId { get; set; }

    }
}
