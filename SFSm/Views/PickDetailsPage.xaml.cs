using SFSm.ViewModels;

namespace SFSm.Views;

public partial class PickDetailsPage : ContentPage
{
	private PickDetailsViewModel viewModel;
	public PickDetailsPage(int refsono)
	{
		InitializeComponent();
		viewModel = new PickDetailsViewModel(refsono);
		BindingContext = viewModel;
	}
}