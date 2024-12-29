using MessagerieInstantanee.Objects;

namespace MessagerieInstantanee.Components.Pages;

public partial class Home
{
    private List<User> _users;
    private bool _isLoading = false;
    
    protected override async Task OnInitializedAsync()
    {
        _users = await MessagerieService.GetUsers();
    }
}