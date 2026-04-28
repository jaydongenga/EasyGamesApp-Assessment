using EasyGamesApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add MVC (controllers + views)
builder.Services.AddControllersWithViews();

// Register our repository - reads connection string from appsettings.json
builder.Services.AddScoped<TransactionRepository>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var connString = config.GetConnectionString("DefaultConnection")!;
    return new TransactionRepository(connString);
});

var app = builder.Build();

// Standard middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Default route: goes to ClientController → Index action
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Client}/{action=Index}/{id?}");

app.Run();