using ApiEcommerce.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dbConnectionString = builder.Configuration.GetConnectionString("ConexionSql");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnectionString));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(cfg => {

    // cfg.AddProfile<CategoryProfile>();

    cfg.AddMaps(typeof(Program).Assembly);

});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            // Si quieres permitir cualquier origen:
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();

            // ⚠️ O si quieres permitir solo ciertos dominios, usa esto en vez de AllowAnyOrigin:
            // policy.WithOrigins("http://localhost:3000")
            //       .AllowAnyMethod()
            //       .AllowAnyHeader();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
