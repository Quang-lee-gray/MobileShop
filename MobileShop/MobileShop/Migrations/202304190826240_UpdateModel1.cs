namespace MobileShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Product", "ProductCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Product", "ProductCode");
        }
    }
}
