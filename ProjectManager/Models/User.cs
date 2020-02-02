using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    /// <summary>
    /// Define a user
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id of the user
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the user
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Define the role of the user :
        /// true : project mangager
        /// false : employee
        /// </summary>
        public bool ProjectManager { get; set;}
    }
}