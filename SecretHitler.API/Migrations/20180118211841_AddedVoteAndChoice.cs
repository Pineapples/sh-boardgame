using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SecretHitler.API.Migrations
{
    public partial class AddedVoteAndChoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChoiceRound",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    GameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceRound", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChoiceRound_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "VoteRound",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    GameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteRound", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoteRound_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Choice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChoiceRoundId = table.Column<int>(nullable: false),
                    ChooserId = table.Column<int>(nullable: false),
                    ChosenId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Choice_ChoiceRound_ChoiceRoundId",
                        column: x => x.ChoiceRoundId,
                        principalTable: "ChoiceRound",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Choice_Player_ChooserId",
                        column: x => x.ChooserId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Choice_Player_ChosenId",
                        column: x => x.ChosenId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    InFavor = table.Column<bool>(nullable: false),
                    VoteRoundId = table.Column<int>(nullable: false),
                    VoterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vote_VoteRound_VoteRoundId",
                        column: x => x.VoteRoundId,
                        principalTable: "VoteRound",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Vote_Player_VoterId",
                        column: x => x.VoterId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Choice_ChoiceRoundId",
                table: "Choice",
                column: "ChoiceRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Choice_ChooserId",
                table: "Choice",
                column: "ChooserId");

            migrationBuilder.CreateIndex(
                name: "IX_Choice_ChosenId",
                table: "Choice",
                column: "ChosenId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceRound_GameId",
                table: "ChoiceRound",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_VoteRoundId",
                table: "Vote",
                column: "VoteRoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_VoterId",
                table: "Vote",
                column: "VoterId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteRound_GameId",
                table: "VoteRound",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Choice");

            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropTable(
                name: "ChoiceRound");

            migrationBuilder.DropTable(
                name: "VoteRound");
        }
    }
}
