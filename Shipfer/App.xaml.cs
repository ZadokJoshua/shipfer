using Shipfer.Views;

namespace Shipfer;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        //MainPage = new AppShell();
        MainPage = new AuthPage(); //I am getting a null exception
    }
}
