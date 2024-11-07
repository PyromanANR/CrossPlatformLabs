using LAB9.ViewModels;
namespace LAB9;

public partial class GraphicChartPage : ContentPage
{
	public GraphicChartPage()
	{
        BindingContext = new ChartPageViewModel();
        InitializeComponent();
	}
}