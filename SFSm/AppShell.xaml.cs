using SFSm.ViewModels;
using SFSm.Views;

namespace SFSm
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            this.Navigated += OnShellNavigated;
            Routing.RegisterRoute(nameof(SODetailsPage), typeof(SODetailsPage));
        }

        private void OnShellNavigated(object sender, ShellNavigatedEventArgs e)
        {
            if (e.Current.Location.OriginalString == "//Login" || e.Current.Location.OriginalString == "//SignOut")
            {
                this.FlyoutBehavior = FlyoutBehavior.Disabled;
            }
            else
            {
                this.FlyoutBehavior = FlyoutBehavior.Flyout;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            var currentPage = Shell.Current.CurrentPage;

            if (currentPage is SODetailsPage)
            {
                return base.OnBackButtonPressed();
            }

            ShowExit();
            return true;
        }

        protected override async void OnNavigating(ShellNavigatingEventArgs args)
        {
            if (args.Target.Location.OriginalString == "//SignOut")
            {
                args.Cancel();

                bool confirm = await Application.Current.MainPage.DisplayAlert("Log Out", "Do you want to Log Out?", "Yes", "No");

                if (confirm)
                {
                    await Shell.Current.GoToAsync("//Login");
                }
            }
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
}
