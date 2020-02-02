using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProjectManager.Models
{
    /// <summary>
    /// Class that define an Overlap case of two task for an employee
    /// </summary>
    public class OverlapDTO
    {
        #region Attributes
        /// <summary>
        /// Name of the Employee
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// Period of time on which the two tasks overlap
        /// </summary>
        public TimeRange OverlapTime
        {
            get
            {
                TimeRange task1Time = new TimeRange(Task1.DateStart, Task1.DateEnd);
                TimeRange task2Time = new TimeRange(Task2.DateStart, Task2.DateEnd);
                TimeRange taskOverlap = (TimeRange)task1Time.GetIntersection(task2Time);
                return taskOverlap;
            }
        }
        /// <summary>
        /// Description in string of the Overlap period
        /// </summary>
        public string OverlapTimeString
        {
            get
            {
                return OverlapTime.ToString();
            }
        }
        #endregion

        #region Foreign Keys
        /// <summary>
        /// Foreign key of the first task
        /// </summary>
        public int TaskId1 { get; set; }
        /// <summary>
        /// Foreign key of the second task
        /// </summary>
        public int TaskId2 { get; set; }
        #endregion

        #region Navigation Property
        /// <summary>
        /// First task
        /// </summary>
        public virtual TaskDTO Task1 { get; set; }
        /// <summary>
        /// Second task
        /// </summary>
        public virtual TaskDTO Task2 { get; set; }
        #endregion

    }
}