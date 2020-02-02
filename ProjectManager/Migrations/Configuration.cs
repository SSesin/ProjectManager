namespace ProjectManager.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectManager.Models.ProjectManagerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ProjectManager.Models.ProjectManagerContext context)
        {
            context.Users.AddOrUpdate(x => x.Id,
                new User() { Id = 1, Name = "Alain", ProjectManager = true },
                new User() { Id = 2, Name = "Jean", ProjectManager = false },
                new User() { Id = 3, Name = "Pierre", ProjectManager = false },
                new User() { Id = 4, Name = "Julie", ProjectManager = false },
                new User() { Id = 5, Name = "Stephanie", ProjectManager = false }
                );

            context.Tasks.AddOrUpdate(x => x.Id,
                new Task()
                {
                    Id = 1,
                    ProjectManagerId = 1,
                    EmployeeId = 2,
                    TaskName = "Premiere Tache",
                    DateStart = DateTime.Parse("01/01/2020"),
                    Workload = 10,
                    DateEnd = DateTime.Parse("14/01/2020")
                },
                new Task()
                {
                    Id = 2,
                    ProjectManagerId = 1,
                    EmployeeId = 3,
                    TaskName = "Deuxième Tache",
                    DateStart = DateTime.Parse("20/01/2020"),
                    Workload = 10,
                    DateEnd = DateTime.Parse("31/01/2020")
                },
                new Task()
                {
                    Id = 3,
                    ProjectManagerId = 1,
                    EmployeeId = 2,
                    TaskName = "Troisième Tache",
                    DateStart = DateTime.Parse("01/01/2020"),
                    Workload = 10,
                    DateEnd = DateTime.Parse("14/01/2020")
                },
                new Task()
                {
                    Id = 4,
                    ProjectManagerId = 1,
                    EmployeeId = 5,
                    TaskName = "Quatrième Tache",
                    DateStart = DateTime.Parse("01/01/2020"),
                    Workload = 10,
                    DateEnd = DateTime.Parse("14/01/2020")
                });
        }
    }
}
