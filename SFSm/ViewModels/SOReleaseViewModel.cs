using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SFSm.Models;
using SFSm.Services;
using SFSm.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SFSm.ViewModels
{
    public class SOReleaseViewModel : INotifyPropertyChanged
    {
        private const string checkSerialAPI = "https://sfs.com.ph/filetransfer/api/mobile/validate-sn";
        public event PropertyChangedEventHandler? PropertyChanged;

        private DatabaseHelper _databaseHelper;
        private bool isLoading;
        private string loadingMessage;
        private ObservableCollection<SOPickModel> _pickList;

        public ObservableCollection<SOPickModel> PickList
        {
            get => _pickList;
            set
            {
                _pickList = value;
                OnPropertyChanged(nameof(PickList));
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

        public ICommand UploadCommand { get; }

        public SOReleaseViewModel()
        {
            _databaseHelper = new DatabaseHelper();
            PickList = new ObservableCollection<SOPickModel>();
            UploadCommand = new Command(async () => await DisplayPickListData());
            ShowPick1();
            IsLoading = false;
            LoadingMessage = string.Empty;
        }

        private async Task DisplayPickListData()
        {
            if (PickList.Any())
            {
                List<UploadingSerialModel> resultList = new List<UploadingSerialModel>();
                foreach (var pick in PickList)
                {
                    var pick2 = _databaseHelper.ShowPick2(pick.Refsono);

                    if (pick2.Any())
                    {
                        var groupPick2 = pick2.GroupBy(p => p.Description)
                                          .Select(g => new SOPick2Model
                                          {
                                              Refsono = g.First().Refsono,
                                              Description = g.Key,
                                              Qty = g.First().Qty,
                                              Barcode = g.First().Barcode,
                                              Serialno = string.Join(",", g.Select(p => p.Serialno))
                                          });

                        foreach (var item in groupPick2)
                        {
                            resultList.Add(new UploadingSerialModel
                            {
                                so = item.Refsono,
                                description = item.Description,
                                barcode = item.Barcode,
                                serialnumber = item.Serialno,
                                pickby = Preferences.Get("EmpID", "0")
                            });
                        }
                    }
                }

                if (resultList.Any())
                {
                    //await Application.Current.MainPage.DisplayAlert("Picked SO List", string.Join("\n", resultList), "OK");

                    UploadingSerialModel[] resultArray = resultList.ToArray();
                    string responseMessage = await SendResultListToAPI(resultArray);
                    await Application.Current.MainPage.DisplayAlert("Upload Complete!", responseMessage, "OK");
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Upload Error", "No details found.", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Upload Error", "No records found.", "OK");
            }
        }

        private async Task<string> SendResultListToAPI(UploadingSerialModel[] resultList)
        {
            IsLoading = true;
            LoadingMessage = "Please wait... Uploading Serials";

            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Application.Current.MainPage.DisplayAlert("No Internet", "Please check your internet connection", "OK");
                    return "No Internet, Please check your internet connection";
                }

                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using (HttpClient client = new HttpClient(handler))
                {
                    var jsonContent = JsonConvert.SerializeObject(resultList);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(checkSerialAPI, content);
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                        return $"Error: {response.StatusCode}\n{responseBody}";

                    var responseData = JObject.Parse(responseBody);
                    var existingData = responseData["existingData"];
                    var nonExistingData = responseData["nonExistingData"];
                    var duplicateData = responseData["skippedBarcodes"];

                    string failedMessage = BuildFailedMessage(nonExistingData);
                    string duplicateMessage = BuildDuplicateMessage(duplicateData, existingData);
                    string successMessage = BuildSuccessMessage(existingData, nonExistingData, duplicateData);

                    MainThread.BeginInvokeOnMainThread(() => ShowPick1());

                    //uncomment if you want to display all the message
                    //return $"{successMessage}\n\n{failedMessage}\n\n{duplicateMessage}";

                    //comment if you want to display message with no blank space
                    string successText = successMessage.ToString().Trim();
                    string failedText = failedMessage.ToString().Trim();
                    string duplicateText = duplicateMessage.ToString().Trim();

                    List<string> msg = new List<string>();

                    if (!string.IsNullOrWhiteSpace(successText))
                    {
                        string header = successText.Replace("Upload Successful:", "").Trim();
                        if (!string.IsNullOrWhiteSpace(header))
                        {
                            msg.Add(successText);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(failedText))
                    {
                        string header = failedText.Replace("Failed to Upload:", "").Trim();
                        if (!string.IsNullOrWhiteSpace(header))
                        {
                            msg.Add(failedText);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(duplicateText))
                    {
                        string header = duplicateText.Replace("Serial Number already uploaded:", "").Trim();
                        if (!string.IsNullOrWhiteSpace(header))
                        {
                            msg.Add(duplicateText);
                        }
                    }
                    return string.Join("\n\n", msg);
                }
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
                LoadingMessage = string.Empty;
            }
        }

        private string BuildFailedMessage(JToken nonExistingData)
        {
            var sb = new StringBuilder("Failed to Upload:\n");
            if (nonExistingData != null)
            {
                foreach (var item in nonExistingData)
                {
                    sb.AppendLine($"Description: {item["description"]}");
                    sb.AppendLine($"SO #: {item["so"]}");
                    sb.AppendLine($"Serial No: {item["serialno"]}");
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }

        private string BuildDuplicateMessage(JToken duplicateData, JToken existingData)
        {
            var barcodeDescriptionMap = new Dictionary<string, string>();
            if (existingData != null)
            {
                foreach (var item in existingData)
                {
                    var barcode = item["barcode"]?.ToString();
                    var description = item["description"]?.ToString();
                    if (!string.IsNullOrEmpty(barcode) && !barcodeDescriptionMap.ContainsKey(barcode))
                    {
                        barcodeDescriptionMap[barcode] = description;
                    }
                }
            }

            var sb = new StringBuilder("Serial Number already uploaded:\n");
            if (duplicateData != null && duplicateData.Type == JTokenType.Object)
            {
                JObject duplicateObj = (JObject)duplicateData;
                foreach (var property in duplicateObj.Properties())
                {
                    string barcode = property.Name;
                    //sb.AppendLine($"Barcode: {barcode}");

                    if (barcodeDescriptionMap.TryGetValue(barcode, out string description) && !string.IsNullOrEmpty(description))
                    {
                        sb.AppendLine($"\bDescription: {description}");
                    }

                    //display the serial number of already uploaded
                    if (property.Value is JArray serialNumbers)
                    {
                        foreach (var serial in serialNumbers)
                        {
                            sb.AppendLine($"\bSerial No: {serial}");
                        }
                    }

                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }

        private string BuildSuccessMessage(JToken existingData, JToken nonExistingData, JToken duplicateData)
        {
            var nonExistingBarcodes = new HashSet<string>();
            if (nonExistingData != null)
            {
                foreach (var item in nonExistingData)
                {
                    nonExistingBarcodes.Add(item["barcode"].ToString());
                }
            }

            var duplicateBarcodes = new HashSet<string>();
            if (duplicateData != null && duplicateData.Type == JTokenType.Object)
            {
                foreach (var property in ((JObject)duplicateData).Properties())
                {
                    duplicateBarcodes.Add(property.Name);
                }
            }

            var sb = new StringBuilder("Upload Successful:\n");
            var uniqueSO = new HashSet<int>();
            if (existingData != null)
            {
                foreach (var item in existingData)
                {
                    string barcode = item["barcode"].ToString();
                    if (!nonExistingBarcodes.Contains(barcode) && !duplicateBarcodes.Contains(barcode))
                    {
                        string desc = item["description"].ToString();
                        int soNumber = (int)item["so"];
                        decimal count = (decimal)item["count"];
                        string serialNo = item["serialno"].ToString();


                        if (!nonExistingBarcodes.Contains(barcode) && !duplicateBarcodes.Contains(barcode) && uniqueSO.Add(soNumber))
                        {
                            sb.AppendLine($"SO #: {soNumber}");
                            sb.AppendLine();
                        }
                        //sb.AppendLine($"Description: {desc}");
                        //sb.AppendLine($"Serial No: {serialNo}");
                        //sb.AppendLine($"Count: {count}");

                        _databaseHelper.UpdateQTYSO2Model(soNumber, desc, count, serialNo, barcode);
                    }
                }
            }
            return sb.ToString();
        }

        private bool UpdateSOPickTable(JToken existingData, JToken nonExistingData)
        {
            HashSet<string> existingBarcode = new HashSet<string>();
            foreach (var item in existingData)
            {
                existingBarcode.Add(item["barcode"].ToString());
            }

            HashSet<string> nonExistingBarcode = new HashSet<string>();
            foreach (var item in nonExistingData)
            {
                nonExistingBarcode.Add(item["barcode"].ToString());
            }

            foreach (var barcode in existingBarcode)
            {
                if (!nonExistingBarcode.Contains(barcode))
                {
                    return true;
                }
            }

            return false;
        }


        public void ShowPick1()
        {
            var pick1Data = _databaseHelper.ShowPick1();
            PickList.Clear();
            foreach (var item in pick1Data)
            {
                PickList.Add(item);
            }
        }

        public async void RemoveSOPick(SOPickModel selectedItem)
        {
            bool result = await Application.Current.MainPage.DisplayAlert(
                                "Remove Item",
                                $"Do you want to remove Pick SO Number {selectedItem.Refsono}?",
                                "Yes",
                                "No");

            if (result)
            {
                _databaseHelper.DeleteSO(selectedItem.Refsono, false);
                PickList.Remove(selectedItem);
                ShowPick1();
            }
        }

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
