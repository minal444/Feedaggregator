namespace WindowsFormsApplication1
{
    partial class Notification
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtDeviceID = new System.Windows.Forms.TextBox();
            this.txtNotificationText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(28, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(214, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "Send Notification";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDeviceID
            // 
            this.txtDeviceID.Location = new System.Drawing.Point(28, 61);
            this.txtDeviceID.Name = "txtDeviceID";
            this.txtDeviceID.Size = new System.Drawing.Size(214, 22);
            this.txtDeviceID.TabIndex = 1;
            // 
            // txtNotificationText
            // 
            this.txtNotificationText.Location = new System.Drawing.Point(28, 103);
            this.txtNotificationText.Name = "txtNotificationText";
            this.txtNotificationText.Size = new System.Drawing.Size(214, 22);
            this.txtNotificationText.TabIndex = 2;
            // 
            // Notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 223);
            this.Controls.Add(this.txtNotificationText);
            this.Controls.Add(this.txtDeviceID);
            this.Controls.Add(this.button1);
            this.Name = "Notification";
            this.Text = "Notification";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtDeviceID;
        private System.Windows.Forms.TextBox txtNotificationText;
    }
}