using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Updater.Properties;
using Updater.Utils;

namespace Updater.Controllers
{
    public delegate void ProgressChangedEventHandler(int progress);
    public delegate void StatusChangedEventHandler(string status);

    public class UpdateController
    {
        public event ProgressChangedEventHandler OnProgressChanged;

        public bool BreakUpdateFlag { private set; get; } = false;

        private int _downloadProgress;
        public int DownloadProgress
        {
            get { return _downloadProgress; }
            set
            {
                _downloadProgress = value;
                OnProgressChanged?.Invoke(value);
            }
        }

        public event StatusChangedEventHandler OnStatusChanged;

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnStatusChanged?.Invoke(value);
            }
        }

        public event EventHandler OnUpdateCompleate;

        public void BeginUpdate()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    Update();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    OnUpdateCompleate?.Invoke(this, EventArgs.Empty);
                }
            });
        }

        private void Update()
        {
            Status = "Считывание файла конфигурации...";

            string fileLink = Settings.Default.fileLink;

            if (Settings.Default.lastUpdateDate != default && WebHeaderHelper.GetFileCreateDate(fileLink) < Settings.Default.lastUpdateDate)
            {
                Status = "Установлена актуальная версия!";
                Thread.Sleep(2000);
                OnUpdateCompleate?.Invoke(this, EventArgs.Empty);
                return;
            }

            string fileName = fileLink.Split('/').Last();

            WebClient webClient = new WebClient();
            webClient.DownloadProgressChanged += ProgressChanged;

            Status = $"Скачивание файла: {fileName}";
            Task downloading = webClient.DownloadFileTaskAsync(fileLink, Application.StartupPath + "update.zip");

            while (!downloading.IsCompleted)
            {
                if (BreakUpdateFlag)
                {
                    webClient.CancelAsync();
                    OnUpdateCompleate?.Invoke(this, EventArgs.Empty);
                    return;
                }
                Thread.Sleep(500);
            }

            Thread.Sleep(500);
            Status = $"Установка...";
            DownloadProgress = 0;
            using (FileStream zipToOpen = new FileStream(Application.StartupPath + "update.zip", FileMode.Open))
            {
                var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read, false, Encoding.GetEncoding(System.Globalization.CultureInfo.CurrentCulture.TextInfo.OEMCodePage));

                double progressStep = 100 / archive.Entries.Count;
                int pointer = 0;

                foreach (ZipArchiveEntry archiveEntry in archive.Entries)
                {
                    if (BreakUpdateFlag)
                    {
                        OnUpdateCompleate?.Invoke(this, EventArgs.Empty);
                        return;
                    }

                    string fullPath = Path.Combine(Application.StartupPath, archiveEntry.FullName);
                    if (archiveEntry.Name == "")
                    {
                        Directory.CreateDirectory(fullPath);
                    }
                    else
                    {
                        archiveEntry.ExtractToFile(fullPath, true);
                    }
                    DownloadProgress = Convert.ToInt32(progressStep * (++pointer));
                }
            }

            DownloadProgress = 100;

            Settings.Default.lastUpdateDate = DateTime.Now;
            Settings.Default.Save();

            Status = $"Удаление временных файлов...";
            File.Delete(Application.StartupPath + "update.zip");

            Status = $"Установка завершена!";
            Thread.Sleep(2000);
            OnUpdateCompleate?.Invoke(this, EventArgs.Empty);
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownloadProgress = e.ProgressPercentage;
            Status = $"Скачивание файла: {Settings.Default.fileLink.Split('/').Last()} ({e.BytesReceived / 1048576}\\{e.TotalBytesToReceive / 1048576})MB";
        }

        public void EndUpdate()
        {
            BreakUpdateFlag = true;
        }
    }
}
