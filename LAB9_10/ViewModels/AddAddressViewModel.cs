using LAB9.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LAB9.Models;

namespace LAB9.ViewModels
{
    public class AddAddressViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private bool _isBusy;
        public Address NewAddress { get; set; }
        public ICommand AddAddressCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public AddAddressViewModel()
        {
            _apiService = new ApiService();
            NewAddress = new Address();
            AddAddressCommand = new Command(async () => await AddAddressAsync(), () => !IsBusy);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                ((Command)AddAddressCommand).ChangeCanExecute();
            }
        }

        private async Task AddAddressAsync()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                var success = await _apiService.AddAddressAsync(NewAddress);
                if (success)
                {
                    // Reset the form after successful submission
                    NewAddress = new Address();
                    OnPropertyChanged(nameof(NewAddress));
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
