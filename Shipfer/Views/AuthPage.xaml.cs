using Shipfer.ViewModels;

namespace Shipfer.Views;

public partial class AuthPage : ContentPage
{
	public AuthPage()
	{
		InitializeComponent();

		BindingContext = new AuthViewModel();
	}
}