using SFSm.ViewModels;

namespace SFSm.Views;

public partial class SODownloadPage : ContentPage
{
	public SODownloadPage()
	{
		InitializeComponent();
		BindingContext = new SODownloadViewModel();
	}

    private async void Entry_Completed(object sender, EventArgs e)
    {
		if (BindingContext is SODownloadViewModel vm)
		{
			var entry = sender as Entry;
			if (entry != null && !string.IsNullOrWhiteSpace(entry.Text))
			{
				vm.SOInput = entry.Text;
				vm.AddSO();
				entry.Text = string.Empty;
			}
		}
    }
}