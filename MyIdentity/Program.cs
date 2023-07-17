using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyIdentity.Data;
using MyIdentity.Helpers;
using MyIdentity.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region add DBcontext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(connectionString));

#endregion

#region Identity

/*builder.Services.AddIdentity<IdentityUser, IdentityRole>();*/ //if you didn't add custom user & Role Use this
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<DataBaseContext>()
    .AddDefaultTokenProviders()
    .AddRoles<Role>()
    .AddErrorDescriber<CustomIdentityError>()
    .AddPasswordValidator<MyPasswordValidator>();

builder.Services.Configure<IdentityOptions>(option =>
{
    //UserSetting
    //option.User.AllowedUserNameCharacters = "abcd123";
    option.User.RequireUniqueEmail = true;

    //Password Setting
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireNonAlphanumeric = false;//!@#$%^&*()_+
    option.Password.RequireUppercase = false;
    option.Password.RequiredLength = 8;
    option.Password.RequiredUniqueChars = 1;

    //Lokout Setting
    option.Lockout.MaxFailedAccessAttempts = 3;
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

    //SignIn Setting
    option.SignIn.RequireConfirmedAccount = false;
    option.SignIn.RequireConfirmedEmail = false;
    option.SignIn.RequireConfirmedPhoneNumber = false;


   



});

#endregion

#region Authentication

builder.Services.ConfigureApplicationCookie(option =>
{
    // cookie setting
    option.ExpireTimeSpan = TimeSpan.FromMinutes(10);

    option.LoginPath = "/account/login";
    option.AccessDeniedPath = "/Account/AccessDenied";
    option.SlidingExpiration = true; //because you set LoginPath=10min it means if user didn't do anything it will logout after 10min
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Buyer", policy =>
    {
        policy.RequireClaim("Buyer");
    });
    options.AddPolicy("BloodType", policy =>
    {
        policy.RequireClaim("Blood", "Ap", "Op");
    });

    options.AddPolicy("Cradit", policy =>
    {
        policy.Requirements.Add(new UserCreditRequerment(10000));
    });

    options.AddPolicy("IsBlogForUser", policy =>
    {
        policy.AddRequirements(new BlogRequirement());
    });

    options.AddPolicy("AdminUsers", policy =>
    {
        policy.RequireRole("Admin");
    });

});

#endregion

#region IOC

//builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, AddMyClaims>();
builder.Services.AddScoped<IClaimsTransformation,AddMyClaims.AddClaim>();
builder.Services.AddSingleton<IAuthorizationHandler, UserCreditHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, IsBlogForUserAuthorizationHandler>();
#endregion
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"

    );



app.Run();
