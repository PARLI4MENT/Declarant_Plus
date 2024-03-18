namespace ToDo_Task_WinForm
{
    partial class ToDo_Tasker
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
            components = new System.ComponentModel.Container();
            notifyIcon1 = new NotifyIcon(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            dataGridView_Main = new DataGridView();
            panel1 = new Panel();
            button_Update = new Button();
            button_DeleteTask = new Button();
            button_EditTask = new Button();
            button_CreateTask = new Button();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_Main).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(dataGridView_Main, 1, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridView_Main
            // 
            dataGridView_Main.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_Main.Dock = DockStyle.Fill;
            dataGridView_Main.Location = new Point(123, 3);
            dataGridView_Main.Name = "dataGridView_Main";
            dataGridView_Main.RowTemplate.Height = 25;
            dataGridView_Main.Size = new Size(683, 444);
            dataGridView_Main.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(button_Update);
            panel1.Controls.Add(button_DeleteTask);
            panel1.Controls.Add(button_EditTask);
            panel1.Controls.Add(button_CreateTask);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(114, 444);
            panel1.TabIndex = 1;
            // 
            // button_Update
            // 
            button_Update.Location = new Point(3, 403);
            button_Update.Name = "button_Update";
            button_Update.Size = new Size(108, 38);
            button_Update.TabIndex = 1;
            button_Update.Text = "Обновить список";
            button_Update.UseVisualStyleBackColor = true;
            button_Update.Click += button_Update_Click;
            // 
            // button_DeleteTask
            // 
            button_DeleteTask.ForeColor = Color.Red;
            button_DeleteTask.Location = new Point(3, 106);
            button_DeleteTask.Name = "button_DeleteTask";
            button_DeleteTask.Size = new Size(108, 23);
            button_DeleteTask.TabIndex = 1;
            button_DeleteTask.Text = "Удалить";
            button_DeleteTask.UseVisualStyleBackColor = true;
            button_DeleteTask.Click += button_DeleteTask_Click;
            // 
            // button_EditTask
            // 
            button_EditTask.ForeColor = Color.CornflowerBlue;
            button_EditTask.Location = new Point(3, 77);
            button_EditTask.Name = "button_EditTask";
            button_EditTask.Size = new Size(108, 23);
            button_EditTask.TabIndex = 1;
            button_EditTask.Text = "Изменить здачу";
            button_EditTask.UseVisualStyleBackColor = true;
            button_EditTask.Click += button_EditTask_Click;
            // 
            // button_CreateTask
            // 
            button_CreateTask.ForeColor = Color.ForestGreen;
            button_CreateTask.Location = new Point(3, 23);
            button_CreateTask.Name = "button_CreateTask";
            button_CreateTask.Size = new Size(108, 23);
            button_CreateTask.TabIndex = 1;
            button_CreateTask.Text = "Создать задачу";
            button_CreateTask.UseVisualStyleBackColor = true;
            button_CreateTask.Click += button_CreateTask_Click;
            // 
            // ToDo_Tasker
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "ToDo_Tasker";
            Text = "ToDo-Tasker";
            Load += ToDo_Tasker_Load;
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView_Main).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon notifyIcon1;
        private TableLayoutPanel tableLayoutPanel1;
        private DataGridView dataGridView_Main;
        private Panel panel1;
        private Button button_Update;
        private Button button_EditTask;
        private Button button_CreateTask;
        private Button button_DeleteTask;
    }
}
