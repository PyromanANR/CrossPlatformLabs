using LAB9.ViewModels;
namespace LAB9
{

	public partial class AddBranchPage : ContentPage
	{
		public AddBranchPage()
		{
			InitializeComponent();
			BindingContext = new BranchViewModel();
		}
	}
}