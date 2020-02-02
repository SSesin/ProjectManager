using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ProjectManager.Models;
using Itenso.TimePeriod;
using System.Linq.Expressions;

namespace ProjectManager.Controllers
{
    /// <summary>
    /// Task controller
    /// </summary>
    public class TasksController : ApiController
    {
        private ProjectManagerContext db = new ProjectManagerContext();

        // GET: api/Tasks
        /// <summary>
        /// Get all the tasks in the database
        /// </summary>
        /// <returns>Tasks</returns>
        public IQueryable<TaskDTO> GetTasks()
        {
            var tasks = from b in db.Tasks
                        select new TaskDTO()
                        {
                            Id = b.Id,
                            TaskName = b.TaskName,
                            ProjectManagerName = b.ProjectManager.Name,
                            EmployeeName = b.Employee.Name,
                            DateStart = b.DateStart,
                            Workload = b.Workload,
                            DateEnd = b.DateEnd
                        };

            return tasks;
        }

        // GET: api/Tasks/User/5
        /// <summary>
        /// Get all the tasks assigned to one employee
        /// </summary>
        /// <param name="id">Id of the Employee</param>
        /// <returns>Tasks assigned to the Employee</returns>
        [ResponseType(typeof(TaskDTO))]
        [Route("api/Tasks/User/{id}")]
        public IQueryable<TaskDTO> GetTasksUser(int id)
        {
            var tasks = from b in db.Tasks
                        where b.EmployeeId == id
                        select new TaskDTO()
                        {
                            Id = b.Id,
                            TaskName = b.TaskName,
                            ProjectManagerName = b.ProjectManager.Name,
                            EmployeeName = b.Employee.Name,
                            DateStart = b.DateStart,
                            Workload = b.Workload,
                            DateEnd = b.DateEnd
                        };

            return tasks;
        }

        // GET: api/Tasks/5
        /// <summary>
        /// Get a specific task
        /// </summary>
        /// <param name="id">Id of the specific task</param>
        /// <returns>Task with corresponding id</returns>
        [ResponseType(typeof(TaskDTO))]
        public async Task<IHttpActionResult> GetTask(int id)
        {
            var task = await db.Tasks.Include(b => b.Employee).Include(b=>b.ProjectManager).Select(b =>
                new TaskDTO()
                {
                    Id = b.Id,
                    TaskName = b.TaskName,
                    EmployeeName = b.Employee.Name,
                    ProjectManagerName = b.ProjectManager.Name,
                    DateStart = b.DateStart,
                    Workload = b.Workload,
                    DateEnd= b.DateEnd
                }).SingleOrDefaultAsync(b => b.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // POST: api/Tasks
        /// <summary>
        /// Post a new task in the database
        /// </summary>
        /// <param name="task">Task to add</param>
        /// <returns>Result of the operation</returns>
        [ResponseType(typeof(Models.Task))]
        public async Task<IHttpActionResult> PostTask(Models.Task task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            task.DateEnd = task.AddBusinessDay(task.DateStart, task.Workload);
            db.Tasks.Add(task);
            await db.SaveChangesAsync();


            db.Entry(task).Reference(x => x.Employee).Load();
            db.Entry(task).Reference(x => x.ProjectManager).Load();

            var dto = new TaskDTO()
            {
                Id = task.Id,
                TaskName = task.TaskName,
                ProjectManagerName = task.ProjectManager.Name,
                EmployeeName = task.Employee.Name,
                DateStart = task.DateStart,
                Workload = task.Workload,
                DateEnd = task.DateEnd
            };

            return CreatedAtRoute("DefaultApi", new { id = task.Id }, dto);
        }

        // DELETE: api/Tasks/5
        /// <summary>
        /// Delete task with the corresponding id
        /// </summary>
        /// <param name="id">id of the task to delete</param>
        /// <returns>Result of the operation</returns>
        [ResponseType(typeof(Models.Task))]
        public async Task<IHttpActionResult> DeleteTask(int id)
        {
            Models.Task task = await db.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            db.Tasks.Remove(task);
            await db.SaveChangesAsync();

            return Ok(task);
        }

        // GET : api/Tasks/Overlap/
        /// <summary>
        /// Get the task overlap report. The tasks must have a time period in common and be assigned to the same employee to overlap.
        /// </summary>
        /// <returns>Task which overlap with other task ordered by employee</returns>
        [Route("api/Tasks/Overlap")]
        public IQueryable<OverlapDTO> GetOverlap()
        {
            var tasks = GetTasks();
            var overlap = from b in tasks
                          join t in tasks on b.EmployeeName equals t.EmployeeName
                          where b.Id < t.Id
                          where b.DateStart <= t.DateEnd
                          where t.DateStart <= b.DateEnd
                          select
                            new OverlapDTO()
                            {
                                EmployeeName = b.EmployeeName,
                                TaskId1 = b.Id,
                                TaskId2 = t.Id,
                                Task1 = b,
                                Task2 = t
                            };
            return overlap.OrderBy(o => o.EmployeeName);
        }

        /// <summary>
        /// Release the context
        /// </summary>
        /// <param name="disposing">if true dispose the context</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskExists(int id)
        {
            return db.Tasks.Count(e => e.Id == id) > 0;
        }
    }
}