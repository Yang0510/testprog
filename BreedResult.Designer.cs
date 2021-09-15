namespace DogManage
{
    partial class BreedResult
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
            this.Wb_breedresult = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // Wb_breedresult
            // 
            this.Wb_breedresult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Wb_breedresult.Location = new System.Drawing.Point(0, 0);
            this.Wb_breedresult.MinimumSize = new System.Drawing.Size(20, 20);
            this.Wb_breedresult.Name = "Wb_breedresult";
            this.Wb_breedresult.Size = new System.Drawing.Size(1008, 730);
            this.Wb_breedresult.TabIndex = 0;
            // 
            // BreedResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.Wb_breedresult);
            this.Name = "BreedResult";
            this.Text = "BreedResult";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser Wb_breedresult;
    }
}