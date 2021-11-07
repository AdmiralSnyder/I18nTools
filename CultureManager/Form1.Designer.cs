
namespace CultureManager
{
    partial class Form1
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
            this.CultureNameTB = new System.Windows.Forms.TextBox();
            this.CultureAbbrevTB = new System.Windows.Forms.TextBox();
            this.CultureNameL = new System.Windows.Forms.Label();
            this.CultureAbbrevL = new System.Windows.Forms.Label();
            this.BaseCultureL = new System.Windows.Forms.Label();
            this.cultureCB = new System.Windows.Forms.ComboBox();
            this.RemoveB = new System.Windows.Forms.Button();
            this.RegisterB = new System.Windows.Forms.Button();
            this.LoadCulturesB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CultureNameTB
            // 
            this.CultureNameTB.Location = new System.Drawing.Point(340, 21);
            this.CultureNameTB.Name = "CultureNameTB";
            this.CultureNameTB.Size = new System.Drawing.Size(247, 29);
            this.CultureNameTB.TabIndex = 2;
            this.CultureNameTB.Text = "English (CODie Vessel)";
            // 
            // CultureAbbrevTB
            // 
            this.CultureAbbrevTB.Location = new System.Drawing.Point(340, 68);
            this.CultureAbbrevTB.Name = "CultureAbbrevTB";
            this.CultureAbbrevTB.Size = new System.Drawing.Size(100, 29);
            this.CultureAbbrevTB.TabIndex = 3;
            this.CultureAbbrevTB.Text = "CDVSL";
            // 
            // CultureNameL
            // 
            this.CultureNameL.AutoSize = true;
            this.CultureNameL.Location = new System.Drawing.Point(215, 21);
            this.CultureNameL.Name = "CultureNameL";
            this.CultureNameL.Size = new System.Drawing.Size(64, 25);
            this.CultureNameL.TabIndex = 4;
            this.CultureNameL.Text = "Name";
            // 
            // CultureAbbrevL
            // 
            this.CultureAbbrevL.AutoSize = true;
            this.CultureAbbrevL.Location = new System.Drawing.Point(215, 72);
            this.CultureAbbrevL.Name = "CultureAbbrevL";
            this.CultureAbbrevL.Size = new System.Drawing.Size(68, 25);
            this.CultureAbbrevL.TabIndex = 5;
            this.CultureAbbrevL.Text = "Kürzel";
            // 
            // BaseCultureL
            // 
            this.BaseCultureL.AutoSize = true;
            this.BaseCultureL.Location = new System.Drawing.Point(215, 117);
            this.BaseCultureL.Name = "BaseCultureL";
            this.BaseCultureL.Size = new System.Drawing.Size(60, 25);
            this.BaseCultureL.TabIndex = 6;
            this.BaseCultureL.Text = "Basis";
            // 
            // cultureCB
            // 
            this.cultureCB.FormattingEnabled = true;
            this.cultureCB.Location = new System.Drawing.Point(340, 114);
            this.cultureCB.Name = "cultureCB";
            this.cultureCB.Size = new System.Drawing.Size(247, 32);
            this.cultureCB.TabIndex = 7;
            // 
            // RemoveB
            // 
            this.RemoveB.Location = new System.Drawing.Point(603, 54);
            this.RemoveB.Name = "RemoveB";
            this.RemoveB.Size = new System.Drawing.Size(162, 37);
            this.RemoveB.TabIndex = 8;
            this.RemoveB.Text = "Remove";
            this.RemoveB.UseVisualStyleBackColor = true;
            this.RemoveB.Click += new System.EventHandler(this.RemoveB_Click);
            // 
            // RegisterB
            // 
            this.RegisterB.Location = new System.Drawing.Point(220, 167);
            this.RegisterB.Name = "RegisterB";
            this.RegisterB.Size = new System.Drawing.Size(220, 39);
            this.RegisterB.TabIndex = 9;
            this.RegisterB.Text = "Register";
            this.RegisterB.UseVisualStyleBackColor = true;
            this.RegisterB.Click += new System.EventHandler(this.RegisterB_Click);
            // 
            // LoadCulturesB
            // 
            this.LoadCulturesB.Location = new System.Drawing.Point(603, 117);
            this.LoadCulturesB.Name = "LoadCulturesB";
            this.LoadCulturesB.Size = new System.Drawing.Size(162, 39);
            this.LoadCulturesB.TabIndex = 10;
            this.LoadCulturesB.Text = "Load Cultures";
            this.LoadCulturesB.UseVisualStyleBackColor = true;
            this.LoadCulturesB.Click += new System.EventHandler(this.LoadCulturesB_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 249);
            this.Controls.Add(this.LoadCulturesB);
            this.Controls.Add(this.RegisterB);
            this.Controls.Add(this.RemoveB);
            this.Controls.Add(this.cultureCB);
            this.Controls.Add(this.BaseCultureL);
            this.Controls.Add(this.CultureAbbrevL);
            this.Controls.Add(this.CultureNameL);
            this.Controls.Add(this.CultureAbbrevTB);
            this.Controls.Add(this.CultureNameTB);
            this.Name = "Form1";
            this.Text = "CODie Culture Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox CultureNameTB;
        private System.Windows.Forms.TextBox CultureAbbrevTB;
        private System.Windows.Forms.Label CultureNameL;
        private System.Windows.Forms.Label CultureAbbrevL;
        private System.Windows.Forms.Label BaseCultureL;
        private System.Windows.Forms.ComboBox cultureCB;
        private System.Windows.Forms.Button RemoveB;
        private System.Windows.Forms.Button RegisterB;
        private System.Windows.Forms.Button LoadCulturesB;
    }
}

