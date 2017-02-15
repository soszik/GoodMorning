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
using System.Windows.Shapes;

namespace GoodMorning
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        private CalendarController calendarController;
        private Task currentTask;
        private Day currentDay;
        public EditTaskWindow( CalendarController calCon, Day day, Task task)
        {
            InitializeComponent();
            calendarController = calCon;
            currentTask = task;
            currentDay = day;
            string[] minutesTab = new string[4];
            string[] hoursTab = new string[24];
            int min = 0;
            for (int i = 0; i < 24; i++)
            {
                hoursTab[i] = i.ToString("D2");
            }
            for (int i = 0; i < 4; i++)
            {
                minutesTab[i] = min.ToString("D2");
                min += 15;
            }
            hourBox.ItemsSource = hoursTab;
            minBox.ItemsSource = minutesTab;
            tittleBox.Text = task.Tittle;
            descrBox.Text = task.Description;
            hourBox.SelectedValue = task.Time.Hour.ToString("D2");
            minBox.SelectedValue = task.Time.Minute.ToString("D2");
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (hourBox.SelectedItem == null)
            {
                MessageBox.Show("Select hour");
                return;
            }
            if (minBox.SelectedItem == null)
            {
                MessageBox.Show("Select minutes");
                return;
            }
            if(tittleBox.Text.Length > 60)
            {
                MessageBox.Show("Tittle too long");
                return;
            }
            if (tittleBox.Text.Length == 0)
            {
                MessageBox.Show("Tittle too short");
                return;
            }

            if (descrBox.Text.Length > 250)
            {
                MessageBox.Show("Description too long");
                return;
            }
            calendarController.EditTask(currentDay,currentTask, int.Parse(hourBox.Text), int.Parse(minBox.Text),
                                        tittleBox.Text, descrBox.Text);
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
