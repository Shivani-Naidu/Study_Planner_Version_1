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
using static System.Net.Mime.MediaTypeNames;

namespace ST10084788_PROG6212_P0E_PART_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // gets username
        
        DisplayStartUp icon = new DisplayStartUp();
        
        // delegate
        public delegate void DisplayMessage(string a);
        HomePage HPage = new HomePage();
        string welcome;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LV1_Selected(object sender, RoutedEventArgs e)
        {
            Tg_Btn.IsChecked = false;
            nav_panel.Width = 85;
            string testUser = Properties.Settings.Default.Username;
            FrameMain.Content = HPage;
            DisplayMessage del = new DisplayMessage(DelegateMethod);
            // calls delegate method to generate message
            del(testUser);
            HPage.UsernameBlock.Text = welcome;    

        }

        // delegate method
        public  void DelegateMethod(string message)
        {
             welcome = "Hi! " + message;
         }

        private void LV2_Selected(object sender, RoutedEventArgs e)
        {
            Tg_Btn.IsChecked = false;
            nav_panel.Width = 85;
            SemesterPage SP = new SemesterPage();
            FrameMain.Content = SP;
             
        }

        private void LV_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Tg_Btn.IsChecked == true)
            {
                tt_Home.Visibility = Visibility.Collapsed;
                tt_Semester.Visibility = Visibility.Collapsed;
                tt_Exit.Visibility = Visibility.Collapsed;

            }
            else
            {
                tt_Home.Visibility = Visibility.Visible;
                tt_Semester.Visibility = Visibility.Visible;
                tt_Exit.Visibility = Visibility.Visible;


            }
        }

        private void LV3_Selected(object sender, RoutedEventArgs e)
        {

            Tg_Btn.IsChecked = false;
            nav_panel.Width = 85;
            SelfStudyPage SSP = new SelfStudyPage();
            FrameMain.Content = SSP;
           
        }

        private void FrameMain_Loaded(object sender, RoutedEventArgs e)
        {
            FrameMain.Content = icon;
        }
    }
}
