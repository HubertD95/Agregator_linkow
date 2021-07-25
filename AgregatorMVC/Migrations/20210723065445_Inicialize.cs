using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgregatorMVC.Migrations
{
    public partial class Inicialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
               name: "Links",
               columns: table => new
               {
                   ID = table.Column<long>(type: "bigint", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Points = table.Column<int>(type: "int", nullable: false),
                   UserID = table.Column<long>(type: "bigint", nullable: false),
                   DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                   UpVote = table.Column<bool>(type: "bit", nullable: true),
                   DownVote = table.Column<bool>(type: "bit", nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Links", x => x.ID);
                   table.ForeignKey("FK_Links_Users_UserID", x => x.UserID, "Users", "ID");
               });


            migrationBuilder.CreateTable(
                name: "UsersVotes",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<long>(type: "bigint", nullable: false),
                    LinkID = table.Column<long>(type: "bigint", nullable: false),
                    UpVote = table.Column<bool>(type: "bit", nullable: true),
                    DownVote = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersVotes", x => x.ID);
                    table.ForeignKey("FK_UsersVotes_Users_ID", x => x.UserID, "Users", "ID");
                    table.ForeignKey("FK_UsersVotes_Links_ID", x => x.LinkID, "Links", "ID");
                });

            migrationBuilder.Sql("INSERT INTO dbo.Users (Login, Password) VALUES ('admin@gmail.com','admin');");
                        
        }




        protected override void Down(MigrationBuilder migrationBuilder)
        {            
            migrationBuilder.DropTable(
                name: "UsersVotes");

            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "Users");            
        }
    }
}
