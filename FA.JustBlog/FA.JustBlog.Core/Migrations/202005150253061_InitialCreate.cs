namespace FA.JustBlog.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 225),
                        UrlString = c.String(nullable: false, maxLength: 225),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false, maxLength: 225),
                        ShortDescription = c.String(name: "Short Description"),
                        PostContent = c.String(name: "Post Content"),
                        UrlSlug = c.String(maxLength: 1024),
                        Publisher = c.Boolean(nullable: false),
                        PostedOn = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        ViewCount = c.Int(),
                        RateCount = c.Int(),
                        TotalRate = c.Int(),
                        CategoryId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 225),
                        CommentHeader = c.String(maxLength: 225),
                        CommentText = c.String(maxLength: 225),
                        CommentTime = c.DateTime(nullable: false),
                        PostId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 225),
                        UrlSlug = c.String(maxLength: 225),
                        Description = c.String(),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UseRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UseLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PostTagMap",
                c => new
                    {
                        PostId = c.String(nullable: false, maxLength: 128),
                        TagId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.PostId, t.TagId })
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UseRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UseLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UseRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PostTagMap", "TagId", "dbo.Tags");
            DropForeignKey("dbo.PostTagMap", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CategoryId", "dbo.Categories");
            DropIndex("dbo.PostTagMap", new[] { "TagId" });
            DropIndex("dbo.PostTagMap", new[] { "PostId" });
            DropIndex("dbo.UseLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.UseRoles", new[] { "RoleId" });
            DropIndex("dbo.UseRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.Posts", new[] { "CategoryId" });
            DropTable("dbo.PostTagMap");
            DropTable("dbo.UseLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.UseRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.Tags");
            DropTable("dbo.Comments");
            DropTable("dbo.Posts");
            DropTable("dbo.Categories");
        }
    }
}
