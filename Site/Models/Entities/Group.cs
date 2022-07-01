using Site.Models.Entities.Base;
using System;
using System.Collections.Generic;

namespace Site.Models.Entities
{
    /// <summary>
    /// Учебная группа
    /// </summary>
    public class Group : EntityBase
    {
        public string Name { get; set; }

        public List<User> Students { get; set; }

        public DirectionOfTraining DirectionOfTraining { get; set; }
        public Guid DirectionOfTrainingId { get; set; }

        public List<Course> Courses { get; set; }
    }
}
