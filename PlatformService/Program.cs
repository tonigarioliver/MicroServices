using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;
using PlatformServices.Data;

var builder = WebApplication.CreateBuilder(args);
if(builder.Environment.IsProduction()){
    Console.WriteLine("-->Using SqlServer Db");
    Console.WriteLine(builder.Configuration.GetConnectionString("PlatformsConn"));
    builder.Services.AddDbContext<AppDbContext>(options=>
        options.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
}else{
    Console.WriteLine("-->Using in memory DB");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDatabase"));
}
// Add services to the container.
//Register services here
builder.Services.AddScoped<IPlatformRepo,PlatformRepo>();
builder.Services.AddHttpClient<ICommandDataClient,HttpCommandDataClient>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Console.WriteLine($"--> CommandService Endpoint {builder.Configuration.GetValue<string>("CommandService")}");
var app = builder.Build();
PrepDb.PrepPopulation(app,builder.Environment.IsProduction());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatformService v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
