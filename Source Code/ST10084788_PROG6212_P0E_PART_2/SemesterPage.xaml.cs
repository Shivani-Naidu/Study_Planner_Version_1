using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
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
using Microsoft.SqlServer.Server;
using SemesterClassLibrary;
using ST10084788_PROG6212_P0E_PART_2.Data;
using ST10084788_PROG6212_P0E_PART_2.Model;

namespace ST10084788_PROG6212_P0E_PART_2
{
    /// <summary>
    /// Interaction logic for SemesterPage.xaml
    /// </summary>
    public partial class SemesterPage : Page
    {
        VariableBinding VB = new VariableBinding();
        SemesterClass semester = new SemesterClass();
        ModuleClass modules = new ModuleClass();
        Calculate calc = new Calculate();
        string Username = Properties.Settings.Default.Username;
        DateTime SemDate;
        int semesterWeeks;
        string semestersCombo;


        public SemesterPage()
        {
            InitializeComponent();
            this.DataContext = VB;
        }

        public async Task ShowSemesterDetails()
        {

            SInfoBlock.Visibility = Visibility.Visible;
            SemesterNameBlock.Visibility = Visibility.Visible;
            SemesterDateBlock.Visibility = Visibility.Visible;
            SemesterWeeksBlock.Visibility = Visibility.Visible;
            SNameBox.Visibility = Visibility.Visible;
            DatePicker1.Visibility = Visibility.Visible;
            SWeeksBox.Visibility = Visibility.Visible;
            SemesterBtn.Visibility = Visibility.Visible;    
        }

        public async Task ClearSemesterFields()
        {
            SNameBox.Text = "";
            SWeeksBox.Text = "";
            Notify1.Visibility = Visibility.Hidden;
            Notify2.Visibility = Visibility.Hidden;
            Notify3.Visibility = Visibility.Hidden;



        }

        public async Task HideSemesterDetails()
        {
            SInfoBlock.Visibility = Visibility.Hidden;
            SemesterNameBlock.Visibility = Visibility.Hidden;
            SemesterDateBlock.Visibility = Visibility.Hidden;
            SemesterWeeksBlock.Visibility = Visibility.Hidden;
            SNameBox.Visibility = Visibility.Hidden;
            DatePicker1.Visibility = Visibility.Hidden;
            SWeeksBox.Visibility = Visibility.Hidden;
            SemesterBtn.Visibility = Visibility.Hidden;
            Notify1.Visibility = Visibility.Hidden;
            Notify2.Visibility = Visibility.Hidden;
            Notify3.Visibility = Visibility.Hidden;


        }

        public async Task ShowCurrentSemesterDetails()
        { 
            ExistingSemestersLbl.Visibility = Visibility.Visible;
            SemestersCmb.Visibility = Visibility.Visible;
        
        
        }

        public async Task HideCurrentSemesterDetails()
        {
            ExistingSemestersLbl.Visibility = Visibility.Hidden;
            SemestersCmb.Visibility = Visibility.Hidden;

        }

        public async Task ShowModuleDetails()
        { 
            NewModuleBlock.Visibility = Visibility.Visible;
            ModuleCodeBlock.Visibility = Visibility.Visible;
            ModuleNameBlock.Visibility = Visibility.Visible;
            HoursBlock.Visibility = Visibility.Visible;
            CreditsBlock.Visibility = Visibility.Visible;
            ModuleCodeBox.Visibility = Visibility.Visible;
            ModuleNameBox.Visibility = Visibility.Visible;
            HoursBox.Visibility = Visibility.Visible;   
            CreditsBox.Visibility = Visibility.Visible; 
            ModuleBtn.Visibility = Visibility.Visible;
            ViewModulesBtn.Visibility = Visibility.Visible;
           


        }

        public async Task ClearModuleFields()
        {
            ModuleCodeBox.Text = "";
            ModuleNameBox.Text = "";
            CreditsBox.Text = "";
            HoursBox.Text = "";
            Notify4.Visibility = Visibility.Hidden;
            Notify5.Visibility = Visibility.Hidden;
            Notify6.Visibility = Visibility.Hidden;
            Notify7.Visibility = Visibility.Hidden;

        }

