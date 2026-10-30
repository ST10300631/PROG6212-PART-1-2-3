using RaceDaySystem.Client.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1. Configure the Auth service with your API Base Address URL
builder.Services.AddHttpClient<RaceDaySystem.Client.Services.AuthApiService>(client =>
{
    // Replace with your EXACT backend API running port (e.g., 7123, 5001, etc.)
    client.BaseAddress = new Uri("https://localhost:7142/");
});

// 2. Configure the Race Day service with the same API Base Address URL
builder.Services.AddHttpClient<RaceDaySystem.Client.Services.RaceDayApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7142/");
});


//Dependency injection
builder.Services.AddScoped<RaceDayApiService>();

//Enable session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Register AuthApiService for dependency injection
builder.Services.AddScoped<RaceDaySystem.Client.Services.AuthApiService>();

// Register the named HttpClient for your Web API backend
builder.Services.AddHttpClient("RaceDayApi", client =>
{
    // REPLACE 7123 with the actual port number your RaceDayApi project uses!
    client.BaseAddress = new Uri("https://localhost:7142/");

    client.DefaultRequestHeaders.Add("Accept", "application/json");
});



var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
