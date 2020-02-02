namespace ProjectManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskName = c.String(nullable: false),
                        DateStart = c.DateTime(nullable: false),
                        Workload = c.Int(nullable: false),
                        Criticity = c.Int(nullable: false),
                        ProjectManagerId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.EmployeeId)
                .ForeignKey("dbo.Users", t => t.ProjectManagerId)
                .Index(t => t.ProjectManagerId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ProjectManager = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "ProjectManagerId", "dbo.Users");
            DropForeignKey("dbo.Tasks", "EmployeeId", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "EmployeeId" });
            DropIndex("dbo.Tasks", new[] { "ProjectManagerId" });
            DropTable("dbo.Users");
            DropTable("dbo.Tasks");
        }
    }
}
