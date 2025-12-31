var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Swagger services with enhanced documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Gp7_CA - Mobile App Development API",
        Version = "v1",
        Description = "API for user authentication, score submission, and leaderboard management",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Group 7",
            Email = "group7@example.com"
        }
    });
    
    // Include XML comments if available
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Enable Swagger middleware (available in all environments for demo purposes)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gp7_CA API v1");
    c.RoutePrefix = "swagger";
    c.DocumentTitle = "Gp7_CA API Documentation";
    c.DefaultModelsExpandDepth(2);
    c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
});

// Add request logging middleware for debugging
app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    
    // Log all incoming requests
    logger.LogInformation($"?? {context.Request.Method} {context.Request.Path}{context.Request.QueryString}");
    
    // Log request body for POST/PUT requests
    if (context.Request.Method == "POST" || context.Request.Method == "PUT")
    {
        context.Request.EnableBuffering();
        using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
        {
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
            
            if (!string.IsNullOrEmpty(body))
            {
                logger.LogInformation($"?? Request Body: {body}");
            }
            else
            {
                logger.LogWarning($"?? Request Body is EMPTY");
            }
        }
        
        logger.LogInformation($"?? Content-Type: {context.Request.ContentType}");
    }
    
    await next();
    
    // Log response status
    logger.LogInformation($"? Response: {context.Response.StatusCode}");
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
