var builder = WebApplication.CreateBuilder(args);

// 1. Add YARP Service
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();
// 2. Map YARP Middleware
app.MapReverseProxy();

app.Run();
