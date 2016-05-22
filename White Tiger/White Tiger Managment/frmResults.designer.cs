namespace White_Tiger_Managment
{
    partial class frmResults
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
            this.dgrResults = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgrResults)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrResults
            // 
            this.dgrResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrResults.Location = new System.Drawing.Point(0, 0);
            this.dgrResults.Name = "dgrResults";
            this.dgrResults.Size = new System.Drawing.Size(292, 269);
            this.dgrResults.TabIndex = 0;
            this.dgrResults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrResults_CellContentClick);
            // 
            // frmResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 269);
            this.Controls.Add(this.dgrResults);
            this.Name = "frmResults";
            this.Text = "frmResults";
            this.Load += new System.EventHandler(this.frmResults_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgrResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dgrResults;

    }
}