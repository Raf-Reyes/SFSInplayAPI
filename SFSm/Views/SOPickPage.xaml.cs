using SFSm.Models;
using SFSm.ViewModels;

namespace SFSm.Views;

public partial class SOPickPage : ContentPage
{
    private SOPickPageViewModel viewModel;
	public SOPickPage()
	{
		InitializeComponent();
        viewModel = new SOPickPageViewModel();
        BindingContext = viewModel;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.SO1Show();
    }

    //private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
    //    {
    //        var selectedItem = e.CurrentSelection[0] as SO1Model;

    //        if (selectedItem != null)
    //        {
    //            var tseqno = selectedItem.Tseqno;
    //            var customer = selectedItem.Customer;

    //            await Navigation.PushAsync(new SODetailsPage(tseqno, customer));
    //        }

    //    ((CollectionView)sender).SelectedItem = null;
    //    }
    //}

    private void SwipeItem_Invoked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var tappedItem = swipeItem?.BindingContext as SO1Model;

        viewModel.RemoveBarcodeItem(tappedItem);
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var tappedItem = (sender as StackLayout)?.BindingContext as SO1Model;

        if (tappedItem != null)
        {
            var tseqno = tappedItem.Tseqno;
            var customer = tappedItem.Customer;

            await Navigation.PushAsync(new SODetailsPage(tseqno, customer));
        }
    }
}