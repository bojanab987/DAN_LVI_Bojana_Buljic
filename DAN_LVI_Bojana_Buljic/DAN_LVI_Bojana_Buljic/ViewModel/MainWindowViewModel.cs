using DAN_LVI_Bojana_Buljic.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.IO.Compression;

namespace DAN_LVI_Bojana_Buljic.ViewModel
{
    class MainWindowViewModel:ViewModelBase
    {
        MainWindow mainWindow;
       
        #region Constructor
        /// <summary>
        /// Constructor for main view window
        /// </summary>
        /// <param name="open"></param>
        public MainWindowViewModel(MainWindow open)
        {
            mainWindow = open;
        }
        #endregion

        #region Property
        /// <summary>
        /// HTML string property binded to text box input
        /// </summary>
        private string html;
        public string HTML
        {
            get { return html; }
            set
            {
                html = value;
                OnPropertyChanged("HTML");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Download button command
        /// </summary>
        private ICommand download;
        public ICommand Download
        {
            get
            {
                if(download==null)
                {
                    download = new RelayCommand(param => DownloadExecute(), param => CanDownloadExecute());
                }
                return download;
            }
        }
       
        /// <summary>
        /// Method to execute downloading the html code for inputed address
        /// </summary>
        public void DownloadExecute()
        {
            try
            {
                //creating instance for request from web client

                if (HTML.CheckURLValid())
                {
                    WebRequest request = WebRequest.Create(HTML);

                    //Uri request;

                    //bool isOk = Uri.TryCreate(HTML, UriKind.Absolute, out request) && (request.Scheme == Uri.UriSchemeHttp||request.Scheme==Uri.UriSchemeHttps);
                    request.Method = "GET";


                    //sting object containing the downloaded content
                    string htmlSource;

                    //reading the all content from forwarded web address and keeping it into htmlSource string variable
                    using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))
                    {
                        htmlSource = reader.ReadToEnd();
                    }
                    //path of the file to keep downloaded content                
                    string filePath = string.Format(@"..\..\HTMLfiles\HTML_{0}_{1}_{2}_{3}.html", DateTime.Now.ToShortDateString(), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    //writing content into file
                    File.WriteAllText(filePath, htmlSource);
                    //notification if download is successfull
                    if (filePath != null)
                    {
                        MessageBox.Show("Successfully downloaded html file", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Could not download the file", "Error", MessageBoxButton.OK);
                    }
                }
                else
                {
                    MessageBox.Show("URL address is not in correct!", "Error", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method to check if download button is available and returns false if text box is empty
        /// </summary>
        /// <returns></returns>
        public bool CanDownloadExecute()
        {
            if(HTML==null||String.IsNullOrEmpty(HTML))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Zip button command
        /// </summary>
        private ICommand zip;
        public ICommand Zip
        {
            get
            {
                if (zip == null)
                {
                    zip= new RelayCommand(param => CreateZipExecute(), param => CanCreateZipExecute());
                }
                return zip;
            }
        }

        /// <summary>
        /// Method for executing zipping of the files
        /// </summary>
        public void CreateZipExecute()
        {
            try
            {
                string startPath = @"..\\..\HTMLfiles\";
                string zipPath = @"..\\..\ZippedFiles\HTMLfiles.zip";
                if (zipPath == null)
                {
                    ZipFile.CreateFromDirectory(startPath, zipPath);
                }
                else
                {
                    ZipFile.CreateFromDirectory(startPath, string.Format(@"..\\..\ZippedFiles\HTMLfiles_{0}_{1}_{2}_{3}.zip", DateTime.Now.ToShortDateString(), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
                }
                MessageBox.Show("Files are successfully zipped", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method to confirm zip is possible
        /// </summary>
        /// <returns>true</returns>
        public bool CanCreateZipExecute()
        {
            return true;
        }

        /// <summary>
        /// Exit button command
        /// </summary>
        private ICommand exit;
        public ICommand Exit
        {
            get
            {
                if (exit == null)
                {
                    exit = new RelayCommand(param => ExitExecute(), param => CanExitExecute());
                }
                return exit;
            }
        }
        /// <summary>
        /// Method to execute closing the main window and exiting from app
        /// </summary>
        public void ExitExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to exit the application?", "Check", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    mainWindow.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method checks that exit command is possible to execute
        /// </summary>
        /// <returns>true</returns>
        public bool CanExitExecute()
        {
            return true;
        }

        /// <summary>
        /// Refresh button command for refreshing text input
        /// </summary>
        private ICommand refresh;
        public ICommand Refresh
        {
            get
            {
                if (refresh == null)
                {
                    refresh = new RelayCommand(param => RefreshExecute(), param => CanRefreshExecute());
                }
                return refresh;
            }
        }

        /// <summary>
        /// Method to execute refresh button command and removing text input from text box
        /// </summary>
        private void RefreshExecute()
        {
            try
            {
                HTML = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Check if refresh is possible
        /// </summary>
        /// <returns>true</returns>
        private bool CanRefreshExecute()
        {
            return true;
        }
        #endregion
    }
}
