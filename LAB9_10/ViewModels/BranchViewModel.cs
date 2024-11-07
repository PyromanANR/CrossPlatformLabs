using LAB9.Services;
using LAB9.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace LAB9.ViewModels
{
    public class BranchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly ApiService _apiService;
        public ObservableCollection<Branch> Branches { get; set; }
        public ObservableCollection<RefBranchType> BranchTypes { get; set; }
        public Branch NewBranch { get; set; }

        public ICommand AddBranchCommand { get; }

        private RefBranchType _selectedBranchType;
        public RefBranchType SelectedBranchType
        {
            get { return _selectedBranchType; }
            set
            {
                _selectedBranchType = value;
                NewBranch.BranchTypeCode = value?.BranchTypeCode;
                OnPropertyChanged(nameof(SelectedBranchType));
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public BranchViewModel()
        {
            _apiService = new ApiService();
            Branches = new ObservableCollection<Branch>();
            BranchTypes = new ObservableCollection<RefBranchType>();
            NewBranch = new Branch();
            AddBranchCommand = new Command(async () => await AddBranchAsync());

            LoadData();
        }

        private async Task LoadData()
        {
            IsBusy = true; 
            try
            {
                var branches = await _apiService.GetBranchesAsync();
                foreach (var branch in branches)
                    Branches.Add(branch);

                var branchTypes = await _apiService.GetBranchTypesAsync();
                BranchTypes.Clear();
                foreach (var type in branchTypes)
                    BranchTypes.Add(type);
            }
            finally
            {
                IsBusy = false; 
            }
        }

        private async Task AddBranchAsync()
        {
            IsBusy = true; 
            try
            {
                var success = await _apiService.AddBranchAsync(NewBranch);
                if (success)
                {
                    Branches.Add(NewBranch);
                    NewBranch = new Branch();
                }
            }
            finally
            {
                IsBusy = false; 
            }
        }
    }
}
