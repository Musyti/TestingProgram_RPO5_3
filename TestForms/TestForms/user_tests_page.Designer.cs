namespace TestForms
{
    partial class user_tests_page
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(user_tests_page));
            user_profile_button = new Button();
            tests_empty_label = new Label();
            SuspendLayout();
            // 
            // user_profile_button
            // 
            user_profile_button.BackgroundImage = (Image)resources.GetObject("user_profile_button.BackgroundImage");
            user_profile_button.FlatStyle = FlatStyle.Flat;
            user_profile_button.ForeColor = SystemColors.ScrollBar;
            user_profile_button.Location = new Point(1282, 12);
            user_profile_button.Name = "user_profile_button";
            user_profile_button.Size = new Size(85, 85);
            user_profile_button.TabIndex = 2;
            user_profile_button.UseVisualStyleBackColor = true;
            // 
            // tests_empty_label
            // 
            tests_empty_label.AutoSize = true;
            tests_empty_label.Font = new Font("Rubik", 50.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tests_empty_label.ForeColor = SystemColors.ActiveCaptionText;
            tests_empty_label.Location = new Point(44, 224);
            tests_empty_label.Name = "tests_empty_label";
            tests_empty_label.Size = new Size(1285, 80);
            tests_empty_label.TabIndex = 3;
            tests_empty_label.Text = "На данный момент тесты отсутствуют";
            // 
            // user_tests_page
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(1383, 622);
            Controls.Add(tests_empty_label);
            Controls.Add(user_profile_button);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "user_tests_page";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button user_profile_button;
        private Label tests_empty_label;
    }
}