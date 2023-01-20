using carrinhoBack.Auth;
using carrinhoBack.Context;
using carrinhoBack.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//security
builder.Services.AddSwaggerGen(option=>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Informe o token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }

                });
});
//DB Im Memory
builder.Services.AddDbContext<CarrinhoContext>(opt => opt.UseInMemoryDatabase("Carrinho"));

//Injecao de dependencia do Repository
builder.Services.AddScoped(typeof(UsuarioRepository));
builder.Services.AddScoped(typeof(ProdutoRepository));


//Injeção de dependência do JWT Token
var tokenConfiguration = new TokenConfig();
new ConfigureFromConfigurationOptions<TokenConfig>(builder.Configuration.GetSection("TokenConfiguration")).Configure(tokenConfiguration);
builder.Services.AddSingleton(tokenConfiguration);
var generateToken = new TokenService(tokenConfiguration);
builder.Services.AddScoped(typeof(TokenService));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.Secret)),
        ValidateAudience = false,
        ValidateIssuer = false,
    };
});
//Registra Data Generator
builder.Services.AddTransient<DataGenerator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
//popula o banco de dados
var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
using (var scope = scopedFactory.CreateScope())
{
    var service = scope.ServiceProvider.GetService<DataGenerator>();
    service.Generate();
}

app.Run();

