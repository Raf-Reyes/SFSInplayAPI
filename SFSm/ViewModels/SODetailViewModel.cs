using Camera.MAUI;
using CommunityToolkit.Mvvm.Input;
using SFSm.Models;
using SFSm.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZXing.Net.Maui;
using Microsoft.Maui.Controls;

namespace SFSm.ViewModels
{
    public class SODetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private DatabaseHelper _databaseHelper;
        public int Tseqno { get; set; }
        public string Customer { get; set; }
        public List<string> enteredSerials = new List<string>();

        private ObservableCollection<SO2Model> soDetails;
        public ObservableCollection<SO2Model> SOList
        {
            get => soDetails;
            set
            {
                soDetails = value;
                OnPropertyChanged(nameof(SOList));
            }
        }

        public SODetailViewModel(int tseqno, string customer)
        {
            Tseqno = tseqno;
            Customer = customer;
            SOList = new ObservableCollection<SO2Model>();
            SO2Show();
        }

        private void SO2Show()
        {
            var so2Data = DatabaseHelper.ShowSO2(Tseqno);
            SOList.Clear();
            foreach (var so2 in so2Data)
            {
                so2.EnteredSerial = string.Join(Environment.NewLine, LoadSerials(Tseqno, so2.Barcode));

                var enteredSerialsCount = so2.EnteredSerial.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Length;

                if (enteredSerialsCount == so2.Qty)
                {
                    so2.BGColor = "#008000";
                }
                else if (enteredSerialsCount > 0)
                {
                    so2.BGColor = "#FFFF00";
                }
                else
                {
                    so2.BGColor = "#D3D3D3";
                }
                SOList.Add(so2);
            }
        }

        public void ToggleIsExpanded(SO2Model selectedItem)
        {
            foreach (var item in SOList)
            {
                item.IsExpanded = false;
            }

            selectedItem.IsExpanded = true;
            if (string.IsNullOrEmpty(selectedItem.EnteredSerial))
            {
                enteredSerials.Clear();
            }
            else
            {
                enteredSerials = selectedItem.EnteredSerial.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            }
        }


        public void AddSerial(string serial)
        {
            var selectedItem = SOList.FirstOrDefault(x => x.IsExpanded);
            if (string.IsNullOrWhiteSpace(serial))
            {
                Application.Current.MainPage.DisplayAlert("Serial Number Required", "Please enter Serial Number.", "OK");
                return;
            }
            if (enteredSerials.Count >= selectedItem.Qty)
            {
                Application.Current.MainPage.DisplayAlert("Limit Reached", "You cannot add more serials than the quantity.", "OK");
                return;
            }

            if (enteredSerials.Contains(serial))
            {
                Application.Current.MainPage.DisplayAlert("Duplicate Serial", "You cannot add same Serial Number.", "OK");
                return;
            }

            enteredSerials.Add(serial);
            selectedItem.EnteredSerial = string.Join(Environment.NewLine, enteredSerials);

            var enteredSerialsCount = selectedItem.EnteredSerial.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Length;

            if (enteredSerialsCount == selectedItem.Qty) 
            {
                selectedItem.BGColor = "#008000";
            }
            else if (enteredSerialsCount > 0)
            {
                selectedItem.BGColor = "#FFFF00";
            }
            else
            {
                selectedItem.BGColor = "#D3D3D3";
            }

            SaveSerials(Tseqno, selectedItem.Barcode, enteredSerials);
        }

        public async void ClearSerial(SO2Model selectedItem)
        {
            if (string.IsNullOrEmpty(selectedItem.EnteredSerial))
            {
                return;
            }

            bool result = await Application.Current.MainPage.DisplayAlert(
                                "Clear Serial",
                                "Do you want to clear the serial numbers for this item?",
                                "Yes",
                                "No");

            if (result)
            {
                selectedItem.EnteredSerial = string.Empty;
                enteredSerials.Clear();
                selectedItem.BGColor = "#D3D3D3";
                //SaveSerials(selectedItem.Tseqno, enteredSerials);

                string key = $"Serials_{Tseqno}_{selectedItem.Barcode.Trim()}";
                Preferences.Remove(key);
            }
        }

