using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CyclingWorkoutRobot.Extensions;
using static CyclingWorkoutRobot.Models;

namespace CyclingWorkoutRobot
{
    public partial class Form1 : Form
    {
        #region parameters

        IWebDriver driver;

        public static string userId = "user1";

        int bmpFinalWidth = 270;

        DataTable dtWorkout = new DataTable();

        public static LanguageOption defaultLanguage =
            Common.LanguageOptionStringToEnum(System.Configuration.ConfigurationManager.AppSettings["DefaultLanguage"]);

        public int WorkoutNo = 1;

        public static bool pageLoadING = false;

        public static string outputText = "";

        public static List<string> chromeArguments = new List<string>();

        public string exampleWorkoutFileBase64 = "cG93ZXIgcGVyY2VudGFnZSBsb3dlciBib3VuZCxwb3dlciBwZXJjZW50YWdlIHVwcGVyIGJvdW5kLHRpbWUoc2Vjb25kKQ0KNDAsNDAsMzAwDQo5MCw5MCwzNjANCjQwLDQwLDMwMA0KOTAsOTAsMzYwDQo0MCw0MCwzMDANCjkwLDkwLDM2MA0KNDAsNDAsMzAwDQo=";


        #endregion

        #region Form events

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pageLoadING = true;

            bwUpload.WorkerReportsProgress = true;
            bwUpload.WorkerSupportsCancellation = true;

            timerOutput.Start();

            KillChromeDriver("chrome");

            lblMsg.Text = "";

            txtFTP.LostFocus += txtFTP_LostFocus;

            CreateDtColumns();
            CreateGridColumns();

            gridWorkout.RowTemplate.Height = 100;
            gridWorkout.DataSource = dtWorkout;


            //combobox:set preferable language            
            foreach (LanguageOption val in Enum.GetValues(typeof(LanguageOption)))
            {
                cboLanguage.Items.Add(val.ToString());
            }

            LoadFTPFromDB();
            LoadDefaultLanguageFromDB();
            LoadHideBrowserOptionFromDB();
            LoadRememberIdPwdOptionFromDB();
            LoadIdPwdFromDB();

            //chkHideBrowser.Visible = false;
            chkShowAdv_CheckedChanged(null, null);
            chkHideBrowser_CheckedChanged(null, null);

            btnOpenFile.Select();
            pageLoadING = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            KillChromeDriver("chrome");
          
        }


        #endregion



        #region Events

        private void txtFTP_TextChanged(object sender, EventArgs e)
        {
            if (chkRememberFTP.Checked == true)
            {
                if (Common.IsNumeric(txtFTP.Text) == true)
                {
                    //update ftp setting in db
                    dbRepo.InserUpdateUserFTP(userId, txtFTP.Text);                    
                }



            }
        }

