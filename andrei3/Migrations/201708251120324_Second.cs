namespace andrei3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "key1", c => c.String());
            AddColumn("dbo.AspNetUsers", "key2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "key2");
            DropColumn("dbo.AspNetUsers", "key1");
        }
    }
}
