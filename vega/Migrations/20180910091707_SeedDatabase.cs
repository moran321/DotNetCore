using Microsoft.EntityFrameworkCore.Migrations;

namespace vega.Migrations {
    public partial class SeedDatabase : Migration {
        protected override void Up (MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql ("INSERT INTO Makes (Name) VALUES ('Make1')");
            migrationBuilder.Sql ("INSERT INTO Makes (Name) VALUES ('Make2')");
            migrationBuilder.Sql ("INSERT INTO Makes (Name) VALUES ('Make3')");

            migrationBuilder.Sql 
            ("INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModalA', (SELECT ID FROM Makes WHERE Name='Make1'))");
            migrationBuilder.Sql 
            ("INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModalB', (SELECT ID FROM Makes WHERE Name='Make1'))");
            migrationBuilder.Sql 
            ("INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModalC', (SELECT ID FROM Makes WHERE Name='Make1'))");

            migrationBuilder.Sql 
            ("INSERT INTO Models (Name, MakeId) VALUES ('Make2-ModalA', (SELECT ID FROM Makes WHERE Name='Make2'))");
            migrationBuilder.Sql 
            ("INSERT INTO Models (Name, MakeId) VALUES ('Make2-ModalB', (SELECT ID FROM Makes WHERE Name='Make2'))");
            migrationBuilder.Sql 
            ("INSERT INTO Models (Name, MakeId) VALUES ('Make2-ModalC', (SELECT ID FROM Makes WHERE Name='Make2'))");

            migrationBuilder.Sql 
            ("INSERT INTO Models (Name, MakeId) VALUES ('Make3-ModalA', (SELECT ID FROM Makes WHERE Name='Make3'))");
            migrationBuilder.Sql 
            ("INSERT INTO Models (Name, MakeId) VALUES ('Make3-ModalB', (SELECT ID FROM Makes WHERE Name='Make3'))");
            migrationBuilder.Sql 
            ("INSERT INTO Models (Name, MakeId) VALUES ('Make3-ModalC', (SELECT ID FROM Makes WHERE Name='Make3'))");
        }

        protected override void Down (MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql ("DELETE FROM Makes WHERE Name IN ('Make1','Make2','Make3')"); 
            //redundant line- if we delete the makes it will delete the model
            // migrationBuilder.Sql ("DELETE FROM Models");
        }
    }
}