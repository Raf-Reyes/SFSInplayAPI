using Newtonsoft.Json;
using SFSm.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SFSm.Models;
using System.Collections;
using System.Collections.Specialized;

namespace SFSm.ViewModels
{
    public class SODownloadViewModel : INotifyPropertyChanged
    {
        private const string ApiUrl = "https://sfs.com.ph/filetransfer/api/mobile/fetch-so";
        private const string CheckEntrySo = "https://sfs.com.ph/filetransfer/api/mobile/validate-so";

        private DatabaseHelper _databaseHelper;
        private ObservableCollection<SO1Model> soList;
        private ObservableCollection<SO2Model> so2List;
        public ObservableCollection<string> EnteredSOList { get; } = new ObservableCollection<string>();
        private bool isLoading;
        private string loadingMessage;
        private string _soInput;
        private string _concatenatedSOList;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string SOInput
        {
            get => _soInput;
            set
            {
                if (_soInput != value)
                {
                    _soInput = value;
                    OnPropertyChanged(nameof(SOInput));
                }
            }
        }

        public string ConcatenatedSOList
        {
            get => _concatenatedSOList;
            private set
            {
                if (_concatenatedSOList != value)
                {
                    _concatenatedSOList = value;
                    OnPropertyChanged(nameof(ConcatenatedSOList));
                }
            }
        }

        public ObservableCollection<SO1Model> SOList
        {
            get => soList;
            set
            {
                soList = value;
                OnPropertyChanged(nameof(SOList));
            }
        }

        public ObservableCollection<SO2Model> SO2List
        {
            get => so2List;
            set
            {
                so2List = value;
                OnPropertyChanged(nameof(SO2List));
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

        public ICommand DownloadCommand { get; }

        public ICommand RefreshCommand { get; }

        public ICommand RemoveItemCommand { get; }

        public SODownloadViewModel()
        {
            _databaseHelper = new DatabaseHelper();
            SOList = new ObservableCollection<SO1Model>();
            SO2List = new ObservableCollection<SO2Model>();
            DownloadCommand = new Command(async () => await DownloadSOAsync());
            RefreshCommand = new Command(async () => await RefreshSOAsync());
            RemoveItemCommand = new Command<string>(async (item) => await RemoveItemAsync(item));
            EnteredSOList.CollectionChanged += UpdateConcatenatedSOList;
            IsLoading = false;
            LoadingMessage = string.Empty;
            LoadSO2Data();
        }

        public async Task RemoveItemAsync(String item)
        {
            bool confirm = await App.Current.MainPage.DisplayAlert("Remove Item", $"Do you want to remove '{item}'?", "Yes", "No");
            if (confirm && EnteredSOList.Contains(item))
            {
                EnteredSOList.Remove(item);
            }
        }

        private void UpdateConcatenatedSOList(object sender, NotifyCollectionChangedEventArgs e)
        {
            ConcatenatedSOList = string.Join("-", EnteredSOList);
        }

        public async void AddSO()
        {
            //DatabaseHelper.DropSODatabase();  //for testing
            if (!string.IsNullOrWhiteSpace(SOInput))
            {
                if (int.TryParse(SOInput, out int soInputInt) && DatabaseHelper.ShowSO1().Any(so => so.Tseqno == soInputInt))
                {
                    await Application.Current.MainPage.DisplayAlert("SO Number exist", "SO Number already Picked", "OK");
                    return;
                }

                if (EnteredSOList.Contains(SOInput))
                {
                    await Application.Current.MainPage.DisplayAlert("Duplicate SO Number", "The entered SO number already exist on the list.", "OK");
                    return;
                }

                var isValid = await CheckSOExistence(SOInput);

                if (isValid)
                {
                    EnteredSOList.Add(SOInput);
                    SOInput = string.Empty;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Invalid SO", "The entered SO number does not exist.", "OK");
                }
                
            }
        }

        private async Task<bool> CheckSOExistence(string soNumber)
        {
            try
            {
                IsLoading = true;
                LoadingMessage = "Please wait... Verifying SO";

                var current = Connectivity.NetworkAccess;
                if (current != NetworkAccess.Internet)
                {
                    IsLoading = false;
                    await Application.Current.MainPage.DisplayAlert("No Internet", "Please check your internet connection", "OK");
                    return false;
                }

                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var apiParameter = $"{CheckEntrySo}/{soNumber}";

                    var response = await client.GetAsync(apiParameter);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        return bool.Parse(jsonResponse);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Failed to validate SO.", "OK");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                return false;
            }
            finally
            {
                IsLoading = false;
                LoadingMessage = string.Empty;
            }
        }


        private async Task RefreshSOAsync()
        {
            if (EnteredSOList.Count == 0)
            {
                return;
            }

            bool confirm = await App.Current.MainPage.DisplayAlert("Confirm", "Do you want to clear all SO?", "Yes", "No");
            if (confirm)
            {
                EnteredSOList.Clear();
                await App.Current.MainPage.DisplayAlert("Success", "All SO have been removed.", "OK");
            }
        }

        private async Task DownloadSOAsync()
        {
            try
            {
                IsLoading = true;
                LoadingMessage = "Please wait... Downloading data";

                if (EnteredSOList.Count == 0)
                {
                    IsLoading = false;
                    await App.Current.MainPage.DisplayAlert("Error", "No records found.", "OK");
                    return;
                }

                var current = Connectivity.NetworkAccess;
                if (current != NetworkAccess.Internet)
                {
                    IsLoading = false;
                    await Application.Current.MainPage.DisplayAlert("No Internet", "Please check your internet connection", "OK");
                    return;
                }

                /*DatabaseHelper.DropSODatabase();*/  //for testing
                
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var apiParameter = $"{ApiUrl}/{ConcatenatedSOList}";

                    var jsonParameter = JsonConvert.SerializeObject(ConcatenatedSOList);
                    var content = new StringContent(jsonParameter, Encoding.UTF8, "application/json");

                    //var response = await client.PostAsync(apiParameter, content);
                    var response = await client.GetAsync(apiParameter);
                    response.EnsureSuccessStatusCode();

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

                    var so1Data = apiResponse.so1;
                    var so2Data = apiResponse.so2;

                    _databaseHelper.InsertSO1Data(so1Data);
                    _databaseHelper.InsertSO2Data(so2Data);
                    LoadSO2Data();
                }

                await Application.Current.MainPage.DisplayAlert("Sync Complete", "SO Download successfully", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to download SO: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
                LoadingMessage = string.Empty;
                EnteredSOList.Clear();
            }
        }

        private void LoadSO2Data()
        {
            var so2data = DatabaseHelper.ShowSO2();
            SO2List.Clear();
            foreach (var so2 in so2data)
            {
                SO2List.Add(so2);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ApiResponse
    {
        public List<SO1Model> so1 { get; set; }
        public List<SO2Model> so2 { get; set; }
    }
}
