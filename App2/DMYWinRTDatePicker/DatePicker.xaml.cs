using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DMYWinRTDatePicker

{

    public sealed partial class DatePicker : UserControl, INotifyPropertyChanged
    {
        // Using a DependencyProperty as the backing store for MaxYear.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxYearProperty =
            DependencyProperty.Register("MaxYear", typeof(int?), typeof(DatePicker), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for MinYear.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinYearProperty =
            DependencyProperty.Register("MinYear", typeof(int?), typeof(DatePicker), new PropertyMetadata(null));

        private int day = DateTime.Now.Day;
        private int month = DateTime.Now.Month;
        private int year = DateTime.Now.Year;

        public DatePicker()
        {
            this.InitializeComponent();
            SetDaysRange();
            this.MonthControl.ItemsSource = Enumerable.Range(1, 12);
            this.Loaded += DatePicker_Loaded;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime Date
        {
            get
            {
                return new DateTime(this.Year, this.Month, this.day);
            }
        }

        public int? Day
        {
            get { return day; }
            set
            {
                this.day = value ?? day;
                OnPropertyChanged("Day");
                OnPropertyChanged("Date");
            }
        }

        public int MaxYear
        {
            get { return (int)GetValue(MaxYearProperty); }
            set { SetValue(MaxYearProperty, value); }
        }

        public int MinYear
        {
            get { return (int)GetValue(MinYearProperty); }
            set { SetValue(MinYearProperty, value); }
        }

        public int Month
        {
            get { return month; }
            set
            {
                month = value;
                SetDaysRange();
                OnPropertyChanged("Month");
                OnPropertyChanged("Day");
                OnPropertyChanged("Date");
            }
        }

        public int Year
        {
            get { return year; }
            set
            {
                year = value;
                SetDaysRange();
                OnPropertyChanged("Day");
                OnPropertyChanged("Year");
                OnPropertyChanged("Date");
            }
        }

        private void DatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            var minYear = (int?)GetValue(MinYearProperty) ?? DateTime.MinValue.Year;
            var maxYear = (int?)GetValue(MaxYearProperty) ?? DateTime.MaxValue.Year;
            this.YearControl.ItemsSource = Enumerable.Range(minYear, maxYear - minYear + 1);

            if (DateTime.Now.Year < minYear)
            {
                this.Year = minYear;
            }
            else if (DateTime.Now.Year > maxYear)
            {
                this.Year = maxYear;
            }
            else
            {
                this.Year = DateTime.Now.Year;
            }
        }

        private void OnPropertyChanged([CallerMemberName]string caller = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(caller));
            }
        }

        private void SetDaysRange()
        {
            var r = Enumerable.Range(1, DateTime.DaysInMonth(this.year, this.month));
            if (day > r.Max())
            {
                this.day = r.Last();
            }
            this.DayControl.ItemsSource = r;
        }
    }
}
