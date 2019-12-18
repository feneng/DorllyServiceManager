using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DorllyServiceManager.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Garden",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Address = table.Column<string>(maxLength: 512, nullable: true),
                    State = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garden", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyValue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(maxLength: 24, nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(maxLength: 24, nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Description = table.Column<string>(maxLength: 512, nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Code = table.Column<string>(maxLength: 24, nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 512, nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCategory_ServiceCategory_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ServiceCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(maxLength: 24, nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Type = table.Column<int>(nullable: false),
                    InputType = table.Column<int>(nullable: false),
                    NotNull = table.Column<bool>(nullable: false),
                    State = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProperty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubSystem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Description = table.Column<string>(maxLength: 512, nullable: true),
                    Code = table.Column<string>(maxLength: 24, nullable: true),
                    State = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSystem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupplierLevel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(maxLength: 24, nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    MinScore = table.Column<int>(nullable: false),
                    MaxScore = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceSupplier",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(maxLength: 24, nullable: true),
                    FullName = table.Column<string>(maxLength: 64, nullable: true),
                    Abbreviation = table.Column<string>(maxLength: 32, nullable: true),
                    BelongGardenId = table.Column<int>(nullable: true),
                    ServiceScope = table.Column<string>(maxLength: 128, nullable: true),
                    Level = table.Column<int>(nullable: false),
                    ChargePerson = table.Column<string>(maxLength: 64, nullable: true),
                    ContactPhone = table.Column<string>(maxLength: 48, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 64, nullable: true),
                    WorkTel = table.Column<string>(maxLength: 64, nullable: true),
                    SupplierFrom = table.Column<string>(maxLength: 64, nullable: true),
                    Avatar = table.Column<string>(maxLength: 128, nullable: true),
                    ApproveState = table.Column<byte>(nullable: false),
                    CooperationState = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceSupplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceSupplier_Garden_BelongGardenId",
                        column: x => x.BelongGardenId,
                        principalTable: "Garden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryProperties",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false),
                    PropertyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProperties", x => new { x.CategoryId, x.PropertyId });
                    table.ForeignKey(
                        name: "FK_CategoryProperties_ServiceCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ServiceCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProperties_ServiceProperty_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "ServiceProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Type = table.Column<byte>(nullable: false),
                    Path = table.Column<string>(maxLength: 128, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Icon = table.Column<string>(maxLength: 64, nullable: true),
                    Level = table.Column<int>(nullable: false),
                    BelongSystemId = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Module_SubSystem_BelongSystemId",
                        column: x => x.BelongSystemId,
                        principalTable: "SubSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Module_Module_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(maxLength: 24, nullable: true),
                    Value = table.Column<byte>(maxLength: 512, nullable: false),
                    DataType = table.Column<string>(maxLength: 24, nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    SystemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemSetting_SubSystem_SystemId",
                        column: x => x.SystemId,
                        principalTable: "SubSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 128, nullable: true),
                    ContractNo = table.Column<string>(maxLength: 48, nullable: true),
                    FirstParty = table.Column<string>(maxLength: 64, nullable: true),
                    SecondParty = table.Column<string>(maxLength: 64, nullable: true),
                    BelongGardenId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Enclosure = table.Column<string>(maxLength: 128, nullable: true),
                    SignDate = table.Column<DateTime>(nullable: false),
                    State = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_Garden_BelongGardenId",
                        column: x => x.BelongGardenId,
                        principalTable: "Garden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_ServiceSupplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "ServiceSupplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 8, nullable: true),
                    Account = table.Column<string>(maxLength: 8, nullable: true),
                    Password = table.Column<string>(maxLength: 64, nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Sex = table.Column<byte>(nullable: false),
                    BelongGardenId = table.Column<int>(nullable: true),
                    ContactPhone = table.Column<string>(maxLength: 48, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastLoginTime = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 64, nullable: true),
                    WorkTel = table.Column<string>(maxLength: 64, nullable: true),
                    Avatar = table.Column<string>(maxLength: 128, nullable: true),
                    State = table.Column<byte>(nullable: false),
                    SupplierId = table.Column<int>(nullable: true),
                    Salt = table.Column<string>(maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Garden_BelongGardenId",
                        column: x => x.BelongGardenId,
                        principalTable: "Garden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_ServiceSupplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "ServiceSupplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(maxLength: 24, nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Type = table.Column<byte>(nullable: false),
                    Path = table.Column<string>(maxLength: 128, nullable: true),
                    Description = table.Column<string>(maxLength: 512, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Icon = table.Column<string>(maxLength: 64, nullable: true),
                    BelongModuleId = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permission_Module_BelongModuleId",
                        column: x => x.BelongModuleId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(maxLength: 30, nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    State = table.Column<byte>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdateUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_ServiceCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ServiceCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Service_User_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderNo = table.Column<string>(maxLength: 48, nullable: true),
                    Buyer = table.Column<string>(maxLength: 48, nullable: true),
                    ContactUser = table.Column<string>(maxLength: 36, nullable: true),
                    ContactPhone = table.Column<string>(maxLength: 36, nullable: true),
                    BuyerRemark = table.Column<string>(maxLength: 512, nullable: true),
                    BuyerAddress = table.Column<string>(maxLength: 256, nullable: true),
                    BuyerCompany = table.Column<string>(maxLength: 64, nullable: true),
                    BelongGardenId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false),
                    OrderTime = table.Column<DateTime>(nullable: false),
                    State = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Garden_BelongGardenId",
                        column: x => x.BelongGardenId,
                        principalTable: "Garden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_ServiceSupplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "ServiceSupplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appraise",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(nullable: false),
                    AppraiseTime = table.Column<DateTime>(nullable: false),
                    StarClass = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Anonymous = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appraise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appraise_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appraise_OrderId",
                table: "Appraise",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProperties_PropertyId",
                table: "CategoryProperties",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_BelongGardenId",
                table: "Contract",
                column: "BelongGardenId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_SupplierId",
                table: "Contract",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_BelongSystemId",
                table: "Module",
                column: "BelongSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_ParentId",
                table: "Module",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_BelongGardenId",
                table: "Order",
                column: "BelongGardenId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ServiceId",
                table: "Order",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_SupplierId",
                table: "Order",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_BelongModuleId",
                table: "Permission",
                column: "BelongModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_CategoryId",
                table: "Service",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_UpdateUserId",
                table: "Service",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategory_ParentId",
                table: "ServiceCategory",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSupplier_BelongGardenId",
                table: "ServiceSupplier",
                column: "BelongGardenId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemSetting_SystemId",
                table: "SystemSetting",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_User_BelongGardenId",
                table: "User",
                column: "BelongGardenId");

            migrationBuilder.CreateIndex(
                name: "IX_User_SupplierId",
                table: "User",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appraise");

            migrationBuilder.DropTable(
                name: "CategoryProperties");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "PropertyValue");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "SupplierLevel");

            migrationBuilder.DropTable(
                name: "SystemSetting");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "ServiceProperty");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "ServiceCategory");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "SubSystem");

            migrationBuilder.DropTable(
                name: "ServiceSupplier");

            migrationBuilder.DropTable(
                name: "Garden");
        }
    }
}
