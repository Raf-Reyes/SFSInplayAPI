using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFSm.ViewModels
{
    public class SerialListViewModel : INotifyPropertyChanged
    {
        private string selectedSerial;
        private string enteredSerial;

        public ObservableCollection<string> SerialsList { get; set; }

        public string SelectedSerial
        {
            get => selectedSerial;
            set
            {
                if (selectedSerial != value)
                {
                    selectedSerial = value;
                    OnPropertyChanged(nameof(SelectedSerial));
                    RemoveCommand.ChangeCanExecute();
                }
            }
        }

        public Command RemoveCommand { get; }
        public Command SaveCommand { get; }

        public event Action<string> OnSerialUpdated;  // Event to notify the caller (e.g., parent page)

        public SerialListViewModel(ObservableCollection<string> serials)
        {
            SerialsList = serials;
            RemoveCommand = new Command(RemoveSerial, CanRemoveSerial);
            SaveCommand = new Command(SaveSerial);
        }

        private bool CanRemoveSerial()
        {
            return !string.IsNullOrEmpty(SelectedSerial);
        }

        private void RemoveSerial()
        {
            if (!string.IsNullOrEmpty(SelectedSerial))
            {
                SerialsList.Remove(SelectedSerial);
                SelectedSerial = null;
            }
        }

        private async void SaveSerial()
        {
            OnSerialUpdated?.Invoke(string.Join(Environment.NewLine, SerialsList));

            // Pop the page from the navigation stack
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
