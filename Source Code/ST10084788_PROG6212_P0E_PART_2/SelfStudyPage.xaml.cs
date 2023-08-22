using Microsoft.SqlServer.Server;
using ST10084788_PROG6212_P0E_PART_2.Data;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml.Linq;
using SemesterClassLibrary;
using ST10084788_PROG6212_P0E_PART_2.Model;

namespace ST10084788_PROG6212_P0E_PART_2
{
    /// <summary>
    /// Interaction logic for SelfStudyPage.xaml
    /// </summary>
    public partial class SelfStudyPage : Page
    {
        // object Calculate class -- whic is found in the DLL
        Calculate calc = new Calculate();
        
        // variable which is used hold the semester start date
        DateTime semesterStartDate;
       
        // variable to hold the amount of hours studied this week
        int hoursStudiedThisWeek;

        // variable that calulates the remaining number of study hours for the current week
        int remainingHours;
        
        // Variable that calculates the current week in the semester
        int currentWeekInSemester;

        // variable that holds the value for the total hours needed to study per week
        int requiredHours;


        DateTime semesterStartDate2;

        public SelfStudyPage()
        {
            InitializeComponent();
        }

        public async Task HideStudySessionDetails()
        {
            studyLbl.Visibility = Visibility.Hidden;
            MCodeBlock.Visibility = Visibility.Hidden;
            SNameBlock.Visibility = Visibility.Hidden;
            StudyDateBlock.Visibility = Visibility.Hidden;  
            HoursBlock.Visibility = Visibility.Hidden;
            ModuleCodeBlock.Visibility = Visibility.Hidden;
            SemesterNameBlock.Visibility = Visibility.Hidden;
            DatePicker3.Visibility = Visibility.Hidden;
            HoursStudyBox.Visibility = Visibility.Hidden;   
            Notify1.Visibility = Visibility.Hidden;
            Notify2.Visibility = Visibility.Hidden;
            StudySessionBtn.Visibility = Visibility.Hidden;
       
        }

        public async Task ShowStudySessionDetails()
        {
            studyLbl.Visibility = Visibility.Visible;
            MCodeBlock.Visibility = Visibility.Visible;
            SNameBlock.Visibility = Visibility.Visible;
            StudyDateBlock.Visibility = Visibility.Visible;
            HoursBlock.Visibility = Visibility.Visible;
            ModuleCodeBlock.Visibility = Visibility.Visible;
            SemesterNameBlock.Visibility = Visibility.Visible;
            DatePicker3.Visibility = Visibility.Visible;
            HoursStudyBox.Visibility = Visibility.Visible;
            Notify1.Visibility = Visibility.Visible;
            Notify2.Visibility = Visibility.Visible;
            StudySessionBtn.Visibility = Visibility.Visible;
        }



        public async Task <int> GetTotalHours()
        {
            // This task is used to retrieve the amount of self study hours per week for a specific module

            string TestUser = Properties.Settings.Default.Username;
            Model.Module mod = new Model.Module();
            using (var modCcontext = new ModuleContext())
            {
               var moduleData = from rec in modCcontext.ModuleInfo
                                where rec.ModuleCode == ModuleCodeBlock.Text && rec.SemesterName == SemesterNameBlock.Text && rec.Username == TestUser
                                select rec.NeededHoursToStudy;

               var list = moduleData.ToList();
               requiredHours = (from x in list select x).Max();

   
            }
            return requiredHours;
        }
 