        public async Task HideModuleDetails()
        {
            NewModuleBlock.Visibility = Visibility.Hidden;
            ModuleCodeBlock.Visibility = Visibility.Hidden;
            ModuleNameBlock.Visibility = Visibility.Hidden;
            HoursBlock.Visibility = Visibility.Hidden;
            CreditsBlock.Visibility = Visibility.Hidden;
            ModuleCodeBox.Visibility = Visibility.Hidden;
            ModuleNameBox.Visibility = Visibility.Hidden;
            HoursBox.Visibility = Visibility.Hidden;
            CreditsBox.Visibility = Visibility.Hidden;
            Notify4.Visibility = Visibility.Hidden;
            Notify5.Visibility = Visibility.Hidden;
            Notify6.Visibility = Visibility.Hidden;
            Notify7.Visibility = Visibility.Hidden;
            ModuleBtn.Visibility = Visibility.Hidden;
            ViewModulesBtn.Visibility = Visibility.Hidden;
            DG1.Visibility = Visibility.Hidden;

        }

        public async Task populateCMB(string testUser)
        {
            //string testUser = Properties.Settings.Default.Username;
            SemesterContext semContext = new SemesterContext();
            Model.Semester sem = new Model.Semester();
            using (var context = new SemesterContext()) 
            {
                var populateCmb = from rec in semContext.SemesterInfo
                                  where rec.Username == testUser
                                  select rec.SemesterName;
                var results = populateCmb.ToList();
                results.ForEach(x => SemestersCmb.Items.Add(x));   
            }
        }

