namespace ScuroLauncher
{
    partial class EditInstanceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            SplitContainer = new SplitContainer();
            Main = new Button();
            MainSettings = new Panel();
            InstanceName = new TextBox();
            Save = new Button();
            InstanceImage = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)SplitContainer).BeginInit();
            SplitContainer.Panel1.SuspendLayout();
            SplitContainer.Panel2.SuspendLayout();
            SplitContainer.SuspendLayout();
            MainSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)InstanceImage).BeginInit();
            SuspendLayout();
            // 
            // SplitContainer
            // 
            SplitContainer.Dock = DockStyle.Fill;
            SplitContainer.FixedPanel = FixedPanel.Panel1;
            SplitContainer.Location = new Point(0, 0);
            SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            SplitContainer.Panel1.Controls.Add(Main);
            // 
            // SplitContainer.Panel2
            // 
            SplitContainer.Panel2.Controls.Add(MainSettings);
            SplitContainer.Size = new Size(648, 450);
            SplitContainer.SplitterDistance = 191;
            SplitContainer.SplitterWidth = 2;
            SplitContainer.TabIndex = 0;
            // 
            // Main
            // 
            Main.FlatAppearance.BorderSize = 0;
            Main.FlatStyle = FlatStyle.Flat;
            Main.Font = new Font("Segoe UI Semilight", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Main.Location = new Point(0, 0);
            Main.Name = "Main";
            Main.Size = new Size(192, 30);
            Main.TabIndex = 0;
            Main.Text = "Main";
            Main.TextAlign = ContentAlignment.MiddleLeft;
            Main.UseVisualStyleBackColor = true;
            // 
            // MainSettings
            // 
            MainSettings.Controls.Add(InstanceName);
            MainSettings.Controls.Add(Save);
            MainSettings.Controls.Add(InstanceImage);
            MainSettings.Dock = DockStyle.Fill;
            MainSettings.Location = new Point(0, 0);
            MainSettings.Name = "MainSettings";
            MainSettings.Size = new Size(455, 450);
            MainSettings.TabIndex = 0;
            // 
            // InstanceName
            // 
            InstanceName.Location = new Point(12, 146);
            InstanceName.Name = "InstanceName";
            InstanceName.Size = new Size(128, 23);
            InstanceName.TabIndex = 2;
            InstanceName.TextChanged += InstanceName_TextChanged;
            // 
            // Save
            // 
            Save.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Save.Location = new Point(368, 415);
            Save.Name = "Save";
            Save.Size = new Size(75, 23);
            Save.TabIndex = 1;
            Save.Text = "Save";
            Save.UseVisualStyleBackColor = true;
            Save.Click += Save_Click;
            // 
            // InstanceImage
            // 
            InstanceImage.Location = new Point(12, 12);
            InstanceImage.Name = "InstanceImage";
            InstanceImage.Size = new Size(128, 128);
            InstanceImage.TabIndex = 0;
            InstanceImage.TabStop = false;
            // 
            // EditInstanceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(648, 450);
            Controls.Add(SplitContainer);
            Name = "EditInstanceForm";
            Text = "EditInstanceForm";
            SplitContainer.Panel1.ResumeLayout(false);
            SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer).EndInit();
            SplitContainer.ResumeLayout(false);
            MainSettings.ResumeLayout(false);
            MainSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)InstanceImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer SplitContainer;
        private Button Main;
        private Panel MainSettings;
        private PictureBox InstanceImage;
        private Button Save;
        private TextBox InstanceName;
    }
}