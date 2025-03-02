namespace MyNaughtyWindow
{
    partial class NW
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
            lblMessage = new Label();
            SuspendLayout();
            // 
            // lblMessage
            // 
            lblMessage.AutoEllipsis = true;
            lblMessage.BackColor = Color.Red;
            lblMessage.BorderStyle = BorderStyle.FixedSingle;
            lblMessage.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblMessage.ForeColor = Color.Black;
            lblMessage.Location = new Point(282, 40);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(400, 60);
            lblMessage.TabIndex = 0;
            lblMessage.Text = "You can't close me!";
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // NW
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(512, 213);
            Controls.Add(lblMessage);
            Font = new Font("Segoe UI", 9F);
            Margin = new Padding(3, 4, 3, 4);
            Name = "NW";
            Text = "My Window";
            ResumeLayout(false);
        }

        #endregion

        private Button btnClose;
        private Label lblMessage;
    }
}
