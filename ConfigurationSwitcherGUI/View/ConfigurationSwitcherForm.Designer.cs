namespace ConfigurationSwitcherGUI.View
{
    partial class ConfigurationSwitcherForm
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
            this.twConfigurations = new System.Windows.Forms.TreeView();
            this.tbFilePath = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblRoot = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // twConfigurations
            // 
            this.twConfigurations.Location = new System.Drawing.Point(12, 25);
            this.twConfigurations.Name = "twConfigurations";
            this.twConfigurations.Size = new System.Drawing.Size(319, 388);
            this.twConfigurations.TabIndex = 0;
            // 
            // tbFilePath
            // 
            this.tbFilePath.Location = new System.Drawing.Point(13, 420);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.Size = new System.Drawing.Size(318, 20);
            this.tbFilePath.TabIndex = 1;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(13, 447);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 13);
            this.lblError.TabIndex = 2;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(256, 447);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // lblRoot
            // 
            this.lblRoot.AutoSize = true;
            this.lblRoot.Location = new System.Drawing.Point(10, 9);
            this.lblRoot.Name = "lblRoot";
            this.lblRoot.Size = new System.Drawing.Size(36, 13);
            this.lblRoot.TabIndex = 4;
            this.lblRoot.Text = "Root: ";
            // 
            // ConfigurationSwitcherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 473);
            this.Controls.Add(this.lblRoot);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.tbFilePath);
            this.Controls.Add(this.twConfigurations);
            this.Name = "ConfigurationSwitcherForm";
            this.Text = "Configuration Switcher";
            this.Load += new System.EventHandler(this.ConfigurationSwitcherForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView twConfigurations;
        private System.Windows.Forms.TextBox tbFilePath;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label lblRoot;
    }
}

