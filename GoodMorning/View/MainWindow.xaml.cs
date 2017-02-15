using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GoodMorning
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Days days;
        public Day currentDay;
        CalendarController calendarController;
        NewsController newsController;
        CurrencyController currencyController;
        List<News> news;
        public MainWindow()
        {
            InitializeComponent();
            calendarController = new CalendarController(this);
            newsController = new NewsController();
            currencyController = new CurrencyController();
            //load data from xml
            days = calendarController.LoadCalendar();
            currentDay = days.DaysList.Find(d => d.Date.Equals(DateTime.Today));
            tasksList.ItemsSource = days.DaysList.Find(d => d.Date.Equals(currentDay.Date)).Tasks;
            currentDayLabel.Content = currentDay.Date.ToShortDateString() + " " + currentDay.Date.DayOfWeek.ToString();
            //news
            news = newsController.GetData();
            newsListBox.ItemsSource = news;
            //currency
            euroLabel.Content = currencyController.GetData(currencyController.currencySymbols[(int)CurrencyIndexEnum.EUR]) + " PLN";
            poundLabel.Content = currencyController.GetData(currencyController.currencySymbols[(int)CurrencyIndexEnum.GBP]) +  "PLN";
            dollarLabel.Content = currencyController.GetData(currencyController.currencySymbols[(int)CurrencyIndexEnum.USD]) + " PLN";
        }

        private void nextDayButton_Click(object sender, RoutedEventArgs e)
        {
            currentDay = days.DaysList.Find(d => d.Date.Equals(currentDay.Date.AddDays(1)));
            tasksList.ItemsSource = days.DaysList.Find(d => d.Date.Equals(currentDay.Date)).Tasks;
            currentDayLabel.Content = currentDay.Date.ToShortDateString() + " " + currentDay.Date.DayOfWeek.ToString();
            tasksList.Items.Refresh();
        }

        private void prevDayButton_Click(object sender, RoutedEventArgs e)
        {
            currentDay = days.DaysList.Find(d => d.Date.Equals(currentDay.Date.AddDays(-1)));
            tasksList.ItemsSource = days.DaysList.Find(d => d.Date.Equals(currentDay.Date)).Tasks;
            currentDayLabel.Content = currentDay.Date.ToShortDateString() + " " + currentDay.Date.DayOfWeek.ToString();
            tasksList.Items.Refresh();
        }

        private void addTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow addTaskWin = new AddTaskWindow(calendarController, currentDay);
            addTaskWin.ShowDialog();
            tasksList.Items.Refresh();
            calendarController.SaveCalendar(days);
        }

        private void editMenu_Click(object sender, RoutedEventArgs e)
        {
            EditTaskWindow editTaskWin = new EditTaskWindow(calendarController, currentDay, (Task)tasksList.SelectedValue);
            editTaskWin.ShowDialog();
            tasksList.Items.Refresh();
            calendarController.SaveCalendar(days);
        }

        private void deleteMenu_Click(object sender, RoutedEventArgs e)
        {
            calendarController.DeleteTask(currentDay, (Task)tasksList.SelectedValue);
            tasksList.Items.Refresh();
            DescrTextBlock.Text = "";
            calendarController.SaveCalendar(days);

        }

        private void detailsMenu_Click(object sender, RoutedEventArgs e)
        {
            var item = tasksList.SelectedValue;
            if (item != null)
            {
                DescrTextBlock.Text = ((Task)item).Description;
            }
        }

      
        //load form xml
        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            days = calendarController.LoadCalendar();
            currentDay = days.DaysList.Find(d => d.Date.Equals(DateTime.Today));
            tasksList.ItemsSource = days.DaysList.Find(d => d.Date.Equals(currentDay.Date)).Tasks;
            currentDayLabel.Content = currentDay.Date.ToShortDateString() + " " + currentDay.Date.DayOfWeek.ToString();
            tasksList.Items.Refresh();
        }

        private void newsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
