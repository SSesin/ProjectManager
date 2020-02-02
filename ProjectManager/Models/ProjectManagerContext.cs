using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    /// <summary>
    /// Define the context of the web API
    /// </summary>
    public class ProjectManagerContext : DbContext
    {
        /// <summary>
        /// Create the context of the web API and initiate the log on the database
        /// </summary>
        public ProjectManagerContext() : base("name=ProjectManagerContext")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        /// <summary>
        /// Database containing the Users
        /// </summary>
        public System.Data.Entity.DbSet<ProjectManager.Models.User> Users { get; set; }

        /// <summary>
        /// Database containing the Tasks
        /// </summary>
        public System.Data.Entity.DbSet<ProjectManager.Models.Task> Tasks { get; set; }
    
    }
}
