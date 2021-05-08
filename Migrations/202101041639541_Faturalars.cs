namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Faturalars : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Faturalars", "Saaat", c => c.String(maxLength: 5, fixedLength: true, unicode: false));
            AddColumn("dbo.Faturalars", "Toplam", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Faturalars", "FaturaSiraNo", c => c.String(maxLength: 6, unicode: false));
            AlterColumn("dbo.Faturalars", "FaturaSeriNo", c => c.String(maxLength: 6, fixedLength: true, unicode: false));
            AlterColumn("dbo.Faturalars", "Tarih", c => c.String());
            //DropColumn("dbo.Faturalars", "dateTime");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Faturalars", "dateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Faturalars", "Tarih", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Faturalars", "FaturaSeriNo", c => c.String(maxLength: 6, unicode: false));
            AlterColumn("dbo.Faturalars", "FaturaSiraNo", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            DropColumn("dbo.Faturalars", "Toplam");
            DropColumn("dbo.Faturalars", "Saaat");
        }
    }
}
