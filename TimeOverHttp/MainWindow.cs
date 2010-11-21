using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Microsoft.Win32;

namespace TimeOverHttp
{
    public partial class MainWindow : Form
    {
        DateTime reference = new DateTime(0001, 01, 01, 00, 00, 00);
        SetTime timeSetter = new SetTime();
        GetTime timeGetter = new GetTime();

        public MainWindow()
        {
            InitializeComponent();
            Shown += new EventHandler(MainWindow_Shown);
        }

        void MainWindow_Shown(object sender, EventArgs e)
        {
            // Force to hide window on startup
            Visible = false;
            WindowState = FormWindowState.Minimized;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Hide();
                WindowState = FormWindowState.Minimized;
            }
            else if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            Close();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                e.Cancel = true;
                Hide();
                WindowState = FormWindowState.Minimized;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // show proxy settings
            ProxyWindow proxy = new ProxyWindow();
            proxy.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // show about box
            AboutWindow about = new AboutWindow();
            about.ShowDialog();
        }

        private void aboutStripMenuItem1_Click(object sender, EventArgs e)
        {
            // show about box
            AboutWindow about = new AboutWindow();
            about.ShowDialog();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if (SettingsManager.Autostart == "0")
            {
                checkBoxAutostart.Checked = false;
                SetStartup(false);
                
            }
            else if (SettingsManager.Autostart == "1")
            {
                checkBoxAutostart.Checked = true;
                SetStartup(true);
            }

            textBoxHttpServer.Text = SettingsManager.HttpServer;
            textBoxSyncEveryMinutes.Text = SettingsManager.SyncEveryMinutes;

            // set timer for time sync in minutes
            timer1.Interval = int.Parse(SettingsManager.SyncEveryMinutes) * 60000;

            // start timer for sync
            timer1.Start();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allow only numbers and backspace
            if ((char.IsNumber(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DateTime result = timeGetter.OverHttp();

            if (result > reference)
            {
                timeSetter.SystemTime(result);
                MessageBox.Show("Connection sucessfully established! \nReceived time: " + result, "Connection OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Connection not sucessfully established! \nPleas check your settings.", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime result = timeGetter.OverHttp();
            if (result > reference)
            {
                timeSetter.SystemTime(result);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SettingsManager.HttpServer = textBoxHttpServer.Text;
            SettingsManager.SyncEveryMinutes = textBoxSyncEveryMinutes.Text;
            if (!checkBoxAutostart.Checked)
            {
                SettingsManager.Autostart = "0";
                SetStartup(false);
            }
            else if (checkBoxAutostart.Checked)
            {
                 SettingsManager.Autostart = "1";
                 SetStartup(true);
            }

            timer1.Interval = int.Parse(SettingsManager.SyncEveryMinutes) * 60000;
            Hide();
            WindowState = FormWindowState.Minimized;
        }

        // Add/Remove registry entries for windows startup.
        private void SetStartup(bool enable)
        {
            string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";           
            Microsoft.Win32.RegistryKey startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey);

            if (enable)
            {
                if (startupKey.GetValue("TimeOverHTTP") == null)
                {
                    startupKey.Close();
                    startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);
                    // Add startup reg key
                    startupKey.SetValue("TimeOverHTTP", Application.ExecutablePath.ToString());
                    startupKey.Close();
                }
            }
            else
            {
                // remove startup
                startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);
                startupKey.DeleteValue("TimeOverHTTP", false);
                startupKey.Close();
            }
        }

        
    }
}