        private void chkRememberFTP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRememberFTP.Checked == true)
            {
                if (Common.IsNumeric(txtFTP.Text) == true)
                {
                    //InserUpdateUserFTP(userId);
                    dbRepo.InserUpdateUserFTP(userId, txtFTP.Text);
                    string msg = "FTP saved!".ToDefaultLanguage();
                    MessageBox.Show(msg);
                }

            }
            else
            {
                dbRepo.DeleteUserFTP(userId);
            }



        }

        //language setup change event
        private void cboLanguage_SelectedValueChanged(object sender, EventArgs e)
        {
            string userSelectLanguage = cboLanguage.SelectedItem.ToString();
            dbRepo.InserUpdateDefaultLanguage(userSelectLanguage);
            defaultLanguage = Common.LanguageOptionStringToEnum(userSelectLanguage);

            //SetMultiLanguage();
            LoadDefaultLanguageFromDB();
            if (pageLoadING == false)
            {               
                txtOutput.Text = "Language changed, please reopen app!".ToDefaultLanguage();
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {

            if (bwUpload.IsBusy == true)
            {
                MessageBox.Show("Robot is still busy, Please wait.".ToDefaultLanguage());

                return;
            }

            OpenFileDialog dialog = new OpenFileDialog();
            BindingSource source = new BindingSource();

            if (txtFTP.Text == "")
            {
                MessageBox.Show("Please set your FTP !".ToDefaultLanguage());
                txtFTP.Focus();
                return;
            }

            dialog.Multiselect = true;
            dialog.Title = "Select csv files".ToDefaultLanguage();
            dialog.InitialDirectory = ".\\";
            dialog.Filter = "csv files (*.csv)|*.csv".ToDefaultLanguage();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = "";
                dtWorkout.Rows.Clear();
                List<WorkoutData> dataList = new List<WorkoutData>();
                WorkoutNo = 0;
                foreach (var fileName in dialog.FileNames)
                {
                    txtFileName.Text = txtFileName.Text + fileName + Environment.NewLine;
                    InsertWorkoutDataToDataTable(fileName);
                }

                gridWorkout.DataSource = dtWorkout;

            }

        }

        private async void btnUpload_Click(object sender, EventArgs e)
        {
            txtOutput.Text = "";
            if (bwUpload.IsBusy == true)
            {
                MessageBox.Show("Robot is still busy, Please wait.".ToDefaultLanguage());

                return;
            }
            bool workoutNeedToBeUploaded = false;//
            for (int i = 0; i < gridWorkout.Rows.Count - 1; i++)
            {
                DataGridViewRow row = gridWorkout.Rows[i];
                string rowUploadFinished = (string)row.Cells[uploadFinished.ToDefaultLanguage()].Value;
                if (rowUploadFinished == "Yes".ToDefaultLanguage())
                {
                    //workout uploaded
                }
                else
                {
                    //workout not uploaded
                    workoutNeedToBeUploaded = true;
                }

            }
            if (workoutNeedToBeUploaded == false)
            {
                MessageBox.Show("All workouts are uploaded!".ToDefaultLanguage());
                return;
            }

            if (bwUpload.IsBusy == false)
            {
                bwUpload.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Robot is still busy, Please wait.".ToDefaultLanguage());
                return;
            }
           
        }


        private void chkShowAdv_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowAdv.Checked == true)
            {
                chkHideBrowser.Visible = true;
                chkRememberIdPwd.Visible = true;
                txtLoginId.Visible = true;
                txtPassword.Visible = true;
                lblIdPwd.Visible = true;
                lblSlash.Visible = true;
                btnSaveIdPwd.Visible = true;

            }
            else
            {
                chkHideBrowser.Visible = false;
                chkRememberIdPwd.Visible = false;
                txtLoginId.Visible = false;
                txtPassword.Visible = false;
                lblIdPwd.Visible = false;
                lblSlash.Visible = false;
                btnSaveIdPwd.Visible = false;
            }
        }

        private void chkHideBrowser_CheckedChanged(object sender, EventArgs e)
        {
            if (pageLoadING == false)
            {
                if (chkHideBrowser.Checked == true)
                {
                    if (chkRememberIdPwd.Checked == true)
                    {
                        chkRememberIdPwd.Checked = false;
                    }


                   

                    MessageBox.Show("Hide web browser option will login automatically.".ToDefaultLanguage()
                   + Environment.NewLine
                   + "Login id and password for garmin are needed to save in your local PC.".ToDefaultLanguage());


                }
                else
                {                   

                    //if unchecking (hideBrowser checkbox) and (only remember id/pwd checkbox)
                    //at the same time, remove id/pwd from local db
                    if(chkHideBrowser.Checked == false && chkRememberIdPwd.Checked == false)
                    {
                        dbRepo.DeleteUserIdPwdFromDb();
                        txtLoginId.Text = "";
                        txtPassword.Text = "";
                    }

                }

                dbRepo.InserUpdateHideBrowserOption(chkHideBrowser.Checked);
            }

            return;


        }

        private void txtFTP_LostFocus(object sender, EventArgs e)
        {
            if (chkRememberFTP.Checked == true)
            {
                string msg = "FTP saved!".ToDefaultLanguage();
                MessageBox.Show(msg);
            }

        }

        private void btnSaveIdPwd_Click(object sender, EventArgs e)
        {

            //string codeValue = "";
            if (txtLoginId.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {

                //InsertUserLoginIdPwdToDB();
                dbRepo.InsertUserLoginIdPwdToDB(txtLoginId.Text.Trim(), txtPassword.Text.Trim());
                MessageBox.Show("Save Success!".ToDefaultLanguage());
            }
            else
            {
                MessageBox.Show("Id/Password are required!".ToDefaultLanguage());
            }
        }

        private void chkRememberIdPwd_CheckedChanged(object sender, EventArgs e)
        {
            if (pageLoadING == false)
            {
                if (chkRememberIdPwd.Checked == true)
                {
                    if (chkHideBrowser.Checked == true)
                    {
                        chkHideBrowser.Checked = false;
                    }
                }
                else
                {
                    //if unchecking (hideBrowser checkbox) and (only remember id/pwd checkbox)
                    //at the same time, remove id/pwd from local db
                    if (chkHideBrowser.Checked == false && chkRememberIdPwd.Checked == false)
                    {
                        dbRepo.DeleteUserIdPwdFromDb();
                        txtLoginId.Text = "";
                        txtPassword.Text = "";
                    }

                }
                dbRepo.InserRememberIdPwdOption(chkRememberIdPwd.Checked);
            }
        }

        private void timerOutput_Tick(object sender, EventArgs e)
        {
            if (txtOutput.Text.Length > 1000)
            {
                txtOutput.Text = "";

            }
            if (outputText != "")
            {
                if (txtOutput.Text == "")
                {
                    txtOutput.AppendText(outputText);
                }
                else
                {
                    txtOutput.AppendText(Environment.NewLine + outputText);
                }

                
                outputText = "";
            }

            //Application.DoEvents();
        }

        private void btnExampleWorkoutFileDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            // set a default file name
            savefile.FileName = "example_workout.csv";
            // set filters - this can be done in properties as well
            savefile.Filter = "Csv files (*.csv)|*.csv|All files (*.*)|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                Byte[] bytes = Convert.FromBase64String(exampleWorkoutFileBase64);
                File.WriteAllBytes(savefile.FileName, bytes);
            }          
        }

        #endregion

        #region functions : background worker 

        private void bwUpload_DoWork(object sender, DoWorkEventArgs e)
        {

            string regPattern = @"" ;
            AppendOutput("Robot starts working.");
            AppendOutput("Checking chromedriver exists or not.");
            if (Process.GetProcessesByName("chromedriver").Count() == 0)
            {
                AppendOutput("chromedriver not exist.");
                ChromeOptions chromeBrowserOptions = new ChromeOptions();
                //var driverService = ChromeDriverService.CreateDefaultService(@"ChromeDriver");
                var driverService = ChromeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;
                chromeBrowserOptions.AddArguments(new List<string>() { "no-sandbox", "disable-gpu" });//save pc resource
                chromeArguments.AddRange(new List<string>() { "no-sandbox", "disable-gpu" });
                if (chkHideBrowser.Checked == true && txtLoginId.Text != "" && txtPassword.Text != "")
                {
                    chromeBrowserOptions.AddArgument("headless");
                    chromeArguments.Add("headless");
                }
              
                driver = new ChromeDriver(driverService, chromeBrowserOptions);                
                LoadCookieFromFile();
                AppendOutput("Checking login status.");
                regPattern = @"main-nav-group dashboards";
                bool isLoginSuccess = WaitForSomething(regPattern, 5);
                
                if(isLoginSuccess == false)
                {
                    AppendOutput("Preparing to login.");
                    isLoginSuccess = Login();
                }
               
                if (isLoginSuccess == true)
                {
                    AppendOutput("Login finished.");
                    SaveNewCookie();
                }
                else
                {
                    AppendOutput("Login failed, try again later.");
                    bwUpload.CancelAsync();
                    e.Cancel = true;
                    return;

                }

            }
            else
            {
                bool isLoginSuccess = false;
                AppendOutput("chromedriver exists, check login status.");
                try
                {
                    driver.Navigate().GoToUrl("https://connect.garmin.com/");
                    AppendOutput("Checking login status.");
                    regPattern = @"main-nav-group dashboards";

                    isLoginSuccess = WaitForSomething(regPattern, 5);
                }
                catch
                {
                    AppendOutput("Error occur to web browser. Re-launch browser.");
                    isLoginSuccess = false;
                    KillChromeDriver("chrome");
                    
                    ChromeOptions chromeBrowserOptions = new ChromeOptions();
                    //var driverService = ChromeDriverService.CreateDefaultService(@"ChromeDriver");
                    var driverService = ChromeDriverService.CreateDefaultService();
                    driverService.HideCommandPromptWindow = true;
                    chromeBrowserOptions.AddArguments(chromeArguments);//save pc resource

                    driver = new ChromeDriver(driverService, chromeBrowserOptions);                   
                    LoadCookieFromFile();
                    isLoginSuccess = WaitForSomething(regPattern, 5);
                }
                
                if (isLoginSuccess == true)
                {
                    //do nothing
                    SaveNewCookie();
                }
                else
                {
                    bool loginAgainSuccess = Login();
                    if (loginAgainSuccess == false)
                    {
                        AppendOutput("Login fail, try again later.");
                        bwUpload.CancelAsync();
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        SaveNewCookie();
                    }
                }
            }

            //upload all workouts
            AppendOutput("Uploading all workouts.");
            for (int i = 0; i < gridWorkout.Rows.Count - 1; i++)
            {
                DataGridViewRow row = gridWorkout.Rows[i];

                int rowNo = (int)row.Cells[no].Value;
                bool rowChosenToUpload = (bool)row.Cells["Upload".ToDefaultLanguage()].Value;
                string rowWorkoutName = (string)row.Cells[workoutName.ToDefaultLanguage()].Value;
                string rowWorkoutLength = (string)row.Cells[workoutLength.ToDefaultLanguage()].Value;
                string rowFullFilePath = (string)row.Cells[fullFilePath].Value;
                string rowUploadFinished = (string)row.Cells[uploadFinished.ToDefaultLanguage()].Value;
                string rowResult;
               
                if (rowUploadFinished == "Yes".ToDefaultLanguage())
                {
                    continue;
                }

                AppendOutput("Workout:" + rowWorkoutName + " is processing.");
                bool createWorkoutSuccess = CreateWorkout(rowFullFilePath);
                if (createWorkoutSuccess == true)
                {
                    //do nothing
                }
                else
                {
                    AppendOutput("Workout:" + rowWorkoutName + " upload fail, try again later.");
                    bwUpload.CancelAsync();
                    e.Cancel = true;
                    return;
                }
                row.Cells[uploadFinished.ToDefaultLanguage()].Value = "Yes".ToDefaultLanguage();
                AppendOutput("Workout:" + rowWorkoutName + " upload success.");
            }
            AppendOutput("upload all workouts finished!");
            //MessageBox.Show("finish!");

            //return;
        }

        #endregion

        #region selenium functions

        private bool Login()
        {
            
            //bool loginSuccess = true;   
            string regPattern = @"";
            string msg = @"";
            driver.Navigate().GoToUrl("https://connect.garmin.com/");
            
            //prevent RWD to hide element

            driver.Manage().Window.Maximize();
            AppendOutput("Maximize window to prevent RWD to hide element.");
            //agree cookies
            regPattern = @"truste-consent-button";
            WaitForSomething(regPattern);
            try
            {
                IWebElement agreeBtn = driver.FindElement(By.CssSelector("button[id='truste-consent-button']"));
                agreeBtn.Click();
                AppendOutput("Click truste-consent-button");
            }
            catch (Exception ex)
            {
                AppendOutput("Error occur while clicking agree cookies button");
            }
           

            //check if login success status
            //if dashboard exists, then you are loginned
            AppendOutput("Checking login status.");
            regPattern = @"main-nav-group dashboards";
            bool isLoginSuccess = false;
            isLoginSuccess = WaitForSomething(regPattern, 5);
            if (isLoginSuccess == true)
            {
                return true;//you are loginned, following login processes are not needed.
            }

            //press login button
            AppendOutput("Sign in garmin connect.");
            regPattern = @"signin";
            msg = @"SignIn button not visible on garmin connect webpage, please upload later.";
            if (WaitForSomething(regPattern) == false)
            {
                //MessageBox.Show(msg.ToDefaultLanguage());
                AppendOutput(msg.ToDefaultLanguage());
                return false;
            }

            try
            {
                //sometimes although the upper right text on screen tells you are loginned
                //the page still show id/pwd column to input
                ((IJavaScriptExecutor)driver).ExecuteScript("document.querySelector('a[href*=signin]').click()");

            }
            catch
            {

            }

            regPattern = @"https://sso.garmin.com/sso/signin";
            WaitForSomething(regPattern);

            driver.SwitchTo().Frame(0);
            AppendOutput("Input username / password");
            regPattern = @"username";
            WaitForSomething(regPattern);
            SpinWait.SpinUntil(() => false, 1000);//login button need extra 1 second to compleletely show itself on the screen

            AppendOutput("Check if auto login");
            string idPwd = GetIdPwdFromDB();
            bool isHide = dbRepo.GetHideBrowserOption();
            bool isRemember = dbRepo.GetRememberIdPwdOption();
            bool userMaullyLogin = false;   
            if (idPwd != "" && (isHide == true || isRemember == true))
            {
                AppendOutput("Auto login confirmed.");
                //auto login
                string id = idPwd.Split(',')[0];
                string pwd = idPwd.Split(',')[1];

                ((IJavaScriptExecutor)driver).ExecuteScript(@"document.getElementById(""username"").value = """ + id + "\"");
                ((IJavaScriptExecutor)driver).ExecuteScript(@"document.getElementById(""password"").value = """ + pwd + "\"");
                IWebElement submit = driver.FindElement(By.CssSelector("button[type='submit']"));
                submit.Click();

            }
            else
            {
                //user manually login
                AppendOutput("Use your browser to login please.");
                userMaullyLogin = true;
                //MessageBox.Show("Use your browser to login please.");
            }

            AppendOutput("Check if any login error message.");
            regPattern = @"class=""error"">";
            WaitForSomething(regPattern, 5);
            try
            {
                IWebElement errDiv = driver.FindElement(By.CssSelector("div.error"));
                if (errDiv != null)
                {
                    AppendOutput(errDiv.Text);
                    return false;
                }
            }
            catch(Exception ex)
            {
                
            }
            

            //wait for dashboard page load complete  
            AppendOutput("Wait for dashboard.");
            regPattern = @"main-nav-group dashboards";
            Int32 userWaitTime;
            if(userMaullyLogin == true)
            {
                userWaitTime = 86400;
            }
            else
            {
                userWaitTime = 5;
            }
            if (WaitForSomething(regPattern, userWaitTime) == false)
            {
                AppendOutput("Error loading dashboards page.");
                return false;
            }
            //wait for workouts href show up
            regPattern = @"modern/workouts";
            WaitForSomething(regPattern);
            AppendOutput("Login Success.");

            return true;
        }

        private bool WaitForSomething(string regPattern, int retryCount = 20, int retryInterval = 1000, bool ifThrowEx = false)
        {
            try
            {
                for (int i = 0; i < retryCount; i++)
                {
                    Match m = Regex.Match(driver.PageSource, regPattern, RegexOptions.Singleline);

                    if (m.Success == false)
                    {
                        SpinWait.SpinUntil(() => false, retryInterval);

                    }
                    else
                    {
                        return true;
                    }
                }
                if (ifThrowEx == true)
                {
                    throw new Exception("this element must exists. so we have to wait");
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void WaitAndShowMsg(string regPattern, string msg)
        {
            if (WaitForSomething(regPattern, 20) == false)
            {
                MessageBox.Show(msg);
                return;
            }
        }

        private bool CreateWorkout(string fullFileName)
        {
            string regPattern;
            try
            {
                //string regPattern = @"";
                AppendOutput("Click create workout.");
                ((IJavaScriptExecutor)driver).ExecuteScript(@"document.querySelector(""a[href = '/modern/workouts']"").click()");
                //wait for select workout dropdownlist to show up and select cycling option
                AppendOutput("Select cycling workout.");
                regPattern = @"select-workout";

                if (WaitForSomething(regPattern, 5) == false)
                {
                    AppendOutput("Select cycling workout fail.");
                    return false;
                }
                IWebElement iSelectWorkout = driver.FindElement(By.CssSelector("select[name*=select-workout]"));
                SelectElement selectWorkout = new SelectElement(iSelectWorkout);
                selectWorkout.SelectByValue("cycling");
                //click create workout button
                ((IJavaScriptExecutor)driver).ExecuteScript(@"document.querySelector(""button.create-workout"").click()");
                //delete all default steps
                regPattern = @"step-delete";
                WaitForSomething(regPattern);

                AppendOutput("Delete workout default steps.");
                ((IJavaScriptExecutor)driver).ExecuteScript(@"document.querySelector(""div.step-delete a"").click()");
                ((IJavaScriptExecutor)driver).ExecuteScript(@"document.querySelector(""div.step-delete a"").click()");
                ((IJavaScriptExecutor)driver).ExecuteScript(@"document.querySelector(""div.step-delete a"").click()");

                //Start to create your custom workout:
                //go to workout page
                List<int> powerList = new List<int>();
                List<int> durationList = new List<int>();
                string[] lines = ReadLines(fullFileName).ToArray();                
                for (int i = 1; i < lines.Count(); i++)
                {
                    //txtOutput.Text = txtOutput.Text + lines[i] + Environment.NewLine;
                    AppendOutput(lines[i] + " is processing.");
                    int power_percentage_lower_bound = Convert.ToInt16(lines[i].Split(',')[0]);
                    int power_percentage_upper_bound = Convert.ToInt16(lines[i].Split(',')[1]);
                    int power_percentage_average = (power_percentage_lower_bound + power_percentage_upper_bound) / 2;
                    int durationTimeInSecond = Convert.ToInt16(lines[i].Split(',')[2]);

                    powerList.Add(power_percentage_average);
                    durationList.Add(durationTimeInSecond);

                    //upper bound watt will increase ? % of your FTP
                    //lower bound watt will decrease ? % of your FTP
                    //target watt will be within the range of set
                    if (power_percentage_lower_bound == power_percentage_upper_bound)
                    {
                        power_percentage_upper_bound = power_percentage_upper_bound + 5;

                        power_percentage_lower_bound = power_percentage_lower_bound - 5;
                    }


                    //press new step
                    regPattern = @"new-step";
                    WaitForSomething(regPattern);
                    ((IJavaScriptExecutor)driver).ExecuteScript(@"document.querySelector(""#new-step"").click()");
                    AppendOutput("Click next step.");

                    //select last duration dropdownlist

                    var allSelectDuration = driver.FindElements(By.CssSelector("select[name=duration]"));
                    IWebElement iLastSelectDuration = allSelectDuration[allSelectDuration.Count() - 1];
                    SelectElement lastSelectDuration = new SelectElement(iLastSelectDuration);
                    lastSelectDuration.SelectByValue("time");
                    AppendOutput("Select duration:time.");

                    //set duration time in the last duration time textbox                                
                    int minute = durationTimeInSecond / 60;
                    int second = durationTimeInSecond % 60;
                    var allInputDurationTime = driver.FindElements(By.CssSelector("input[name=duration-time]"));
                    IWebElement iLastInputDuration = allInputDurationTime[allInputDurationTime.Count() - 1];
                    iLastInputDuration.SendKeys(minute + ":" + second.ToString("00"));
                    AppendOutput("Set duration Time:" + minute + ":" + second.ToString("00"));

                    //click last add-step-target href
                    var clickHrefSuccess = false;
                    int clickAddStepCount = 0;
                    do
                    {
                        if (clickAddStepCount > 10)
                        {
                            AppendOutput("Click add-step button fail more than 10 times, try again later.");
                            return false;
                        }
                        try
                        {
                            AppendOutput("Try to click add-step-target.");
                            var allHrefAddStepTarget = driver.FindElements(By.CssSelector("a[data-target=add-step-target]"));
                            IWebElement iLastHrefAddStepTarget = allHrefAddStepTarget[allHrefAddStepTarget.Count() - 1];
                            WaitForSomething("add-step-target");
                            iLastHrefAddStepTarget.Click();
                            WaitForSomething(@"select-step-target", 3, 1000, true);
                            //select last  select-step-target ddl and set value to custom power                
                            var allSelectCustomPower = driver.FindElements(By.CssSelector("select[name=select-step-target]"));
                            IWebElement iLastSelectCustomPower = allSelectCustomPower[allSelectCustomPower.Count() - 1];
                            SelectElement lastSelectCustomPower = new SelectElement(iLastSelectCustomPower);
                            lastSelectCustomPower.SelectByIndex(lastSelectCustomPower.Options.Count - 1);
                            clickHrefSuccess = true;
                            AppendOutput("Click add-step-target success.");
                        }
                        catch (Exception ex)
                        {
                            clickAddStepCount++;
                            AppendOutput("Click add-step-target fail!!");
                            Console.WriteLine(ex.ToString());
                            SpinWait.SpinUntil(() => false, 1000);
                        }
                    } while (clickHrefSuccess == false);


                    IWebElement iFooterLink = driver.FindElement(By.CssSelector("a[target*=footer_link]"));
                    OpenQA.Selenium.Interactions.Actions scrollAction = new OpenQA.Selenium.Interactions.Actions(driver);
                    scrollAction.MoveToElement(iFooterLink);
                    scrollAction.Perform();



                    //set lowerbound of this workout step        
                    int ftp = Convert.ToInt16(txtFTP.Text);
                    int lowerBoundWatt = (int)(ftp * power_percentage_lower_bound / 100);
                    var allInputTargetPowerLowerBound = driver.FindElements(By.CssSelector("input[name=target-power-zone-custom-from]"));
                    IWebElement iLastInputTargetPowerLowerBound = allInputTargetPowerLowerBound[allInputTargetPowerLowerBound.Count() - 1];
                    iLastInputTargetPowerLowerBound.SendKeys(lowerBoundWatt.ToString());
                    AppendOutput("Set lower bound of target power = " + lowerBoundWatt.ToString());

                    //set upperbound of this workout step        
                    int upperBoundWatt = (int)(ftp * power_percentage_upper_bound / 100);
                    var allInputTargetPowerUpperBound = driver.FindElements(By.CssSelector("input[name=target-power-zone-custom-to]"));
                    IWebElement iLastInputTargetPowerUpperBound = allInputTargetPowerUpperBound[allInputTargetPowerUpperBound.Count() - 1];
                    iLastInputTargetPowerUpperBound.SendKeys(upperBoundWatt.ToString());
                    AppendOutput("Set upper bound of target power = " + upperBoundWatt.ToString());
                }

                //change workout name based on csv file name                       
                //step1:press pencil icon
                ((IJavaScriptExecutor)driver).ExecuteScript(@"document.querySelector('button.inline-edit-trigger.has-tooltip').click()");
                //step2:send new workout name to textbox
                ((IJavaScriptExecutor)driver).ExecuteScript(@"document.querySelector('div[class=inline-edit-editable-text]').innerHTML =
                '" + Path.GetFileNameWithoutExtension(fullFileName) + "'");
                //step3: press enter to finish edit of workout name
                driver.FindElement(By.CssSelector("div[class=inline-edit-editable-text]")).SendKeys(OpenQA.Selenium.Keys.Return);


                //save workout
                ((IJavaScriptExecutor)driver).ExecuteScript(@"document.querySelector(""#save-workout"").click()");
                AppendOutput("Set workout name:" + Path.GetFileNameWithoutExtension(fullFileName));

                return true;
            }
            catch (Exception ex)
            {
                AppendOutput("Error occur when createing workout:" + ex.Message);
                return false;
            }


        }

        private void AppendOutput(string output)
        {
            if (outputText == "")
            {
                outputText = output;

            }
            else
            {
                outputText = outputText + Environment.NewLine + output;

            }


        }

        #endregion

        #region custom functions

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        //columns for datatable and datagridview
        private static string no = "No";
        private static string chosenToUpload = "ChosenToUpload";
        private string workoutName = "WorkoutName";
        private string workoutLength = "WorkoutLength";
        private string workoutCurve = "WorkoutCurve";
        private string fullFilePath = "FullFilePath";
        private string uploadFinished = "UploadFinished";


        private void CreateDtColumns()
        {
           
            dtWorkout.Columns.Add(no, typeof(int));
            dtWorkout.Columns.Add(chosenToUpload, typeof(bool));
            dtWorkout.Columns.Add(workoutName, typeof(string));
            dtWorkout.Columns.Add(workoutLength, typeof(string));
            dtWorkout.Columns.Add(workoutCurve, typeof(byte[]));
            dtWorkout.Columns.Add(fullFilePath, typeof(string));
            dtWorkout.Columns.Add(uploadFinished, typeof(string));
        }

        private void CreateGridColumns()
        {
            if (gridWorkout.Columns.Count == 0)
            {
                gridWorkout.AutoGenerateColumns = false;
                gridWorkout.AutoSize = false;

                //string no = "No";
                gridWorkout.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = no,
                    Name = no,
                    Width = 50,
                });
                this.gridWorkout.Columns[no].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;

                gridWorkout.Columns.Add(new DataGridViewCheckBoxColumn()
                {
                    DataPropertyName = chosenToUpload,
                    Name = "Upload".ToDefaultLanguage(),
                    Width = 50,
                });

                //string workoutName = "WorkoutName";
                gridWorkout.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = workoutName,
                    Name = workoutName.ToDefaultLanguage(),
                });

                //string workoutLength = "WorkoutLength";
                gridWorkout.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = workoutLength,
                    Name = workoutLength.ToDefaultLanguage(),

                });
                this.gridWorkout.Columns[workoutLength.ToDefaultLanguage()].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleCenter;

                //string workoutCurve = "WorkoutCurve";
                gridWorkout.Columns.Add(new DataGridViewImageColumn()
                {
                    DataPropertyName = workoutCurve,
                    Name = workoutCurve.ToDefaultLanguage() +
                    "（GreenLine=FTP）".ToDefaultLanguage(),
                    Width = 300,
                    ImageLayout = DataGridViewImageCellLayout.Normal,
                });

                //string fullFilePath = "FullFilePath";
                gridWorkout.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = fullFilePath,
                    Name = fullFilePath,


                });
                this.gridWorkout.Columns[fullFilePath].Visible = false;

                //string uploadFinished = "UploadFinished";
                gridWorkout.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    DataPropertyName = uploadFinished,
                    Name = uploadFinished.ToDefaultLanguage(),
                    Width = 140,

                });
                this.gridWorkout.Columns[uploadFinished.ToDefaultLanguage()].DefaultCellStyle.Alignment =
                          DataGridViewContentAlignment.MiddleCenter;

               

            }
        }

        public byte[] BmpToBytes(Bitmap bmp)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] b = ms.GetBuffer();
            return b;
        }

        private void KillChromeDriver(string ProcessName)
        {
            Process[] ProcList = Process.GetProcessesByName(ProcessName);
            foreach (Process proc in ProcList)
            {
                try
                {
                    ManagementObject managementObject = new ManagementObject(string.Format("win32_process.handle='{0}'", proc.Id));
                    managementObject.Get();
                    int parentId = Convert.ToInt32(managementObject["ParentProcessId"]);
                    try
                    {
                        if (parentId == 0)
                        {
                            continue;
                        }
                        Process parentProc = Process.GetProcessById(parentId);
                        if (parentProc.ProcessName == "chromedriver")
                        {

                            parentProc.Kill();

                            proc.Kill();
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                catch
                {
                    continue;
                }
            }


            foreach (var process in Process.GetProcessesByName("chromedriver"))
            {
                try
                {
                    process.Kill();
                }
                catch
                {
                    SpinWait.SpinUntil(() => false, 5000);
                    return;
                }
            }
        }
       

        #endregion


        #region db functions

        private void LoadDefaultLanguageFromDB()
        {
            //check if user select a default language          
            string defaultLangFromDB = dbRepo.GetDefaultLanguageFromDB();
            if (defaultLangFromDB != "")
            {
                defaultLanguage = Common.LanguageOptionStringToEnum(defaultLangFromDB);
            }
            cboLanguage.SelectedItem = defaultLanguage.ToString();

            SetMultiLanguage();
        }

        private void SetMultiLanguage()
        {
            using (mydbEntities db = new mydbEntities())
            {
              
                //set text of language setup label
                var languageLbl = dbRepo.GetTranslateObj("Language");
                //set text of remember FTP checkbox
                var rememberFTPCheckbox = dbRepo.GetTranslateObj("remember FTP");
                //set text of open file button
                var openCsvFileButton = dbRepo.GetTranslateObj("select workout file");
                //upload button 
                var uploadButton = dbRepo.GetTranslateObj("Upload");
                //checkbox: Show Advanced Options
                var showAdvancedOptionsCheckbox = dbRepo.GetTranslateObj("Show Advanced Options");
                //checkbox: Hide Web Browser and remember id/password
                var hideWebBrowserCheckbox = dbRepo.GetTranslateObj("Hide Web Browser and remember id/password");
                //checkbox: remember id/password only
                var rememberIdPwdCheckbox = dbRepo.GetTranslateObj("Only remember Id/Password");
                //save button
                var saveLoginPwdButton = dbRepo.GetTranslateObj("Save");
                //example wokrout file button
                var exampleWorkoutFileButton = dbRepo.GetTranslateObj("example workout file");               

                if (defaultLanguage == LanguageOption.English)
                {
                    lblLanguage.Text = languageLbl.eng;
                    chkRememberFTP.Text = rememberFTPCheckbox.eng;
                    btnOpenFile.Text = openCsvFileButton.eng;
                    btnUpload.Text = uploadButton.eng;
                    chkShowAdv.Text = showAdvancedOptionsCheckbox.eng;
                    chkHideBrowser.Text = hideWebBrowserCheckbox.eng;
                    btnSaveIdPwd.Text = saveLoginPwdButton.eng;
                    chkRememberIdPwd.Text = rememberIdPwdCheckbox.eng;
                    btnExampleWorkoutFileDownload.Text = exampleWorkoutFileButton.eng;
                }
                else if (defaultLanguage == LanguageOption.日本語)
                {
                    lblLanguage.Text = languageLbl.jpn;
                    chkRememberFTP.Text = rememberFTPCheckbox.jpn;
                    btnOpenFile.Text = openCsvFileButton.jpn;
                    btnUpload.Text = uploadButton.jpn;
                    chkShowAdv.Text = showAdvancedOptionsCheckbox.jpn;
                    chkHideBrowser.Text = hideWebBrowserCheckbox.jpn;
                    btnSaveIdPwd.Text = saveLoginPwdButton.jpn;
                    chkRememberIdPwd.Text = rememberIdPwdCheckbox.jpn;
                    btnExampleWorkoutFileDownload.Text = exampleWorkoutFileButton.jpn;
                }
                else if (defaultLanguage == LanguageOption.简体中文)
                {
                    lblLanguage.Text = languageLbl.chs;
                    chkRememberFTP.Text = rememberFTPCheckbox.chs;
                    btnOpenFile.Text = openCsvFileButton.chs;
                    btnUpload.Text = uploadButton.chs;
                    chkShowAdv.Text = showAdvancedOptionsCheckbox.chs;
                    chkHideBrowser.Text = hideWebBrowserCheckbox.chs;
                    btnSaveIdPwd.Text = saveLoginPwdButton.chs;
                    chkRememberIdPwd.Text = rememberIdPwdCheckbox.chs;
                    btnExampleWorkoutFileDownload.Text = exampleWorkoutFileButton.chs;
                }
                else if (defaultLanguage == LanguageOption.繁體中文)
                {
                    lblLanguage.Text = languageLbl.cht;
                    chkRememberFTP.Text = rememberFTPCheckbox.cht;
                    btnOpenFile.Text = openCsvFileButton.cht;
                    btnUpload.Text = uploadButton.cht;
                    chkShowAdv.Text = showAdvancedOptionsCheckbox.cht;
                    chkHideBrowser.Text = hideWebBrowserCheckbox.cht;
                    btnSaveIdPwd.Text = saveLoginPwdButton.cht;
                    chkRememberIdPwd.Text = rememberIdPwdCheckbox.cht;
                    btnExampleWorkoutFileDownload.Text = exampleWorkoutFileButton.cht;
                }
                lblLanguage.Text = lblLanguage.Text + ":";
               

            }

        }

        private void LoadFTPFromDB()
        {
            int ftp;
            ftp = dbRepo.GetUserFTP(userId);
            if (ftp != 0)
            {
                this.txtFTP.TextChanged -= this.txtFTP_TextChanged;
                txtFTP.Text = ftp.ToString();
                this.txtFTP.TextChanged += this.txtFTP_TextChanged;


                this.chkRememberFTP.CheckedChanged -= this.chkRememberFTP_CheckedChanged;
                chkRememberFTP.Checked = true;
                this.chkRememberFTP.CheckedChanged += this.chkRememberFTP_CheckedChanged;
            }
            else
            {
                this.chkRememberFTP.CheckedChanged -= this.chkRememberFTP_CheckedChanged;
                chkRememberFTP.Checked = false;
                this.chkRememberFTP.CheckedChanged += this.chkRememberFTP_CheckedChanged;
            }
        }

        private void LoadHideBrowserOptionFromDB()
        {
            bool isHide;
            isHide = dbRepo.GetHideBrowserOption();
            if (isHide == true)
            {
                chkHideBrowser.Checked = true;
            }
            else
            {
                chkHideBrowser.Checked = false;
            }
        }

        private void LoadRememberIdPwdOptionFromDB()
        {
            bool isRemember;
            isRemember = dbRepo.GetRememberIdPwdOption();
            if (isRemember == true)
            {
                chkRememberIdPwd.Checked = true;

            }
            else
            {
                chkRememberIdPwd.Checked = false;

            }
        }

        private void LoadIdPwdFromDB()
        {
            string idPwd = GetIdPwdFromDB();
            if (idPwd != "")
            {
                //auto login
                string id = idPwd.Split(',')[0];
                string pwd = idPwd.Split(',')[1];

                txtLoginId.Text = id;
                txtPassword.Text = pwd;

            }
        }

        private void InsertWorkoutDataToDataTable(string fileName)
        {
            //WorkoutData data = new WorkoutData();
            List<int> powerList = new List<int>();
            List<int> durationList = new List<int>();
            int blockWidthFactor;
            int denoOfDuration;
            int bmpOriginalWidth = 400;
            int bmpHeight;

            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();

            bmpHeight = bmpOriginalWidth * 3 / 10;
            denoOfDuration = bmpOriginalWidth * 36 / 40;

            Bitmap originalBmp = new Bitmap(bmpOriginalWidth, bmpHeight);
            Graphics originalBmpGraphics = Graphics.FromImage(originalBmp);
            int localBaseX = 0;
            int localBaseY = 125;//good for width:400px

            string[] lines;
            lines = ReadLines(fileName).ToArray();            

            //get total duration time to calculate blockWidthFactor
            int totalDurationTimeInSecond = 0;
            for (int i = 1; i < lines.Count(); i++)
            {
                int durationTimeInSecond = Convert.ToInt16(lines[i].Split(',')[2]);
                totalDurationTimeInSecond = totalDurationTimeInSecond + durationTimeInSecond;

            }
            blockWidthFactor = totalDurationTimeInSecond / denoOfDuration;


            for (int i = 1; i < lines.Count(); i++)
            {

                int power_percentage_lower_bound = Convert.ToInt16(lines[i].Split(',')[0]);
                int power_percentage_upper_bound = Convert.ToInt16(lines[i].Split(',')[1]);
                int power_percentage_average = (power_percentage_lower_bound + power_percentage_upper_bound) / 2;
                int durationTimeInSecond = Convert.ToInt16(lines[i].Split(',')[2]);

                powerList.Add(power_percentage_average);
                durationList.Add(durationTimeInSecond);

                //draw workout chart
                int[] powerArray = powerList.ToArray();
                for (int p = 0; p < powerArray.Length; p++)
                {
                    powerArray[p] = powerArray[p];
                }
                int[] durationArray = durationList.ToArray();
                for (int d = 0; d < durationArray.Length; d++)
                {
                    durationArray[d] = durationArray[d];
                }
                int sumForBaseX = 0;
                for (int j = 0; j < i - 1; j++)
                {
                    sumForBaseX = sumForBaseX + durationArray[j] / blockWidthFactor;
                }
                originalBmpGraphics.FillRectangle(myBrush,
                    new Rectangle(localBaseX + sumForBaseX, localBaseY - powerArray[i - 1],
                    durationArray[i - 1] / blockWidthFactor, powerArray[i - 1]));


            }

            originalBmpGraphics.FillRectangle(Brushes.Green, localBaseX, 24, bmpOriginalWidth, 2);//寬度400適用           
            Bitmap resized = new Bitmap(originalBmp, new Size(originalBmp.Width * bmpFinalWidth / bmpOriginalWidth,
                originalBmp.Height * bmpFinalWidth / bmpOriginalWidth));

            DataRow dr = dtWorkout.NewRow();
            TimeSpan totalTime = TimeSpan.FromSeconds(durationList.Sum());
            string strTotalTime = totalTime.ToString(@"hh\hmm\mss\s");

            dr["No"] = WorkoutNo;
            dr["ChosenToUpload"] = true;
            dr["WorkoutName"] = Path.GetFileNameWithoutExtension(fileName);
            dr["WorkoutLength"] = strTotalTime;
            dr["WorkoutCurve"] = BmpToBytes(resized);
            dr["FullFilePath"] = fileName;
            dr["UploadFinished"] = "No".ToDefaultLanguage();
            dtWorkout.Rows.Add(dr);
            WorkoutNo++;
        }


        public static IEnumerable<string> ReadLines(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 0x1000, FileOptions.SequentialScan))
            using (var sr = new StreamReader(fs, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        private string GetIdPwdFromDB()
        {
            string idPwd = "";
            using (mydbEntities db = new mydbEntities())
            {
                var idPwdObj = db.tdcode.Where(t => t.code_id == "UserLoginIdPwd" && t.code_type == "AutoLogin").FirstOrDefault();
                if (idPwdObj != null)
                {                   
                    idPwd = Common.CryptDecryptString(idPwdObj.code_value);
                }


            }
            return idPwd;
        }


        #endregion


        #region cookie related

        private string expiryDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        // Create a file to store Login Information 
        string cookieFileName = "Cookiefile.txt";
        private void LoadCookieFromFile()
        {            
            if(File.Exists(cookieFileName) == true)
            {
                driver.Manage().Cookies.DeleteAllCookies();
                driver.Navigate().GoToUrl("https://connect.garmin.com/");
                string[] lines = System.IO.File.ReadAllLines(cookieFileName);
                foreach(var line in lines)
                {
                    string[] cookieInfoArr = line.Split(';');
                    string name = cookieInfoArr[0];
                    string value = cookieInfoArr[1];
                    string domain = cookieInfoArr[2];
                    string path = cookieInfoArr[3];
                    string expiry = cookieInfoArr[4];
                    DateTime? expiryVarNullable = null;
                    if(!string.IsNullOrEmpty(expiry))
                    {
                        DateTime expiryVar = DateTime.ParseExact(expiry,
                                                   expiryDateTimeFormat,
                                                   System.Globalization.CultureInfo.InvariantCulture);
                        expiryVarNullable = expiryVar;
                    }
                   
                    
                    string secure = cookieInfoArr[5];
                    Cookie ck = new Cookie(name, value, path, expiryVarNullable);
                    //ck.Domain = domain;
                    //ck.Secure = Convert.ToBoolean(secure);
                    driver.Manage().Cookies.AddCookie(ck);

                }
                //go to garmin again and we have cookie now, so no need to login again
                driver.Navigate().GoToUrl("https://connect.garmin.com/");
                driver.Manage().Window.Maximize();
            }
        }

        private void SaveNewCookie()
        {            
            try
            {
                File.Delete(cookieFileName);
                using (System.IO.StreamWriter file =new System.IO.StreamWriter(cookieFileName))
                {
                    foreach(var ck in driver.Manage().Cookies.AllCookies)
                    {
                        string expiryString = string.Empty;
                        if(ck.Expiry == null)
                        {
                            // do nothing
                        }
                        else
                        {
                            DateTime tempExpriry = Convert.ToDateTime(ck.Expiry);
                            expiryString = tempExpriry.ToString(expiryDateTimeFormat);
                        }

                        string line = ck.Name + ";" + ck.Value + ";" + ck.Domain + ";" + ck.Path + ";" 
                            + expiryString + ";" + ck.Secure.ToString();
                        file.WriteLine(line);
                    }

                }
            }
            catch(Exception ex)
            {
                AppendOutput("SaveNewCookie() fail:" + ex.Message);
            }
            
        }

        #endregion

      
    }
}
