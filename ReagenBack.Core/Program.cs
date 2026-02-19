
using Measure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Mappers genéricos

builder.Services.AddScoped<IMapper<Package, PackageCreateDto, PackageReadDto>, PackageMapper>();
builder.Services.AddScoped<IMapper<Vial, VialCreateDto, VialReadDto>, VialMapper>();
builder.Services.AddScoped<IMapper<Reagent, ReagentCreateDto, ReagentReadDto>, ReagentMapper>();
builder.Services.AddScoped<IMapper<Size, SizeCreateDto, SizeReadDto>, SizeMapper>();
builder.Services.AddScoped<IMapper<Supplier, SupplierCreateDto, SupplierReadDto>, SupplierMapper>();
builder.Services.AddScoped<IMapper<FundingSource, FundingSourceCreateDto, FundingSourceReadDto>, FundingSourceMapper>();
builder.Services.AddScoped<IMapper<Laboratory, LaboratoryCreateDto, LaboratoryReadDto>, LaboratoryMapper>();
builder.Services.AddScoped<IMapper<ControlAgency, ControlAgencyCreateDto, ControlAgencyReadDto>, ControlAgencyMapper>();

// Mappers específicos

builder.Services.AddScoped<PackageMapper>();
builder.Services.AddScoped<VialMapper>();
builder.Services.AddScoped<ReagentMapper>();
builder.Services.AddScoped<SizeMapper>();
builder.Services.AddScoped<SupplierMapper>();
builder.Services.AddScoped<FundingSourceMapper>();
builder.Services.AddScoped<LaboratoryMapper>();
builder.Services.AddScoped<ControlAgencyMapper>();


// const bool TEST =  true;

// if (TEST)
// {
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite("DataSource=test.db;Foreign Keys=True"));
    
// }
// else
// {
//     builder.Services.AddDbContext<AppDbContext>(options =>
//         options.UseNpgsql(
//             builder.Configuration.GetConnectionString("DefaultConnection")
//         )
//     );
// }

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Reagen API", Version = "v1" });
});

var app = builder.Build();

// if (TEST)
// {
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.OpenConnection();
    db.Database.Migrate();
// }


if (app.Environment.IsDevelopment())
{
    // É um middleware!
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reagen API V1");
    });
}

// if (app.Environment.IsProduction())
// {
// app.UseHttpsRedirection();
// }

app.MapControllers();
app.Run();
