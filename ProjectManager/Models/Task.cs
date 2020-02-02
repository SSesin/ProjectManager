using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Models
{
    /// <summary>
    /// Define a task assigned to a employee
    /// </summary>
    public class Task
    {
        #region Attributes
        /// <summary>
        /// Key of the task
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the task
        /// </summary>
        [Required]
        public string TaskName { get; set; }
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
        #endregion

        #region Foreign Keys
        /// <summary>
        /// Foreign key of the project manager who assigned the task
        /// </summary>
        public int ProjectManagerId { get; set; }
        /// <summary>
        /// Foreign key of the employee which is assigned on this task
        /// </summary>
        public int EmployeeId { get; set; }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// Define the author of the task
        /// </summary>
        public virtual User ProjectManager { get; set; }
        /// <summary>
        /// Define the employee assigned to the task
        /// </summary>
        public virtual User Employee { get; set; }
        #endregion

        #region Functions
        /// <summary>
        /// Function to add business days to a date (not including Saturdays and Sundays).
        /// Used to calculate the end date of a task.
        /// </summary>
        /// <param name="StartingDate">Start date</param>
        /// <param name="Workload">Number of business days to add (including the starting day)</param>
        /// <returns></returns>
        public DateTime AddBusinessDay(DateTime StartingDate, int Workload)
        {
            DateTime EndDate = StartingDate;
            // sign is used to be able to remove business day
            double sign = Convert.ToDouble(Math.Sign(Workload));
            int unsignedDays = Math.Abs(Workload);
            // remove the starting day of the days to add (cause the 1 day of work is the StartingDate)
            if (unsignedDays > 0)
            {
                unsignedDays -= 1;
            }
            for (int i = 0; i < unsignedDays; i++)
            {
                do
                {
                    EndDate = EndDate.AddDays(sign);
                }
                while (EndDate.DayOfWeek == DayOfWeek.Saturday ||
                    EndDate.DayOfWeek == DayOfWeek.Sunday);
            }
            return EndDate;
        }
        #endregion
    }
}