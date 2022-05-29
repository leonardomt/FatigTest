using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FtitestUSB
{
    public partial class TipoHardware : MetroFramework.Forms.MetroForm
    {
        public TipoHardware()
        {
            InitializeComponent();

            VerificarHardware res = new VerificarHardware();
            res.configurarHardware();
        }

        private void TipoHardware_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;

            String temp = ConfigurationManager.AppSettings["Tipomultitest"];
            if (ConfigurationManager.AppSettings["Tipomultitest"] == "Arduino")
                radioButton1.Checked = true;
            if (ConfigurationManager.AppSettings["Tipomultitest"] == "FTDI")
                radioButton2.Checked = true;
 
        }

  
    }
}
