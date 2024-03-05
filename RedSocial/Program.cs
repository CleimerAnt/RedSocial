using RedSocial.Infraestructure.Identity;
using RedSocial.Infraestructure.Identity.Seeds;
using RedSocial.Core.Application;
using RedSocial.Middlewares;
using RedSocial.Infraestructure.Persitence;
using RedSocial.Infraestructure.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentityInfraestructure(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.Services.AddSharedInfraestrucutre(builder.Configuration);
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddApplucationLayer(builder.Configuration);
builder.Services.AddScoped<LoginAuthorize>();
builder.Services.AddTransient<ValidateUserSession, ValidateUserSession>();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession();

var app = builder.Build();

await app.Services.AddIdentitySeedsInfraestrucuture();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();   
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
