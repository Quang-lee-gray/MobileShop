namespace MobileShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Post", "CategoryID", c => c.Int(nullable: false));
            DropColumn("dbo.tb_Post", "CategoruID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Post", "CategoryID", c => c.Int(nullable: false));
            DropColumn("dbo.tb_Post", "CategoruID");
        }
    }
}
