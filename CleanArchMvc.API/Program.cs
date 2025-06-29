using CleanArchMvc.Domain.Account;
using CleanArchMvc.Infra.Data.Settings;
using CleanArchMvc.Infra.IoC;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructureAPI(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson(x =>
{
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JWT")
);

builder.Services.AddControllers();  
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CleanArchMvc.API",
        Description = "Clean Arch",
        Version = "1.0.0"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seedUserRoleInitial = services.GetRequiredService<ISeedUserRoleInitial>();
    seedUserRoleInitial.SeedRoles();
    seedUserRoleInitial.SeedUsers();
}
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();