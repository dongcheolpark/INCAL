namespace testweb2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VoteRes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoteId = c.Int(nullable: false),
                        ChoiceCounts = c.Int(nullable: false),
                        ChoiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VoteRes");
        }
    }
}
