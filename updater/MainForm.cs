using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace updater
{
    public partial class MainForm : Form
    {
        private Update _updateController;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        public void SetUpdateController(Update controller)
        {
            _updateController = controller;
        }
    }
}
