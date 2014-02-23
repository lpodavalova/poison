using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Poison.Train
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bt_Run_Click(object sender, EventArgs e)
        {
            Train train = new Train();

            tb_Stat.Text = train.Simulate();
        }
    }
}
