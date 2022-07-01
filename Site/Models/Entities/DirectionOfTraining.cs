using Site.Models.Entities.Base;
using System;
using System.Collections.Generic;

namespace Site.Models.Entities
{
    public class DirectionOfTraining : EntityBase
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public List<Group> Groups { get; set; }

        public Faculty Faculty { get; set; }
        public Guid? FacultyId { get; set; }

        public StructuralDivision StructuralDivision { get; set; }
        public Guid? StructuralDivisionId { get; set; }
    }
}
