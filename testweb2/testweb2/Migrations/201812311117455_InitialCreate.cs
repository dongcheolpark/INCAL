namespace testweb2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Homework",
                c => new
                    {
                        NoteNo = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        T_Name = c.String(),
                        Contents = c.String(),
                        Title = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NoteNo);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Homework");
        }
    }
}
