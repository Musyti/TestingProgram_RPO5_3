namespace TestForms
{
    partial class test_maker
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
            grpQuestionInput = new GroupBox();
            nudCorrectAnswer = new NumericUpDown();
            lblCorrectAnswer = new Label();
            txtAnswer4 = new TextBox();
            lblAnswer4 = new Label();
            txtAnswer3 = new TextBox();
            lblAnswer3 = new Label();
            txtAnswer2 = new TextBox();
            lblAnswer2 = new Label();
            txtAnswer1 = new TextBox();
            lblAnswer1 = new Label();
            txtQuestion = new TextBox();
            lblQuestion = new Label();
            grpQuestionsList = new GroupBox();
            listQuestions = new ListBox();
            lblTotalQuestions = new Label();
            grpControls = new GroupBox();
            btnFinish = new Button();
            btnNext = new Button();
            btnPrevious = new Button();
            btnSaveQuestion = new Button();
            btnClear = new Button();
            btnExit = new Button();
            lblCurrentQuestion = new Label();
            lblStatus = new Label();
            grpQuestionInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCorrectAnswer).BeginInit();
            grpQuestionsList.SuspendLayout();
            grpControls.SuspendLayout();
            SuspendLayout();
            // 
            // grpQuestionInput
            // 
            grpQuestionInput.Controls.Add(nudCorrectAnswer);
            grpQuestionInput.Controls.Add(lblCorrectAnswer);
            grpQuestionInput.Controls.Add(txtAnswer4);
            grpQuestionInput.Controls.Add(lblAnswer4);
            grpQuestionInput.Controls.Add(txtAnswer3);
            grpQuestionInput.Controls.Add(lblAnswer3);
            grpQuestionInput.Controls.Add(txtAnswer2);
            grpQuestionInput.Controls.Add(lblAnswer2);
            grpQuestionInput.Controls.Add(txtAnswer1);
            grpQuestionInput.Controls.Add(lblAnswer1);
            grpQuestionInput.Controls.Add(txtQuestion);
            grpQuestionInput.Controls.Add(lblQuestion);
            grpQuestionInput.Font = new Font("Rubik", 10F);
            grpQuestionInput.Location = new Point(23, 27);
            grpQuestionInput.Margin = new Padding(3, 4, 3, 4);
            grpQuestionInput.Name = "grpQuestionInput";
            grpQuestionInput.Padding = new Padding(3, 4, 3, 4);
            grpQuestionInput.Size = new Size(686, 560);
            grpQuestionInput.TabIndex = 0;
            grpQuestionInput.TabStop = false;
            // 
            // nudCorrectAnswer
            // 
            nudCorrectAnswer.Font = new Font("Rubik", 10F);
            nudCorrectAnswer.Location = new Point(200, 544);
            nudCorrectAnswer.Margin = new Padding(3, 4, 3, 4);
            nudCorrectAnswer.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            nudCorrectAnswer.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCorrectAnswer.Name = "nudCorrectAnswer";
            nudCorrectAnswer.Size = new Size(69, 27);
            nudCorrectAnswer.TabIndex = 11;
            nudCorrectAnswer.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblCorrectAnswer
            // 
            lblCorrectAnswer.AutoSize = true;
            lblCorrectAnswer.Font = new Font("Rubik", 10F, FontStyle.Bold);
            lblCorrectAnswer.Location = new Point(23, 547);
            lblCorrectAnswer.Name = "lblCorrectAnswer";
            lblCorrectAnswer.Size = new Size(182, 20);
            lblCorrectAnswer.TabIndex = 10;
            lblCorrectAnswer.Text = "Правильный ответ:";
            // 
            // txtAnswer4
            // 
            txtAnswer4.Font = new Font("Rubik", 10F);
            txtAnswer4.Location = new Point(23, 493);
            txtAnswer4.Margin = new Padding(3, 4, 3, 4);
            txtAnswer4.Name = "txtAnswer4";
            txtAnswer4.Size = new Size(639, 27);
            txtAnswer4.TabIndex = 9;
            // 
            // lblAnswer4
            // 
            lblAnswer4.AutoSize = true;
            lblAnswer4.Font = new Font("Rubik", 10F, FontStyle.Bold);
            lblAnswer4.Location = new Point(23, 460);
            lblAnswer4.Name = "lblAnswer4";
            lblAnswer4.Size = new Size(105, 20);
            lblAnswer4.TabIndex = 8;
            lblAnswer4.Text = "Вариант 4:";
            // 
            // txtAnswer3
            // 
            txtAnswer3.Font = new Font("Rubik", 10F);
            txtAnswer3.Location = new Point(23, 407);
            txtAnswer3.Margin = new Padding(3, 4, 3, 4);
            txtAnswer3.Name = "txtAnswer3";
            txtAnswer3.Size = new Size(639, 27);
            txtAnswer3.TabIndex = 7;
            // 
            // lblAnswer3
            // 
            lblAnswer3.AutoSize = true;
            lblAnswer3.Font = new Font("Rubik", 10F, FontStyle.Bold);
            lblAnswer3.Location = new Point(23, 373);
            lblAnswer3.Name = "lblAnswer3";
            lblAnswer3.Size = new Size(104, 20);
            lblAnswer3.TabIndex = 6;
            lblAnswer3.Text = "Вариант 3:";
            // 
            // txtAnswer2
            // 
            txtAnswer2.Font = new Font("Rubik", 10F);
            txtAnswer2.Location = new Point(23, 320);
            txtAnswer2.Margin = new Padding(3, 4, 3, 4);
            txtAnswer2.Name = "txtAnswer2";
            txtAnswer2.Size = new Size(639, 27);
            txtAnswer2.TabIndex = 5;
            // 
            // lblAnswer2
            // 
            lblAnswer2.AutoSize = true;
            lblAnswer2.Font = new Font("Rubik", 10F, FontStyle.Bold);
            lblAnswer2.Location = new Point(23, 287);
            lblAnswer2.Name = "lblAnswer2";
            lblAnswer2.Size = new Size(104, 20);
            lblAnswer2.TabIndex = 4;
            lblAnswer2.Text = "Вариант 2:";
            // 
            // txtAnswer1
            // 
            txtAnswer1.Font = new Font("Rubik", 10F);
            txtAnswer1.Location = new Point(23, 233);
            txtAnswer1.Margin = new Padding(3, 4, 3, 4);
            txtAnswer1.Name = "txtAnswer1";
            txtAnswer1.Size = new Size(639, 27);
            txtAnswer1.TabIndex = 3;
            // 
            // lblAnswer1
            // 
            lblAnswer1.AutoSize = true;
            lblAnswer1.Font = new Font("Rubik", 10F, FontStyle.Bold);
            lblAnswer1.Location = new Point(23, 200);
            lblAnswer1.Name = "lblAnswer1";
            lblAnswer1.Size = new Size(102, 20);
            lblAnswer1.TabIndex = 2;
            lblAnswer1.Text = "Вариант 1:";
            // 
            // txtQuestion
            // 
            txtQuestion.Font = new Font("Rubik", 10F);
            txtQuestion.Location = new Point(23, 73);
            txtQuestion.Margin = new Padding(3, 4, 3, 4);
            txtQuestion.Multiline = true;
            txtQuestion.Name = "txtQuestion";
            txtQuestion.Size = new Size(639, 105);
            txtQuestion.TabIndex = 1;
            // 
            // lblQuestion
            // 
            lblQuestion.AutoSize = true;
            lblQuestion.Font = new Font("Rubik", 10F, FontStyle.Bold);
            lblQuestion.Location = new Point(23, 40);
            lblQuestion.Name = "lblQuestion";
            lblQuestion.Size = new Size(78, 20);
            lblQuestion.TabIndex = 0;
            lblQuestion.Text = "Вопрос:";
            // 
            // grpQuestionsList
            // 
            grpQuestionsList.Controls.Add(listQuestions);
            grpQuestionsList.Controls.Add(lblTotalQuestions);
            grpQuestionsList.Font = new Font("Rubik", 10F);
            grpQuestionsList.Location = new Point(731, 27);
            grpQuestionsList.Margin = new Padding(3, 4, 3, 4);
            grpQuestionsList.Name = "grpQuestionsList";
            grpQuestionsList.Padding = new Padding(3, 4, 3, 4);
            grpQuestionsList.Size = new Size(343, 560);
            grpQuestionsList.TabIndex = 1;
            grpQuestionsList.TabStop = false;
            // 
            // listQuestions
            // 
            listQuestions.Font = new Font("Rubik", 10F);
            listQuestions.FormattingEnabled = true;
            listQuestions.Location = new Point(11, 40);
            listQuestions.Margin = new Padding(3, 4, 3, 4);
            listQuestions.Name = "listQuestions";
            listQuestions.Size = new Size(319, 444);
            listQuestions.TabIndex = 0;
            // 
            // lblTotalQuestions
            // 
            lblTotalQuestions.AutoSize = true;
            lblTotalQuestions.Font = new Font("Rubik", 10F, FontStyle.Bold);
            lblTotalQuestions.Location = new Point(11, 520);
            lblTotalQuestions.Name = "lblTotalQuestions";
            lblTotalQuestions.Size = new Size(156, 20);
            lblTotalQuestions.TabIndex = 1;
            lblTotalQuestions.Text = "Заполнено: 0/10";
            // 
            // grpControls
            // 
            grpControls.Controls.Add(btnFinish);
            grpControls.Controls.Add(btnNext);
            grpControls.Controls.Add(btnPrevious);
            grpControls.Controls.Add(btnSaveQuestion);
            grpControls.Controls.Add(btnClear);
            grpControls.Controls.Add(btnExit);
            grpControls.Font = new Font("Rubik", 10F);
            grpControls.Location = new Point(23, 600);
            grpControls.Margin = new Padding(3, 4, 3, 4);
            grpControls.Name = "grpControls";
            grpControls.Padding = new Padding(3, 4, 3, 4);
            grpControls.Size = new Size(1051, 93);
            grpControls.TabIndex = 2;
            grpControls.TabStop = false;
            grpControls.Text = "Навигация";
            // 
            // btnFinish
            // 
            btnFinish.Enabled = false;
            btnFinish.Font = new Font("Rubik", 10F, FontStyle.Bold);
            btnFinish.Location = new Point(640, 33);
            btnFinish.Margin = new Padding(3, 4, 3, 4);
            btnFinish.Name = "btnFinish";
            btnFinish.Size = new Size(206, 47);
            btnFinish.TabIndex = 4;
            btnFinish.Text = "Завершить создание";
            btnFinish.UseVisualStyleBackColor = true;
            btnFinish.Click += BtnExit_Click;
            // 
            // btnNext
            // 
            btnNext.Font = new Font("Rubik", 10F);
            btnNext.Location = new Point(149, 33);
            btnNext.Margin = new Padding(3, 4, 3, 4);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(114, 47);
            btnNext.TabIndex = 1;
            btnNext.Text = "Вперед ▶";
            btnNext.UseVisualStyleBackColor = true;
            // 
            // btnPrevious
            // 
            btnPrevious.Font = new Font("Rubik", 10F);
            btnPrevious.Location = new Point(23, 33);
            btnPrevious.Margin = new Padding(3, 4, 3, 4);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(114, 47);
            btnPrevious.TabIndex = 0;
            btnPrevious.Text = "◀ Назад";
            btnPrevious.UseVisualStyleBackColor = true;
            // 
            // btnSaveQuestion
            // 
            btnSaveQuestion.Font = new Font("Rubik", 10F);
            btnSaveQuestion.Location = new Point(274, 33);
            btnSaveQuestion.Margin = new Padding(3, 4, 3, 4);
            btnSaveQuestion.Name = "btnSaveQuestion";
            btnSaveQuestion.Size = new Size(171, 47);
            btnSaveQuestion.TabIndex = 2;
            btnSaveQuestion.Text = "Сохранить вопрос";
            btnSaveQuestion.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Rubik", 10F);
            btnClear.Location = new Point(457, 33);
            btnClear.Margin = new Padding(3, 4, 3, 4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(171, 47);
            btnClear.TabIndex = 3;
            btnClear.Text = "Очистить вопрос";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            btnExit.Font = new Font("Rubik", 10F);
            btnExit.Location = new Point(857, 33);
            btnExit.Margin = new Padding(3, 4, 3, 4);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(171, 47);
            btnExit.TabIndex = 5;
            btnExit.Text = "Выход";
            btnExit.UseVisualStyleBackColor = true;
            // 
            // lblCurrentQuestion
            // 
            lblCurrentQuestion.AutoSize = true;
            lblCurrentQuestion.Font = new Font("Rubik", 12F, FontStyle.Bold);
            lblCurrentQuestion.Location = new Point(23, 707);
            lblCurrentQuestion.Name = "lblCurrentQuestion";
            lblCurrentQuestion.Size = new Size(156, 24);
            lblCurrentQuestion.TabIndex = 3;
            lblCurrentQuestion.Text = "Вопрос 1 из 10";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Rubik", 10F);
            lblStatus.Location = new Point(23, 747);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 20);
            lblStatus.TabIndex = 4;
            // 
            // test_maker
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1097, 800);
            Controls.Add(lblStatus);
            Controls.Add(lblCurrentQuestion);
            Controls.Add(grpControls);
            Controls.Add(grpQuestionsList);
            Controls.Add(grpQuestionInput);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "test_maker";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "test_maker";
            grpQuestionInput.ResumeLayout(false);
            grpQuestionInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCorrectAnswer).EndInit();
            grpQuestionsList.ResumeLayout(false);
            grpQuestionsList.PerformLayout();
            grpControls.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox grpQuestionInput;
        private System.Windows.Forms.NumericUpDown nudCorrectAnswer;
        private System.Windows.Forms.Label lblCorrectAnswer;
        private System.Windows.Forms.TextBox txtAnswer4;
        private System.Windows.Forms.Label lblAnswer4;
        private System.Windows.Forms.TextBox txtAnswer3;
        private System.Windows.Forms.Label lblAnswer3;
        private System.Windows.Forms.TextBox txtAnswer2;
        private System.Windows.Forms.Label lblAnswer2;
        private System.Windows.Forms.TextBox txtAnswer1;
        private System.Windows.Forms.Label lblAnswer1;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.GroupBox grpQuestionsList;
        private System.Windows.Forms.ListBox listQuestions;
        private System.Windows.Forms.Label lblTotalQuestions;
        private System.Windows.Forms.GroupBox grpControls;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnSaveQuestion;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblCurrentQuestion;
        private System.Windows.Forms.Label lblStatus;
    }
}