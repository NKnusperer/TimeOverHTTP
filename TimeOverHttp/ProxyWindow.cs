using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TimeOverHttp
{
    public partial class ProxyWindow : Form
    {
        public ProxyWindow()
        {
            InitializeComponent();
        }

        private void checkBoxUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            // enabled or disabled the texboxes when changing checkbox
            textBoxServer.Enabled = !textBoxServer.Enabled;
            textBoxPort.Enabled = !textBoxPort.Enabled;
            textBoxUsername.Enabled = !textBoxUsername.Enabled;
            textBoxPassword.Enabled = !textBoxPassword.Enabled;
            textBoxDomain.Enabled = !textBoxDomain.Enabled;
        }

        private void ProxyWindow_Load(object sender, EventArgs e)
        {
            // load settings from app.config
            if (SettingsManager.UseProxyServer == "0")
            {
                checkBoxUseProxy.Checked = false;
            }
            else if (SettingsManager.UseProxyServer == "1")
            {
                checkBoxUseProxy.Checked = true;
            }

            textBoxServer.Text = SettingsManager.ProxyServer;
            textBoxPort.Text = SettingsManager.ProxyPort;
            textBoxUsername.Text = SettingsManager.ProxyUsername;
            textBoxPassword.Text = SettingsManager.ProxyPasswort;
            textBoxDomain.Text = SettingsManager.ProxyDomain;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // close the form
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            // save settings to app.config and close form
            if (!checkBoxUseProxy.Checked)
            {
                SettingsManager.UseProxyServer = "0";
            }
            else if (checkBoxUseProxy.Checked)
            {
                SettingsManager.UseProxyServer = "1";
            }

            SettingsManager.ProxyServer = textBoxServer.Text;
            SettingsManager.ProxyPort = textBoxPort.Text;
            SettingsManager.ProxyUsername = textBoxUsername.Text;
            SettingsManager.ProxyPasswort = textBoxPassword.Text;
            SettingsManager.ProxyDomain = textBoxDomain.Text;

            Close();
        }

        private void textBoxPort_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
