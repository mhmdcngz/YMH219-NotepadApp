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
            splitMain = new SplitContainer();
            lstNotes = new ListBox();
            flpListButtons = new FlowLayoutPanel();
            btnNew = new Button();
            btnDelete = new Button();
            txtSearch = new TextBox();
            txtTitle = new TextBox();
            lblDates = new Label();
            flpEditorButtons = new FlowLayoutPanel();
            btnExport = new Button();
            btnSave = new Button();
            rtbContent = new RichTextBox();
            menuStripMain.SuspendLayout();
            statusStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            flpListButtons.SuspendLayout();
            flpEditorButtons.SuspendLayout();
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
            // splitMain
            // 
            splitMain.Dock = DockStyle.Fill;
            splitMain.FixedPanel = FixedPanel.Panel1;
            splitMain.Location = new Point(0, 24);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(lstNotes);
            splitMain.Panel1.Controls.Add(flpListButtons);
            splitMain.Panel1.Controls.Add(txtSearch);
            splitMain.Panel1.Padding = new Padding(6);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(rtbContent);
            splitMain.Panel2.Controls.Add(flpEditorButtons);
            splitMain.Panel2.Controls.Add(lblDates);
            splitMain.Panel2.Controls.Add(txtTitle);
            splitMain.Panel2.Padding = new Padding(6);
            splitMain.Size = new Size(884, 465);
            splitMain.SplitterDistance = 260;
            splitMain.TabIndex = 2;
            // 
            // lstNotes
            // 
            lstNotes.Dock = DockStyle.Fill;
            lstNotes.FormattingEnabled = true;
            lstNotes.IntegralHeight = false;
            lstNotes.Location = new Point(6, 29);
            lstNotes.Name = "lstNotes";
            lstNotes.Size = new Size(248, 390);
            lstNotes.TabIndex = 2;
            // 
            // flpListButtons
            // 
            flpListButtons.Controls.Add(btnNew);
            flpListButtons.Controls.Add(btnDelete);
            flpListButtons.Dock = DockStyle.Bottom;
            flpListButtons.Location = new Point(6, 419);
            flpListButtons.Name = "flpListButtons";
            flpListButtons.Size = new Size(248, 40);
            flpListButtons.TabIndex = 1;
            // 
            // btnNew
            // 
            btnNew.Location = new Point(3, 3);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(75, 23);
            btnNew.TabIndex = 0;
            btnNew.Text = "Yeni";
            btnNew.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(84, 3);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "Sil";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.Dock = DockStyle.Top;
            txtSearch.Location = new Point(6, 6);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Başlıkta veya içerikte ara...";
            txtSearch.Size = new Size(248, 23);
            txtSearch.TabIndex = 0;
            // 
            // txtTitle
            // 
            txtTitle.Dock = DockStyle.Top;
            txtTitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 162);
            txtTitle.Location = new Point(6, 6);
            txtTitle.Name = "txtTitle";
            txtTitle.PlaceholderText = "Başlık";
            txtTitle.Size = new Size(608, 29);
            txtTitle.TabIndex = 0;
            // 
            // lblDates
            // 
            lblDates.Dock = DockStyle.Top;
            lblDates.Location = new Point(6, 35);
            lblDates.Name = "lblDates";
            lblDates.Size = new Size(608, 24);
            lblDates.TabIndex = 1;
            lblDates.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flpEditorButtons
            // 
            flpEditorButtons.Controls.Add(btnSave);
            flpEditorButtons.Controls.Add(btnExport);
            flpEditorButtons.Dock = DockStyle.Bottom;
            flpEditorButtons.FlowDirection = FlowDirection.RightToLeft;
            flpEditorButtons.Location = new Point(6, 419);
            flpEditorButtons.Name = "flpEditorButtons";
            flpEditorButtons.Size = new Size(608, 40);
            flpEditorButtons.TabIndex = 2;
            // 
            // btnExport
            // 
            btnExport.AutoSize = true;
            btnExport.Location = new Point(435, 3);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(89, 25);
            btnExport.TabIndex = 0;
            btnExport.Text = ".txt Dışa Aktar";
            btnExport.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(530, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 1;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // rtbContent
            // 
            rtbContent.Dock = DockStyle.Fill;
            rtbContent.Location = new Point(6, 59);
            rtbContent.Name = "rtbContent";
            rtbContent.Size = new Size(608, 360);
            rtbContent.TabIndex = 3;
            rtbContent.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 511);
            Controls.Add(splitMain);
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
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel1.PerformLayout();
            splitMain.Panel2.ResumeLayout(false);
            splitMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            flpListButtons.ResumeLayout(false);
            flpEditorButtons.ResumeLayout(false);
            flpEditorButtons.PerformLayout();
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
        private SplitContainer splitMain;
        private TextBox txtSearch;
        private FlowLayoutPanel flpListButtons;
        private Button btnNew;
        private Button btnDelete;
        private ListBox lstNotes;
        private Label lblDates;
        private TextBox txtTitle;
        private RichTextBox rtbContent;
        private FlowLayoutPanel flpEditorButtons;
        private Button btnSave;
        private Button btnExport;
    }
}
