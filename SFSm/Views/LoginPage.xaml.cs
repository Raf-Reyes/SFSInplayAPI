using SFSm.Services;
using SFSm.ViewModels;

namespace SFSm.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new LoginPageViewModel();
        DatabaseHelper.InitializeDatabase();
    }

    protected override bool OnBackButtonPressed()
    {
        ShowExit();
        return true;
    }

    private async void ShowExit()
    {
        bool answer = await Application.Current.MainPage.DisplayAlert(
        "Exit Application",
        "Do you want to exit the application?",
        "Yes",
        "No");

        if (answer)
        {
#if ANDROID
        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#elif IOS
        // iOS apps are generally not closed programmatically
        System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
#endif
        }
    }
}