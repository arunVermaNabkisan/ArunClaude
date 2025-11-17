using Microsoft.AspNetCore.Authentication.Cookies;
using SambandhCRM.Core.Interfaces;
using SambandhCRM.Core.Services;
using SambandhCRM.Data.Context;
using SambandhCRM.Data.Repositories;
using SambandhCRM.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Register DapperContext
builder.Services.AddSingleton<DapperContext>();

// Register Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ILeadRepository, LeadRepository>();
builder.Services.AddScoped<IMasterDataRepository, MasterDataRepository>();
// Register Services
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<IAuthenticationTokenService, AuthenticationTokenService>();

// Register Services
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// Add Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.Name = "SambandhCRM.Auth";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", policy => policy.RequireRole("Administrator"));
    options.AddPolicy("SeniorManagement", policy => policy.RequireRole("Administrator", "Senior Management"));
    options.AddPolicy("RegionalManager", policy => policy.RequireRole("Administrator", "Regional Manager"));
    options.AddPolicy("RelationshipManager", policy => policy.RequireRole("Administrator", "Regional Manager", "Relationship Manager"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
