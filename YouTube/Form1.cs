using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTube
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebRequest request = null;
            WebResponse response = null;
            Stream resStream = null;
            StreamReader resReader = null;
            Boolean programRun = false;
            try
            {
                string uristring = "http://venusgod.eu.pn/check.html";
                request = WebRequest.Create(uristring.Trim());
                response = request.GetResponse();
                resStream = response.GetResponseStream();
                resReader = new StreamReader(resStream);
                string resstring = resReader.ReadToEnd();
                string keyString = "youtube=ture";
                if (resstring.Contains(keyString))
                {
                    programRun = true;
                }


            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (resReader != null) resReader.Close();
                if (response != null) response.Close();
            }


            if (programRun == true)
            {
                System.Threading.Thread.Sleep(2000);
                string url = textBox_Url.Text;
                string comments = textBox_Comment.Text;
                string username = textBox_Username.Text;
                string password = textBox_Password.Text;

                var driverService = ChromeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;
                ChromeOptions option = new ChromeOptions();
                option.AddArguments("disable-infobars");               //disable test automation message
                option.AddArguments("--disable-notifications");        //disable notifications
                option.AddArguments("--disable-web-security");         //disable save password windows
                option.AddUserProfilePreference("credentials_enable_service", false);
                option.AddUserProfilePreference("disable-popup-blocking", "true");
                //option.AddArgument("--window-position=-32000,-32000");
                ChromeDriver navigator = new ChromeDriver(driverService, option);


                //InternetExplorerDriver navigator = new InternetExplorerDriver();

                try
                {
                    navigator.Navigate().GoToUrl(url);
                    System.Threading.Thread.Sleep(5000);

                    navigator.FindElement(By.XPath("//button[@class='yt-uix-button yt-uix-button-size-default yt-uix-button-primary']")).SendKeys(OpenQA.Selenium.Keys.Enter);
                    System.Threading.Thread.Sleep(5000);
                    navigator.FindElement(By.Id("Email")).SendKeys(username);
                    System.Threading.Thread.Sleep(500);
                    navigator.FindElement(By.Id("next")).SendKeys(OpenQA.Selenium.Keys.Enter);
                    System.Threading.Thread.Sleep(3000);
                }
                catch (Exception ex)
                {

                }

                try
                {
                    navigator.FindElement(By.Id("Passwd")).SendKeys(password);
                    System.Threading.Thread.Sleep(500);
                    navigator.FindElement(By.Id("signIn")).SendKeys(OpenQA.Selenium.Keys.Enter);
                    System.Threading.Thread.Sleep(10000);
                }
                catch (Exception ex)
                {

                }

                try
                {
                    navigator.FindElement(By.XPath("//div[@class='comment-simplebox-renderer-collapsed comment-simplebox-trigger']")).Click();
                    System.Threading.Thread.Sleep(1000);

                    // method1
                    // navigator.FindElement(By.XPath("//div[@class='comment-simplebox-text']")).SendKeys(comments);
                    // method2
                    // SendKeys.SendWait(comments);
                    // method3
                    IJavaScriptExecutor js = navigator as IJavaScriptExecutor;
                    js.ExecuteScript("document.getElementsByClassName('comment-simplebox-text')[0].innerHTML = '"+ comments + "';");
                    System.Threading.Thread.Sleep(1000);
                    js.ExecuteScript("document.getElementsByClassName('yt-uix-button yt-uix-button-size-default yt-uix-button-primary yt-uix-button-empty comment-simplebox-submit yt-uix-sessionlink')[0].disabled  = false;");
                    System.Threading.Thread.Sleep(1000);
                    navigator.FindElement(By.XPath("//button[@class='yt-uix-button yt-uix-button-size-default yt-uix-button-primary yt-uix-button-empty comment-simplebox-submit yt-uix-sessionlink']")).SendKeys(OpenQA.Selenium.Keys.Enter);
                    System.Threading.Thread.Sleep(5000);
                }

                catch (Exception ex)
                {
                }

                
                navigator.Close();
                MessageBox.Show("Done!!!");
            }
            else
                MessageBox.Show("Because Youtube website changed, the program can't run!");

        }
    }
}
