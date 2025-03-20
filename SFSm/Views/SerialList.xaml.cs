using SFSm.Models;
using SFSm.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SFSm.Views;

public partial class SerialList : ContentPage
{
    public ObservableCollection<string> SerialNumbers { get; set; }
    public ICommand RemoveCommand { get; }
    public event Action<ObservableCollection<string>> OnSave;

    private SO2Model _selectedItem;
    private int _tseqno;

    public SerialList(ObservableCollection<string> serials)
    {
        InitializeComponent();
        SerialNumbers = new ObservableCollection<string>(serials);
        RemoveCommand = new Command<string>(OnRemoveItem);

        BindingContext = this; 
    }

    private void OnRemoveItem(string item)
    {
        SerialNumbers.Remove(item);
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        OnSave?.Invoke(SerialNumbers);
        await Navigation.PopModalAsync();
    }

    //private void UpdatePreferences()
    //{
    //    if (_se)
    //    {

    //    }
    //}
}