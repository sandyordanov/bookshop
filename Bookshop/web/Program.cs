using BLL;
using DAL;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddTransient<BookManagement>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/Login");
    options.AccessDeniedPath = new PathString("/Error");
});
builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("Admin",
        policy => policy.RequireClaim("UserType", "Admin"));
    options.AddPolicy("User",
        policy => policy.RequireClaim("UserType", "User"));
});
builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "BookSesh";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

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
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();

app.Run();
