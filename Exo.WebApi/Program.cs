using Exo.WebApi.Contexts;
using Exo.WebApi.Repositories;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ExoContext, ExoContext>();
builder.Services.AddControllers();

// Forma de autenticac�o.
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "JwtBearer";
        options.DefaultChallengeScheme = "JwtBearer";
    })
// Par�metros de validac�o do token.
    .AddJwtBearer("JwtBearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Valida quem est� solicitando.
            ValidateIssuer = true,
            // Valida quem est� recebendo.
            ValidateAudience = true,
            // Define se o tempo de expira��o ser� validado.
            ValidateLifetime = true,
            // Criptografia e valida��o da chave de autenticac�o.
            IssuerSigningKey = new
                SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chaveautenticacao")),
            // Valida o tempo de expira��o do token.
            ClockSkew = TimeSpan.FromMinutes(30),
            // Nome do issuer, da origem.
            ValidIssuer = "exoapi.webapi",
            // Nome do audience, para o destino.
            ValidAudience = "exoapi.webapi"
        };
    });

builder.Services.AddTransient<ProjetoRepository, ProjetoRepository>();
builder.Services.AddTransient<UsuarioRepository, UsuarioRepository>();


var app = builder.Build();

app.UseRouting();

// Habilita a autentica��o.
app.UseAuthentication();
// Habilita a autoriza��o.
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
