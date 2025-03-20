using SFSm.Models;
using SFSm.ViewModels;

namespace SFSm.Views;

public partial class SOReleasePage : ContentPage
{
	private SOReleaseViewModel viewModel;
    private bool _isNavigate = false;
	public SOReleasePage()
	{
		InitializeComponent();
		viewModel = new SOReleaseViewModel();
		BindingContext = viewModel;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
		viewModel.ShowPick1();

    }

    //private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    if (e.CurrentSelection.Count == 0)
    //        return;

    //    var selectedItem = e.CurrentSelection.FirstOrDefault() as SOPickModel;

    //    if (selectedItem == null)
    //        return;

    //    await Navigation.PushModalAsync(new PickDetailsPage(selectedItem.Refsono));

    //    ((CollectionView)sender).SelectedItem = null;
    //}

    private void SwipeItem_Invoked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var tappedItem = swipeItem?.BindingContext as SOPickModel;

        viewModel.RemoveSOPick(tappedItem);
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        //if (_isNavigate) return;
        //_isNavigate = true;

        //try
        //{
        //    var tappedItem = (sender as StackLayout)?.BindingContext as SOPickModel;

        //    if (tappedItem != null)
        //    {
        //        await Navigation.PushModalAsync(new PickDetailsPage(tappedItem.Refsono));
        //    }
        //}
        //finally { _isNavigate = false; }

        if (Application.Current.MainPage.Navigation.ModalStack.Any(p => p is PickDetailsPage))
            return; // Prevent opening multiple instances

        var tappedItem = (sender as StackLayout)?.BindingContext as SOPickModel;

        if (tappedItem != null)
        {
            await Navigation.PushModalAsync(new PickDetailsPage(tappedItem.Refsono));
        }

    }
}