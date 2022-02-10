
namespace Updater.Views
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.updatingStatusLabel = new System.Windows.Forms.Label();
            this.updateControllerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.updateControllerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.progressBar.Location = new System.Drawing.Point(12, 54);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(379, 23);
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 0;
            // 
            // updatingStatusLabel
            // 
            this.updatingStatusLabel.AutoSize = true;
            this.updatingStatusLabel.Location = new System.Drawing.Point(12, 38);
            this.updatingStatusLabel.Name = "updatingStatusLabel";
            this.updatingStatusLabel.Size = new System.Drawing.Size(43, 13);
            this.updatingStatusLabel.TabIndex = 1;
            this.updatingStatusLabel.Text = "Starting";
            // 
            // updateControllerBindingSource
            // 
            this.updateControllerBindingSource.DataSource = typeof(Updater.Controllers.UpdateController);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 121);
            this.Controls.Add(this.updatingStatusLabel);
            this.Controls.Add(this.progressBar);
            this.MaximumSize = new System.Drawing.Size(420, 160);
            this.MinimumSize = new System.Drawing.Size(420, 160);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updating";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.updateControllerBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label updatingStatusLabel;
        private System.Windows.Forms.BindingSource updateControllerBindingSource;
    }
}

