using MessagerieInstantanee.Client.Pages;
using MessagerieInstantanee.Components;
using MessagerieInstantanee.Objects;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

//Inscription d'un HttpClient nomm�
builder.Services.AddHttpClient("message", (httpClient) =>
{
    httpClient.BaseAddress = new("https://localhost:7053/");
    // string apiKey = builder.Configuration.GetSection("ApiSettings:ApiKey").Value
    //                 ?? throw new Exception("La cl� API est manquante dans la configuration");
    // httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
});

//Inscription d'un service de r�cup�ration de donn�es depuis un WebService
builder.Services.AddScoped<MessagerieApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    
    //Cr�ation d'un scrope pour acc�der aux services via le ServiceProvider
    using IServiceScope serviceScope = app.Services.CreateScope();

    MessagerieApiService apiService = serviceScope.ServiceProvider.GetRequiredService<MessagerieApiService>();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MessagerieInstantanee.Client._Imports).Assembly);

app.Run();