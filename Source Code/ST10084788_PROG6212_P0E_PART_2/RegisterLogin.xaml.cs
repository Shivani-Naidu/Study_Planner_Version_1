using SemesterClassLibrary;
using ST10084788_PROG6212_P0E_PART_2.Data;
using ST10084788_PROG6212_P0E_PART_2.InitializeDB;
using ST10084788_PROG6212_P0E_PART_2.Model;
using System;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;


namespace ST10084788_PROG6212_P0E_PART_2
{
   
    public partial class RegisterLogin : Window
    {
        // global variable to check if the password entered by the user has a special character
        private bool hasSpecialCharacter;
        
        //object of MainWindow window
        MainWindow MW = new MainWindow();
        
        // object of LoginContext -- attached to Logins database table
        LoginContext logContext = new LoginContext();

        // object of the VariableBinding class
        VariableBinding binding = new VariableBinding();

        // object of the GenerateDB class - found in InitializeDB folder
        GenerateDB GDB = new GenerateDB();
        public  RegisterLogin()
        {
            var splashScreen = new SplashScreen("../Icons/loading.jpg");
            splashScreen.Show(true);   
            InitializeComponent();

            // calls methods from the GenerateDB class which converts all database tables to lists
            GDB.LoginDB();
            GDB.SemesterDB();
            GDB.ModuleDB();
            GDB.StudySessionDB();
            splashScreen.Close(TimeSpan.FromSeconds(0.5));
        }

        
        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {

            // string variables that hold values that later determines if the user has entered the 
            // necessary fields
            string ValidateUsername = "";
            string ValidatePassword = "";

            // check if the user has entered an username
            if (string.IsNullOrEmpty(UsernameBox.Text.Trim()))
            {
                // display an error message to the user, stating that they have not entered an username
                MessageBox.Show("Please Enter A Valid Username.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
                // set variable to Incorrect since no username has been entered
                ValidateUsername = "Incorrect";
            }

            else
            {
                // if the user has entered a valid username, save the ValidateUsername field to Correct
                ValidateUsername = "Correct";
            }


            //check if the user has entered a password
            if (string.IsNullOrEmpty(PasswordBox.Password.Trim()))
            {
                // display an error message to the user, stating that they have not entered a password
                MessageBox.Show("Please Enter A Valid Password.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Error);
                // set variable to Incorrect since no password has been entered
                ValidatePassword = "Incorrect";
            }

            else
            {
                // set variable to correct since a password has been entered
                ValidatePassword = "Correct";
            }


            // --> if both fields have been entered by the user, the program will check to see if
            // the username exists in the database. 
            // --> if the username does not exist, it will display an error.
            // --> if the username exists, it convert the password to a hash and check it against the
            // the hashed password stored in the database.
            // --> if the hashed password is found then the user will be logged into the program.
            // --> Lastly, if the hashed passwords do not match then the user be notified with an 
            // incorrect password error. 

            if (ValidateUsername == "Correct" && ValidatePassword == "Correct") // both fields have been entered
            {
                using (var context = new LoginContext()) // set the correct context that points to the Logins table
                {
                    var stud = logContext.User.Find(UsernameBox.Text.Trim()); // searches for the username in table

                    if (stud == null) // Username is not found in table
                    {
                        // Notifies user that the username does not exists in table
                        MessageBox.Show("Username Does Not Exist.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                    }

                    else if (stud != null) // Username is found in table
                    {
                        // it will now check if the password that is entered by the user matches the hashed password in the table
                        if (stud.Password == await Encrypt(PasswordBox.Password.Trim())) //calls method to hash the password that user has entered
                        {
                            // if the hashed password matches with the assiociated username, it will allow the user
                            // to login
                            MessageBox.Show("Welcome Back, " + UsernameBox.Text.Trim() + "!\n" +
                                "We've Missed You!", "Login Successful!", MessageBoxButton.OK, MessageBoxImage.Information);
                            // saves current username to the variable Username found under the properties part of the program
                            Properties.Settings.Default.Username = UsernameBox.Text.Trim();
                            Properties.Settings.Default.Save();
                            Properties.Settings.Default.Upgrade();
                          
                            // shows the MainWindow window
                            MW.Show();

                            // closes current window
                            this.Close();

                        }

                        else // if the passwords do not match
                        {
                            // Notifies user that they have entered the incorrect password
                            MessageBox.Show("You Have Entered The Incorrect Password. Please Try Again.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                        }
                    }

                }

            }

        }

        public async Task <string> Encrypt(string value)
        {
            // This task is used to Hash the password that the user enters
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding uTF8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(uTF8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }

        private async void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            // string variables that are used to validate if the user has entered the
            // necessary fields
            string ValidateUsername = "";
            string ValidatePassword = "";

            // check if the user has entered an username
            if (string.IsNullOrEmpty(UsernameBox.Text.Trim()))
            {
                // display an error message if the user has not entered an username
                //MessageBox.Show("Please Enter A Valid Username.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                // set ValidateUsername variable to Incorrect since no username has been entered
                ValidateUsername = "Incorrect";
            }

            else
            {
                // if the user has entered a valid username, save the ValidateUsername field to Correct
                ValidateUsername = "Correct";
            }


            //check if the user has entered a password
            if (string.IsNullOrEmpty(PasswordBox.Password.Trim()))
            {
                // display an error message if the user has not entered a password
                //MessageBox.Show("Please Enter A Valid Password.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                // set ValidatePassword variable to Incorrect since no password has been entered
                ValidatePassword = "Incorrect";
            }

            else
            {
                // set ValidatePassword variable to Correct since a password has been entered
                ValidatePassword = "Correct";
            }



            // Check to see if the user has entered both a username and password
            // and that their password meets the standard
            if (ValidateUsername == "Correct" && ValidatePassword == "Correct" && await ValidatePasswordMethod(PasswordBox.Password) == true)
            {
                //Check if username already exists in database
                Login user = new Login(); // object of Login class that contains variables found in database table
                using (var context = new LoginContext()) // set the correct context that points to the Logins table
                {
                    var stud = logContext.User.Find(UsernameBox.Text.Trim()); // searches for username in table

                    if (stud == null) // Username is not found in table
                    {
                        // saves username and password to table
                        user.Username = UsernameBox.Text.Trim();
                        user.Password = await Encrypt(PasswordBox.Password.Trim()); // Hashes password before saving it to table
                        logContext.User.Add(user); // adds data
                        int a = logContext.SaveChanges(); // returns 1 if data was saved successfully
                        if (a > 0)
                        {
                            // Notifies user if their user info has been saved
                            MessageBox.Show("You May Now Use These Credentials To Login.", "Account Registration Successful!", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                        else
                        {
                            // Notifies user if their user info has not been saved
                            MessageBox.Show("User Information Could Not Be Saved.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }  
                    }


                    else if (stud != null) // Username is found in table
                    {
                        // Notifies user that the username that they have entered, is already in use
                        MessageBox.Show("This Username Already Exists. Please Try Again.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                

            }

            // Password does not meet the required password standard
            else if (ValidateUsername == "Correct" && ValidatePassword == "Correct" && await ValidatePasswordMethod(PasswordBox.Password) == false)
            {
                // Notify user that the entered password does not meet the standard
                MessageBox.Show("Please Ensure That Your Password Meets The Password Standard.\n" +
                    "The Password Should Be Between 8-14 Characters In Length. It Should Contain At Least One Uppercase Letter And At Least One Lowercase Letter.\n" +
                    "Lastly, It Must Contain At Least One Digit And One Special Character.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else /*if (ValidateUsername == "Incorrect" && ValidatePassword == "Incorrect")*/
            {
                //Notify user that they have not entered both fields
                MessageBox.Show("Please Enter Both A Username And Password.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }


        public async Task <bool> ValidatePasswordMethod(string password)
        {

            // --> This task is used in the registration feature
            // --> It checks if the password that is entered by the user, meets the password standard
            
            // For a password to meet the password standard, it needs to:
            // --> Be between 8-14 characters in length
            // --> Have at least one uppercase and lowercase letter
            // --> Contain no spaces between characters
            // --> Have at least one special character
            
            // bool variables used validate password
            bool passwordLength;
            bool oneUpperCase;
            bool oneLowerCase;
            bool noWhiteSpace;
            bool passwordValidation;

            // checks the length of the password
            if (password.Length < 8 || password.Length > 14)
            {
                passwordLength = false; // password does not fall in range of standard length
            }

            else
            {
                passwordLength = true; // password falls in range of standard length
            }

            // checks if the password contains at least one uppercase letter
            if (!password.Any(Char.IsUpper))
            {
                oneUpperCase = false; // password has no uppercase letters
            }

            else
            {
                oneUpperCase = true; // password has at least one uppercase letter
            }

            // checks if the password contains at least one lowercase letter
            if (!password.Any(Char.IsLower))
            {
                oneLowerCase = false; // password has no lowercase letters 
            }

            else
            {
                oneLowerCase = true; // password has at least one lowercase letter
            }

            // checks if the password has any spaces between characters
            if (password.Contains(" "))
            {
                noWhiteSpace = false; // password contains one or more spaces between characters  
            }

            else
            {
                noWhiteSpace = true; // password contains no spaces
            }

            // Checks if password has at least one special character
            var specialCharacters = new[] { "*", "_", "@", "!", "#" }; // array of acceptable special characters
            if (specialCharacters.Any(password.Contains) == true)
            {
                hasSpecialCharacter = true; // password has one or more special characters
               
            }
            else
            {
                hasSpecialCharacter = false; // password has no special characters
                
            }

            if (passwordLength == true &&
                oneUpperCase == true &&
                oneLowerCase == true &&
                noWhiteSpace == true &&
                hasSpecialCharacter == true) // all conditions have been met
            {
                passwordValidation = true; // password meets standard
               
            }

            else
            {
                passwordValidation = false; // password does not meet standard
               
            }

            return passwordValidation; // returns boolean
        }

        
    }
}
