using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaspberryPiWifiUpdater
{
    public partial class WifiAccessPoint : UserControl
    {
        public TextBox SSID { get; set; }
        public TextBox Password { get; set; }
        public ComboBox ProtocolComboBox { get; set; }
        public Button DeleteButton { get; set; }

        public WifiAccessPoint()
        {
            InitializeComponent();
            DeleteButton = this.button1;
            cbProtocol.SelectedIndex = 0;
            ProtocolComboBox = cbProtocol;
            SSID = tbSSID;
            Password = tbPass;
        }
    }
}
