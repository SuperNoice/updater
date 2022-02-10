using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Updater.Views;

namespace Updater
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] argv)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm updateForm = new MainForm();

            for (int pointer = 0; pointer < argv.Length; pointer++)
            {
                switch (argv[pointer])
                {
                    case "-killproc":
                        if (pointer + 1 < argv.Length && int.TryParse(argv[pointer + 1], out int procId))
                        {
                            var proc = Process.GetProcessById(procId);
                            var filePath = proc.MainModule.FileName;
                            proc.Kill();
                            updateForm = new MainForm(filePath);
                        }
                        break;

                    case "-reinstall":
                        Properties.Settings.Default.lastUpdateDate = DateTime.MinValue;
                        Properties.Settings.Default.Save();

                        var dirInfo = new DirectoryInfo(Application.StartupPath);
                        var files = dirInfo.GetFiles();
                        var dirs = dirInfo.GetDirectories();

                        foreach (var dir in dirs)
                        {
                            try
                            {
                                Directory.Delete(dir.FullName, true);
                            }
                            catch (Exception) { }
                        }

                        var whiteList = new string[] { "updater.exe", "updater.exe.config" };
                        foreach (var file in files)
                        {
                            if (!whiteList.Contains(file.Name))
                            {
                                try
                                {
                                    file.Delete();
                                }
                                catch (Exception) { }
                            }
                        }

                        break;

                    default:
                        break;
                }
            }

            Application.Run(updateForm);
        }
    }
}