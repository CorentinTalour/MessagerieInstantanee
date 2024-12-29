namespace MessagerieInstantanee.Objects;

public class MessagerieApiService
{
    #region Fields

    private IHttpClientFactory _httpClientFactory;
    // private readonly string _apiKey;
    
    #endregion
    
    #region Constructors

    public MessagerieApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        // _apiKey = configuration.GetSection("ApiSettings:ApiKey").Value
        //           ?? throw new Exception("La clé API est manquante dans la configuration");
    }

    #endregion
    
    #region Methods

    public async Task<List<User>> GetUsers()
    {
        try
        {
            using HttpClient httpClient = _httpClientFactory.CreateClient("message");

            //Permet de pouvoir passer un en-tête
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "User");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            List<User>? users = await response.Content.ReadFromJsonAsync<List<User>>()
                              ?? throw new Exception("Impossible d'obtenir la liste d'utilisateurs depuis l'API.");

            return users;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
            throw;
        }
    }
    
    #endregion
}