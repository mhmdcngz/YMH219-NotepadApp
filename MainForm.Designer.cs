namespace YMH219_NotepadApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStripMain = new MenuStrip();
            mnuFile = new ToolStripMenuItem();
            mnuNew = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            mnuExport = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            mnuExit = new ToolStripMenuItem();
            mnuEdit = new ToolStripMenuItem();
            mnuDelete = new ToolStripMenuItem();
            statusStripMain = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            menuStripMain.SuspendLayout();
            statusStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // menuStripMain
            // 
            menuStripMain.Items.AddRange(new ToolStripItem[] { mnuFile, mnuEdit });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new Size(884, 24);
            menuStripMain.TabIndex = 0;
            menuStripMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuNew, toolStripMenuItem1, mnuExport, toolStripMenuItem2, mnuExit });
            mnuFile.Name = "mnuFile";
            mnuFile.Size = new Size(51, 20);
            mnuFile.Text = "Dosya";
            // 
            // mnuNew
            // 
            mnuNew.Name = "mnuNew";
            mnuNew.ShortcutKeys = Keys.Control | Keys.N;
            mnuNew.Size = new Size(223, 22);
            mnuNew.Text = "Yeni Not";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(220, 6);
            // 
            // mnuExport
            // 
            mnuExport.Name = "mnuExport";
            mnuExport.ShortcutKeys = Keys.Control | Keys.E;
            mnuExport.Size = new Size(223, 22);
            mnuExport.Text = ".txt Olarak Dışa Aktar";
            mnuExport.Click += txtOlarakDışaAktarToolStripMenuItem_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(220, 6);
            // 
            // mnuExit
            // 
            mnuExit.Name = "mnuExit";
            mnuExit.Size = new Size(223, 22);
            mnuExit.Text = "Çıkış";
            // 
            // mnuEdit
            // 
            mnuEdit.DropDownItems.AddRange(new ToolStripItem[] { mnuDelete });
            mnuEdit.Name = "mnuEdit";
            mnuEdit.Size = new Size(52, 20);
            mnuEdit.Text = "Düzen";
            // 
            // mnuDelete
            // 
            mnuDelete.Name = "mnuDelete";
            mnuDelete.Size = new Size(116, 22);
            mnuDelete.Text = "Notu Sil";
            // 
            // statusStripMain
            // 
            statusStripMain.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStripMain.Location = new Point(0, 489);
            statusStripMain.Name = "statusStripMain";
            statusStripMain.Size = new Size(884, 22);
            statusStripMain.TabIndex = 1;
            statusStripMain.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(34, 17);
            lblStatus.Text = "Hazır";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 511);
            Controls.Add(statusStripMain);
            Controls.Add(menuStripMain);
            MainMenuStrip = menuStripMain;
            MinimumSize = new Size(700, 400);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Not Defteri";
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            statusStripMain.ResumeLayout(false);
            statusStripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStripMain;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuNew;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem mnuExport;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem mnuExit;
        private ToolStripMenuItem mnuEdit;
        private ToolStripMenuItem mnuDelete;
        private StatusStrip statusStripMain;
        private ToolStripStatusLabel lblStatus;
    }
}
