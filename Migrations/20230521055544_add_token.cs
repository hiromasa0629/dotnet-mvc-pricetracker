using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace assessment.Migrations
{
    /// <inheritdoc />
    public partial class add_token : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"CREATE TABLE `token` (
				`id` INT(11) NOT NULL AUTO_INCREMENT,
				`symbol` VARCHAR(5) NOT NULL COLLATE 'utf8_general_ci',
				`name` VARCHAR(50) NOT NULL COLLATE 'utf8_general_ci',
				`total_supply` BIGINT(20) NOT NULL,
				`contract_address` VARCHAR(66) NOT NULL COLLATE 'utf8_general_ci',
				`total_holders` INT(11) NOT NULL,
				`price` DECIMAL(65,2) NULL DEFAULT '0.00',
				PRIMARY KEY (`id`) USING BTREE
				)
				COLLATE='utf8_general_ci'
				ENGINE=InnoDB
				ROW_FORMAT=DYNAMIC
				AUTO_INCREMENT=8
				;"
			);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP TABLE `token`");
        }
    }
}