        private void ViewModules_Click(object sender, RoutedEventArgs e)
        {
            SummaryBlock.Visibility = Visibility.Visible;
            DG2.Visibility = Visibility.Visible;  
            TitleLbl.Visibility = Visibility.Visible; 
            // check if the user has any existing modules
            string testuser = Properties.Settings.Default.Username;
            Model.Module mod = new Model.Module();
            using (var modContext = new ModuleContext())
            {
                var moduleSearch = from rec in modContext.ModuleInfo
                                  where rec.Username == testuser
                                  select rec;

                var modulesList = moduleSearch.ToList(); // converts results to a list

                if (moduleSearch == null || (!modulesList.Any())) // Checks if the user has entered any previous modules data
                {
                    // Notify user that they have no module data added
                    MessageBox.Show("You Do Not Have Any Existing Modules Saved. Please Add A Module And Try Again.", "No Module Found!", MessageBoxButton.OK, MessageBoxImage.Error);
                    SummaryBlock.Visibility = Visibility.Hidden;
                    DG2.Visibility = Visibility.Hidden;
                    TitleLbl.Visibility = Visibility.Hidden;
                }

                else
                {
                    DG2.ItemsSource = modulesList;

                }

            }
        }

        private async void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // gather information from the selected row
            var row = sender as DataGridRow;
            var modules = row.DataContext as Model.Module;

            await ShowStudySessionDetails();
            
            // populates selected data onto textblocks
            SemesterNameBlock.Text = modules.SemesterName;
            ModuleCodeBlock.Text = modules.ModuleCode;

            // set TestUser variable to hold current username
            string TestUser = Properties.Settings.Default.Username;
            
            ViewSessionsBtn.Visibility = Visibility.Visible;
            // get semester startDate
            using (var context = new SemesterContext()) // set the correct database that points to the login table
            {
                var stud = context.SemesterInfo.Find(SemesterNameBlock.Text, TestUser);
                if (stud != null)
                {
                    semesterStartDate2 = stud.SemesterDate;
                }

            }

            // calculate current week in semester
            currentWeekInSemester = calc.currentWeek(semesterStartDate2);


            // Checks if the user any study records for that module in the current week
            using (var StudyContext = new StudySessionContext()) // set the correct context that points to the StudySessions table
            {
                var studySessions = from rec in StudyContext.StudySessionInfo
                                  where rec.ModuleCode == ModuleCodeBlock.Text && rec.Username == TestUser && rec.SemesterName == SemesterNameBlock.Text && rec.StudyWeek == currentWeekInSemester
                                  select rec.SemesterName;
                var studyList = studySessions.ToList(); // converts results to a list

                if (studySessions == null || (!studyList.Any()))
                {
                    // The user has no study records for the selected module in the current week
                    // Since there are no records foun in the current week, the remaining study hours
                    // is to the original value that user is required to study for per week
                   
                    remainingHours = modules.NeededHoursToStudy;// gets hours from the selected row

                    SummaryBlock.Text = "Self-Study Hours Required For " + ModuleCodeBlock.Text + " This Week: " + remainingHours.ToString() + " Hours\n" +
                                        "Hours Studied For This Weeks: " + "0 Hours\n" +
                                        "Remaining Hours For This Week: " + remainingHours.ToString() + " Hours";
                    
                }

                else
                { 
                    // the user has self study sessions for this week
                    
                    // searches for all self study records with the current week and returns sum of hours studied
                    
                    // variable for current user
                    string testuser = Properties.Settings.Default.Username;
                    
                    // searches for all study sessions that the user has done for the current week
                    using (var studySCcontext = new StudySessionContext())
                    {
                        var calculateHours = from rec in studySCcontext.StudySessionInfo
                                             where rec.ModuleCode == ModuleCodeBlock.Text && rec.SemesterName == SemesterNameBlock.Text && rec.Username == testuser && rec.StudyWeek == currentWeekInSemester
                                             select rec.HoursStudied;

                        var SumList = calculateHours.ToList(); // converts results into a list

                        // gets the total hours study
                        hoursStudiedThisWeek = (from x in SumList select x).Sum();
                       
                        // calls GetTotalHours Task to determine what the required number of self study hours
                        await  GetTotalHours();

                        remainingHours = requiredHours - hoursStudiedThisWeek;

                        if (remainingHours == 0)
                        {
                            SummaryBlock.Text = "Self-Study Hours Required For " + ModuleCodeBlock.Text + " This Week: " + requiredHours.ToString() + " Hours\n" +
                                                "Hours Studied For This Week: " + hoursStudiedThisWeek.ToString() + " Hours\n" +
                                                "Remaining Hours For This Week: " + remainingHours.ToString() + " Hours.\n" +
                                                "You Have Completed The Required Number Of Self-Study Hours For This Week.";



                        }

                        else if (remainingHours < 0)
                        {
                            SummaryBlock.Text = "Self-Study Hours Required For " + ModuleCodeBlock.Text + " This Week: " + requiredHours.ToString() + " Hours\n" +
                                                "Hours Studied For This Week: " + hoursStudiedThisWeek.ToString() + " Hours\n" +
                                                "Remaining Hours For This Week: " + "0 Hours.\n" +
                                                "You Have Completed The Required Number Of Self-Study Hours For This Week.";

                        }

                        else
                        {
                            SummaryBlock.Text = "Self-Study Hours Required For " + ModuleCodeBlock.Text + " This Week: " + requiredHours.ToString() + " Hours\n" +
                                                "Hours Studied For This Weeks: " + hoursStudiedThisWeek.ToString() + " Hours\n" +
                                                "Remaining Hours For This Week: " + remainingHours.ToString() + " Hours.";

                        }

                    }

                }
            }

        }


