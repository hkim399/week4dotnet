using Microsoft.EntityFrameworkCore;
// using HealthAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// comment this out for docker
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HealthContext>(options => options.UseSqlServer(connectionString));



// this will use mysql instead of just sql
// var host = builder.Configuration["DBHOST"] ?? "localhost";
// var port = builder.Configuration["DBPORT"] ?? "3333";
// // root password
// var password = builder.Configuration["DBPASSWORD"] ?? "secret";
// var db = builder.Configuration["DBNAME"] ?? "HealthDB";

// string connectionString = $"server={host}; userid=root; pwd={password};"
//         + $"port={port}; database={db};SslMode=none;allowpublickeyretrieval=True";



// for mysql and the pomello, you need to specify the version
var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));
builder.Services.AddDbContext<HealthContext>(options => options.UseMySql(connectionString, serverVersion));



// json generator
builder.Services.AddControllers().AddNewtonsoftJson(options =>
  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Add Cors
// will use this policy
// now paste [EnableCors("HealthPolicy")] this to everything that needs (expose) this policy
builder.Services.AddCors(o => o.AddPolicy("HealthPolicy", builder => {
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));


var app = builder.Build(); //=========================================
// anything above this is intent to use service
// anything below is using the service (what you want to use)

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// swagger is only used in development, if you deploy it, it disappears.


app.UseHttpsRedirection();

// for CORS
app.UseRouting();
app.UseCors(); 


app.UseAuthorization();

app.MapControllers();
// automatically maps the controller etc..


// build.service at the top was saying we will be using service and this will
// this is singleton
// this is automatically do same thing as "dotnet -ef database update"
// need this for dockerizing your application
using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<HealthContext>();    
    context.Database.Migrate();
}


app.Run();
