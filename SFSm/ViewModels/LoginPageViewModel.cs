using Newtonsoft.Json;
using SFSm.Models;
using SFSm.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SFSm.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private const string ApiUrl = "https://sfs.com.ph/filetransfer/api/mobile/fetch-users";

        public event PropertyChangedEventHandler? PropertyChanged;

        private DatabaseHelper _databaseHelper;
        private string _username;
        private string _password;
        private bool isLoading;
        private string loadingMessage;
        public string AppVersion { get; }
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value?.ToUpper();
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public string LoadingMessage
        {
            get => loadingMessage;
            set
            {
                loadingMessage = value;
                OnPropertyChanged(nameof(LoadingMessage));
            }
        }

        public ICommand SyncCommand { get; }
        public ICommand LoginCommand { get; }

        public LoginPageViewModel()
        {
            _databaseHelper = new DatabaseHelper();
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            AppVersion = $"Version {version.ToString()}";
            SyncCommand = new Command(async () => await SyncUserFromServer());
            LoginCommand = new Command(async () => await ExecuteLogin());
            IsLoading = false;
            LoadingMessage = string.Empty;
            Preferences.Remove("Username");
            Preferences.Remove("EmpID");
        }

        private async Task SyncUserFromServer()
        {
            IsLoading = true;
            LoadingMessage = "Please wait... Downloading Users Account";

            try
            {

                var current = Connectivity.NetworkAccess;

                if (current != NetworkAccess.Internet)
                {
                    IsLoading = false;
                    await Application.Current.MainPage.DisplayAlert("No Internet", "Please check your internet connection", "OK");
                    return;
                }

                // Drop the local user table before syncing
                _databaseHelper.DropUserTable();

                var users = await FetchUsers();
                _databaseHelper.InsertUpdateUsers(users);
                await Application.Current.MainPage.DisplayAlert("Sync Complete", "User data synchronized successfully", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to sync data: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
                LoadingMessage = string.Empty;
            }
        }


        private async Task<List<UserModel>> FetchUsers()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

            var users = new List<UserModel>();

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true 
            };

            using (var httpClient = new HttpClient(handler))
            {
                try
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(60);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await httpClient.GetAsync(ApiUrl);
                    response.EnsureSuccessStatusCode();

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    users = JsonConvert.DeserializeObject<List<UserModel>>(jsonResponse);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }

                    throw new ApplicationException($"Failed to fetch users from API: {ex.Message}", ex);
                }
            }
            return users;
        }

        private async Task ExecuteLogin()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please enter both username and password", "OK");
                    return;
                }

                if (_databaseHelper.Authentication(Username, Password))
                {
                    UserModel.UsernameProfile = Username;
                    UserModel.EmpIDProfile = _databaseHelper.GetEmpID(Username);
                    //Preferences.Set("EMPID", DatabaseHelper.GetEmpID(Username));

                    await Application.Current.MainPage.DisplayAlert("Success", "Login successful!", "OK");
                    Preferences.Set("Username", Username);
                    Preferences.Set("EmpID", _databaseHelper.GetEmpID(Username));
                    await Shell.Current.GoToAsync("//Dashboard");

                    Username = string.Empty;
                    Password = string.Empty;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Invalid username or password", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
