using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using ebay.application;
using ebay.application.Interfaces;
using ebay.domain.Interfaces;
using ebay.infrastructure.Data;
using ebay.infrastructure.Queries;
using ebay.infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();


// service EF
var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddDbContext<EBayDbContext>(options =>
    options.UseSqlServer(connectionString));


// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("allow_origin", policy =>
    {
        policy.WithOrigins("http://localhost:3001")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});
// MediaT
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);

});

// Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AppSettings:Audience"],
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Key"]!)),
        ValidateIssuerSigningKey = true,
        RoleClaimType = ClaimTypes.Role,
        NameClaimType = ClaimTypes.Name

    };
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = ValidateUserToken.HandleValidateUserToken,
        OnChallenge = JwtEventsHandlers.HandleJwtChallenge,
        OnForbidden = async context =>
        {
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new
            {
                message = AuthMessages.FORBIDDEN,
            });
            await context.Response.WriteAsync(result);
        }
    };
});

// ADD AUTHORIZATION
// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("ActiveUser", policy =>
//     {
//         policy.RequireAuthenticatedUser();
//         policy.AddRequirements(new ActiveUserRequirement());
//     });
// });
builder.Services.AddAuthorization();
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // ❗ Đừng dùng CamelCase
    });

builder.Services.AddSwaggerGen();

// Auto Mapper
builder.Services.AddAutoMapper(typeof(MapperTool).Assembly);
builder.Services.AddAutoMapper(typeof(MapperToolInfras).Assembly);

// Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductListCategoryRepository, ProductListCategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IListingProductDetailRepository, ListingProductDetailRepository>();
builder.Services.AddScoped<IOrdersListingDetailRepository, OrdersListingDetailRepository>();
builder.Services.AddScoped<IOrderDetailQuery, OrderDetailQuery>();
builder.Services.AddScoped<IGetOrdersBySellerQuery, GetOrdersBySellerQuery>();
builder.Services.AddScoped<IListingRepository, ListingRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddHttpContextAccessor();
// Config default response
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = CustomModelStateResponse.HandleCustomModelStateResponse;
});

var app = builder.Build();

// CORS
app.UseCors("allow_origin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();