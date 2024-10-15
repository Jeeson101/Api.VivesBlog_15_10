
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using VivesBlog.Configuration;
using VivesBlog.Core;
using VivesBlog.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	var securityDefinititon = new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Description = "JWT Authorization header using the Bearer scheme.",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,

	};

	options.AddSecurityDefinition("Bearer", securityDefinititon);

	var securityRequirementScheme = new OpenApiSecurityScheme
	{
		Reference = new OpenApiReference
		{
			Id = "Bearer",
			Type = ReferenceType.SecurityScheme
		}
	};

	var securityRequirement = new OpenApiSecurityRequirement
	{
		{ securityRequirementScheme, new string[] { } }
	};
	options.AddSecurityRequirement(securityRequirement);
}) .AddIdentity<IdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<VivesBlogDbContext>();


var jwtSettings = new JwtSettings();
builder.Configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.SaveToken = true;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
		ValidateIssuer = false,
		ValidateAudience = false,
		RequireExpirationTime = false,
		ValidateLifetime = true
	};
});


builder.Services.AddDbContext<VivesBlogDbContext>(options =>
{
    options.UseInMemoryDatabase(nameof(VivesBlogDbContext));
});

builder.Services.AddScoped<IdentityService>();
builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<PersonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var database = scope.ServiceProvider.GetRequiredService<VivesBlogDbContext>();
    database.Seed();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
