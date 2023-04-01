using CustomerAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
//register start up
var startup = new Startup(builder.Configuration);
//register DI
startup.ConfigureServices(builder.Services);
// register app level configuration
var app = builder.Build();
startup.Configure(app, builder.Environment);
