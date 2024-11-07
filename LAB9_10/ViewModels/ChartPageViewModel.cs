using LAB9.Services;
using LAB9.Models;
using Microcharts;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace LAB9.ViewModels
{
    public class ChartPageViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService;
        private Chart _transactionChart;
        private ObservableCollection<Transaction> _transactions;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Transaction> Transactions
        {
            get => _transactions;
            set
            {
                _transactions = value;
                OnPropertyChanged();
            }
        }

        public Chart TransactionChart
        {
            get => _transactionChart;
            set
            {
                _transactionChart = value;
                OnPropertyChanged();
            }
        }

        public ChartPageViewModel()
        {
            _apiService = new ApiService();
            Transactions = new ObservableCollection<Transaction>();
            LoadTransactionsAsync().ConfigureAwait(false);
        }

        private async Task LoadTransactionsAsync()
        {
            try
            {
                var transactions = await _apiService.GetTransactionsAsync();
                if (transactions != null)
                {
                    Transactions.Clear();
                    foreach (var transaction in transactions)
                    {
                        Transactions.Add(transaction);
                    }

                    // Group transactions by date
                    var dailyTransactions = Transactions
                        .GroupBy(t => t.TransactionDateTime.Date)
                        .OrderBy(t => t.Key)
                        .ToList();

                    var entries = new List<ChartEntry>();

                    foreach (var transactionGroup in dailyTransactions)
                    {
        
                        var randomTransactions = transactionGroup
                            .OrderBy(t => Guid.NewGuid()) 
                            .Take(10)  
                            .ToList();

                    
                        foreach (var transaction in randomTransactions)
                        {
                            entries.Add(new ChartEntry((float)transaction.TransactionAmount)
                            {
                                Label = transactionGroup.Key.ToString("dd/MM"),
                                ValueLabel = transaction.TransactionAmount.ToString("C"),
                                Color = SKColor.Parse("#FF1493")
                            });
                        }
                    }

                    // Create line chart with the random transactions
                    TransactionChart = new LineChart
                    {
                        Entries = entries,
                        LabelTextSize = 24,
                        BackgroundColor = SKColor.Parse("#FFFFFF"),
                        LineMode = LineMode.Spline, // Smoothed line
                        LineSize = 3,
                        ValueLabelOrientation = Orientation.Horizontal,
                        ValueLabelTextSize = 18,
                        LabelOrientation = Orientation.Horizontal,
                        PointMode = PointMode.Circle,
                        PointSize = 10
                    };
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    $"Error loading transactions: {ex.Message}",
                    "OK");
            }
        }



        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}