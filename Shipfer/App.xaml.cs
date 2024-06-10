using Shipfer.Helpers;
using Shipfer.Models;
using Shipfer.Services;
using Shipfer.Views;

namespace Shipfer;

public partial class App : Application
{
    private readonly IShipping360Service _shipping360Service;
    public App()
    {
        InitializeComponent();

        //PreferenceHelper.ClearUserDetails();
        _shipping360Service = new Shipping360Service();
        GenerateToken();

        var userDetails = PreferenceHelper.GetUserDetails();
        var session = userDetails.session;

        if (session != null && !session.Expired())
        {
            MainPage = new AppShell();
        }
        else
        {
            MainPage = new AuthPage();
        }
    }

    private async Task GenerateToken()
    {

        await _shipping360Service.GenerateAccessToken();
    }
}
