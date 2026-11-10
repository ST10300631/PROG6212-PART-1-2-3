using RaceDaySystem.Client.Services;


var builder = WebApplication.CreateBuilder(args);

// Add standard MVC controller services to the container
builder.Services.AddControllersWithViews();

// Enable distributed cache and session middleware configuration profiles
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// =========================================================================
// CORRECTION: Only use AddHttpClient. (REMOVED AddScoped overrides below)
// =========================================================================

// 1. Configure the Auth service with your API Base Address URL
builder.Services.AddHttpClient<AuthApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7142/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// 2. Configure the Race Day service with the same API Base Address URL
builder.Services.AddHttpClient<RaceDayApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7142/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// 3. Register the generic named HttpClient for manual controller factory invocation
builder.Services.AddHttpClient("RaceDayApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7142/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});



var app = builder.Build();


// Configure the HTTP request pipeline middleware modules
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// CRUCIAL: UseSession must be placed AFTER UseRouting and BEFORE UseAuthorization
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
