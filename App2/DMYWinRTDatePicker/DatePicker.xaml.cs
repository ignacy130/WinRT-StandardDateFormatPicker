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
        private int year = DateTime.Now.Year;
        private int month = DateTime.Now.Month;
        private int day = DateTime.Now.Day;

        public DatePicker()
        {
            this.InitializeComponent();
            SetDaysRange();
            this.MonthControl.ItemsSource = Enumerable.Range(1, 12);
            this.YearControl.ItemsSource = Enumerable.Range(DateTime.MinValue.Year, DateTime.MaxValue.Year - DateTime.MinValue.Year);
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
