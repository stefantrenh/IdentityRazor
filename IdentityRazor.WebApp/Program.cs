using IdentityRazor.WebApp.Data;
using IdentityRazor.WebApp.Data.Account;
using IdentityRazor.WebApp.Services;
using IdentityRazor.WebApp.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddIdentity<User, IdentityRole>(options => 
                 {
                     options.Password.RequiredLength = 8;
                     options.Password.RequireLowercase = true;
                     options.Password.RequireUppercase = true;

                     options.Lockout.MaxFailedAccessAttempts = 5;
                     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

                     options.User.RequireUniqueEmail = true;
                 })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options => 
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/AccessDenied";
});

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SMTP"));
builder.Services.AddSingleton<IEmailService, EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
