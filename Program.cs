using YellowPagesAPI.EntityData;
using Microsoft.EntityFrameworkCore;
using YellowPagesAPI.Services.Interface;
using YellowPagesAPI.Services.Implementation;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDBContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("ApiDBConnection")));

builder.Services.AddControllers();
builder.Services.AddMvcCore().AddDataAnnotations();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Application DI services
builder.Services.AddTransient<IContactService, ContactService>();
#endregion

#region Application CORS Policy
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var allOrigins = "allOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: allOrigins,
        policy =>
        {
            policy.WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader();
        }
    );
    options.AddPolicy(
        name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:3000")
                .AllowAnyMethod()
                .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization, "x-custom-header")
                .AllowCredentials();
        }
    );
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors(allOrigins);
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseCors(MyAllowSpecificOrigins);
}

//app.UseRouting();
//app.UseEndpoints();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
