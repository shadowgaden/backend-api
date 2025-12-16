var builder = WebApplication.CreateBuilder(args);

// ======================
// ADD SERVICES
// ======================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  CORS cho FE (Render + Local đều OK)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// ======================
// MIDDLEWARE
// ======================

//  LUÔN bật Swagger (kể cả Production – Render)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend API v1");
    c.RoutePrefix = "swagger"; // https://domain/swagger
});

//  Render free / Docker KHÔNG cần HTTPS redirect
// app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

// ======================
// DEFAULT ROOT (fix 404 /)
// ======================
app.MapGet("/", () => Results.Ok(new
{
    message = "Backend API is running ",
    swagger = "/swagger"
}));

// ======================
// MAP CONTROLLERS
// ======================
app.MapControllers();

app.Run();
