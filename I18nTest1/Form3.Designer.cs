
namespace I18nTest1
{
    partial class Form3
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.myComponent1 = new CodeDomSerializerSample.MyComponent();
            this.myOtherComponent1 = new I18nTest1.MyOtherComponent(this.components);
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(457, 114);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(209, 59);
            this.button1.TabIndex = 0;
            this.button1.Text = "My Winforms Button";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(262, 232);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(221, 58);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "MyDevexSimpleButton";
            // This comment was added to this object by a custom serializer.
            // 
            // myComponent1
            // 
            this.myComponent1.LocalProperty = "schnubbel";
            // This comment was added to this object by a custom serializer.
            // 
            // myOtherComponent1
            // 
            this.myOtherComponent1.LocalProperty = "Component Property Value";
            this.myOtherComponent1.OtherDATA = "Schanff";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.button1);
            this.Name = "Form3";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private CodeDomSerializerSample.MyComponent myComponent1;
        private MyOtherComponent myOtherComponent1;
    }
}

