using Shipfer.Helpers;
using Shipfer.Models;
using Shipfer.Services;
using Shipfer.Views;

namespace Shipfer;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // PreferenceHelper.ClearUserDetails();

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
}
