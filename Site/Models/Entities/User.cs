using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Models.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string MiddleName { get; set; }
        public Group Group { get; set; }
        public Guid? GroupId { get; set; }

        public bool IsConfirmed { get; set; }

        /// <summary>
        /// Курсы (если преподаватель)
        /// </summary>
        public List<Course> Courses { get; set; }

        /// <summary>
        /// Статистика по курсам (если студент)
        /// </summary>
        public List<StudentStatistic> Statistics { get; set; }


    }
}
