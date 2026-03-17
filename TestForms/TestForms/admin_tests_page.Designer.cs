namespace TestForms
{
    partial class admin_tests_page
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(admin_tests_page));
            tests_empty_label = new Label();
            admin_profile_button = new Button();
            add_new_test_button = new Button();
            SuspendLayout();
            // 
            // tests_empty_label
            // 
            tests_empty_label.AutoSize = true;
            tests_empty_label.Font = new Font("Rubik", 50.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tests_empty_label.ForeColor = SystemColors.ActiveCaptionText;
            tests_empty_label.Location = new Point(44, 224);
            tests_empty_label.Name = "tests_empty_label";
            tests_empty_label.Size = new Size(1285, 80);
            tests_empty_label.TabIndex = 0;
            tests_empty_label.Text = "На данный момент тесты отсутствуют";
            // 
            // admin_profile_button
            // 
            admin_profile_button.BackgroundImage = (Image)resources.GetObject("admin_profile_button.BackgroundImage");
            admin_profile_button.FlatStyle = FlatStyle.Flat;
            admin_profile_button.ForeColor = SystemColors.ScrollBar;
            admin_profile_button.Location = new Point(1282, 12);
            admin_profile_button.Name = "admin_profile_button";
            admin_profile_button.Size = new Size(85, 85);
            admin_profile_button.TabIndex = 1;
            admin_profile_button.UseVisualStyleBackColor = true;
            // 
            // add_new_test_button
            // 
            add_new_test_button.BackColor = Color.Gray;
            add_new_test_button.FlatStyle = FlatStyle.Flat;
            add_new_test_button.Font = new Font("Rubik", 30F);
            add_new_test_button.ForeColor = SystemColors.ActiveCaptionText;
            add_new_test_button.Location = new Point(428, 489);
            add_new_test_button.Name = "add_new_test_button";
            add_new_test_button.Size = new Size(509, 101);
            add_new_test_button.TabIndex = 2;
            add_new_test_button.Text = "Добавить новый тест";
            add_new_test_button.UseVisualStyleBackColor = false;
            // 
            // admin_tests_page
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(1379, 618);
            Controls.Add(add_new_test_button);
            Controls.Add(admin_profile_button);
            Controls.Add(tests_empty_label);
            ForeColor = SystemColors.ActiveCaption;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "admin_tests_page";
            Text = "Разработчик";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label tests_empty_label;
        private Button admin_profile_button;
        private Button add_new_test_button;
    }
}
