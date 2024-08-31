using Regent.AI.RPG.Components;
using Regent.AI.RPG.OpenAI.Services;
using Regent.AI.RPG.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddSingleton<IOpenAIClient>(provider =>
{
    // Retrieve the API key from configuration
    var apiKey = builder.Configuration["OpenAI:ApiKey"];
    if (string.IsNullOrEmpty(apiKey))
    {
        throw new InvalidOperationException("OpenAI API key is not configured.");
    }

    // Get the HttpClientFactory service
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();

    // Create and return the OpenAIClient instance
    return new OpenAIClient(httpClientFactory, apiKey);
});

builder.Services.AddSingleton<IChatGptService, ChatGptService>();
builder.Services.AddSingleton<GameMasterService>();

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
    endpoints.MapHub<GameMasterService>("/gameHub");
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();