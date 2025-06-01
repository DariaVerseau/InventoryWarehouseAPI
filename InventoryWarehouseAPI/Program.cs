using BLL.Services;
using DAL.EF;
using DAL.Interfaces;
using DAL.Repositories;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Для PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
// Регистрация репозиториев
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryTransactionRepository, InventoryTransactionRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();

// Регистрация сервисов BLL
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IInventoryTransactionService, InventoryTransactionService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();

// Настройка AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();