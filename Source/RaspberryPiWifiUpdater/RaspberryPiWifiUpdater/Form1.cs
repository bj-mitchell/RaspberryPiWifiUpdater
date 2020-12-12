using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaspberryPiWifiUpdater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void AddAccessPoint_Click(object sender, EventArgs e)
        {
            WifiAccessPoint ap = new WifiAccessPoint();
            flowLayoutPanel1.Controls.Add(ap);
            ap.DeleteButton.Click += DeleteButton_Click;
            ap.SSID.Focus();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Button butt = (Button)sender;
            WifiAccessPoint ap = (WifiAccessPoint)butt.Parent;
            ap.DeleteButton.Click -= new System.EventHandler(DeleteButton_Click);
            flowLayoutPanel1.Controls.Remove(ap);
            ap.Dispose();
        }

        private void SaveConfiguration_Click(object sender, EventArgs e)
        {
            string location = Path.GetDirectoryName(this.GetType().Assembly.Location);
            string config = location + Path.DirectorySeparatorChar + "wpa_supplicant.conf";
            
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("ctrl_interface=DIR=/var/run/wpa_supplicant GROUP=netdev");
            builder.AppendLine("update_config=1");
            builder.AppendLine("country=US");
            builder.AppendLine("");

            foreach (WifiAccessPoint ap in flowLayoutPanel1.Controls)
            {
                builder.AppendLine("network={");
                builder.AppendLine("ssid=\"" + ap.SSID.Text + "\"");
                builder.AppendLine("psk=\"" + ap.Password.Text + "\"");
                builder.AppendLine("key_mgmt=" + ap.ProtocolComboBox.Text);
                builder.AppendLine("}");
                builder.AppendLine("");
            }

            File.WriteAllText(config, builder.ToString());
            File.WriteAllText(location + Path.DirectorySeparatorChar + "ssh", "");

            MessageBox.Show("Saved configuration file to flash card.");
        }
    }
}
