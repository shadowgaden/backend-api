var builder = WebApplication.CreateBuilder(args);

// ======================
// ADD SERVICES
// ======================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 👉 CORS cho FE
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ❌ Somee free KHÔNG hỗ trợ HTTPS chuẩn
// app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

// ======================
// MAP CONTROLLERS
// ======================
app.MapControllers();

app.Run();
