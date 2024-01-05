namespace ScuroLauncher;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        Panel = new Panel();
        SplitContainer = new SplitContainer();
        Kill = new Button();
        DeleteInstance = new Button();
        CheckUpdates = new Button();
        EditInstance = new Button();
        LaunchInstance = new Button();
        InstanceVersion = new Label();
        InstanceName = new Label();
        InstanceImage = new PictureBox();
        NewInstance = new Button();
        InstanceList = new ListView();
        MainMenu = new MenuStrip();
        FileCategoryMenuItem = new ToolStripMenuItem();
        EditCategoryMenuItem = new ToolStripMenuItem();
        SettingsToolMenuItem = new ToolStripMenuItem();
        downloadsManagerToolStripMenuItem = new ToolStripMenuItem();
        aboutMenuItem = new ToolStripMenuItem();
        Panel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)SplitContainer).BeginInit();
        SplitContainer.Panel1.SuspendLayout();
        SplitContainer.Panel2.SuspendLayout();
        SplitContainer.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)InstanceImage).BeginInit();
        MainMenu.SuspendLayout();
        SuspendLayout();
        // 
        // Panel
        // 
        Panel.Controls.Add(SplitContainer);
        Panel.Controls.Add(MainMenu);
        Panel.Dock = DockStyle.Fill;
        Panel.Location = new Point(0, 0);
        Panel.Name = "Panel";
        Panel.Size = new Size(623, 417);
        Panel.TabIndex = 0;
        // 
        // SplitContainer
        // 
        SplitContainer.Dock = DockStyle.Fill;
        SplitContainer.FixedPanel = FixedPanel.Panel1;
        SplitContainer.IsSplitterFixed = true;
        SplitContainer.Location = new Point(0, 26);
        SplitContainer.Name = "SplitContainer";
        // 
        // SplitContainer.Panel1
        // 
        SplitContainer.Panel1.Controls.Add(Kill);
        SplitContainer.Panel1.Controls.Add(DeleteInstance);
        SplitContainer.Panel1.Controls.Add(CheckUpdates);
        SplitContainer.Panel1.Controls.Add(EditInstance);
        SplitContainer.Panel1.Controls.Add(LaunchInstance);
        SplitContainer.Panel1.Controls.Add(InstanceVersion);
        SplitContainer.Panel1.Controls.Add(InstanceName);
        SplitContainer.Panel1.Controls.Add(InstanceImage);
        SplitContainer.Panel1.Controls.Add(NewInstance);
        // 
        // SplitContainer.Panel2
        // 
        SplitContainer.Panel2.Controls.Add(InstanceList);
        SplitContainer.Size = new Size(623, 391);
        SplitContainer.SplitterDistance = 152;
        SplitContainer.SplitterWidth = 1;
        SplitContainer.TabIndex = 1;
        // 
        // Kill
        // 
        Kill.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        Kill.FlatAppearance.BorderSize = 0;
        Kill.FlatStyle = FlatStyle.Flat;
        Kill.Location = new Point(12, 185);
        Kill.Name = "Kill";
        Kill.Size = new Size(128, 23);
        Kill.TabIndex = 6;
        Kill.Text = "Kill";
        Kill.UseVisualStyleBackColor = true;
        Kill.Visible = false;
        Kill.Click += Kill_Click;
        // 
        // DeleteInstance
        // 
        DeleteInstance.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        DeleteInstance.FlatAppearance.BorderSize = 0;
        DeleteInstance.FlatStyle = FlatStyle.Flat;
        DeleteInstance.Location = new Point(12, 272);
        DeleteInstance.Name = "DeleteInstance";
        DeleteInstance.Size = new Size(128, 23);
        DeleteInstance.TabIndex = 5;
        DeleteInstance.Text = "Delete";
        DeleteInstance.UseVisualStyleBackColor = true;
        DeleteInstance.Visible = false;
        DeleteInstance.Click += DeleteInstance_Click;
        // 
        // CheckUpdates
        // 
        CheckUpdates.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        CheckUpdates.FlatAppearance.BorderSize = 0;
        CheckUpdates.FlatStyle = FlatStyle.Flat;
        CheckUpdates.Location = new Point(12, 243);
        CheckUpdates.Name = "CheckUpdates";
        CheckUpdates.Size = new Size(128, 23);
        CheckUpdates.TabIndex = 5;
        CheckUpdates.Text = "Check for Updates";
        CheckUpdates.UseVisualStyleBackColor = true;
        CheckUpdates.Visible = false;
        CheckUpdates.Click += CheckUpdates_Click;
        // 
        // EditInstance
        // 
        EditInstance.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        EditInstance.FlatAppearance.BorderSize = 0;
        EditInstance.FlatStyle = FlatStyle.Flat;
        EditInstance.Location = new Point(12, 214);
        EditInstance.Name = "EditInstance";
        EditInstance.Size = new Size(128, 23);
        EditInstance.TabIndex = 4;
        EditInstance.Text = "Edit instance";
        EditInstance.UseVisualStyleBackColor = true;
        EditInstance.Visible = false;
        EditInstance.Click += EditInstance_Click;
        // 
        // LaunchInstance
        // 
        LaunchInstance.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        LaunchInstance.FlatAppearance.BorderSize = 0;
        LaunchInstance.FlatStyle = FlatStyle.Flat;
        LaunchInstance.Location = new Point(12, 185);
        LaunchInstance.Name = "LaunchInstance";
        LaunchInstance.Size = new Size(128, 23);
        LaunchInstance.TabIndex = 3;
        LaunchInstance.Text = "Launch";
        LaunchInstance.UseVisualStyleBackColor = true;
        LaunchInstance.Visible = false;
        LaunchInstance.Click += Launch_Click;
        // 
        // InstanceVersion
        // 
        InstanceVersion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        InstanceVersion.Font = new Font("Segoe UI Semilight", 9F, FontStyle.Italic, GraphicsUnit.Point, 204);
        InstanceVersion.Location = new Point(12, 164);
        InstanceVersion.Name = "InstanceVersion";
        InstanceVersion.Size = new Size(128, 18);
        InstanceVersion.TabIndex = 2;
        InstanceVersion.Text = "InstanceVersion";
        InstanceVersion.Visible = false;
        // 
        // InstanceName
        // 
        InstanceName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        InstanceName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        InstanceName.Location = new Point(12, 146);
        InstanceName.Name = "InstanceName";
        InstanceName.Size = new Size(128, 18);
        InstanceName.TabIndex = 2;
        InstanceName.Text = "InstanceName";
        InstanceName.Visible = false;
        // 
        // InstanceImage
        // 
        InstanceImage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        InstanceImage.Location = new Point(12, 12);
        InstanceImage.Name = "InstanceImage";
        InstanceImage.Size = new Size(128, 128);
        InstanceImage.SizeMode = PictureBoxSizeMode.StretchImage;
        InstanceImage.TabIndex = 1;
        InstanceImage.TabStop = false;
        // 
        // NewInstance
        // 
        NewInstance.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        NewInstance.AutoSize = true;
        NewInstance.FlatAppearance.BorderSize = 0;
        NewInstance.FlatStyle = FlatStyle.Flat;
        NewInstance.Location = new Point(12, 346);
        NewInstance.Margin = new Padding(0);
        NewInstance.Name = "NewInstance";
        NewInstance.Padding = new Padding(4);
        NewInstance.Size = new Size(128, 36);
        NewInstance.TabIndex = 0;
        NewInstance.Text = "New Instance";
        NewInstance.UseVisualStyleBackColor = true;
        NewInstance.Click += NewInstance_Click;
        // 
        // InstanceList
        // 
        InstanceList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        InstanceList.BorderStyle = BorderStyle.None;
        InstanceList.GridLines = true;
        InstanceList.Location = new Point(3, 0);
        InstanceList.MultiSelect = false;
        InstanceList.Name = "InstanceList";
        InstanceList.Size = new Size(476, 391);
        InstanceList.TabIndex = 0;
        InstanceList.UseCompatibleStateImageBehavior = false;
        InstanceList.SelectedIndexChanged += InstanceList_SelectedIndexChanged;
        InstanceList.MouseDown += InstanceList_MouseClick;
        // 
        // MainMenu
        // 
        MainMenu.Items.AddRange(new ToolStripItem[] { FileCategoryMenuItem, EditCategoryMenuItem, downloadsManagerToolStripMenuItem, aboutMenuItem });
        MainMenu.Location = new Point(0, 0);
        MainMenu.Name = "MainMenu";
        MainMenu.Size = new Size(623, 26);
        MainMenu.TabIndex = 0;
        MainMenu.Text = "menuStrip1";
        // 
        // FileCategoryMenuItem
        // 
        FileCategoryMenuItem.Name = "FileCategoryMenuItem";
        FileCategoryMenuItem.Size = new Size(39, 22);
        FileCategoryMenuItem.Text = "File";
        // 
        // EditCategoryMenuItem
        // 
        EditCategoryMenuItem.DropDownItems.AddRange(new ToolStripItem[] { SettingsToolMenuItem });
        EditCategoryMenuItem.Name = "EditCategoryMenuItem";
        EditCategoryMenuItem.Size = new Size(41, 22);
        EditCategoryMenuItem.Text = "Edit";
        // 
        // SettingsToolMenuItem
        // 
        SettingsToolMenuItem.Name = "SettingsToolMenuItem";
        SettingsToolMenuItem.Size = new Size(121, 22);
        SettingsToolMenuItem.Text = "Settings";
        SettingsToolMenuItem.Click += SettingsToolMenuItem_Click;
        // 
        // downloadsManagerToolStripMenuItem
        // 
        downloadsManagerToolStripMenuItem.Name = "downloadsManagerToolStripMenuItem";
        downloadsManagerToolStripMenuItem.Size = new Size(136, 22);
        downloadsManagerToolStripMenuItem.Text = "Downloads manager";
        downloadsManagerToolStripMenuItem.Click += DownloadsManagerToolStripMenuItem_Click;
        // 
        // aboutMenuItem
        // 
        aboutMenuItem.Name = "aboutMenuItem";
        aboutMenuItem.Size = new Size(53, 22);
        aboutMenuItem.Text = "About";
        aboutMenuItem.Click += AboutMenuItem_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 18F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(623, 417);
        Controls.Add(Panel);
        MainMenuStrip = MainMenu;
        MinimumSize = new Size(639, 456);
        Name = "MainForm";
        Text = "Scuro Launcher";
        Panel.ResumeLayout(false);
        Panel.PerformLayout();
        SplitContainer.Panel1.ResumeLayout(false);
        SplitContainer.Panel1.PerformLayout();
        SplitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)SplitContainer).EndInit();
        SplitContainer.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)InstanceImage).EndInit();
        MainMenu.ResumeLayout(false);
        MainMenu.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel Panel;
    private SplitContainer SplitContainer;
    private Button NewInstance;
    private ListView InstanceList;
    private MenuStrip MainMenu;
    private ToolStripMenuItem FileCategoryMenuItem;
    private ToolStripMenuItem EditCategoryMenuItem;
    private ToolStripMenuItem SettingsToolMenuItem;
    private PictureBox InstanceImage;
    private Label InstanceName;
    private Button LaunchInstance;
    private Button EditInstance;
    private Button CheckUpdates;
    private Label InstanceVersion;
    private ToolStripMenuItem downloadsManagerToolStripMenuItem;
    private Button DeleteInstance;
    private Button Kill;
    private ToolStripMenuItem aboutMenuItem;
}