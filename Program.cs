using BSDigital.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();


app.Run();
