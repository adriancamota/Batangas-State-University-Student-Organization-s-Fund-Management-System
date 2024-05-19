using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Funds_Management_System
{
    public partial class Developers : Form
    {
        public Developers()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Creators_Admin_Interface adminForm = new Creators_Admin_Interface();
            adminForm.ShowDialog();
        }
    }
}
