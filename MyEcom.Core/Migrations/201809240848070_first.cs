namespace MyEcom.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        Description = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hotel_Image",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HotelId = c.Int(nullable: false),
                        ImageId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: true)
                .ForeignKey("dbo.Images", t => t.ImageId, cascadeDelete: true)
                .Index(t => t.HotelId)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HotelContents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HotelId = c.Int(nullable: false),
                        Spa = c.Boolean(nullable: false),
                        Restaurant = c.Boolean(nullable: false),
                        Gym = c.Boolean(nullable: false),
                        RoomService = c.Boolean(nullable: false),
                        Pool = c.Boolean(nullable: false),
                        Rank = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.HotelId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        hotel_id = c.Int(nullable: false),
                        customerid = c.Int(nullable: false),
                        roomid = c.Int(nullable: false),
                        checkin = c.DateTime(nullable: false),
                        checkout = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HotelId = c.Int(nullable: false),
                        Stock = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        RoomType = c.String(),
                        RoomContentId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: true)
                .ForeignKey("dbo.RoomContents", t => t.RoomContentId, cascadeDelete: true)
                .Index(t => t.HotelId)
                .Index(t => t.RoomContentId);
            
            CreateTable(
                "dbo.RoomContents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageId = c.Int(nullable: false),
                        Name = c.String(),
                        PersonCount = c.Int(nullable: false),
                        Wifi = c.Boolean(nullable: false),
                        TV = c.Boolean(nullable: false),
                        Bathroom = c.Boolean(nullable: false),
                        Aircon = c.Boolean(nullable: false),
                        Jacuzzi = c.Boolean(nullable: false),
                        Minibar = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.ImageId, cascadeDelete: true)
                .Index(t => t.ImageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "RoomContentId", "dbo.RoomContents");
            DropForeignKey("dbo.RoomContents", "ImageId", "dbo.Images");
            DropForeignKey("dbo.Rooms", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.HotelContents", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.Hotel_Image", "ImageId", "dbo.Images");
            DropForeignKey("dbo.Hotel_Image", "HotelId", "dbo.Hotels");
            DropIndex("dbo.RoomContents", new[] { "ImageId" });
            DropIndex("dbo.Rooms", new[] { "RoomContentId" });
            DropIndex("dbo.Rooms", new[] { "HotelId" });
            DropIndex("dbo.HotelContents", new[] { "HotelId" });
            DropIndex("dbo.Hotel_Image", new[] { "ImageId" });
            DropIndex("dbo.Hotel_Image", new[] { "HotelId" });
            DropTable("dbo.RoomContents");
            DropTable("dbo.Rooms");
            DropTable("dbo.Reservations");
            DropTable("dbo.HotelContents");
            DropTable("dbo.Images");
            DropTable("dbo.Hotel_Image");
            DropTable("dbo.Hotels");
            DropTable("dbo.Customers");
        }
    }
}
