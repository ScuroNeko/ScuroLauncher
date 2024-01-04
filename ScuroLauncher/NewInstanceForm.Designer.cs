using System.ComponentModel;

namespace ScuroLauncher;

partial class NewInstanceForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        TabControl = new TabControl();
        TabPage1 = new TabPage();
        BetaCheckBox = new CheckBox();
        ReleasesCheckBox = new CheckBox();
        VersionSelector = new ListBox();
        Honkai = new Button();
        Hsr = new Button();
        Genshin = new Button();
        CreateInstance = new Button();
        ChooseFolder = new Button();
        InstanceName = new TextBox();
        InstancePath = new TextBox();
        TabPage2 = new TabPage();
        ImportInstance = new Button();
        ImportChooseFolder = new Button();
        ImportInstanceName = new TextBox();
        ImportInstancePath = new TextBox();
        TabControl.SuspendLayout();
        TabPage1.SuspendLayout();
        TabPage2.SuspendLayout();
        SuspendLayout();
        // 
        // TabControl
        // 
        TabControl.Controls.Add(TabPage1);
        TabControl.Controls.Add(TabPage2);
        TabControl.Dock = DockStyle.Fill;
        TabControl.Location = new Point(0, 0);
        TabControl.Name = "TabControl";
        TabControl.Padding = new Point(0, 0);
        TabControl.SelectedIndex = 0;
        TabControl.Size = new Size(384, 434);
        TabControl.TabIndex = 0;
        // 
        // TabPage1
        // 
        TabPage1.Controls.Add(BetaCheckBox);
        TabPage1.Controls.Add(ReleasesCheckBox);
        TabPage1.Controls.Add(VersionSelector);
        TabPage1.Controls.Add(Honkai);
        TabPage1.Controls.Add(Hsr);
        TabPage1.Controls.Add(Genshin);
        TabPage1.Controls.Add(CreateInstance);
        TabPage1.Controls.Add(ChooseFolder);
        TabPage1.Controls.Add(InstanceName);
        TabPage1.Controls.Add(InstancePath);
        TabPage1.Location = new Point(4, 27);
        TabPage1.Margin = new Padding(0);
        TabPage1.Name = "TabPage1";
        TabPage1.Size = new Size(376, 403);
        TabPage1.TabIndex = 0;
        TabPage1.Text = "New Instance";
        TabPage1.UseVisualStyleBackColor = true;
        // 
        // BetaCheckBox
        // 
        BetaCheckBox.AutoSize = true;
        BetaCheckBox.Location = new Point(273, 31);
        BetaCheckBox.Name = "BetaCheckBox";
        BetaCheckBox.Size = new Size(53, 22);
        BetaCheckBox.TabIndex = 15;
        BetaCheckBox.Text = "Beta";
        BetaCheckBox.UseVisualStyleBackColor = true;
        BetaCheckBox.CheckedChanged += BetaCheckBox_CheckedChanged;
        // 
        // ReleasesCheckBox
        // 
        ReleasesCheckBox.AutoSize = true;
        ReleasesCheckBox.Checked = true;
        ReleasesCheckBox.CheckState = CheckState.Checked;
        ReleasesCheckBox.Location = new Point(273, 3);
        ReleasesCheckBox.Name = "ReleasesCheckBox";
        ReleasesCheckBox.Size = new Size(77, 22);
        ReleasesCheckBox.TabIndex = 16;
        ReleasesCheckBox.Text = "Releases";
        ReleasesCheckBox.UseVisualStyleBackColor = true;
        ReleasesCheckBox.CheckedChanged += ReleasesCheckBox_CheckedChanged;
        // 
        // VersionSelector
        // 
        VersionSelector.BorderStyle = BorderStyle.None;
        VersionSelector.FormattingEnabled = true;
        VersionSelector.ItemHeight = 18;
        VersionSelector.Location = new Point(8, 6);
        VersionSelector.Name = "VersionSelector";
        VersionSelector.Size = new Size(262, 252);
        VersionSelector.TabIndex = 14;
        VersionSelector.SelectedIndexChanged += VersionSelector_SelectedIndexChanged;
        // 
        // Honkai
        // 
        Honkai.FlatAppearance.BorderSize = 0;
        Honkai.FlatStyle = FlatStyle.Flat;
        Honkai.Location = new Point(259, 322);
        Honkai.Name = "Honkai";
        Honkai.Size = new Size(117, 36);
        Honkai.TabIndex = 11;
        Honkai.Text = "Honkai";
        Honkai.UseVisualStyleBackColor = true;
        Honkai.Click += Honkai_Click;
        Honkai.EnabledChanged += Button_EnabledChanged;
        // 
        // Hsr
        // 
        Hsr.FlatAppearance.BorderSize = 0;
        Hsr.FlatStyle = FlatStyle.Flat;
        Hsr.Location = new Point(142, 322);
        Hsr.Name = "Hsr";
        Hsr.Size = new Size(105, 36);
        Hsr.TabIndex = 12;
        Hsr.Text = "HSR";
        Hsr.UseVisualStyleBackColor = true;
        Hsr.Click += Hsr_Click;
        Hsr.EnabledChanged += Button_EnabledChanged;
        // 
        // Genshin
        // 
        Genshin.FlatAppearance.BorderSize = 0;
        Genshin.FlatStyle = FlatStyle.Flat;
        Genshin.Location = new Point(0, 322);
        Genshin.Name = "Genshin";
        Genshin.Size = new Size(118, 36);
        Genshin.TabIndex = 13;
        Genshin.Text = "Genshin";
        Genshin.UseVisualStyleBackColor = true;
        Genshin.Click += Genshin_Click;
        Genshin.EnabledChanged += Button_EnabledChanged;
        // 
        // CreateInstance
        // 
        CreateInstance.Enabled = false;
        CreateInstance.FlatAppearance.BorderSize = 0;
        CreateInstance.FlatStyle = FlatStyle.Flat;
        CreateInstance.Location = new Point(202, 376);
        CreateInstance.Name = "CreateInstance";
        CreateInstance.Size = new Size(174, 24);
        CreateInstance.TabIndex = 10;
        CreateInstance.Text = "Create";
        CreateInstance.UseVisualStyleBackColor = true;
        CreateInstance.Click += CreateInstance_Click;
        // 
        // ChooseFolder
        // 
        ChooseFolder.FlatAppearance.BorderSize = 0;
        ChooseFolder.FlatStyle = FlatStyle.Flat;
        ChooseFolder.Location = new Point(0, 376);
        ChooseFolder.Name = "ChooseFolder";
        ChooseFolder.Size = new Size(174, 24);
        ChooseFolder.TabIndex = 9;
        ChooseFolder.Text = "Choose Folder";
        ChooseFolder.UseVisualStyleBackColor = true;
        ChooseFolder.Click += ChooseFolder_Click;
        // 
        // InstanceName
        // 
        InstanceName.BorderStyle = BorderStyle.None;
        InstanceName.Location = new Point(8, 264);
        InstanceName.Name = "InstanceName";
        InstanceName.PlaceholderText = "Instance name...";
        InstanceName.Size = new Size(362, 16);
        InstanceName.TabIndex = 7;
        InstanceName.TextChanged += InstanceName_TextChanged;
        // 
        // InstancePath
        // 
        InstancePath.BorderStyle = BorderStyle.None;
        InstancePath.Location = new Point(8, 286);
        InstancePath.Name = "InstancePath";
        InstancePath.PlaceholderText = "Instance folder path...";
        InstancePath.Size = new Size(362, 16);
        InstancePath.TabIndex = 8;
        InstancePath.TextChanged += InstancePath_TextChanged;
        // 
        // TabPage2
        // 
        TabPage2.Controls.Add(ImportInstance);
        TabPage2.Controls.Add(ImportChooseFolder);
        TabPage2.Controls.Add(ImportInstanceName);
        TabPage2.Controls.Add(ImportInstancePath);
        TabPage2.Location = new Point(4, 27);
        TabPage2.Name = "TabPage2";
        TabPage2.Padding = new Padding(3);
        TabPage2.Size = new Size(376, 403);
        TabPage2.TabIndex = 1;
        TabPage2.Text = "Import Instance";
        TabPage2.UseVisualStyleBackColor = true;
        // 
        // ImportInstance
        // 
        ImportInstance.Enabled = false;
        ImportInstance.FlatAppearance.BorderSize = 0;
        ImportInstance.FlatStyle = FlatStyle.Flat;
        ImportInstance.Location = new Point(196, 50);
        ImportInstance.Name = "ImportInstance";
        ImportInstance.Size = new Size(174, 24);
        ImportInstance.TabIndex = 14;
        ImportInstance.Text = "Import";
        ImportInstance.UseVisualStyleBackColor = true;
        ImportInstance.Click += ImportInstance_Click;
        // 
        // ImportChooseFolder
        // 
        ImportChooseFolder.FlatAppearance.BorderSize = 0;
        ImportChooseFolder.FlatStyle = FlatStyle.Flat;
        ImportChooseFolder.Location = new Point(8, 50);
        ImportChooseFolder.Name = "ImportChooseFolder";
        ImportChooseFolder.Size = new Size(174, 24);
        ImportChooseFolder.TabIndex = 13;
        ImportChooseFolder.Text = "Choose Folder";
        ImportChooseFolder.UseVisualStyleBackColor = true;
        ImportChooseFolder.Click += ImportChooseFolder_Click;
        // 
        // ImportInstanceName
        // 
        ImportInstanceName.BorderStyle = BorderStyle.None;
        ImportInstanceName.Location = new Point(6, 6);
        ImportInstanceName.Name = "ImportInstanceName";
        ImportInstanceName.PlaceholderText = "Instance name...";
        ImportInstanceName.Size = new Size(362, 16);
        ImportInstanceName.TabIndex = 11;
        ImportInstanceName.TextChanged += ImportInstanceName_TextChanged;
        // 
        // ImportInstancePath
        // 
        ImportInstancePath.BorderStyle = BorderStyle.None;
        ImportInstancePath.Location = new Point(6, 28);
        ImportInstancePath.Name = "ImportInstancePath";
        ImportInstancePath.PlaceholderText = "Instance folder path...";
        ImportInstancePath.Size = new Size(362, 16);
        ImportInstancePath.TabIndex = 12;
        ImportInstancePath.TextChanged += ImportInstancePath_TextChanged;
        // 
        // NewInstanceForm
        // 
        AutoScaleDimensions = new SizeF(7F, 18F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(384, 434);
        Controls.Add(TabControl);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Name = "NewInstanceForm";
        Text = "Create Instance";
        TabControl.ResumeLayout(false);
        TabPage1.ResumeLayout(false);
        TabPage1.PerformLayout();
        TabPage2.ResumeLayout(false);
        TabPage2.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private TabControl TabControl;
    private TabPage TabPage1;
    private CheckBox BetaCheckBox;
    private CheckBox ReleasesCheckBox;
    private ListBox VersionSelector;
    private Button Honkai;
    private Button Hsr;
    private Button Genshin;
    private Button CreateInstance;
    private Button ChooseFolder;
    private TextBox InstanceName;
    private TextBox InstancePath;
    private Button ImportInstance;
    private Button ImportChooseFolder;
    private TextBox ImportInstanceName;
    private TextBox ImportInstancePath;
    public TabPage TabPage2;
}