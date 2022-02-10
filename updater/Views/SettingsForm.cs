using System;
using System.Linq;
using System.Windows.Forms;
using Updater.Properties;
using Updater.Utils;

namespace Updater.Views
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            CheckAndSaveFileLink();
        }

        private void linkTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
            {
                CheckAndSaveFileLink();
            }
        }

        private void CheckAndSaveFileLink()
        {
            if (linkTextBox.Text.Trim().Split('.').Last() != "zip")
            {
                MessageBox.Show("Неверная ссылка!");
                return;
            }

            try
            {
                WebHeaderHelper.GetHeader(linkTextBox.Text.Trim());
                Settings.Default.fileLink = linkTextBox.Text.Trim().Replace(@"\", "/");
                Settings.Default.Save();
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Неверная ссылка!");
            }
        }
    }
}
