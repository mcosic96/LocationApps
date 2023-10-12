using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Locations.Common.DataContext.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class locationsDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lat = table.Column<float>(type: "real", nullable: true),
                    lng = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Northeasts",
                columns: table => new
                {
                    NortheastId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lat = table.Column<float>(type: "real", nullable: true),
                    lng = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Northeasts", x => x.NortheastId);
                });

            migrationBuilder.CreateTable(
                name: "Opening_Hours",
                columns: table => new
                {
                    OpeningHoursId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    open_now = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opening_Hours", x => x.OpeningHoursId);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    PlaceIdentifier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    html_attr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    error_message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    info_messages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    next_page_token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.PlaceIdentifier);
                });

            migrationBuilder.CreateTable(
                name: "Plus_Codes",
                columns: table => new
                {
                    PlusCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    compound_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    global_code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plus_Codes", x => x.PlusCodeId);
                });

            migrationBuilder.CreateTable(
                name: "Southwests",
                columns: table => new
                {
                    SouthwestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lat = table.Column<float>(type: "real", nullable: true),
                    lng = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Southwests", x => x.SouthwestId);
                });

            migrationBuilder.CreateTable(
                name: "SearchLocations",
                columns: table => new
                {
                    SearchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Place = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchLocations", x => x.SearchId);
                    table.ForeignKey(
                        name: "FK_SearchLocations_Places_Place",
                        column: x => x.Place,
                        principalTable: "Places",
                        principalColumn: "PlaceIdentifier");
                });

            migrationBuilder.CreateTable(
                name: "Viewports",
                columns: table => new
                {
                    ViewPortId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Northeast = table.Column<int>(type: "int", nullable: true),
                    Southwest = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viewports", x => x.ViewPortId);
                    table.ForeignKey(
                        name: "FK_Viewports_Northeasts_Northeast",
                        column: x => x.Northeast,
                        principalTable: "Northeasts",
                        principalColumn: "NortheastId");
                    table.ForeignKey(
                        name: "FK_Viewports_Southwests_Southwest",
                        column: x => x.Southwest,
                        principalTable: "Southwests",
                        principalColumn: "SouthwestId");
                });

            migrationBuilder.CreateTable(
                name: "Geometries",
                columns: table => new
                {
                    GeometryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<int>(type: "int", nullable: true),
                    ViewPort = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geometries", x => x.GeometryId);
                    table.ForeignKey(
                        name: "FK_Geometries_Locations_Location",
                        column: x => x.Location,
                        principalTable: "Locations",
                        principalColumn: "LocationId");
                    table.ForeignKey(
                        name: "FK_Geometries_Viewports_ViewPort",
                        column: x => x.ViewPort,
                        principalTable: "Viewports",
                        principalColumn: "ViewPortId");
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    resultID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    business_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Geometry = table.Column<int>(type: "int", nullable: true),
                    icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    icon_background_color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    icon_mask_base_uri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpeningHours = table.Column<int>(type: "int", nullable: true),
                    place_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlusCode = table.Column<int>(type: "int", nullable: true),
                    price_level = table.Column<int>(type: "int", nullable: true),
                    rating = table.Column<float>(type: "real", nullable: true),
                    reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    scope = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    categories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_ratings_total = table.Column<int>(type: "int", nullable: true),
                    vicinity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Result = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.resultID);
                    table.ForeignKey(
                        name: "FK_Results_Geometries_Geometry",
                        column: x => x.Geometry,
                        principalTable: "Geometries",
                        principalColumn: "GeometryId");
                    table.ForeignKey(
                        name: "FK_Results_Opening_Hours_OpeningHours",
                        column: x => x.OpeningHours,
                        principalTable: "Opening_Hours",
                        principalColumn: "OpeningHoursId");
                    table.ForeignKey(
                        name: "FK_Results_Places_Result",
                        column: x => x.Result,
                        principalTable: "Places",
                        principalColumn: "PlaceIdentifier");
                    table.ForeignKey(
                        name: "FK_Results_Plus_Codes_PlusCode",
                        column: x => x.PlusCode,
                        principalTable: "Plus_Codes",
                        principalColumn: "PlusCodeId");
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    PhotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    height = table.Column<int>(type: "int", nullable: true),
                    photo_reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    width = table.Column<int>(type: "int", nullable: true),
                    Photo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.PhotoId);
                    table.ForeignKey(
                        name: "FK_Photos_Results_Photo",
                        column: x => x.Photo,
                        principalTable: "Results",
                        principalColumn: "resultID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Geometries_Location",
                table: "Geometries",
                column: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_Geometries_ViewPort",
                table: "Geometries",
                column: "ViewPort");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_Photo",
                table: "Photos",
                column: "Photo");

            migrationBuilder.CreateIndex(
                name: "IX_Results_Geometry",
                table: "Results",
                column: "Geometry");

            migrationBuilder.CreateIndex(
                name: "IX_Results_OpeningHours",
                table: "Results",
                column: "OpeningHours");

            migrationBuilder.CreateIndex(
                name: "IX_Results_PlusCode",
                table: "Results",
                column: "PlusCode");

            migrationBuilder.CreateIndex(
                name: "IX_Results_Result",
                table: "Results",
                column: "Result");

            migrationBuilder.CreateIndex(
                name: "IX_SearchLocations_Place",
                table: "SearchLocations",
                column: "Place");

            migrationBuilder.CreateIndex(
                name: "IX_Viewports_Northeast",
                table: "Viewports",
                column: "Northeast");

            migrationBuilder.CreateIndex(
                name: "IX_Viewports_Southwest",
                table: "Viewports",
                column: "Southwest");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "SearchLocations");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Geometries");

            migrationBuilder.DropTable(
                name: "Opening_Hours");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Plus_Codes");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Viewports");

            migrationBuilder.DropTable(
                name: "Northeasts");

            migrationBuilder.DropTable(
                name: "Southwests");
        }
    }
}
