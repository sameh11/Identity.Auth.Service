using IdentityServer4;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:5001";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true
                };
            });
builder.Services.AddAuthorization(options =>
 {
     options.AddPolicy("ApiScope", policy =>
     {
         policy.RequireAuthenticatedUser();
         policy.RequireClaim("scope", new List<string>{
             IdentityServerConstants.StandardScopes.OpenId,
             IdentityServerConstants.StandardScopes.Profile,
             "administration-parent-client",
                    "parent.write",
                    "parent.read",
                    "child.write",
                    "child.read",
         });
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

app.MapControllers().RequireAuthorization("ApiScope");

app.Run();