        private void SaveSerials(int tseqno, string barcode, List<string> serials)
        {
            string key = $"Serials_{tseqno}_{barcode.Trim()}";
            Preferences.Remove(key);
            Preferences.Set(key, string.Join(',', serials));
        }
        public void UpdateSerial(int tseqno, string barcode, List<string> serials)
        {
            string key = $"Serials_{tseqno}_{barcode.Trim()}";
            Preferences.Set(key, string.Join(',', serials));
        }

        private List<string> LoadSerials(int tseqno, string barcode)
        {
            string key = $"Serials_{tseqno}_{barcode.Trim()}";
            string savedSerials = Preferences.Get(key, string.Empty);
            return string.IsNullOrEmpty(savedSerials) ? new List<string>() : savedSerials.Split(',').ToList();
        }

        public async void InsertSOPick()
        {
            if (Tseqno == 0)
            {
                App.Current.MainPage.DisplayAlert("Invalid SO Number", "Please select SO Number", "OK");
                return;
            }

            var newRecord = new SOPickModel
            {
                Refsono = Tseqno
            };

            var sopick2 = new List<SOPick2Model>();

            foreach (var item in SOList)
            {
                if (string.IsNullOrWhiteSpace(item.EnteredSerial))
                    continue;

                var splitSerial = item.EnteredSerial.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();

                //uncomment if serial is not equal to QTY
                //if (item.Qty != splitSerial.Length)
                //    continue;

                var serialNumber = item.EnteredSerial.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var serial in serialNumber)
                {
                    sopick2.Add(new SOPick2Model
                    {
                        Refsono = Tseqno,
                        Description = item.Desc,
                        Qty = item.Qty,                       
                        Barcode = item.Barcode,
                        Serialno = serial,
                        Pickby = UserModel.EmpIDProfile
                    });
                }
            }

            if (sopick2.Count > 0)
            {
                DatabaseHelper.DeleteSOPick1(Tseqno);
                DatabaseHelper.DeleteSOPick2(Tseqno);
                DatabaseHelper.InsertSOPick1(newRecord);
                DatabaseHelper.InsertSOPick2(sopick2);
                App.Current.MainPage.DisplayAlert("Success", $"SO {Tseqno} saved successfully!", "OK");

                await Shell.Current.GoToAsync("//SORelease");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("No Serial Numbers Found", "No serial numbers were entered for this SO.", "OK");
            }

        }

        public async void RemoveBarcodeItem(SO2Model selectedItem)
        {
            bool result = await Application.Current.MainPage.DisplayAlert(
                                "Remove Item",
                                $"Do you want to remove Item {selectedItem.Desc}?",
                                "Yes",
                                "No");

            if (result)
            {
                selectedItem.EnteredSerial = string.Empty;
                enteredSerials.Clear();

                string key = $"Serials_{Tseqno}_{selectedItem.Barcode}";
                Preferences.Remove(key);

                DatabaseHelper.DeleteBarcode(Tseqno, selectedItem.Barcode);
                SOList.Remove(selectedItem);
            }
        }

        //public void UpdateSerialsInModel(SO2Model selectedItem)
        //{
        //    if (selectedItem == null)
        //    {
        //        return;
        //    }

        //    List<string> updatedSerials = enteredSerials.ToList(); 

        //    if (updatedSerials.Count > 0)
        //    {
        //        selectedItem.EnteredSerial = string.Join(Environment.NewLine, updatedSerials);

        //        SaveSerials(Tseqno, selectedItem.Barcode, updatedSerials);

        //        Application.Current.MainPage.DisplayAlert("Serials Updated", "Serial numbers have been updated.", "OK");
        //    }
        //    else
        //    {
        //        Application.Current.MainPage.DisplayAlert("No Serial Numbers", "No serial numbers were entered for this item.", "OK");
        //    }
        //}

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }


}
