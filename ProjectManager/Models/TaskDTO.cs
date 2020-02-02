
using System;

namespace ProjectManager.Models
{
    /// <summary>
    /// DTO of task
    /// </summary>
    public class TaskDTO
    {
        /// <summary>
        /// Key of the task
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the task
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// Name of the employee assigned to the task
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// Name of the project manager assigned to the task
        /// </summary>
        public string ProjectManagerName { get; set; }
        /// <summary>
        /// Begining date of the task
        /// </summary>
        public DateTime DateStart { get; set; }
        /// <summary>
        /// Number of business day to complete the task
        /// </summary>
        public int Workload { get; set; }
        /// <summary>
        /// Calculated end date of the task
        /// </summary>
        public DateTime DateEnd { get; set; }

        #region Date Format dd/MM/yyyy
        /// <summary>
        /// DateStart in string format
        /// </summary>
        public string DateStartString {
            get
            {
                return DateStart.ToString("dd/MM/yyyy");
            }
        }
        /// <summary>
        /// DateEnd in string format
        /// </summary>
        public string DateEndString
        {
            get
            {
                return DateEnd.ToString("dd/MM/yyyy");
            }
        }
        #endregion
    }
}