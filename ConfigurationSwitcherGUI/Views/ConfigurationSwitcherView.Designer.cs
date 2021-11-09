namespace ConfigurationSwitcherGUI.Views
{
    partial class ConfigurationSwitcherView
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
            this.cbEnvironments = new System.Windows.Forms.ComboBox();
            this.cbConfigurations = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblConfigurations = new System.Windows.Forms.Label();
            this.lblEnvironments = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbEnvironments
            // 
            this.cbEnvironments.FormattingEnabled = true;
            this.cbEnvironments.Location = new System.Drawing.Point(12, 25);
            this.cbEnvironments.Name = "cbEnvironments";
            this.cbEnvironments.Size = new System.Drawing.Size(205, 21);
            this.cbEnvironments.TabIndex = 0;
            this.cbEnvironments.SelectedIndexChanged += new System.EventHandler(this.cbEnvironments_SelectedIndexChanged);
            // 
            // cbConfigurations
            // 
            this.cbConfigurations.FormattingEnabled = true;
            this.cbConfigurations.Location = new System.Drawing.Point(12, 76);
            this.cbConfigurations.Name = "cbConfigurations";
            this.cbConfigurations.Size = new System.Drawing.Size(205, 21);
            this.cbConfigurations.TabIndex = 1;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(142, 127);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lblConfigurations
            // 
            this.lblConfigurations.AutoSize = true;
            this.lblConfigurations.Location = new System.Drawing.Point(12, 60);
            this.lblConfigurations.Name = "lblConfigurations";
            this.lblConfigurations.Size = new System.Drawing.Size(72, 13);
            this.lblConfigurations.TabIndex = 3;
            this.lblConfigurations.Text = "Configuration:";
            // 
            // lblEnvironments
            // 
            this.lblEnvironments.AutoSize = true;
            this.lblEnvironments.Location = new System.Drawing.Point(9, 9);
            this.lblEnvironments.Name = "lblEnvironments";
            this.lblEnvironments.Size = new System.Drawing.Size(69, 13);
            this.lblEnvironments.TabIndex = 4;
            this.lblEnvironments.Text = "Environment:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 172);
            this.Controls.Add(this.lblEnvironments);
            this.Controls.Add(this.lblConfigurations);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.cbConfigurations);
            this.Controls.Add(this.cbEnvironments);
            this.Name = "Form1";
            this.Text = "Configuration Switcher";
            this.Load += new System.EventHandler(this.ConfigurationSwitcherView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbEnvironments;
        private System.Windows.Forms.ComboBox cbConfigurations;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label lblConfigurations;
        private System.Windows.Forms.Label lblEnvironments;
    }
}