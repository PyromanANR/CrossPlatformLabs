using LAB9.ViewModels;
namespace LAB9;

public partial class AddBankPage : ContentPage
{
	public AddBankPage()
	{
		InitializeComponent();
        BindingContext = new AddBankViewModel();
    }
}