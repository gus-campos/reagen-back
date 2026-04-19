
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ReagenBack.Core.Contexts;
using ReagenBack.Core.Interceptors;
using ReagenBack.Core.Middlewares;
using ReagenBack.Core.Models;
using ReagenBack.Core.Repositories;
using ReagenBack.Core.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ReagenBack.Core.Configurations;
using ReagenBack.Core.Utils;

// Console
Console.ForegroundColor = ConsoleColor.Magenta;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
        opt.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        )
    );

// INJEÇÃO DE DEPENDÊNCIAS

// Middlewares

builder.Services.AddScoped<UserIdProviderMiddleware>();

// Interceptors

builder.Services.AddScoped<OwnedEntityInterceptor>();

// Mappers

builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<PackageMapper>();
builder.Services.AddScoped<VialMapper>();
builder.Services.AddScoped<ReagentMapper>();
builder.Services.AddScoped<SizeMapper>();
builder.Services.AddScoped<SupplierMapper>();
builder.Services.AddScoped<FundingSourceMapper>();
builder.Services.AddScoped<LaboratoryMapper>();
builder.Services.AddScoped<ControlAgencyMapper>();

// Repositories

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<PackageRepository>();
builder.Services.AddScoped<VialRepository>();
builder.Services.AddScoped<ReagentRepository>();
builder.Services.AddScoped<SizeRepository>();
builder.Services.AddScoped<SupplierRepository>();
builder.Services.AddScoped<FundingSourceRepository>();
builder.Services.AddScoped<LaboratoryRepository>();
builder.Services.AddScoped<ControlAgencyRepository>();

// Services

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PackageService>();
builder.Services.AddScoped<VialService>();
builder.Services.AddScoped<ReagentService>();
builder.Services.AddScoped<SizeService>();
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<FundingSourceService>();
builder.Services.AddScoped<LaboratoryService>();
builder.Services.AddScoped<ControlAgencyService>();

// Utils

// Pode injetar em várias interfaces de responsabilidade unica que limitam acesso
builder.Services.AddScoped<IStructuredHttpContext, StructuredHttpContext>(); 

// Para acessar contexto http e verificar user
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>((sp, options) =>
    options.UseSqlite("DataSource=test.db;Foreign Keys=True")
        // A cada salvamento verifica o pertencimento
        .AddInterceptors(sp.GetRequiredService<OwnedEntityInterceptor>())
);

// Autenticação
var section = "JwtSettings";
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection(section)
);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwt = builder.Configuration.GetSection(section).Get<JwtSettings>() 
            ?? throw new Exception("Chave de JWT não encontrada.");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false, // FIXME: Errado para prod
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwt.Key)
            )
        };
    });

builder.Services.AddAuthorization();

// Swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Reagen API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// App

var app = builder.Build();

// Pipeline

// Para permitir que o banco em arquivo funcione devidamente
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
db.Database.OpenConnection();
db.Database.Migrate();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reagen API V1");
    });
}

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

// Autenticação
app.UseAuthentication();
app.UseMiddleware<UserIdProviderMiddleware>();
app.UseAuthorization();

app.MapControllers();
app.Run();
