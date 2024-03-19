namespace ToDo_Task_WinForm
{
    partial class CreateTaskWindow
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
            label1 = new Label();
            textBox_TileTask = new TextBox();
            label2 = new Label();
            richTextBox_TextTask = new RichTextBox();
            dateTimePicker_DateEnd = new DateTimePicker();
            button_CreateTask = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 1;
            label1.Text = "Название";
            // 
            // textBox_TileTask
            // 
            textBox_TileTask.Location = new Point(12, 24);
            textBox_TileTask.Name = "textBox_TileTask";
            textBox_TileTask.Size = new Size(303, 23);
            textBox_TileTask.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 64);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 1;
            label2.Text = "Описание";
            // 
            // richTextBox_TextTask
            // 
            richTextBox_TextTask.Location = new Point(12, 82);
            richTextBox_TextTask.Name = "richTextBox_TextTask";
            richTextBox_TextTask.Size = new Size(303, 155);
            richTextBox_TextTask.TabIndex = 2;
            richTextBox_TextTask.Text = "";
            // 
            // dateTimePicker_DateEnd
            // 
            dateTimePicker_DateEnd.CustomFormat = "dd.MM.yyyy HH:mm";
            dateTimePicker_DateEnd.Format = DateTimePickerFormat.Custom;
            dateTimePicker_DateEnd.Location = new Point(12, 243);
            dateTimePicker_DateEnd.Name = "dateTimePicker_DateEnd";
            dateTimePicker_DateEnd.Size = new Size(141, 23);
            dateTimePicker_DateEnd.TabIndex = 3;
            // 
            // button_CreateTask
            // 
            button_CreateTask.Location = new Point(12, 279);
            button_CreateTask.Name = "button_CreateTask";
            button_CreateTask.Size = new Size(303, 23);
            button_CreateTask.TabIndex = 4;
            button_CreateTask.Text = "Создать задачу";
            button_CreateTask.UseVisualStyleBackColor = true;
            button_CreateTask.Click += button_CreateTask_Click;
            // 
            // CreateTaskWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(338, 314);
            Controls.Add(button_CreateTask);
            Controls.Add(dateTimePicker_DateEnd);
            Controls.Add(richTextBox_TextTask);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox_TileTask);
            Name = "CreateTaskWindow";
            Text = "Создать задачу";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private TextBox textBox_TileTask;
        private Label label2;
        private RichTextBox richTextBox_TextTask;
        private DateTimePicker dateTimePicker_DateEnd;
        private Button button_CreateTask;
    }
}