﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace assessment.Migrations
{
    /// <inheritdoc />
    public partial class add_token : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			// Added UNIQUE to symbol
			migrationBuilder.Sql(@"CREATE TABLE `Token` (
				`id` INT(11) NOT NULL AUTO_INCREMENT,
				`symbol` VARCHAR(5) NOT NULL UNIQUE COLLATE 'utf8_general_ci',
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
				;
				
				INSERT INTO `Token` (`symbol`, `name`, `total_supply`, `contract_address`,
				`total_holders`, `price`) VALUES ('VEN', 'Vechain', 35987133,
				'0xd850942ef8811f2a866692a623011bde52a462c1', 65, 0.00);
				
				INSERT INTO `Token` (`symbol`, `name`, `total_supply`, `contract_address`,
				`total_holders`, `price`) VALUES ('ZIR', 'Zilliqa', 53272942,
				'0x05f4a42e251f2d52b8ed15e9fedaacfcef1fad27', 54, 0.00);
				
				INSERT INTO `Token` (`symbol`, `name`, `total_supply`, `contract_address`,
				`total_holders`, `price`) VALUES ('MKR', 'Maker', 45987133,
				'0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2', 567, 0.00);
				
				INSERT INTO `Token` (`symbol`, `name`, `total_supply`, `contract_address`,
				`total_holders`, `price`) VALUES ('BNB', 'Binance', 16579517,
				'0xB8c77482e45F1F44dE1745F52C74426C631bDD52', 4234234, 0.00);
				
				"
			);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP TABLE `token`");
        }
    }
}
