using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuditNow.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            #region TbUser
            migrationBuilder.Sql("INSERT INTO TbUser(CreationUserId, CreationDate, ModificationUserId, ModificationDate, IsActive, UserName, Email, Password, LastAccessDate) VALUES(1, NOW(), 1, NOW(), 1, 'Admin', 'admin@gmail.com', 'HGBxpkxYGxDY/TiAmqBBmw==', NOW())");
            #endregion

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
