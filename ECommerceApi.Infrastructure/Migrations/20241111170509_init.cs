using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParentCategoryId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    CouponId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DiscountType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumPurchase = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UsageLimit = table.Column<int>(type: "int", nullable: true),
                    UsageCount = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.CouponId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompareAtPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(26)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(26)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(26)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(26)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    LastPurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    VariantId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompareAtPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InventoryQuantity = table.Column<int>(type: "int", nullable: false),
                    LowStockThreshold = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    WeightUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.VariantId);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    AddressLine = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistItems",
                columns: table => new
                {
                    WishlistId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistItems", x => x.WishlistId);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ImageId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    VariantId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AltText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_ProductImages_ProductVariants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Shipping = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingAddressId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShippingMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CouponId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "CouponId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    VariantId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_ProductVariants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StripePaymentIntentId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StripeClientSecret = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01JCE2ZC2H0TQ51629JZP9PGMW", "01JCE2ZC2HH1S1696JKT831BGN", "Admin", "ADMIN" },
                    { "01JCE2ZC2HGQ6R5R8ZJ93724N9", "01JCE2ZC2H2BBKTERFKY8XGHSX", "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CreatedAt", "Description", "ImageUrl", "IsActive", "Name", "ParentCategoryId", "UpdatedAt" },
                values: new object[] { "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2343), "أشهر الأطباق المصرية التقليدية والشعبية", "https://foodimages.store/egyptian/traditional.jpg", true, "مأكولات مصرية تقليدية", null, new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2345) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CompareAtPrice", "CostPrice", "CreatedAt", "Description", "Name", "Price", "ShortDescription", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { "01HQ6BZJXP1MAWB3DQXS5QNZE1", 40.00m, 20.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(5874), "كشري مصري تقليدي مكون من عدس وأرز ومكرونة وصلصة طماطم حارة", "كشري مصري تقليدي", 35.00m, "طبق مصري شعبي مميز", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(5875) },
                    { "01HQ6BZJXP2MAWB3DQXS5QNZE2", 75.00m, 40.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(5898), "ملوخية خضراء طازجة مع قطع دجاج مشوي وأرز", "ملوخية بالفراخ", 65.00m, "طبق مصري عائلي مميز", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(5900) },
                    { "01HQ6BZJXP3MAWB3DQXS5QNZE3", 18.00m, 8.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(5919), "فول مدمس بزيت الزيتون والكمون والليمون", "فول مدمس", 15.00m, "فطار مصري تقليدي", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(5920) },
                    { "01HQ6BZJXP4MAWB3DQXS5QNZE4", 200.00m, 120.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(5940), "تشكيلة من اللحوم المشوية تشمل كفتة وريش وشيش طاووق", "مشويات مشكلة", 180.00m, "مشويات مصرية فاخرة", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(5941) },
                    { "01HQ6BZJXP5MAWB3DQXS5QNZE5", 50.00m, 25.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(5959), "كنافة محشوة بالقشطة والمكسرات", "كنافة نابلسية", 45.00m, "حلوى شرقية شهية", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(5961) },
                    { "01HQ6BZJXP6MAWB3DQXS5QNZE6", 2.50m, 1.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(5981), "خبز بلدي طازج مصنوع يومياً", "عيش بلدي", 2.00m, "خبز مصري تقليدي", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(5982) },
                    { "01HQ6BZJXP7MAWB3DQXS5QNZE7", 180.00m, 100.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6003), "سمك قاروص طازج مشوي مع الخضروات والصلصة", "سمك قاروص مشوي", 160.00m, "سمك طازج مشوي", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6004) },
                    { "01HQ6BZJXP8MAWB3DQXS5QNZE8", 95.00m, 55.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6023), "فتة مصرية باللحم البتلو والخل والثوم", "فتة باللحمة", 85.00m, "طبق مصري تقليدي", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6024) },
                    { "01HQ6BZJXP9MAWB3DQXS5QNZE9", 30.00m, 15.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6042), "متبل باذنجان مشوي بالطحينة والثوم", "بابا غنوج", 25.00m, "مقبلات شرقية", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6044) },
                    { "01HQ6BZJXPAMAWB3DQXS5QNZF1", 25.00m, 10.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6161), "شوربة عدس مصرية بالكمون والليمون", "شوربة عدس", 20.00m, "شوربة مصرية تقليدية", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6163) },
                    { "01HQ6BZJXPBMAWB3DQXS5QNZF2", 50.00m, 25.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6184), "أرز صيادية بالسمك والمكسرات", "أرز صيادية", 45.00m, "أرز بالسمك على الطريقة المصرية", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6185) },
                    { "01HQ6BZJXPCMAWB3DQXS5QNZF3", 80.00m, 45.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6205), "مكرونة بالجمبري والصلصة البيضاء", "مكرونة إسكندراني", 70.00m, "باستا على الطريقة الإسكندرانية", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6207) },
                    { "01HQ6BZJXPDMAWB3DQXS5QNZF4", 7.00m, 2.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6226), "شاي مصري أسود مع النعناع الطازج", "شاي مصري", 5.00m, "مشروب مصري تقليدي", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6227) },
                    { "01HQ6BZJXPEMAWB3DQXS5QNZF5", 12.00m, 5.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6246), "عصير قصب طازج", "عصير قصب", 10.00m, "مشروب مصري منعش", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6247) },
                    { "01HQ6BZJXPFMAWB3DQXS5QNZF6", 100.00m, 60.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6275), "فراخ مشوية متبلة بالتوابل المصرية", "فراخ مشوية", 90.00m, "دجاج مشوي على الفحم", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6276) },
                    { "01HQ6BZJXPGMAWB3DQXS5QNZF7", 95.00m, 55.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6367), "كفتة لحم مشوية مع البهارات المصرية", "كفتة مشوية", 85.00m, "لحم مفروم مشوي", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6369) },
                    { "01HQ6BZJXPHMAWB3DQXS5QNZF8", 18.00m, 8.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6515), "تشكيلة من المخللات المصرية", "مخلل مشكل", 15.00m, "مخللات مصرية متنوعة", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6517) },
                    { "01HQ6BZJXPIMAWB3DQXS5QNZF9", 30.00m, 12.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6533), "خلطة بهارات مصرية تقليدية", "دقة مصرية", 25.00m, "توابل مصرية", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6534) },
                    { "01HQ6BZJXPJMAWB3DQXS5QNZG1", 25.00m, 10.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6550), "طحينة مصرية مع الليمون والثوم", "صلصة طحينة", 20.00m, "صلصة مصرية تقليدية", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6552) },
                    { "01HQ6BZJXPKMAWB3DQXS5QNZG2", 45.00m, 25.00m, new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6568), "حواوشي باللحم المفروم والتوابل", "حواوشي", 40.00m, "ساندوتش مصري شهير", "Active", new DateTime(2024, 11, 11, 17, 5, 7, 639, DateTimeKind.Utc).AddTicks(6570) }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CreatedAt", "Description", "ImageUrl", "IsActive", "Name", "ParentCategoryId", "UpdatedAt" },
                values: new object[,]
                {
                    { "01HQ5RZJXP2MAWB3DQXS5QNZEI", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2384), "تشكيلة متنوعة من المشويات المصرية", "https://foodimages.store/grilled/meats.jpg", true, "مشويات", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2385) },
                    { "01HQ5RZJXP3MAWB3DQXS5QNZEJ", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2403), "حلويات مصرية تقليدية وعربية", "https://foodimages.store/sweets/oriental.jpg", true, "حلويات شرقية", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2404) },
                    { "01HQ5RZJXP4MAWB3DQXS5QNZEK", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2422), "الخبز والمعجنات المصرية", "https://foodimages.store/bakery/bread.jpg", true, "مخبوزات", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2424) },
                    { "01HQ5RZJXP5MAWB3DQXS5QNZEL", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2442), "أطباق السمك والمأكولات البحرية", "https://foodimages.store/seafood/fish.jpg", true, "مأكولات بحرية", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2443) },
                    { "01HQ5RZJXP6MAWB3DQXS5QNZEM", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2475), "أطباق الفطور المصرية التقليدية", "https://foodimages.store/breakfast/traditional.jpg", true, "وجبات فطور", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2476) },
                    { "01HQ5RZJXP7MAWB3DQXS5QNZEN", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2494), "المقبلات والسلطات المصرية", "https://foodimages.store/appetizers/salads.jpg", true, "مقبلات", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2495) },
                    { "01HQ5RZJXP8MAWB3DQXS5QNZEO", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2513), "أنواع الحساء والشوربة المصرية", "https://foodimages.store/soups/traditional.jpg", true, "حساء وشوربة", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2514) },
                    { "01HQ5RZJXP9MAWB3DQXS5QNZEP", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2661), "تشكيلة متنوعة من أطباق الأرز", "https://foodimages.store/rice/dishes.jpg", true, "أطباق الأرز", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2662) },
                    { "01HQ5RZJXPAMAWB3DQXS5QNZEQ", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2681), "أطباق المكرونة والعجائن المصرية", "https://foodimages.store/pasta/dishes.jpg", true, "مكرونة وعجائن", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2683) },
                    { "01HQ5RZJXPBMAWB3DQXS5QNZER", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2699), "أطباق الخضروات والبقوليات المصرية", "https://foodimages.store/vegetables/legumes.jpg", true, "خضروات وبقوليات", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2700) },
                    { "01HQ5RZJXPCMAWB3DQXS5QNZES", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2718), "المشروبات الساخنة التقليدية", "https://foodimages.store/beverages/hot.jpg", true, "مشروبات ساخنة", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2719) },
                    { "01HQ5RZJXPDMAWB3DQXS5QNZET", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2736), "العصائر الطبيعية والمشروبات الباردة", "https://foodimages.store/beverages/juices.jpg", true, "عصائر طبيعية", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2737) },
                    { "01HQ5RZJXPEMAWB3DQXS5QNZEU", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2755), "تشكيلة متنوعة من أطباق الدجاج", "https://foodimages.store/chicken/dishes.jpg", true, "أطباق الدجاج", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2756) },
                    { "01HQ5RZJXPFMAWB3DQXS5QNZEV", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2780), "أطباق اللحوم المصرية المتنوعة", "https://foodimages.store/meat/dishes.jpg", true, "أطباق اللحوم", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2781) },
                    { "01HQ5RZJXPGMAWB3DQXS5QNZEW", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2799), "أطباق نباتية مصرية صحية", "https://foodimages.store/vegetarian/dishes.jpg", true, "وجبات نباتية", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2800) },
                    { "01HQ5RZJXPHMAWB3DQXS5QNZEX", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2816), "المخللات المصرية التقليدية", "https://foodimages.store/pickles/traditional.jpg", true, "مخللات", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2818) },
                    { "01HQ5RZJXPIMAWB3DQXS5QNZEY", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2835), "التوابل والبهارات المصرية", "https://foodimages.store/spices/traditional.jpg", true, "توابل وبهارات", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2836) },
                    { "01HQ5RZJXPJMAWB3DQXS5QNZEZ", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2853), "الصلصات والمقبلات المصرية", "https://foodimages.store/sauces/dips.jpg", true, "صلصات ومقبلات", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2854) },
                    { "01HQ5RZJXPKMAWB3DQXS5QNZF0", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2871), "الوجبات السريعة المصرية", "https://foodimages.store/fastfood/egyptian.jpg", true, "وجبات سريعة", "01HQ5RZJXP1MAWB3DQXS5QNZEH", new DateTime(2024, 11, 11, 17, 5, 7, 635, DateTimeKind.Utc).AddTicks(2872) }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ImageId", "AltText", "CreatedAt", "DisplayOrder", "ImageUrl", "ProductId", "UpdatedAt", "VariantId" },
                values: new object[,]
                {
                    { "01HQ7BZJXP0MAWB3DQXS5QNZ36", "توابل مصرية", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4569), 2, "https://foodimages.store/spices/mix-alt.jpg", "01HQ6BZJXPIMAWB3DQXS5QNZF9", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4571), null },
                    { "01HQ7BZJXP1MAWB3DQXS5QNZ01", "كشري مصري تقليدي", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3819), 1, "https://foodimages.store/koshary/main.jpg", "01HQ6BZJXP1MAWB3DQXS5QNZE1", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3820), null },
                    { "01HQ7BZJXP1NAWB3DQXS5QNZ37", "صلصة طحينة", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4587), 1, "https://foodimages.store/sauces/tahini-main.jpg", "01HQ6BZJXPJMAWB3DQXS5QNZG1", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4588), null },
                    { "01HQ7BZJXP2MAWB3DQXS5QNZ02", "مكونات الكشري المصري", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3840), 2, "https://foodimages.store/koshary/alt.jpg", "01HQ6BZJXP1MAWB3DQXS5QNZE1", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3842), null },
                    { "01HQ7BZJXP2NAWB3DQXS5QNZ38", "طحينة مع السلطة", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4605), 2, "https://foodimages.store/sauces/tahini-alt.jpg", "01HQ6BZJXPJMAWB3DQXS5QNZG1", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4606), null },
                    { "01HQ7BZJXP3MAWB3DQXS5QNZ03", "ملوخية بالفراخ", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3858), 1, "https://foodimages.store/molokhia/main.jpg", "01HQ6BZJXP2MAWB3DQXS5QNZE2", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3860), null },
                    { "01HQ7BZJXP3NAWB3DQXS5QNZ39", "حواوشي مصري", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4622), 1, "https://foodimages.store/fastfood/hawawshi-main.jpg", "01HQ6BZJXPKMAWB3DQXS5QNZG2", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4623), null },
                    { "01HQ7BZJXP4MAWB3DQXS5QNZ04", "طبق ملوخية مع الأرز", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3876), 2, "https://foodimages.store/molokhia/alt.jpg", "01HQ6BZJXP2MAWB3DQXS5QNZE2", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3877), null },
                    { "01HQ7BZJXP4NAWB3DQXS5QNZ40", "حواوشي مع السلطة", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4639), 2, "https://foodimages.store/fastfood/hawawshi-alt.jpg", "01HQ6BZJXPKMAWB3DQXS5QNZG2", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4640), null },
                    { "01HQ7BZJXP5MAWB3DQXS5QNZ05", "فول مدمس مصري", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3893), 1, "https://foodimages.store/ful/main.jpg", "01HQ6BZJXP3MAWB3DQXS5QNZE3", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3894), null },
                    { "01HQ7BZJXP6MAWB3DQXS5QNZ06", "فول مدمس مع الزيت والليمون", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3911), 2, "https://foodimages.store/ful/alt.jpg", "01HQ6BZJXP3MAWB3DQXS5QNZE3", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3912), null },
                    { "01HQ7BZJXP7MAWB3DQXS5QNZ07", "مشويات مشكلة مصرية", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3928), 1, "https://foodimages.store/mixedgrill/main.jpg", "01HQ6BZJXP4MAWB3DQXS5QNZE4", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3929), null },
                    { "01HQ7BZJXP8MAWB3DQXS5QNZ08", "تشكيلة من اللحوم المشوية", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3945), 2, "https://foodimages.store/mixedgrill/alt.jpg", "01HQ6BZJXP4MAWB3DQXS5QNZE4", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3946), null },
                    { "01HQ7BZJXP9MAWB3DQXS5QNZ09", "كنافة نابلسية", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3962), 1, "https://foodimages.store/kunafa/main.jpg", "01HQ6BZJXP5MAWB3DQXS5QNZE5", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3963), null },
                    { "01HQ7BZJXPAMAWB3DQXS5QNZ10", "كنافة بالقشطة والمكسرات", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3979), 2, "https://foodimages.store/kunafa/alt.jpg", "01HQ6BZJXP5MAWB3DQXS5QNZE5", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3980), null },
                    { "01HQ7BZJXPBMAWB3DQXS5QNZ11", "عيش بلدي طازج", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3996), 1, "https://foodimages.store/bread/baladi-main.jpg", "01HQ6BZJXP6MAWB3DQXS5QNZE6", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(3997), null },
                    { "01HQ7BZJXPCMAWB3DQXS5QNZ12", "عيش بلدي مقطع", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4013), 2, "https://foodimages.store/bread/baladi-alt.jpg", "01HQ6BZJXP6MAWB3DQXS5QNZE6", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4014), null },
                    { "01HQ7BZJXPDMAWB3DQXS5QNZ13", "سمك قاروص مشوي", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4030), 1, "https://foodimages.store/fish/seabass-main.jpg", "01HQ6BZJXP7MAWB3DQXS5QNZE7", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4031), null },
                    { "01HQ7BZJXPEMAWB3DQXS5QNZ14", "طبق سمك قاروص مع الخضار", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4047), 2, "https://foodimages.store/fish/seabass-alt.jpg", "01HQ6BZJXP7MAWB3DQXS5QNZE7", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4048), null },
                    { "01HQ7BZJXPFMAWB3DQXS5QNZ15", "فتة باللحمة المصرية", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4064), 1, "https://foodimages.store/fattah/meat-main.jpg", "01HQ6BZJXP8MAWB3DQXS5QNZE8", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4065), null },
                    { "01HQ7BZJXPGMAWB3DQXS5QNZ16", "فتة مع الخبز المحمص", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4081), 2, "https://foodimages.store/fattah/meat-alt.jpg", "01HQ6BZJXP8MAWB3DQXS5QNZE8", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4082), null },
                    { "01HQ7BZJXPHMAWB3DQXS5QNZ17", "بابا غنوج", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4099), 1, "https://foodimages.store/appetizers/babaganoush-main.jpg", "01HQ6BZJXP9MAWB3DQXS5QNZE9", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4100), null },
                    { "01HQ7BZJXPIMAWB3DQXS5QNZ18", "بابا غنوج مع الخبز", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4116), 2, "https://foodimages.store/appetizers/babaganoush-alt.jpg", "01HQ6BZJXP9MAWB3DQXS5QNZE9", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4117), null },
                    { "01HQ7BZJXPJMAWB3DQXS5QNZ19", "شوربة عدس", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4134), 1, "https://foodimages.store/soups/lentil-main.jpg", "01HQ6BZJXPAMAWB3DQXS5QNZF1", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4135), null },
                    { "01HQ7BZJXPKMAWB3DQXS5QNZ20", "شوربة عدس مع الليمون", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4151), 2, "https://foodimages.store/soups/lentil-alt.jpg", "01HQ6BZJXPAMAWB3DQXS5QNZF1", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4153), null },
                    { "01HQ7BZJXPLMAWB3DQXS5QNZ21", "أرز صيادية", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4169), 1, "https://foodimages.store/rice/sayadia-main.jpg", "01HQ6BZJXPBMAWB3DQXS5QNZF2", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4171), null },
                    { "01HQ7BZJXPMMAWB3DQXS5QNZ22", "أرز صيادية مع السمك", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4187), 2, "https://foodimages.store/rice/sayadia-alt.jpg", "01HQ6BZJXPBMAWB3DQXS5QNZF2", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4189), null },
                    { "01HQ7BZJXPNMAWB3DQXS5QNZ23", "مكرونة إسكندراني", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4202), 1, "https://foodimages.store/pasta/alexandria-main.jpg", "01HQ6BZJXPCMAWB3DQXS5QNZF3", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4203), null },
                    { "01HQ7BZJXPOMAWB3DQXS5QNZ24", "مكرونة إسكندراني بالجمبري", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4219), 2, "https://foodimages.store/pasta/alexandria-alt.jpg", "01HQ6BZJXPCMAWB3DQXS5QNZF3", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4221), null },
                    { "01HQ7BZJXPPMAWB3DQXS5QNZ25", "شاي مصري", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4236), 1, "https://foodimages.store/beverages/tea-main.jpg", "01HQ6BZJXPDMAWB3DQXS5QNZF4", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4237), null },
                    { "01HQ7BZJXPQMAWB3DQXS5QNZ26", "شاي مصري بالنعناع", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4253), 2, "https://foodimages.store/beverages/tea-alt.jpg", "01HQ6BZJXPDMAWB3DQXS5QNZF4", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4255), null },
                    { "01HQ7BZJXPRMAWB3DQXS5QNZ27", "عصير قصب طازج", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4270), 1, "https://foodimages.store/beverages/sugarcane-main.jpg", "01HQ6BZJXPEMAWB3DQXS5QNZF5", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4272), null },
                    { "01HQ7BZJXPSMAWB3DQXS5QNZ28", "عصير قصب في كوب", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4288), 2, "https://foodimages.store/beverages/sugarcane-alt.jpg", "01HQ6BZJXPEMAWB3DQXS5QNZF5", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4290), null },
                    { "01HQ7BZJXPTMAWB3DQXS5QNZ29", "فراخ مشوية", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4446), 1, "https://foodimages.store/chicken/grilled-main.jpg", "01HQ6BZJXPFMAWB3DQXS5QNZF6", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4448), null },
                    { "01HQ7BZJXPUMAWB3DQXS5QNZ30", "فراخ مشوية مع الأرز", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4464), 2, "https://foodimages.store/chicken/grilled-alt.jpg", "01HQ6BZJXPFMAWB3DQXS5QNZF6", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4465), null },
                    { "01HQ7BZJXPVMAWB3DQXS5QNZ31", "كفتة مشوية", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4480), 1, "https://foodimages.store/meat/kofta-main.jpg", "01HQ6BZJXPGMAWB3DQXS5QNZF7", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4482), null },
                    { "01HQ7BZJXPWMAWB3DQXS5QNZ32", "كفتة مشوية مع السلطة", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4498), 2, "https://foodimages.store/meat/kofta-alt.jpg", "01HQ6BZJXPGMAWB3DQXS5QNZF7", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4500), null },
                    { "01HQ7BZJXPXMAWB3DQXS5QNZ33", "مخلل مشكل", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4515), 1, "https://foodimages.store/pickles/mixed-main.jpg", "01HQ6BZJXPHMAWB3DQXS5QNZF8", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4517), null },
                    { "01HQ7BZJXPYMAWB3DQXS5QNZ34", "تشكيلة مخللات", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4532), 2, "https://foodimages.store/pickles/mixed-alt.jpg", "01HQ6BZJXPHMAWB3DQXS5QNZF8", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4534), null },
                    { "01HQ7BZJXPZMAWB3DQXS5QNZ35", "دقة مصرية", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4553), 1, "https://foodimages.store/spices/mix-main.jpg", "01HQ6BZJXPIMAWB3DQXS5QNZF9", new DateTime(2024, 11, 11, 17, 5, 7, 656, DateTimeKind.Utc).AddTicks(4554), null }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "VariantId", "CompareAtPrice", "CostPrice", "CreatedAt", "InventoryQuantity", "LowStockThreshold", "Name", "Price", "ProductId", "UpdatedAt", "Weight", "WeightUnit" },
                values: new object[,]
                {
                    { "01HQ8BZJXP1MAWB3DQXS5QNZ01", 30.00m, 15.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(826), 100, 20, "كشري صغير", 25.00m, "01HQ6BZJXP1MAWB3DQXS5QNZE1", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(828), 250m, "جرام" },
                    { "01HQ8BZJXP2MAWB3DQXS5QNZ02", 40.00m, 20.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(852), 100, 20, "كشري متوسط", 35.00m, "01HQ6BZJXP1MAWB3DQXS5QNZE1", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(854), 350m, "جرام" },
                    { "01HQ8BZJXP3MAWB3DQXS5QNZ03", 50.00m, 25.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(875), 100, 20, "كشري كبير", 45.00m, "01HQ6BZJXP1MAWB3DQXS5QNZE1", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(876), 500m, "جرام" },
                    { "01HQ8BZJXP4MAWB3DQXS5QNZ04", 50.00m, 25.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(899), 50, 10, "ملوخية بالفراخ - حصة صغيرة", 45.00m, "01HQ6BZJXP2MAWB3DQXS5QNZE2", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(901), 300m, "جرام" },
                    { "01HQ8BZJXP5MAWB3DQXS5QNZ05", 75.00m, 40.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(922), 50, 10, "ملوخية بالفراخ - حصة متوسطة", 65.00m, "01HQ6BZJXP2MAWB3DQXS5QNZE2", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(924), 500m, "جرام" },
                    { "01HQ8BZJXP6MAWB3DQXS5QNZ06", 95.00m, 55.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(947), 50, 10, "ملوخية بالفراخ - حصة كبيرة", 85.00m, "01HQ6BZJXP2MAWB3DQXS5QNZE2", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(949), 700m, "جرام" },
                    { "01HQ8BZJXP7MAWB3DQXS5QNZ07", 12.00m, 5.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(970), 100, 20, "فول ساندويتش", 10.00m, "01HQ6BZJXP3MAWB3DQXS5QNZE3", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(971), 200m, "جرام" },
                    { "01HQ8BZJXP8MAWB3DQXS5QNZ08", 18.00m, 8.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(992), 100, 20, "فول طبق", 15.00m, "01HQ6BZJXP3MAWB3DQXS5QNZE3", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(993), 300m, "جرام" },
                    { "01HQ8BZJXP9MAWB3DQXS5QNZ09", 140.00m, 80.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1013), 30, 10, "مشويات مشكلة - حصة صغيرة", 120.00m, "01HQ6BZJXP4MAWB3DQXS5QNZE4", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1014), 400m, "جرام" },
                    { "01HQ8BZJXPAMAWB3DQXS5QNZ10", 200.00m, 120.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1035), 30, 10, "مشويات مشكلة - حصة متوسطة", 180.00m, "01HQ6BZJXP4MAWB3DQXS5QNZE4", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1038), 600m, "جرام" },
                    { "01HQ8BZJXPBMAWB3DQXS5QNZ11", 260.00m, 160.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1059), 30, 10, "مشويات مشكلة - حصة كبيرة", 240.00m, "01HQ6BZJXP4MAWB3DQXS5QNZE4", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1060), 800m, "جرام" },
                    { "01HQ8BZJXPCMAWB3DQXS5QNZ12", 30.00m, 15.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1081), 50, 10, "كنافة - قطعة", 25.00m, "01HQ6BZJXP5MAWB3DQXS5QNZE5", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1082), 150m, "جرام" },
                    { "01HQ8BZJXPDMAWB3DQXS5QNZ13", 200.00m, 120.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1103), 20, 5, "كنافة - علبة", 180.00m, "01HQ6BZJXP5MAWB3DQXS5QNZE5", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1105), 1000m, "جرام" },
                    { "01HQ8BZJXPEMAWB3DQXS5QNZ14", 120.00m, 70.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1126), 30, 10, "قاروص مشوي - حصة صغيرة", 100.00m, "01HQ6BZJXP7MAWB3DQXS5QNZE7", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1127), 250m, "جرام" },
                    { "01HQ8BZJXPFMAWB3DQXS5QNZ15", 180.00m, 100.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1150), 30, 10, "قاروص مشوي - حصة متوسطة", 160.00m, "01HQ6BZJXP7MAWB3DQXS5QNZE7", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1152), 400m, "جرام" },
                    { "01HQ8BZJXPGMAWB3DQXS5QNZ16", 240.00m, 150.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1171), 30, 10, "قاروص مشوي - حصة كبيرة", 220.00m, "01HQ6BZJXP7MAWB3DQXS5QNZE7", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1173), 600m, "جرام" },
                    { "01HQ8BZJXPHMAWB3DQXS5QNZ17", 60.00m, 30.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1193), 50, 10, "ربع فراخ - قطعة فاتحة", 50.00m, "01HQ6BZJXPFMAWB3DQXS5QNZF6", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1194), 250m, "جرام" },
                    { "01HQ8BZJXPIMAWB3DQXS5QNZ18", 65.00m, 35.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1215), 50, 10, "ربع فراخ - قطعة داكنة", 55.00m, "01HQ6BZJXPFMAWB3DQXS5QNZF6", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1216), 250m, "جرام" },
                    { "01HQ8BZJXPJMAWB3DQXS5QNZ19", 100.00m, 60.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1237), 30, 5, "دجاجة كاملة مشوية", 90.00m, "01HQ6BZJXPFMAWB3DQXS5QNZF6", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1238), 1000m, "جرام" },
                    { "01HQ8BZJXPKMAWB3DQXS5QNZ20", 95.00m, 55.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1455), 50, 10, "كفتة طبق", 85.00m, "01HQ6BZJXPGMAWB3DQXS5QNZF7", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1457), 300m, "جرام" },
                    { "01HQ8BZJXPLMAWB3DQXS5QNZ21", 45.00m, 25.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1479), 100, 20, "كفتة ساندويتش", 40.00m, "01HQ6BZJXPGMAWB3DQXS5QNZF7", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1480), 200m, "جرام" },
                    { "01HQ8BZJXPMMAWB3DQXS5QNZ22", 35.00m, 20.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1502), 100, 20, "حواوشي صغير", 30.00m, "01HQ6BZJXPKMAWB3DQXS5QNZG2", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1503), 200m, "جرام" },
                    { "01HQ8BZJXPNMAWB3DQXS5QNZ23", 45.00m, 25.00m, new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1549), 100, 20, "حواوشي كبير", 40.00m, "01HQ6BZJXPKMAWB3DQXS5QNZG2", new DateTime(2024, 11, 11, 17, 5, 7, 655, DateTimeKind.Utc).AddTicks(1551), 300m, "جرام" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { "01HQ5RZJXP9MAWB3DQXS5QNZEP", "01HQ6BZJXP1MAWB3DQXS5QNZE1" },
                    { "01HQ5RZJXPAMAWB3DQXS5QNZEQ", "01HQ6BZJXP1MAWB3DQXS5QNZE1" },
                    { "01HQ5RZJXPBMAWB3DQXS5QNZER", "01HQ6BZJXP2MAWB3DQXS5QNZE2" },
                    { "01HQ5RZJXPEMAWB3DQXS5QNZEU", "01HQ6BZJXP2MAWB3DQXS5QNZE2" },
                    { "01HQ5RZJXP6MAWB3DQXS5QNZEM", "01HQ6BZJXP3MAWB3DQXS5QNZE3" },
                    { "01HQ5RZJXPBMAWB3DQXS5QNZER", "01HQ6BZJXP3MAWB3DQXS5QNZE3" },
                    { "01HQ5RZJXP2MAWB3DQXS5QNZEI", "01HQ6BZJXP4MAWB3DQXS5QNZE4" },
                    { "01HQ5RZJXPFMAWB3DQXS5QNZEV", "01HQ6BZJXP4MAWB3DQXS5QNZE4" },
                    { "01HQ5RZJXP3MAWB3DQXS5QNZEJ", "01HQ6BZJXP5MAWB3DQXS5QNZE5" },
                    { "01HQ5RZJXP4MAWB3DQXS5QNZEK", "01HQ6BZJXP6MAWB3DQXS5QNZE6" },
                    { "01HQ5RZJXP2MAWB3DQXS5QNZEI", "01HQ6BZJXP7MAWB3DQXS5QNZE7" },
                    { "01HQ5RZJXP5MAWB3DQXS5QNZEL", "01HQ6BZJXP7MAWB3DQXS5QNZE7" },
                    { "01HQ5RZJXPFMAWB3DQXS5QNZEV", "01HQ6BZJXP8MAWB3DQXS5QNZE8" },
                    { "01HQ5RZJXP7MAWB3DQXS5QNZEN", "01HQ6BZJXP9MAWB3DQXS5QNZE9" },
                    { "01HQ5RZJXPGMAWB3DQXS5QNZEW", "01HQ6BZJXP9MAWB3DQXS5QNZE9" },
                    { "01HQ5RZJXP8MAWB3DQXS5QNZEO", "01HQ6BZJXPAMAWB3DQXS5QNZF1" },
                    { "01HQ5RZJXPGMAWB3DQXS5QNZEW", "01HQ6BZJXPAMAWB3DQXS5QNZF1" },
                    { "01HQ5RZJXP5MAWB3DQXS5QNZEL", "01HQ6BZJXPBMAWB3DQXS5QNZF2" },
                    { "01HQ5RZJXP9MAWB3DQXS5QNZEP", "01HQ6BZJXPBMAWB3DQXS5QNZF2" },
                    { "01HQ5RZJXP5MAWB3DQXS5QNZEL", "01HQ6BZJXPCMAWB3DQXS5QNZF3" },
                    { "01HQ5RZJXPAMAWB3DQXS5QNZEQ", "01HQ6BZJXPCMAWB3DQXS5QNZF3" },
                    { "01HQ5RZJXPCMAWB3DQXS5QNZES", "01HQ6BZJXPDMAWB3DQXS5QNZF4" },
                    { "01HQ5RZJXPDMAWB3DQXS5QNZET", "01HQ6BZJXPEMAWB3DQXS5QNZF5" },
                    { "01HQ5RZJXP2MAWB3DQXS5QNZEI", "01HQ6BZJXPFMAWB3DQXS5QNZF6" },
                    { "01HQ5RZJXPEMAWB3DQXS5QNZEU", "01HQ6BZJXPFMAWB3DQXS5QNZF6" },
                    { "01HQ5RZJXP2MAWB3DQXS5QNZEI", "01HQ6BZJXPGMAWB3DQXS5QNZF7" },
                    { "01HQ5RZJXPFMAWB3DQXS5QNZEV", "01HQ6BZJXPGMAWB3DQXS5QNZF7" },
                    { "01HQ5RZJXPHMAWB3DQXS5QNZEX", "01HQ6BZJXPHMAWB3DQXS5QNZF8" },
                    { "01HQ5RZJXPIMAWB3DQXS5QNZEY", "01HQ6BZJXPIMAWB3DQXS5QNZF9" },
                    { "01HQ5RZJXPJMAWB3DQXS5QNZEZ", "01HQ6BZJXPJMAWB3DQXS5QNZG1" },
                    { "01HQ5RZJXPFMAWB3DQXS5QNZEV", "01HQ6BZJXPKMAWB3DQXS5QNZG2" },
                    { "01HQ5RZJXPKMAWB3DQXS5QNZF0", "01HQ6BZJXPKMAWB3DQXS5QNZG2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_ApplicationUserId",
                table: "Admins",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_Code",
                table: "Coupons",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ApplicationUserId",
                table: "Customers",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_VariantId",
                table: "OrderItems",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CouponId",
                table: "Orders",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingAddressId",
                table: "Orders",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_VariantId",
                table: "ProductImages",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId",
                table: "Reviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OrderId",
                table: "Reviews",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_CustomerId_ProductId",
                table: "WishlistItems",
                columns: new[] { "CustomerId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_ProductId",
                table: "WishlistItems",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "WishlistItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
