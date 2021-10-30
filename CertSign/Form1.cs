using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CertSign
{
    public partial class Form1 : Form
    {
        public delegate void DelReadStdOutput(string result);

        public delegate void DelReadPySrv(string result);
        public event DelReadStdOutput ReadStdOutput;

        public event DelReadPySrv ReadPySrv;
        BackgroundWorker worker = new BackgroundWorker();
        BackgroundWorker comprWork = new BackgroundWorker();
        BackgroundWorker sendWorker = new BackgroundWorker();
        public Socket clientSocket = null;
        private Process p = null;
        string zipFilePath = "";

        /*************************************/
        protected string certsign_username;
        protected string certsign_password;
        protected string mspt_usr;
        protected string mspt_pwd;

        List<SignItem> sList;
        public Form1()
        {
            InitializeComponent();
            this.timer1.Interval = 1000;
            this.timer1.Start();
            worker.WorkerSupportsCancellation = true;//是否支持异步取消
            worker.WorkerReportsProgress = false;//能否报告进度更新
            worker.DoWork += Worker_DoWork;

            comprWork.WorkerSupportsCancellation = true;
            comprWork.WorkerReportsProgress = true;
            comprWork.DoWork += DoCompression;
            comprWork.ProgressChanged += progressChangedEventHandler;

            sendWorker.WorkerSupportsCancellation = true;//是否支持异步取消
            sendWorker.WorkerReportsProgress = false;//能否报告进度更新
            sendWorker.DoWork += SendWork;
            ReadStdOutput += new DelReadStdOutput(ReadStdOutputAction);
            ReadPySrv += new DelReadPySrv(ReadRecvFormPySrv);
            sList = new List<SignItem>();
        }



        private void progressChangedEventHandler(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void dealOneFile(string filePath)
        {
            string prjName = string.Empty;
            bool isX64 = false;
            if (filePath == "")
            {
                MessageBox.Show("filePath is empty, please drag file to button on first page");
                return;
            }
            int index = filePath.LastIndexOf("\\");
            if (index < 0)
            {
                MessageBox.Show("can not find \\ in file path");
                return;
            }
            else
            {
                prjName = filePath.Substring(index + 1);
                prjName = prjName.Substring(0, prjName.Length - 4);
            }

            if (filePath.ToLower().IndexOf("x64") < 0)
            {
                isX64 = false;
            }
            else
            {
                isX64 = true;
            }
            SignItem item = new SignItem(prjName, filePath, isX64);
            sList.Add(item);
            ListViewItem li = new ListViewItem();
            li.Text = item.prj_Name;
            li.SubItems.Add(item.prj_filePath);
            li.SubItems.Add(item.isX64.ToString());
            listView1.Items.Add(li);
        }
        private void DragRecvBtn_DragDrop(object sender, DragEventArgs e)
        {
            
            System.Array fileArray = (System.Array)e.Data.GetData(DataFormats.FileDrop);
            foreach ( object x in fileArray) {
                string filePath = x.ToString();
                dealOneFile(filePath);
            }

            //string filePath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            //driverPathTb.Text = filePath;
            
           
        }

        private void DragRecvBtn_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
        }


        private void SendWork(object sender, DoWorkEventArgs e) {
            if (e.Argument.ToString() == "startCli")
            {
                //StartClient();
            }
        }
        private void ReadStdOutputAction(string result)
        {
            this.richTextBox1.AppendText(result + "\r\n");
        }

        private void ReadRecvFormPySrv(string result)
        {
            //this.richTextBox2.AppendText(result + "\r\n");
        }

        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                // 4. 异步调用，需要invoke
                this.Invoke(ReadStdOutput, new object[] { e.Data });
            }
        }



        void OutputHandler(object sendingProcess, DataReceivedEventArgs e)
        {
            //this.richTextBox1.AppendText(e.Data + "\r\n");
            if (!string.IsNullOrEmpty(e.Data))
                this.Invoke(ReadStdOutput, e.Data);
        }

        void StartPyServer()
        {

            p = new Process();
            // assign start information to the process 
            p.StartInfo.FileName = "cmd";//"cmd";
            //p.StartInfo.Arguments = "CertSignServer.py";
            //是否使用操作系统shell启动
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardInput = true;
            //输出信息
            p.StartInfo.RedirectStandardOutput = true;
            // 输出错误
            p.StartInfo.RedirectStandardError = true;
            p.Start();

            //string strInput = "ping www.baidu.com";
            string strInput = "python -u CertSignServer.py";

            // start the process 

            p.StandardInput.WriteLine(strInput);
            //p.StandardInput.Flush();
            p.StandardInput.AutoFlush = true;
            //p.Start();
            Thread.Sleep(5);
            //p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            //p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

            p.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            p.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();

            p.WaitForExit();

            // Read the standard output of the app we called.  
            // in order to avoid deadlock we will read output first 
            // and then wait for process terminate: 
            // StreamReader myStreamReader = myProcess.StandardOutput;
            // string myString = myStreamReader.ReadLine();

            /*if you need to read multiple lines, you might use: 
                string myString = myStreamReader.ReadToEnd() */

            // wait exit signal from the app we called and then close it. 
            // myProcess.WaitForExit();
            // myProcess.Close();

            // write the output we got from python app 
            //Console.WriteLine("Value received from script: " + myString);
        }
        void StartClient()
        {
            //"Start Client!"
            string serverIP = "127.0.0.1";
            int port = 31500;
            string outBuf = "";
            /*Socket */
            clientSocket = new Socket(AddressFamily.InterNetwork,
SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(IPAddress.Parse(serverIP), port));
            }
            catch (SocketException se) {
                MessageBox.Show(se.Message);
            }

            //outBuf += "Connect: " + serverIP + port;
            //this.Invoke(ReadPySrv, outBuf);
            // receive msg
            byte[] data = new byte[1024];
            int count = clientSocket.Receive(data);
            string msg = Encoding.UTF8.GetString(data, 0, count);

            this.Invoke(ReadPySrv, msg);
            //byte[] sendbuf = new byte[255];
            string s = "nimama";
            byte[] sendbuf = Encoding.UTF8.GetBytes(s);

            clientSocket.Send(sendbuf);

            outBuf = "Send: " + Encoding.UTF8.GetString(sendbuf);
            this.Invoke(ReadPySrv, outBuf);


            //clientSocket.Send(data);
            //clientSocket.Close();
        }
        private void DoCompression(object sender, DoWorkEventArgs e)
        {
            if (e.Argument.ToString() == "compression")
            {

                try
                {
                    //Create Zip File
                    if (driverPathTb.Text != "")
                    {
                        string driverDir = driverPathTb.Text.Trim();
                        int i = driverDir.LastIndexOf('\\');

                        zipFilePath = driverDir.Substring(i + 1) + ".zip";
                        if (File.Exists(zipFilePath))
                            File.Delete(zipFilePath);
                        //MessageBox.Show(zipFilePath);
                        ZipFile.CreateFromDirectory(driverDir, zipFilePath);
                    }
                    if (comprWork.WorkerReportsProgress)
                        comprWork.ReportProgress(100);
                    zipFilePath = System.Environment.CurrentDirectory + "\\" + zipFilePath;
                    statusLabel2.Text = "压缩完成 (" + zipFilePath + ")";
                    //if (worker != null && worker.IsBusy)
                    //{
                    //    if (worker.CancellationPending)
                    //    {
                    //        // update the entry size to avoid the ZipStream exception
                    //        break;
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show("压缩失败\r\n" + ex.Message);

                }
                finally
                {
                    comprWork.CancelAsync();

                }

            }

        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument.ToString() == "startPySrv")
            {
                StartPyServer();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            driverPathTb.Text = @"D:\xv\MP_branch\8821C\RTL8821C\RTLWlanU_WindowsDriver_";
            initINIConfig();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = DateTime.Now.ToLongTimeString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            if (worker != null && worker.IsBusy)
            {
                statusLabel2.Text = "The task is busy now";
                return;
            }
            worker.RunWorkerAsync("startPySrv");//会触发worker的DoWork事件

        }

        private void btn_StartCli_Click_1(object sender, EventArgs e)
        {
            StartClient();
            /*
            if (sendWorker.IsBusy)
            {
                statusLabel2.Text = "Client is busy now";
                return;
            }
            sendWorker.RunWorkerAsync("startCli");
            */
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (p != null) {

            }
        }

        private void btnExitPySrv_Click(object sender, EventArgs e)
        {
            if (clientSocket != null)
            {
                byte[] sendbuf = new byte[3] { 0xff, 0xff, 0xfe };
                clientSocket.Send(sendbuf);
            }
            else {
                MessageBox.Show("Client Socket is NULL");
            }
            clientSocket.Close();
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            // byte[] sendbuf = new byte[512];
            // sendbuf[0] = 0xE0;

            zipFilePath = driverPathTb.Text.Trim();
            if (zipFilePath == "")
            {
                MessageBox.Show("zipfilePath is empty");
                return;
            }
            //initINIConfig();
            signFiles(zipFilePath);
            /*byte[] a =Encoding.UTF8.GetBytes(zipFilePath);
            sendbuf[1] = (byte)a.Length;

            for (int i = 0; i < a.Length; i++)
            {
                sendbuf[2 + i] = a[i];
            }
            if(clientSocket == null) { 
                MessageBox.Show("Please connect to server");
                return; }
            try {
               
                  clientSocket.Send(sendbuf, a.Length + 2, SocketFlags.None);
            }
            catch (System.ObjectDisposedException ex) {
                MessageBox.Show("clientSocket is Disposed ");
            }*/

        }

        private void btnCompression_Click(object sender, EventArgs e)
        {
            if (driverPathTb.Text.Trim() == "")
            {
                MessageBox.Show("请将要压缩的文件拖入Button", "Error");
                return;
            }

            if (comprWork != null && comprWork.IsBusy)
            {
                statusLabel2.Text = "It is busy now that the task which compress the directory";
                return;
            }
            comprWork.RunWorkerAsync("compression");//会触发worker的DoWork事件
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        void initINIConfig()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");
            certsign_username = INIHelper.Read("certsign", "username", "", filePath);
            certsign_password = INIHelper.Read("certsign", "password", "", filePath);
            mspt_usr = INIHelper.Read("ms_sign", "username", "", filePath);
            mspt_pwd = INIHelper.Read("ms_sign", "password", "", filePath);
        }
        void signFiles(string filename)
        {


            ChromeDriverService service = ChromeDriverService.CreateDefaultService(System.Environment.CurrentDirectory);
            //  service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--test-type", "--ignore-certificate-errors");
            options.AddArguments("user-agent=mozilla/5.0 (linux; u; android 2.3.3; en-us; sdk build/ gri34) applewebkit/533.1 (khtml, like gecko) version/4.0 mobile safari/533.1");
            options.AddArgument("enable-automation");
            //   options.AddArgument("headless");
            //  options.AddArguments("--proxy-server=http://user:password@yourProxyServer.com:8080");
            IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(service, options, TimeSpan.FromSeconds(120));
            // using (IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(service, options, TimeSpan.FromSeconds(120))) {
            driver.Navigate().GoToUrl("https://certsign.realtek.com/Login.jsp");
            var login_username = driver.FindElement(By.XPath("//*[@id=\"login_username\"]"));
            var login_password = driver.FindElement(By.XPath("//*[@id=\"login_password\"]"));
            IWebElement btnLogin = driver.FindElement(By.XPath("//*[@id=\"btnLogin\"]"));
            login_username.SendKeys(this.certsign_username);
            login_password.SendKeys(this.certsign_password);
            btnLogin.Click();
            Thread.Sleep(200);
            driver.FindElement(By.XPath("//*[@id=\"getOTP\"]")).Click();
            MailSpider ms = new MailSpider();
            string otpcode = "0";
            if (ms.dl_mail(ref otpcode) == 0)
                //MessageBox.Show("Get otpcode = " + otpcode);
                Console.WriteLine("Get otpcode = " + otpcode);
            else
            {
                MessageBox.Show("Get otpcode failed");
                return;
            }

            driver.FindElement(By.XPath("//*[@id=\"otp1\"]")).SendKeys(otpcode);
            driver.FindElement(By.XPath("//*[@id=\"validOTP\"]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(filename);
            int k = 0;
            while (true) {
                try
                {
                    driver.FindElement(By.XPath("//button[@type='button'][contains(.,'上傳檔案')]")).Click();
                    Thread.Sleep(6000);
                    k += 1;
                }
                catch (OpenQA.Selenium.WebDriverTimeoutException ex)
                {
                    Console.WriteLine("raise exception 上傳檔案 time out");
                }

                if (k > 0) break;

            }

            // select the drop down list
            var algorithmType = driver.FindElement(By.XPath("//select[contains(@name,'shaType')]"));
            //create select element object 
            var selectElement = new SelectElement(algorithmType);
            //select by value
            selectElement.SelectByValue("A6419491-A9A6-48FE-AB09-6AF86954FBEC"); //#EVSign  SHA256演算法
            // 8B4F20EC - DB9A - 49FB - 9722 - 7A4E3F9EE077 EVSign SHA1演算法
            // D03E8893 - CBC6 - 4567 - 8781 - 430A30FA6CC3 EVSign SHA256演算法(Append)
            //  D75001C2 - 141D - 42E9 - 9CED - 41A62A8FD0E2 EVSign SHA1演算法 & amp; SHA256演算法
            driver.FindElement(By.XPath("//button[@type='button'][contains(.,'簽署憑證')]")).Click();
            Thread.Sleep(3000);
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    driver.FindElement(By.XPath("//strong[contains(.,'已完成簽署!')]"));
                    break;
                }
                catch (NoSuchElementException ex) {
                    Console.WriteLine("未签署完成，3秒后重试 =============>");
                    Thread.Sleep(3000);
                }
            }

            var element = driver.FindElement(By.XPath("//button[@type='button'][contains(.,'Download')]"));
            //var element = WebDriverWait(self.driver, 10).until(EC.visibility_of_element_located((By.XPATH, "//button[@type='button'][contains(.,'Download')]")))
            //new ActionChains(self.driver).move_to_element(element).click().perform()
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(webDriver =>

                 webDriver.FindElement(By.XPath("//button[@type='button'][contains(.,'Download')]")).Displayed
            );

            IWebElement downElement = driver.FindElement(By.XPath("//button[@type='button'][contains(.,'Download')]"));

            new Actions(driver).MoveToElement(downElement).Click().Perform();
            // }

        }


        #region 异常  退出chromedriver

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;

        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        /// <summary>
        /// 获取窗口句柄
        /// </summary>
        /// <returns></returns>
        public IntPtr GetWindowHandle()
        {
            string name = (Environment.CurrentDirectory + "\\chromedriver.exe");
            IntPtr hwd = FindWindow(null, name);
            return hwd;
        }

        /// <summary>
        /// 关闭chromedriver窗口
        /// </summary>
        public void CloseWindow()
        {
            try
            {
                IntPtr hwd = GetWindowHandle();
                SendMessage(hwd, 0x10, 0, 0);
            }
            catch { }
        }

        /// <summary>
        /// 退出chromedriver
        /// </summary>
        /// <param name="driver"></param>
        public void CloseChromeDriver(IWebDriver driver)
        {
            try
            {
                driver.Quit();
                driver.Dispose();
            }
            catch { }
            CloseWindow();
        }

        #endregion 异常  退出chromedriver

        private void button2_Click(object sender, EventArgs e)
        {
            //initINIConfig();
            // signFiles();

        }
        public class SignItem{
            public string prj_Name { get; set; }
            public string prj_filePath { get; set; }
            public bool isX64 { get; set; }

            public SignItem(string prj_Name, string prj_filePath, bool isX64) {
                this.prj_Name = prj_Name;
                this.prj_filePath = prj_filePath;
                this.isX64 = isX64;
            }
        }
        private void loginMSPartner(List<SignItem> nList) 
        {
            
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(System.Environment.CurrentDirectory);
            //  service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--test-type", "--ignore-certificate-errors");
            options.AddArguments("user-agent=mozilla/5.0 (linux; u; android 2.3.3; en-us; sdk build/ gri34) applewebkit/533.1 (khtml, like gecko) version/4.0 mobile safari/533.1");
            options.AddArgument("enable-automation");
            //   options.AddArgument("headless");
            //  options.AddArguments("--proxy-server=http://user:password@yourProxyServer.com:8080");
            IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(service, options, TimeSpan.FromSeconds(120));
            // using (IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(service, options, TimeSpan.FromSeconds(120))) {
            driver.Navigate().GoToUrl("https://partner.microsoft.com/zh-tw/dashboard/hardware/Search"); //https://partner.microsoft.com/zh-CN/dashboard/hardware/Search
            var login_username = driver.FindElement(By.XPath("//input[contains(@type,'email')]"));
            login_username.SendKeys(this.mspt_usr);
            Thread.Sleep(100);
            var next_btn = driver.FindElement(By.XPath("//input[contains(@type,'submit')]"));
            next_btn.Click();
            Thread.Sleep(2000);
            var login_password = driver.FindElement(By.XPath("//input[contains(@name,'passwd')]"));
            login_password.SendKeys(this.mspt_pwd);
            next_btn = driver.FindElement(By.XPath("//input[contains(@type,'submit')]"));
            next_btn.Click();
            Thread.Sleep(1000);
            next_btn = driver.FindElement(By.XPath("//input[contains(@value,'是')]"));
            next_btn.Click();
            Thread.Sleep(1000);

            foreach(var needSignItem in nList)
            {
                int retry = 0;
                next_btn = driver.FindElement(By.XPath("//div[@class='onedash-navigation-category'][contains(.,'驅動程式')]"));
                next_btn.Click();
                Thread.Sleep(10000);
                do {
                    try
                    {
                        IWebElement newhard = driver.FindElement(By.XPath("//a[contains(@uitestid,'newDriverButton')]"));
                        newhard.Click();
                        var inputHN = driver.FindElement(By.XPath("//input[contains(@uitestid,'inputHardwareName')]"));
                        inputHN.SendKeys(needSignItem.prj_Name);
                        var inputfile = driver.FindElement(By.XPath("//*[@id='file']"));
                        inputfile.SendKeys(needSignItem.prj_filePath);
                        Thread.Sleep(5000);
                        new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(webDriver =>

                      webDriver.FindElement(By.XPath("//span[@uitestid='spanRequestedSignature_WINDOWS_v100_X64_RS1_FULL']")).Displayed
                          );
                        if (needSignItem.isX64)
                        {
                            var checkBox_X64_RS1 = driver.FindElement(By.XPath("//span[@uitestid='spanRequestedSignature_WINDOWS_v100_X64_RS1_FULL']"));
                            var checkBox_X64_19H1 = driver.FindElement(By.XPath("//span[@uitestid='spanRequestedSignature_WINDOWS_v100_X64_19H1_FULL']"));
                            checkBox_X64_RS1.Click();
                            checkBox_X64_19H1.Click();
                        }
                        else
                        {
                            var checkBox_X86_RS1 = driver.FindElement(By.XPath("//span[contains(@uitestid,'spanRequestedSignature_WINDOWS_v100_RS1_FULL')]"));
                            var checkBox_X86_19H1 = driver.FindElement(By.XPath("//span[contains(@uitestid,'spanRequestedSignature_WINDOWS_v100_19H1_FULL')]"));
                            checkBox_X86_RS1.Click();
                            checkBox_X86_19H1.Click();
                        }
                        Thread.Sleep(10000);
                        new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(webDriver =>

                       webDriver.FindElement(By.XPath("//button[@uitestid='buttonSubmit']")).Displayed
                           );
                        var submit_btn = driver.FindElement(By.XPath("//button[@uitestid='buttonSubmit']"));
                        submit_btn.Click();
                        Thread.Sleep(20000);
                        break;
                    }
                    catch (ElementClickInterceptedException ex)
                    {
                        Thread.Sleep(1000);
                        next_btn = driver.FindElement(By.XPath("//div[@class='onedash-navigation-category'][contains(.,'驅動程式')]"));
                        next_btn.Click();
                        new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(webDriver =>

                             webDriver.FindElement(By.XPath("//a[contains(@uitestid,'newDriverButton')]")).Displayed
                         );
                        continue;
                        //Thread.Sleep(1000);
                    }
                } while (true);
               
                
                
            }
        }
        private void btn_ms_sign_Click(object sender, EventArgs e)
        {
            if (sList.Count == 0) return;
               
            //initINIConfig();
           
            loginMSPartner(sList);
        }

        private void DragRecvBtn_Click(object sender, EventArgs e)
        {

        }

        private void btn_clearlist_Click(object sender, EventArgs e)
        {
            sList.Clear();
            listView1.Clear();
        }
    }
}
