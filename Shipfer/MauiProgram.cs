using Microsoft.Extensions.Logging;
using Shipfer.ViewModels;
using Shipfer.Views;
using UraniumUI;

namespace Shipfer;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseUraniumUI()
            .UseUraniumUIMaterial()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFontAwesomeIconFonts();
            });

        builder.Services.AddSingleton<AuthViewModel>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<ChatViewModel>();
        builder.Services.AddSingleton<ProductsViewModel>();

        builder.Services.AddSingleton<AuthPage>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<ChatPage>();
        builder.Services.AddSingleton<ProductsPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
