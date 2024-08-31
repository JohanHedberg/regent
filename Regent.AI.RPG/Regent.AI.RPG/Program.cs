using Regent.AI.RPG.Components;
using Regent.AI.RPG.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IChatGptService, ChatGptService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseEndpoints(endpoints =>
{ 
    //endpoints.MapBlazorHub();
//    //endpoints.MapFallbackToPage("/_Host");
    endpoints.MapHub<GameMasterService>("/gameHub");
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();