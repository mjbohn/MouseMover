namespace MouseMoveWFevent
{
    partial class FormMain
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
            lblStatus = new Label();
            lblLastMove = new Label();
            numericUpDownwaitTime = new NumericUpDown();
            lblWaitTime = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDownwaitTime).BeginInit();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(23, 15);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(38, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "status";
            // 
            // lblLastMove
            // 
            lblLastMove.AutoSize = true;
            lblLastMove.Location = new Point(23, 30);
            lblLastMove.Name = "lblLastMove";
            lblLastMove.Size = new Size(16, 15);
            lblLastMove.TabIndex = 1;
            lblLastMove.Text = "...";
            // 
            // numericUpDownwaitTime
            // 
            numericUpDownwaitTime.Location = new Point(87, 51);
            numericUpDownwaitTime.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            numericUpDownwaitTime.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownwaitTime.Name = "numericUpDownwaitTime";
            numericUpDownwaitTime.Size = new Size(52, 23);
            numericUpDownwaitTime.TabIndex = 2;
            numericUpDownwaitTime.Value = new decimal(new int[] { 180, 0, 0, 0 });
            numericUpDownwaitTime.ValueChanged += numericUpDownwaitTime_ValueChanged;
            // 
            // lblWaitTime
            // 
            lblWaitTime.AutoSize = true;
            lblWaitTime.Location = new Point(23, 53);
            lblWaitTime.Name = "lblWaitTime";
            lblWaitTime.Size = new Size(49, 15);
            lblWaitTime.TabIndex = 3;
            lblWaitTime.Text = "wait sec";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(234, 111);
            Controls.Add(lblWaitTime);
            Controls.Add(numericUpDownwaitTime);
            Controls.Add(lblLastMove);
            Controls.Add(lblStatus);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            Name = "FormMain";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MouseMove";
            FormClosed += FormMain_FormClosed;
            ((System.ComponentModel.ISupportInitialize)numericUpDownwaitTime).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStatus;
        private Label lblLastMove;
        private NumericUpDown numericUpDownwaitTime;
        private Label lblWaitTime;
    }
}
