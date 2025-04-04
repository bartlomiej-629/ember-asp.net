var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();

var app = builder.Build();
app.UseRouting();
app.MapControllerRoute("default", "{controller=Home}/{action=Index}");
app.Run();