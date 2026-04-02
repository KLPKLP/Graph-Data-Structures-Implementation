namespace SemA.GUI
{
    partial class AddRoadForm
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
            labelFrom = new Label();
            comboBoxFrom = new ComboBox();
            labelTo = new Label();
            comboBoxTo = new ComboBox();
            labelTime = new Label();
            textBoxTime = new TextBox();
            checkBoxIsProblematic = new CheckBox();
            buttonOk = new Button();
            buttonCancel = new Button();
            SuspendLayout();
            // 
            // labelFrom
            // 
            labelFrom.AutoSize = true;
            labelFrom.Location = new Point(24, 24);
            labelFrom.Name = "labelFrom";
            labelFrom.Size = new Size(25, 15);
            labelFrom.TabIndex = 0;
            labelFrom.Text = "Od:";
            // 
            // comboBoxFrom
            // 
            comboBoxFrom.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxFrom.FormattingEnabled = true;
            comboBoxFrom.Location = new Point(120, 21);
            comboBoxFrom.Name = "comboBoxFrom";
            comboBoxFrom.Size = new Size(180, 23);
            comboBoxFrom.TabIndex = 1;
            // 
            // labelTo
            // 
            labelTo.AutoSize = true;
            labelTo.Location = new Point(24, 67);
            labelTo.Name = "labelTo";
            labelTo.Size = new Size(21, 15);
            labelTo.TabIndex = 2;
            labelTo.Text = "Do:";
            // 
            // comboBoxTo
            // 
            comboBoxTo.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTo.FormattingEnabled = true;
            comboBoxTo.Location = new Point(120, 64);
            comboBoxTo.Name = "comboBoxTo";
            comboBoxTo.Size = new Size(180, 23);
            comboBoxTo.TabIndex = 3;
            // 
            // labelTime
            // 
            labelTime.AutoSize = true;
            labelTime.Location = new Point(24, 110);
            labelTime.Name = "labelTime";
            labelTime.Size = new Size(31, 15);
            labelTime.TabIndex = 4;
            labelTime.Text = "Čas:";
            // 
            // textBoxTime
            // 
            textBoxTime.Location = new Point(120, 107);
            textBoxTime.Name = "textBoxTime";
            textBoxTime.Size = new Size(180, 23);
            textBoxTime.TabIndex = 5;
            // 
            // checkBoxIsProblematic
            // 
            checkBoxIsProblematic.AutoSize = true;
            checkBoxIsProblematic.Location = new Point(120, 147);
            checkBoxIsProblematic.Name = "checkBoxIsProblematic";
            checkBoxIsProblematic.Size = new Size(101, 19);
            checkBoxIsProblematic.TabIndex = 6;
            checkBoxIsProblematic.Text = "Problematická";
            checkBoxIsProblematic.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            buttonOk.Location = new Point(144, 188);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(75, 27);
            buttonOk.TabIndex = 7;
            buttonOk.Text = "OK";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(225, 188);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 27);
            buttonCancel.TabIndex = 8;
            buttonCancel.Text = "Storno";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // AddRoadForm
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(332, 236);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            Controls.Add(checkBoxIsProblematic);
            Controls.Add(textBoxTime);
            Controls.Add(labelTime);
            Controls.Add(comboBoxTo);
            Controls.Add(labelTo);
            Controls.Add(comboBoxFrom);
            Controls.Add(labelFrom);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddRoadForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Přidat silnici";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelFrom;
        private ComboBox comboBoxFrom;
        private Label labelTo;
        private ComboBox comboBoxTo;
        private Label labelTime;
        private TextBox textBoxTime;
        private CheckBox checkBoxIsProblematic;
        private Button buttonOk;
        private Button buttonCancel;
    }
}