using Site.Models.Entities.Base;
using System;
using System.Collections.Generic;

namespace Site.Models.Entities
{
    /// <summary>
    /// Факультет
    /// </summary>
    public class Faculty : EntityBase
    {
        /// <summary>
        /// Название факультета
        /// </summary>
        public string Name { get; set; }
        public List<DirectionOfTraining> DirectionOfTrainings { get; set; }

        public StructuralDivision StructuralDivision { get; set; }
        public Guid StructuralDivisionId { get; set; }
    }
}
