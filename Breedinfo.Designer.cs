namespace DogManage
{
    partial class Breedinfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

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
            this.Wb_breedinfo = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // Wb_breedinfo
            // 
            this.Wb_breedinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Wb_breedinfo.Location = new System.Drawing.Point(0, 0);
            this.Wb_breedinfo.MinimumSize = new System.Drawing.Size(20, 20);
            this.Wb_breedinfo.Name = "Wb_breedinfo";
            this.Wb_breedinfo.Size = new System.Drawing.Size(1064, 682);
            this.Wb_breedinfo.TabIndex = 0;
            this.Wb_breedinfo.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.Wb_breedinfo_DocumentCompleted);
            // 
            // Breedinfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 682);
            this.Controls.Add(this.Wb_breedinfo);
            this.Name = "Breedinfo";
            this.Text = "Breedinfo（第二步）";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.WebBrowser Wb_breedinfo;
    }
}