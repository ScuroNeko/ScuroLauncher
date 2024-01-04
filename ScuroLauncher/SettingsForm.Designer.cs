namespace ScuroLauncher
{
    partial class SettingsForm
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
            ThemeComboBox = new ComboBox();
            Save = new Button();
            SuspendLayout();
            // 
            // ThemeComboBox
            // 
            ThemeComboBox.FlatStyle = FlatStyle.Flat;
            ThemeComboBox.FormattingEnabled = true;
            ThemeComboBox.Location = new Point(12, 12);
            ThemeComboBox.Name = "ThemeComboBox";
            ThemeComboBox.Size = new Size(249, 26);
            ThemeComboBox.TabIndex = 0;
            ThemeComboBox.Text = "Default";
            ThemeComboBox.SelectedIndexChanged += ThemeComboBoxSelectedIndexChanged;
            // 
            // Save
            // 
            Save.AutoSize = true;
            Save.FlatStyle = FlatStyle.Flat;
            Save.Location = new Point(167, 88);
            Save.Margin = new Padding(0);
            Save.Name = "Save";
            Save.Size = new Size(94, 38);
            Save.TabIndex = 1;
            Save.Text = "Save";
            Save.UseVisualStyleBackColor = true;
            Save.Click += Save_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(270, 135);
            Controls.Add(Save);
            Controls.Add(ThemeComboBox);
            Name = "SettingsForm";
            Text = "Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox ThemeComboBox;
        private Button Save;
    }
}