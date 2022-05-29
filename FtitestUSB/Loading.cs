using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FtitestUSB
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
            label2.Parent = pictureBox1;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
