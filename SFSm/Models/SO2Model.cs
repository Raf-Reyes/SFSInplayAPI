using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SFSm.Models
{
    public class SO2Model : INotifyPropertyChanged
    {
        [Key]
        public int Tseqno { get; set; }
        public string Barcode { get; set; }
        public decimal Qty { get; set; }
        public string Desc { get; set; }
        public int Active { get; set; } //sa ngayon di pa ginagamit delete data muna.

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged(nameof(IsExpanded));
                }
            }
        }

        private string _enteredSerial;
        public string EnteredSerial
        {
            get => _enteredSerial;
            set
            {
                _enteredSerial = value;
                OnPropertyChanged(nameof(EnteredSerial));
            }
        }

        private string _bgColor = "#D3D3D3";
        public string BGColor
        {
            get => _bgColor;
            set
            {
                if (_bgColor != value)
                {
                    _bgColor = value;
                    OnPropertyChanged(nameof(BGColor));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