        private async void SemesterBtn_Click(object sender, RoutedEventArgs e)
        {
           
            // Validate semester name entered by user
            if (string.IsNullOrEmpty(VB.SemesterNameVB))
            {
                // Notify user if they have not entered a semester name
                Notify1.Visibility = Visibility.Visible;
                Notify1.Foreground = Brushes.Red;
                Notify1.Text = "Error. Please Enter A Valid Semester Name.";
            }
            else
            {
                // Save semester name to DLL class
                semester.SemesterName = VB.SemesterNameVB;
                Notify1.Visibility = Visibility.Visible;
                Notify1.Foreground = Brushes.Green;
                Notify1.Text = "Saved Successfully!";
            }

            //Validate amount of semester weeks entered by user
            int sWeeks = 0;
            if (!int.TryParse(VB.SemesterWeeksVB, out sWeeks))
            {
                //Notify user if they have not entered an amount of semester weeks
                Notify2.Visibility = Visibility.Visible;
                Notify2.Foreground = Brushes.Red;
                Notify2.Text = "Error. Please Enter A Valid Amount Of Weeks In The Semester.";
            }
            else
            {
                //Save semester weeks to DLL class
               semester.SemesterWeeks = sWeeks;
                Notify2.Visibility = Visibility.Visible;
                Notify2.Foreground = Brushes.Green;
                Notify2.Text = "Saved Successfully!";
            }

            // Validate semester start date
            string startTime = DatePicker1.Text;
            string timeReplace = startTime.Replace("/", "");
            string[] format = { "yyyyMMdd" };
            DateTime semesterDate;

            // convert time 
            if (DateTime.TryParseExact(timeReplace, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out semesterDate))
            {
                //Save semester StartDate to class
                semester.SemesterStartDate = semesterDate;
                Notify3.Visibility = Visibility.Visible;
                Notify3.Foreground = Brushes.Green;
                Notify3.Text = "Saved Successfully!";
                
            }
            else
            {
                // alert user if they have not entered a valid value
                Notify3.Visibility = Visibility.Visible;
                Notify3.Foreground = Brushes.Red;
                Notify3.Text = "Error. Please Select A Valid Date.";
            }


            //Validate if all fields have been entered correctly
            if (Notify1.Text == "Saved Successfully!" && Notify2.Text == "Saved Successfully!" && Notify3.Text == "Saved Successfully!")

            {
                // Once all fields have been entered, the program will search and determine if the
                // user has already saved this semester info before
                string testUser = Properties.Settings.Default.Username;
                SemesterContext semContext = new SemesterContext();
                Model.Semester sem = new Model.Semester();
                using (var context = new SemesterContext()) // set the correct database that points to the Semesters table
                {
                    var stud = semContext.SemesterInfo.Find(SNameBox.Text,Username); // searches for username in semester table
                    if (stud == null) // if the user semester name has not been found, it will add it to the table
                    {
                        sem.SemesterName = semester.SemesterName;
                        sem.Username = testUser;
                        sem.SemesterDate = semester.SemesterStartDate;
                        sem.SemesterWeeks = semester.SemesterWeeks;
                        semContext.SemesterInfo.Add(sem); // add data to semester table
                        int a = semContext.SaveChanges(); // returns 1 if the data has been saved
                        if (a > 0)
                        {
                            // Notify user that semester info has been saved
                           await HideSemesterDetails();
                            MessageBox.Show("Semester Information Has Been Saved Successfully.", "Semester Saved!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            // Notify user that semester info has not been saved.
                            
                            MessageBox.Show("Semester Information Could Not Be Saved. Please Try Again.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        // adds new semester data to combobox which display all of the users semester
                        SemestersCmb.Items.Clear();
                        await populateCMB(testUser);
                    }

                    else if (stud != null) // the user has already saved that semester info before
                    { 
                        // Notifies user that the semester info has been saved before
                        await HideSemesterDetails();
                        MessageBox.Show("Semester Information Has Already Been Saved.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        // populates combobox with existing semesters
                        SemestersCmb.Items.Clear();
                        await populateCMB(testUser);
                    }
                }
            }

            else // the user has not entered all fields that is required for the semester data
            {
                // display an error message to the user- stating that they have not entered all required fields
                MessageBox.Show("Please Ensure That All Semester Fields Have Been Filled Correctly", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private async void ModuleBtn_Click(object sender, RoutedEventArgs e)
        {
            // variable to hold a value if the semesters cmb has been selected

           
            if (SemestersCmb.SelectedIndex == -1)
            {
                semestersCombo = "Empty";
            }

            else
            {
                semestersCombo = "Not Empty";


            }

            // Validate module code
            if (string.IsNullOrEmpty(VB.ModuleCodeVB))
            {
                //Notify user if they have not entered a module code
                Notify4.Visibility = Visibility.Visible;
                Notify4.Foreground = Brushes.Red;
                Notify4.Text = "Error. Please Enter A Valid Module Code.";

            }
            else
            {
                // save module code
                modules.ModuleCode = VB.ModuleCodeVB;
                Notify4.Visibility = Visibility.Visible;
                Notify4.Foreground = Brushes.Green;
                Notify4.Text = "Saved Successfully!";
            }

            // Validate module name
            if (string.IsNullOrEmpty(VB.ModuleNameVB))
            {
                //Notify user if they have not entered a valid module name
                Notify5.Visibility = Visibility.Visible;
                Notify5.Foreground = Brushes.Red;
                Notify5.Text = "Error. Please Enter A Valid Module Name.";

            }
            else
            {
                // save module name
                modules.ModuleName = VB.ModuleNameVB;
                Notify5.Visibility = Visibility.Visible;
                Notify5.Foreground = Brushes.Green;
                Notify5.Text = "Saved Successfully!";
            }

            //Validate module credits
            int credits = 0;
            if (!int.TryParse(VB.CreditsVB, out credits))
            {
                //notify user if they have not entered a valid amount module credits
                Notify6.Visibility = Visibility.Visible;
                Notify6.Foreground = Brushes.Red;
                Notify6.Text = "Error. Please Enter A Valid Amount Of Module Credits.";

            }
            else
            {
                //save module credits
                modules.ModuleCredits = credits;
                Notify6.Visibility = Visibility.Visible;
                Notify6.Foreground = Brushes.Green;
                Notify6.Text = "Saved Successfully!";
            }

            //Validate class hours per week
            int classHours = 0;
            if (!int.TryParse(VB.ClassHoursVB, out classHours))
            {
                // Notify user if they have not entered a valid amount of class hours per week
                Notify7.Visibility = Visibility.Visible;
                Notify7.Foreground = Brushes.Red;
                Notify7.Text = "Error. Please Enter A Valid Amount Of Class Hours Per Week.";

            }
            else
            {
                // Save amount of class hours per week
                modules.ClassHoursPerWeek = classHours;
                Notify7.Visibility = Visibility.Visible;
                Notify7.Foreground = Brushes.Green;
                Notify7.Text = "Saved Successfully!";
            }

            // Validate if all fieds have been entered correctly
            if (Notify4.Text == "Saved Successfully!"
               && Notify5.Text == "Saved Successfully!"
               && Notify6.Text == "Saved Successfully!"
               && Notify7.Text == "Saved Successfully!"
               && semestersCombo == "Not Empty")
            {
                // First retrieve the number of semester weeks, this needed to calculate the amount 
                // of required self study hours per week for the module
                string testUser = Properties.Settings.Default.Username; // get user name
                using (var context = new SemesterContext()) // set the correct database that points to the Semesters table
                {
                    var stud = context.SemesterInfo.Find(SemestersCmb.SelectedItem.ToString(), testUser); // Searches for semester that user has selected from combobox
                    if (stud != null) // if it finds the semester, it will retrieve the number of weeks in that semester
                    {
                        semesterWeeks = stud.SemesterWeeks;
                    }

                    else
                    {
                        // Notify user that the selected semester does not exist
                        MessageBox.Show("The Selected Semester Does Not Exist.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }

                Model.Module mod = new Model.Module(); // object of module class that holds the variables of the Modules table
                using (var modContext = new ModuleContext()) // set the correct context that points to the Modules table
                {
                    var stud = modContext.ModuleInfo.Find(modules.ModuleCode, SemestersCmb.SelectedItem.ToString(), testUser); // checks if the module already exists
                    if (stud == null) // module information does not exist
                    {
                        // since the module does not already exist, the program will add the data to the Modules table
                        mod.ModuleCode = modules.ModuleCode;
                        mod.SemesterName = SemestersCmb.SelectedItem.ToString();
                        mod.Username = testUser;
                        mod.ModuleName = modules.ModuleName;
                        mod.ModuleCredits = modules.ModuleCredits;
                        mod.ClassHoursPerWeek = modules.ClassHoursPerWeek;
                        mod.NeededHoursToStudy = calc.CalcHoursPerWeek(modules.ModuleCredits, semesterWeeks, modules.ClassHoursPerWeek); // calls method from dll to calculate value of field
                        modContext.ModuleInfo.Add(mod); // adds data to Modules table
                        int a = modContext.SaveChanges(); // returns 1, if the data has been saved
                        if (a > 0)
                        {
                            // Notify user that the module info has been saved
                            await ClearModuleFields();
                            DG1.Visibility = Visibility.Hidden;  
                            MessageBox.Show("Module Information Has Been Saved Successfully.", "Module Saved!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            // Notify user that the module info has not been saved
                            MessageBox.Show("Module Information Has Not been Saved.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    else if (stud != null)  // the module has already been saved
                    {
                        // Notify user that this module already exists
                        await ClearModuleFields();
                        MessageBox.Show("Module Information Already Exists.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                    }

                }
            }

            else if (Notify4.Text == "Saved Successfully!"
               && Notify5.Text == "Saved Successfully!"
               && Notify6.Text == "Saved Successfully!"
               && Notify7.Text == "Saved Successfully!"
               && semestersCombo == "Empty")
            {
                // Notify user that module details have not been entered
                MessageBox.Show("Please Select A Semester.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                MessageBox.Show("Please Ensure That All Module Fields Have Been Filled In Correctly.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private async void CurrentSemester_Click(object sender, RoutedEventArgs e)
        {
           await ClearSemesterFields();
          await  HideSemesterDetails();
            await HideModuleDetails();
            //await ShowCurrentSemesterDetails();
            // displays all semesters that the user has saved
            string TestUser = Properties.Settings.Default.Username;
            SemesterContext semContext = new SemesterContext();
            Model.Semester sem = new Model.Semester();
            using (var context = new SemesterContext()) // set the correct context that points to the Semesters table
            {
                // gets all semesters relating to the user that is logged in
                var populateCmb = from rec in semContext.SemesterInfo
                                  where rec.Username == TestUser
                                  select rec.SemesterName;
                
                var semestersList = populateCmb.ToList(); // converts results to a list

                if (populateCmb == null || (!semestersList.Any())) // Checks if the user has entered any previous semester data
                {
                    // Notify user that they have no semester data added
                    MessageBox.Show("You Do Not Have Any Existing Semester Information. Please Add A Semester And Try Again.", "No Semesters Found!", MessageBoxButton.OK, MessageBoxImage.Error);

                }

                else
                {
                    await ShowCurrentSemesterDetails();
                    SemestersCmb.Items.Clear();
                    await populateCMB(TestUser);

                }


            }
        }

       

        private async void AddSemesterBtn_Click(object sender, RoutedEventArgs e)
        {
          await ClearModuleFields();
          await HideCurrentSemesterDetails();
          await ShowSemesterDetails();
          await  HideModuleDetails();
            
        }

        private async void ViewModulesBtn_Click(object sender, RoutedEventArgs e)
        {
            // display all modules
            string testuser = Properties.Settings.Default.Username;
            Model.Module mod = new Model.Module();
            using (var modContext = new ModuleContext()) // set to correct context that points to Modules table
            {
                // search for all modules relating to user
                var populateDG = from rec in modContext.ModuleInfo
                                  where rec.Username == testuser
                                  select rec;

                var modulesList = populateDG.ToList(); // converts results to a list

                if (populateDG == null || (!modulesList.Any())) // Checks if the user has entered any previous semester data
                {
                    // Notify user that they have no semester data added
                    MessageBox.Show("You Do Not Have Any Existing Modules. Please Add A Module And Try Again.", "No Modules Found!", MessageBoxButton.OK, MessageBoxImage.Error);

                }

                else
                {
                    DG1.Visibility = Visibility.Visible;
                    DG1.ItemsSource = modulesList;

                }


                
            }
        }

        private void SemestersCmb_SelectedItem(object sender, RoutedEventArgs e)
        {
            
        }

        private async void SemestersCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await ShowModuleDetails();
        }
    }
}