        private async void StudySessionBtn_Click(object sender, RoutedEventArgs e)
        {
            int hoursStudied = 0;
            if (!int.TryParse(HoursStudyBox.Text, out hoursStudied))
            {
                Notify2.Visibility = Visibility.Visible;
                Notify2.Foreground = Brushes.Red;
                Notify2.Text = "Error. Please Enter A Valid Amount Of Hours.";
            }
            else
            {
                //Save semester weeks to DLL class
                Notify2.Visibility = Visibility.Visible;
                Notify2.Foreground = Brushes.Green;
                Notify2.Text = "Saved Successfully!";
            }

            // Validate study date
            string startTime = DatePicker3.Text;
            string timeReplace = startTime.Replace("/", "");
            string[] format = { "yyyyMMdd" };
            DateTime userStudyDate;

            // convert time 
            if (DateTime.TryParseExact(timeReplace, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out userStudyDate))
            {
                //Save semester StartDate to class
                
                Notify1.Visibility = Visibility.Visible;
                Notify1.Foreground = Brushes.Green;
                Notify1.Text = "Saved Successfully!";

            }
            else
            {
                // alert user if they have not entered a valid value
                Notify1.Visibility = Visibility.Visible;
                Notify1.Foreground = Brushes.Red;
                Notify1.Text = "Error. Please Select A Valid Date.";
            }

            if (Notify1.Text == "Saved Successfully!" && Notify2.Text == "Saved Successfully!")
            {
                // Adds new study study session to table
                Model.StudySession study = new Model.StudySession();
                string testUser = Properties.Settings.Default.Username; // get current username
                using (var studContext = new StudySessionContext()) // set the correct context that points to the StudySessions table
                {
                    // searches if the study session already exists
                    var stud = studContext.StudySessionInfo.Find(await GenerateStudyID(ModuleCodeBlock.Text, SemesterNameBlock.Text), ModuleCodeBlock.Text, SemesterNameBlock.Text, testUser);

                    if (stud == null)
                    {
                        // if the study session was not found, it will add it

                        //calls GenerateStudyID task to generate the StudyID field
                        study.StudyId = await GenerateStudyID(ModuleCodeBlock.Text, SemesterNameBlock.Text);
                        study.ModuleCode = ModuleCodeBlock.Text;
                        study.SemesterName = SemesterNameBlock.Text;
                        study.Username = testUser;
                        //DateTime tempDate = getDate(DatePicker3.Text);
                        study.StudyDate = userStudyDate;
                        study.HoursStudied = int.Parse(HoursStudyBox.Text);

                        // get semester start data -- used to calculate study week field 
                        using (var context = new SemesterContext()) // set the correct database that points to the login table
                        {
                            var stud2 = context.SemesterInfo.Find(SemesterNameBlock.Text, testUser);
                            if (stud2 != null)
                            {
                                semesterStartDate = stud2.SemesterDate;
                            }

                        }

                        study.StudyWeek = calc.workWeek(DatePicker3.SelectedDate.Value.Date, semesterStartDate);

                        // adds data to table
                        studContext.StudySessionInfo.Add(study);
                        int a = studContext.SaveChanges(); // returns 1, if the data has been saved
                        if (a > 0)
                        {
                            // notify user that the data has been saved
                            DG4.Visibility = Visibility.Hidden;
                            Notify1.Visibility = Visibility.Hidden;
                            Notify2.Visibility = Visibility.Hidden;
                            HoursStudyBox.Text = "";
                            MessageBox.Show("Self-Study Session Has Been Saved Successfully.", "Session Saved!", MessageBoxButton.OK, MessageBoxImage.Information);
                            ViewSessionsBtn.Visibility= Visibility.Visible;
                        }
                        else
                        {
                            // notify user that the data has not been saved
                            MessageBox.Show("Self-Study Session Has Not Been Saved.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }


                    }

                    else if (stud != null) // The self-study session already exists
                    {
                        MessageBox.Show("Self-Study Session Has Already Been Saved.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                }


            }

            else
            {

                MessageBox.Show("Please Ensure That All Self-Study Session Fields Are Filled In Correctly.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);


            }


            
        }

        public DateTime getDate(string dateString)
        {
           // DateTime studyDate;
            string startTime = DatePicker3.Text;
            string timeReplace = startTime.Replace("/", "");
            string[] format = { "yyyyMMdd" };
            DateTime semesterDate;

            // convert time 
            if (DateTime.TryParseExact(timeReplace, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out semesterDate))
            {
                //Save semester StartDate to class
                //semester.SemesterStartDate = semesterDate;

                ;

                
            }
            return semesterDate;
        }

        public async Task <int> GenerateStudyID(string ModuleCode, string SemesterName)
        {
           int StudyID;
           string testUser = Properties.Settings.Default.Username;    
            using (var context = new StudySessionContext()) // set the correct database that points to the login table
            {
                var searchForUser = from rec in context.StudySessionInfo
                                  where rec.Username == testUser
                                  select rec.StudyId;
                var UserList = searchForUser.ToList();

                if (searchForUser == null || (!UserList.Any()))
                {
                    // User Does not exist in study session table
                    StudyID = 1;

                }

                else
                {
                    string testuser = Properties.Settings.Default.Username;
                    //Model.Module mod = new Model.Module();
                    using (var SScontext = new StudySessionContext())
                    {
                        var populateCmb = from rec in SScontext.StudySessionInfo
                                          where rec.Username == testuser
                                          select rec.StudyId;

                        var list = populateCmb.ToList();
                        int max = (from x in list select x).Max();

                        StudyID = max + 1;
                        
                    }
                   

                }


            }



            return StudyID;
        }



       

        

        private void DG2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ViewSessionsBtn_Click(object sender, RoutedEventArgs e)
        {
            DG4.Visibility = Visibility.Visible;
            string testuser = Properties.Settings.Default.Username;
            Model.StudySession study = new Model.StudySession();
            using (var studyContext = new StudySessionContext())
            {
                // search for all modules relating to user
                var studySearch = from rec in studyContext.StudySessionInfo
                                 where rec.Username == testuser
                                 select rec;

                var studyList = studySearch.ToList(); // converts results to a list

                if (studySearch == null || (!studyList.Any())) // Checks if the user has entered any previous semester data
                {
                    // Notify user that they have no semester data added
                    MessageBox.Show("You Do Not Have Any Existing Study Sessions. Please Add A Study Session And Try Again.", "No Study Sessions Found!", MessageBoxButton.OK, MessageBoxImage.Error);

                }

                else
                {
                    DG4.Visibility = Visibility.Visible;
                    DG4.ItemsSource = studyList;

                }
            }

            
        }

        private void SummaryBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
