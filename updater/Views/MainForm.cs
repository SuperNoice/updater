using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Updater.Controllers;
using Updater.Properties;
using Updater.Utils;

namespace Updater.Views
{
    public partial class MainForm : Form
    {
        private UpdateController _updateController;
        private bool _isUpdating = false;
        private string _fileName;
        private bool _startProc = false;

        public MainForm()
        {
            Init();
        }

        public MainForm(string fileName)
        {
            Init();

            _startProc = true;
            _fileName = fileName;
        }

        private void Init()
        {
            InitializeComponent();

            _updateController = new UpdateController();

            _updateController.OnProgressChanged += ProgressChanged;
            _updateController.OnStatusChanged += StatusChanged;
            _updateController.OnUpdateCompleate += UpdateCompleate;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(Application.StartupPath, "filelink.cfg")))
            {
                var text = File.ReadAllText(Path.Combine(Application.StartupPath, "filelink.cfg"));
                Settings.Default.fileLink = text.Trim();
                Settings.Default.Save();
            }

            // Восстановление настроек после смены папки или версии
            if (Settings.Default.UpdateNeeded)
            {
                Settings.Default.Upgrade();
                Settings.Default.Save();
                Settings.Default.Reload();

                Settings.Default.UpdateNeeded = false;
                Settings.Default.Save();
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            // TODO: Сделать WebHeaderHelper асинхронным и выводить ошибки подключения к интернету пользователю
            try
            {
                WebHeaderHelper.GetHeader(Settings.Default.fileLink);
            }
            catch (Exception)
            {
                var settingsForm = new SettingsForm();
                settingsForm.ShowDialog();
                try
                {
                    WebHeaderHelper.GetHeader(Settings.Default.fileLink);
                }
                catch (Exception)
                {
                    Close();
                }
            }

            _updateController.BeginUpdate();
            _isUpdating = true;
        }

        private void ProgressChanged(int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { ProgressChanged(progress); }));
                return;
            }

            progressBar.Value = progress;
        }

        private void StatusChanged(string status)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { StatusChanged(status); }));
                return;
            }

            updatingStatusLabel.Text = status;
        }

        private void UpdateCompleate(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { UpdateCompleate(sender, e); }));
                return;
            }

            _isUpdating = false;

            if (_startProc)
            {
                Process.Start(_fileName);
            }

            Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isUpdating)
            {
                _updateController.EndUpdate();
                e.Cancel = true;
            }
        }
    }
}
