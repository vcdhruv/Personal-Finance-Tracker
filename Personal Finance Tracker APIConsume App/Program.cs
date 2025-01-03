var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Session 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // for custom time span by default minutes will be 20
}
    ); // for session 
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // for registering iHttpContextAccessor for accessing session varaibles in views directly...
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

#region Session
app.UseSession(); // for session
#endregion

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

#region Area
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=User}/{action=Login}/{id?}");
#endregion

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
