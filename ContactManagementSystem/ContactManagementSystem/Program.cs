using Microsoft.EntityFrameworkCore;
using ContactManagementSystem.DataAccessLayer.Context;
using ContactManagementSystem.DataAccessLayer.Repository;
using ContactManagementSystem.BusinessLogicLayer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ContactsContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsContext")));

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
