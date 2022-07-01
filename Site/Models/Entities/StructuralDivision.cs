using Site.Models.Entities.Base;
using System.Collections.Generic;

namespace Site.Models.Entities
{
    /// <summary>
    /// Структурное подразделение
    /// </summary>
    public class StructuralDivision : EntityBase
    {
        /// <summary>
        /// Название структурного подразделения
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Есть ли факультеты
        /// </summary>
        public bool IsThereAreFaculties { get; set; }

        public List<Faculty> Faculties { get; set; }
        public List<DirectionOfTraining> DirectionOfTrainings { get; set; }
        
    }
}
