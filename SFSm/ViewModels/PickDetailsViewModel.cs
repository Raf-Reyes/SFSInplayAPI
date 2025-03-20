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

namespace SFSm.ViewModels
{
    public class PickDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int Refsono { get; set; }
        private DatabaseHelper _databaseHelper;

        private ObservableCollection<SOPick2Model> pickDetails;
        public ObservableCollection<SOPick2Model> PickList
        {
            get => pickDetails; 
            set
            {
                pickDetails = value;
                OnPropertyChanged(nameof(PickList));
                ShowPick2();
            }
        }

        private void ShowPick2()
        {
            var pick2Data = _databaseHelper.ShowPick2(Refsono);

            var groupSerial = pick2Data.GroupBy(p => p.Description)
                                       .Select(g => new SOPick2Model
                                       {
                                           Description = g.Key,
                                           Qty = g.Count(),
                                           Barcode = g.First().Barcode,
                                           Serialno = string.Join(Environment.NewLine, g.Select(p => p.Serialno))
                                       });

            PickList.Clear();
            foreach (var pick2 in groupSerial)
            {
                PickList.Add(pick2);
            }
        }

        public PickDetailsViewModel(int refsono)
        {
            _databaseHelper = new DatabaseHelper();
            Refsono = refsono;
            PickList = new ObservableCollection<SOPick2Model>();
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
