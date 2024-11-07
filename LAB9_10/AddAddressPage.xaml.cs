using LAB9.ViewModels;
namespace LAB9;

public partial class AddAddressPage : ContentPage
{
	public AddAddressPage()
	{
		InitializeComponent();
        BindingContext = new AddAddressViewModel();
    }
}