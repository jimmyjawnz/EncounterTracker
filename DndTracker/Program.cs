using DndTracker.Components;
using DndTracker.Components.Account;
using DndTracker.Components.Pages.Database;
using DndTracker.Data;
using DndTracker.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


static void consoleMessage(string message)
{
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine(message);
    Console.ForegroundColor = ConsoleColor.White;
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

consoleMessage("[s] - Added Components");

builder.Services.AddSingleton<InterTabService>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

consoleMessage("[s] - Added Scoped Services");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies(options => options.ApplicationCookie!.Configure(options => {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/access-denied";
    }));

consoleMessage("[s] - Added and Configured Authentication");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
}
)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

consoleMessage("[s] - Added and Configured DB Service");

var app = builder.Build();

consoleMessage("[s] - Build Successful!");

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    consoleMessage("[s] - DB Service Start Successful!");

    var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        consoleMessage("[s] - DB Migration Start");

        context.Database.Migrate();

        consoleMessage("[s] - DB Migration Finished. DB is up to date");
    }
    catch (Exception ex) {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("[s] - DB was unable to Migrate. See detailed error below.");
        Console.WriteLine(ex.ToString(), ex.Message);
    }

    var emptyEncounters = context.Encounters.Where(e => e.Location == string.Empty && e.Title == string.Empty && e.Image == null)
                                    .Include(e => e.EncounterBlocks).ToList();
    context.Encounters.RemoveRange(emptyEncounters);
    context.SaveChanges();

    consoleMessage("[s] - Removed Empty Encounters from DB");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseMigrationsEndPoint();
}
else
{
    app.Urls.Add("http://0.0.0.0:8888");
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

consoleMessage("[s] - Mapping Complete");

consoleMessage("[s] - Encounter Tracker is starting and will run on \"http://localhost:8888\"");

app.Run();