namespace SFSm.Views;

using SFSm.Models;
using SFSm.ViewModels;
using Camera.MAUI;
using SFSm.Services;
using System.Collections.ObjectModel;

public partial class SODetailsPage : ContentPage
{
    private SODetailViewModel viewModel;
    private readonly int _tseqno;
    private readonly string _customer;
    private SO2Model _selectedItem;
    private int _tapCount = 0;
    private readonly object _tapLock = new object();
    private readonly TimeSpan _doubleTapDelay = TimeSpan.FromMicroseconds(300);

    public SODetailsPage(int tseqno, string customer)
	{
		InitializeComponent();
		Shell.SetFlyoutBehavior(this, FlyoutBehavior.Disabled);
        _tseqno = tseqno;
        _customer = customer;
        LoadData();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadData();
    }

    public void LoadData()
    {
        viewModel = new SODetailViewModel(_tseqno, _customer);
        BindingContext = viewModel;
        myEntry = this.FindByName<Entry>("myEntry");
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is SO2Model selectedItem)
        {
            _selectedItem = selectedItem;
            viewModel.ToggleIsExpanded(selectedItem);
        }
    }

    private void Entry_Completed(object sender, EventArgs e)
    {
        var entry = sender as Entry;
        if (entry != null)
        {
            viewModel.AddSerial(entry.Text);
            entry.Text = string.Empty;        
        }
    }

    private void OnItemDoubleTapped(object sender, TappedEventArgs e)
    {
        var tappedItem = (sender as VisualElement)?.BindingContext as SO2Model;

        if (tappedItem.IsExpanded)
        {
            viewModel.ClearSerial(tappedItem);
        }
    }

    private void OnItemTrippedTapped(object sender, TappedEventArgs e)
    {
        var tappedItem = (sender as VisualElement)?.BindingContext as SO2Model;

        if (tappedItem.IsExpanded)
        {
            viewModel.ClearSerial(tappedItem);
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        viewModel.InsertSOPick();
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

        if (sender is VisualElement element && element.BindingContext is SO2Model tappedItem)
        {
            _selectedItem = tappedItem;
            viewModel.ToggleIsExpanded(tappedItem);

            Device.BeginInvokeOnMainThread(() =>
            {
                myEntry.Focus();
            });
        }
    }

    private async void OnItemTapped(object sender, TappedEventArgs e)
    {
        SO2Model tappedItem = (sender as VisualElement)?.BindingContext as SO2Model;
        if (tappedItem == null) return;

        lock (_tapLock)
        {
            _tapCount++;
        }

        await Task.Delay(500);

        lock (_tapLock)
        {
            switch (_tapCount)
            {
                case 1:
                    _selectedItem = tappedItem;
                    viewModel.ToggleIsExpanded(tappedItem);

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        myEntry.Focus();
                    });
                    break;
                    

                case 2:
                    if (tappedItem.IsExpanded)
                    {
                        viewModel.ClearSerial(tappedItem);
                    }
                    break;

                case 3:
                    if (tappedItem.IsExpanded)
                    {
                        var serialsList = tappedItem.EnteredSerial.Split(Environment.NewLine).ToList();
                        var serialCollection = new ObservableCollection<string>(serialsList);
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            var serialListPage = new SerialList(serialCollection);
                            serialListPage.OnSave += (updatedList) =>
                            {
                                tappedItem.EnteredSerial = string.Join(Environment.NewLine, updatedList);
                                viewModel.UpdateSerial(tappedItem.Tseqno, tappedItem.Barcode, updatedList.ToList());

                                var countUpdatedSerial = tappedItem.EnteredSerial.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Length;
                                if (countUpdatedSerial == tappedItem.Qty)
                                {
                                    tappedItem.BGColor = "#008000";
                                }
                                else if (countUpdatedSerial > 0)
                                {
                                    tappedItem.BGColor = "#FFFF00";
                                }
                                else
                                {
                                    tappedItem.BGColor = "#D3D3D3";
                                }
                            };

                            await Navigation.PushModalAsync(serialListPage);
                        });
                    }
                    break;
            }

            _tapCount = 0; // Reset tap count after processing
        }
    }

    //private async Task OpenCamera(SO2Model tappedItem)
    //{
    //    var scanPage = new CameraPage();
    //    scanPage.on
    //}

    private void SwipeItem_Invoked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var tappedItem = swipeItem?.BindingContext as SO2Model;

        viewModel.RemoveBarcodeItem(tappedItem);

    }
}