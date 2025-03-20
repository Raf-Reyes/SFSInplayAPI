using SFSm.Models;
using SFSm.Services;
using SFSm.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SFSm.ViewModels
{
    public class SOPickPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private DatabaseHelper _databaseHelper;
        private ObservableCollection<SO1Model> soList;

        public ObservableCollection<SO1Model> SOList
        {
            get => soList;
            set
            {
                soList = value;
                OnPropertyChanged(nameof(SOList));
            }
        }

        public SOPickPageViewModel()
        {
            _databaseHelper = new DatabaseHelper();
            SOList = new ObservableCollection<SO1Model>();
            SO1Show();
        }

        public void SO1Show()
        {
            var so1Data = DatabaseHelper.ShowSO1().Where(so => so.Active == 1);
            SOList.Clear();
            foreach (var so1 in so1Data)
            {
                SOList.Add(so1);
            }
        }

        public async void RemoveBarcodeItem(SO1Model selectedItem)
        {
            if (DatabaseHelper.ShowSO2(selectedItem.Tseqno).Count == 0)
            {
                bool result = await Application.Current.MainPage.DisplayAlert(
                            "Remove Item",
                            $"Do you want to remove SO Number {selectedItem.Tseqno}?",
                            "Yes",
                            "No");

                if (result)
                {
                    //not sure kung maalis yung mga nakagay na serial if mag download ulit ng same SO
                    //string key = $"Serials_{selectedItem.Tseqno}";
                    //Preferences.Remove(key);

                    _databaseHelper.DeleteSO(selectedItem.Tseqno);
                    SOList.Remove(selectedItem);
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
