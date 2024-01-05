namespace ScuroLauncher
{
    partial class AboutWindow
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
            Title = new Label();
            Developer = new Label();
            dotnet = new Label();
            os = new Label();
            SuspendLayout();
            // 
            // Title
            // 
            Title.AutoSize = true;
            Title.Font = new Font("Segoe UI Semilight", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Title.Location = new Point(12, 9);
            Title.Name = "Title";
            Title.Size = new Size(150, 30);
            Title.TabIndex = 0;
            Title.Text = "ScuroLauncher";
            // 
            // Developer
            // 
            Developer.AutoSize = true;
            Developer.Location = new Point(12, 39);
            Developer.Name = "Developer";
            Developer.Size = new Size(133, 18);
            Developer.TabIndex = 1;
            Developer.Text = "Developer: ScuroNeko";
            // 
            // dotnet
            // 
            dotnet.AutoSize = true;
            dotnet.Location = new Point(12, 57);
            dotnet.Name = "dotnet";
            dotnet.Size = new Size(84, 18);
            dotnet.TabIndex = 2;
            dotnet.Text = ".NET version: ";
            // 
            // os
            // 
            os.AutoSize = true;
            os.Location = new Point(12, 75);
            os.Name = "os";
            os.Size = new Size(74, 18);
            os.TabIndex = 2;
            os.Text = "OS version: ";
            // 
            // AboutWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(428, 144);
            Controls.Add(os);
            Controls.Add(dotnet);
            Controls.Add(Developer);
            Controls.Add(Title);
            Name = "AboutWindow";
            Text = "About";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Title;
        private Label Developer;
        private Label dotnet;
        private Label os;
    }
}