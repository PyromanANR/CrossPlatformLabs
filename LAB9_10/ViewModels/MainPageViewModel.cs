using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace LAB9.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public ICommand NavigateToAddAddressCommand { get; }
        public ICommand NavigateToAddBankCommand { get; }
        public ICommand NavigateToAddBranchCommand { get; }
        public ICommand NavigateToGraphicChartCommand { get; }

        public ICommand NavigateToAboutPageCommand { get; }

        public MainPageViewModel()
        {
            NavigateToAddAddressCommand = new Command(async () => await NavigateToPageAsync(new AddAddressPage()));
            NavigateToAddBankCommand = new Command(async () => await NavigateToPageAsync(new AddBankPage()));
            NavigateToAddBranchCommand = new Command(async () => await NavigateToPageAsync(new AddBranchPage()));
            NavigateToGraphicChartCommand = new Command(async () => await NavigateToPageAsync(new GraphicChartPage()));
            NavigateToAboutPageCommand = new Command(async () => await NavigateToPageAsync(new AboutPage()));

            if (DeviceInfo.Platform == DevicePlatform.tvOS)
            {
                SetupForTVOS();
            }

        }

        private async Task NavigateToPageAsync(Page page)
        {
            IsBusy = true;  // Show loading animation
            await Task.Delay(500);  // Simulate a delay for loading
            await Application.Current.MainPage.Navigation.PushAsync(page);
            IsBusy = false; // Hide loading animation
        }

        private void SetupForTVOS()
        {
            if (GetButtons().Any())
            {
                var firstButton = GetButtons().First();
                firstButton.Focus(); // Set the focus on the first button when tvOS starts up
            }
        }

        private IEnumerable<Button> GetButtons()
        {
            if (Application.Current.MainPage is ContentPage mainPage)
            {
                var layout = mainPage.Content as StackLayout;
                return layout?.Children.OfType<Button>() ?? Enumerable.Empty<Button>();
            }

            return Enumerable.Empty<Button>();
        }
    }
}
