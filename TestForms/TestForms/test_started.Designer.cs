namespace TestForms
{
    partial class test_started
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
            counter = new Label();
            back_to_menu_button = new Button();
            question_label = new Label();
            var1 = new Button();
            var2 = new Button();
            var4 = new Button();
            var3 = new Button();
            pictureBox1 = new PictureBox();
            congrat_label = new Label();
            result_description_label = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // counter
            // 
            counter.AutoSize = true;
            counter.Font = new Font("Rubik", 40F);
            counter.Location = new Point(588, 9);
            counter.Name = "counter";
            counter.Size = new Size(133, 64);
            counter.TabIndex = 0;
            counter.Text = "1/10";
            // 
            // back_to_menu_button
            // 
            back_to_menu_button.BackgroundImageLayout = ImageLayout.None;
            back_to_menu_button.FlatStyle = FlatStyle.Flat;
            back_to_menu_button.Font = new Font("Rubik", 15F);
            back_to_menu_button.Location = new Point(12, 9);
            back_to_menu_button.Name = "back_to_menu_button";
            back_to_menu_button.Size = new Size(210, 69);
            back_to_menu_button.TabIndex = 1;
            back_to_menu_button.Text = "Вернуться в меню";
            back_to_menu_button.UseVisualStyleBackColor = true;
            // 
            // question_label
            // 
            question_label.AutoSize = true;
            question_label.Font = new Font("Rubik", 29.9999962F, FontStyle.Regular, GraphicsUnit.Point, 204);
            question_label.Location = new Point(419, 99);
            question_label.Name = "question_label";
            question_label.Size = new Size(493, 47);
            question_label.TabIndex = 2;
            question_label.Text = "Вопрос вопрос вопрос 1";
            // 
            // var1
            // 
            var1.FlatStyle = FlatStyle.Flat;
            var1.Font = new Font("Rubik", 30F);
            var1.Location = new Point(225, 178);
            var1.Name = "var1";
            var1.Size = new Size(367, 113);
            var1.TabIndex = 3;
            var1.Text = "Ответ 1";
            var1.UseVisualStyleBackColor = true;
            // 
            // var2
            // 
            var2.FlatStyle = FlatStyle.Flat;
            var2.Font = new Font("Rubik", 30F);
            var2.Location = new Point(716, 178);
            var2.Name = "var2";
            var2.Size = new Size(367, 113);
            var2.TabIndex = 4;
            var2.Text = "Ответ 2";
            var2.UseVisualStyleBackColor = true;
            // 
            // var4
            // 
            var4.FlatStyle = FlatStyle.Flat;
            var4.Font = new Font("Rubik", 30F);
            var4.Location = new Point(716, 349);
            var4.Name = "var4";
            var4.Size = new Size(367, 113);
            var4.TabIndex = 6;
            var4.Text = "Ответ 4";
            var4.UseVisualStyleBackColor = true;
            // 
            // var3
            // 
            var3.FlatStyle = FlatStyle.Flat;
            var3.Font = new Font("Rubik", 30F);
            var3.Location = new Point(225, 349);
            var3.Name = "var3";
            var3.Size = new Size(367, 113);
            var3.TabIndex = 5;
            var3.Text = "Ответ 3";
            var3.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(523, 178);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(300, 300);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            pictureBox1.Visible = false;
            // 
            // congrat_label
            // 
            congrat_label.AutoSize = true;
            congrat_label.Font = new Font("Rubik", 29.9999962F, FontStyle.Regular, GraphicsUnit.Point, 204);
            congrat_label.Location = new Point(501, 99);
            congrat_label.Name = "congrat_label";
            congrat_label.Size = new Size(351, 47);
            congrat_label.TabIndex = 8;
            congrat_label.Text = "Поздравляем, вы";
            congrat_label.Visible = false;
            // 
            // result_description_label
            // 
            result_description_label.AutoSize = true;
            result_description_label.Font = new Font("Rubik", 29.9999962F, FontStyle.Regular, GraphicsUnit.Point, 204);
            result_description_label.Location = new Point(438, 510);
            result_description_label.Name = "result_description_label";
            result_description_label.Size = new Size(0, 47);
            result_description_label.TabIndex = 9;
            result_description_label.Visible = false;
            // 
            // test_started
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(1383, 622);
            Controls.Add(result_description_label);
            Controls.Add(congrat_label);
            Controls.Add(pictureBox1);
            Controls.Add(var4);
            Controls.Add(var3);
            Controls.Add(var2);
            Controls.Add(var1);
            Controls.Add(question_label);
            Controls.Add(back_to_menu_button);
            Controls.Add(counter);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "test_started";
            Text = "test_started";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label counter;
        private Button back_to_menu_button;
        private Label question_label;
        private Button var1;
        private Button var2;
        private Button var4;
        private Button var3;
        private PictureBox pictureBox1;
        private Label congrat_label;
        private Label result_description_label;
    }
}