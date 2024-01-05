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
            InstanceName = new TextBox();
            InstanceImage = new PictureBox();
            UseProxy = new CheckBox();
            ProxyUrl = new TextBox();
            Save = new Button();
            InstancePath = new TextBox();
            ((System.ComponentModel.ISupportInitialize)InstanceImage).BeginInit();
            SuspendLayout();
            // 
            // InstanceName
            // 
            InstanceName.BackColor = SystemColors.Window;
            InstanceName.BorderStyle = BorderStyle.None;
            InstanceName.Location = new Point(146, 12);
            InstanceName.Name = "InstanceName";
            InstanceName.PlaceholderText = "Instance Name";
            InstanceName.Size = new Size(208, 16);
            InstanceName.TabIndex = 5;
            // 
            // InstanceImage
            // 
            InstanceImage.Location = new Point(12, 12);
            InstanceImage.Name = "InstanceImage";
            InstanceImage.Size = new Size(128, 128);
            InstanceImage.SizeMode = PictureBoxSizeMode.StretchImage;
            InstanceImage.TabIndex = 3;
            InstanceImage.TabStop = false;
            InstanceImage.Click += InstanceImage_Click;
            // 
            // UseProxy
            // 
            UseProxy.AutoSize = true;
            UseProxy.Location = new Point(146, 56);
            UseProxy.Name = "UseProxy";
            UseProxy.Size = new Size(88, 22);
            UseProxy.TabIndex = 6;
            UseProxy.Text = "Use Proxy?";
            UseProxy.UseVisualStyleBackColor = true;
            UseProxy.CheckedChanged += UseProxy_CheckedChanged;
            // 
            // ProxyUrl
            // 
            ProxyUrl.BorderStyle = BorderStyle.None;
            ProxyUrl.Location = new Point(146, 84);
            ProxyUrl.Name = "ProxyUrl";
            ProxyUrl.PlaceholderText = "Proxy URL";
            ProxyUrl.Size = new Size(208, 16);
            ProxyUrl.TabIndex = 7;
            ProxyUrl.TextChanged += ProxyUrl_TextChanged;
            // 
            // Save
            // 
            Save.BackColor = SystemColors.Control;
            Save.FlatAppearance.BorderSize = 0;
            Save.FlatStyle = FlatStyle.Flat;
            Save.Location = new Point(279, 117);
            Save.Name = "Save";
            Save.Size = new Size(75, 23);
            Save.TabIndex = 8;
            Save.Text = "Save";
            Save.UseVisualStyleBackColor = false;
            Save.Click += Save_Click;
            // 
            // InstancePath
            // 
            InstancePath.BackColor = SystemColors.Window;
            InstancePath.BorderStyle = BorderStyle.None;
            InstancePath.Location = new Point(146, 34);
            InstancePath.Name = "InstancePath";
            InstancePath.PlaceholderText = "Instance Path";
            InstancePath.Size = new Size(208, 16);
            InstancePath.TabIndex = 5;
            InstancePath.TextChanged += InstancePath_TextChanged;
            // 
            // EditInstanceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(366, 151);
            Controls.Add(Save);
            Controls.Add(ProxyUrl);
            Controls.Add(UseProxy);
            Controls.Add(InstancePath);
            Controls.Add(InstanceName);
            Controls.Add(InstanceImage);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "EditInstanceForm";
            Text = "Edit Instance";
            ((System.ComponentModel.ISupportInitialize)InstanceImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox InstanceName;
        private PictureBox InstanceImage;
        private CheckBox UseProxy;
        private TextBox ProxyUrl;
        private Button Save;
        private TextBox InstancePath;
    }
}