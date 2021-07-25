using Microsoft.EntityFrameworkCore.Migrations;

namespace AgregatorMVC.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            int count = 1;
            int points = 5;
            string sql;


            for (int i = 0; i < 150; i++)
            {
                sql = "INSERT INTO dbo.Links (Title, Link, UserID, Points, DateTime) VALUES ('Title" + count.ToString() + "','https://www.google.pl', 1," + (count%points).ToString() + ", Getdate());";
                migrationBuilder.Sql(sql);
                count++;
            }


            //migrationBuilder.Sql("INSERT INTO dbo.Links (Title, Link, UserID, Points, DateTime) VALUES ('Title','https://www.google.pl', 1,0, Getdate());");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
