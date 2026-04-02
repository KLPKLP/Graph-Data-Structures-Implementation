namespace SemA.GUI
{
    partial class AddTownForm
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
            labelTownName = new Label();
            textBoxTownName = new TextBox();
            labelPositionX = new Label();
            textBoxPositionX = new TextBox();
            labelPositionY = new Label();
            textBoxPositionY = new TextBox();
            buttonOk = new Button();
            buttonCancel = new Button();
            SuspendLayout();
            // 
            // labelTownName
            // 
            labelTownName.AutoSize = true;
            labelTownName.Location = new Point(24, 24);
            labelTownName.Name = "labelTownName";
            labelTownName.Size = new Size(77, 15);
            labelTownName.TabIndex = 0;
            labelTownName.Text = "Název města:";
            // 
            // textBoxTownName
            // 
            textBoxTownName.Location = new Point(120, 21);
            textBoxTownName.Name = "textBoxTownName";
            textBoxTownName.Size = new Size(180, 23);
            textBoxTownName.TabIndex = 1;
            // 
            // labelPositionX
            // 
            labelPositionX.AutoSize = true;
            labelPositionX.Location = new Point(24, 67);
            labelPositionX.Name = "labelPositionX";
            labelPositionX.Size = new Size(17, 15);
            labelPositionX.TabIndex = 2;
            labelPositionX.Text = "X:";
            // 
            // textBoxPositionX
            // 
            textBoxPositionX.Location = new Point(120, 64);
            textBoxPositionX.Name = "textBoxPositionX";
            textBoxPositionX.Size = new Size(180, 23);
            textBoxPositionX.TabIndex = 3;
            // 
            // labelPositionY
            // 
            labelPositionY.AutoSize = true;
            labelPositionY.Location = new Point(24, 110);
            labelPositionY.Name = "labelPositionY";
            labelPositionY.Size = new Size(17, 15);
            labelPositionY.TabIndex = 4;
            labelPositionY.Text = "Y:";
            // 
            // textBoxPositionY
            // 
            textBoxPositionY.Location = new Point(120, 107);
            textBoxPositionY.Name = "textBoxPositionY";
            textBoxPositionY.Size = new Size(180, 23);
            textBoxPositionY.TabIndex = 5;
            // 
            // buttonOk
            // 
            buttonOk.Location = new Point(144, 155);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(75, 27);
            buttonOk.TabIndex = 6;
            buttonOk.Text = "OK";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(225, 155);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 27);
            buttonCancel.TabIndex = 7;
            buttonCancel.Text = "Storno";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // AddTownForm
            // 
            AcceptButton = buttonOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(332, 204);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            Controls.Add(textBoxPositionY);
            Controls.Add(labelPositionY);
            Controls.Add(textBoxPositionX);
            Controls.Add(labelPositionX);
            Controls.Add(textBoxTownName);
            Controls.Add(labelTownName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddTownForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Přidat město";
            Load += AddTownForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelTownName;
        private TextBox textBoxTownName;
        private Label labelPositionX;
        private TextBox textBoxPositionX;
        private Label labelPositionY;
        private TextBox textBoxPositionY;
        private Button buttonOk;
        private Button buttonCancel;
    }
}