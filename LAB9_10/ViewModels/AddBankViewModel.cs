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
    public class AddBankViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private bool _isBusy;
        public Bank NewBank { get; set; }
        public ICommand AddBankCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public AddBankViewModel()
        {
            _apiService = new ApiService();
            NewBank = new Bank();
            AddBankCommand = new Command(async () => await AddBankAsync(), () => !IsBusy);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                ((Command)AddBankCommand).ChangeCanExecute();
            }
        }

        private async Task AddBankAsync()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                var success = await _apiService.AddBankAsync(NewBank);
                if (success)
                {
                    // Reset the form after successful submission
                    NewBank = new Bank();
                    OnPropertyChanged(nameof(NewBank));
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
