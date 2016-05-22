namespace White_Tiger_Managment
{
    partial class findwnd
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.lstFields = new System.Windows.Forms.ListBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.txtSearched = new System.Windows.Forms.TextBox();
            this.rdbSearchStartsWith = new System.Windows.Forms.RadioButton();
            this.rdbSearchEndsWith = new System.Windows.Forms.RadioButton();
            this.rdbSearchForeMatching = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(443, 38);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_click);
            // 
            // lstFields
            // 
            this.lstFields.Dock = System.Windows.Forms.DockStyle.Right;
            this.lstFields.FormattingEnabled = true;
            this.lstFields.Location = new System.Drawing.Point(529, 0);
            this.lstFields.Name = "lstFields";
            this.lstFields.Size = new System.Drawing.Size(94, 142);
            this.lstFields.TabIndex = 1;
            this.lstFields.SelectedIndexChanged += new System.EventHandler(this.lstFields_SelectedIndexChanged);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(38, 20);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(90, 13);
            this.lblMsg.TabIndex = 2;
            this.lblMsg.Text = "Search in Cell for:";
            // 
            // txtSearched
            // 
            this.txtSearched.Location = new System.Drawing.Point(41, 41);
            this.txtSearched.Name = "txtSearched";
            this.txtSearched.Size = new System.Drawing.Size(396, 20);
            this.txtSearched.TabIndex = 3;
            // 
            // rdbSearchStartsWith
            // 
            this.rdbSearchStartsWith.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rdbSearchStartsWith.Location = new System.Drawing.Point(0, 118);
            this.rdbSearchStartsWith.Name = "rdbSearchStartsWith";
            this.rdbSearchStartsWith.Size = new System.Drawing.Size(529, 24);
            this.rdbSearchStartsWith.TabIndex = 4;
            this.rdbSearchStartsWith.TabStop = true;
            this.rdbSearchStartsWith.Text = "Search for the  Records  containing current value in the start";
            this.rdbSearchStartsWith.UseVisualStyleBackColor = true;
            // 
            // rdbSearchEndsWith
            // 
            this.rdbSearchEndsWith.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rdbSearchEndsWith.Location = new System.Drawing.Point(0, 94);
            this.rdbSearchEndsWith.Name = "rdbSearchEndsWith";
            this.rdbSearchEndsWith.Size = new System.Drawing.Size(529, 24);
            this.rdbSearchEndsWith.TabIndex = 5;
            this.rdbSearchEndsWith.TabStop = true;
            this.rdbSearchEndsWith.Text = "Search for the  Records  containing current value in the End";
            this.rdbSearchEndsWith.UseVisualStyleBackColor = true;
            // 
            // rdbSearchForeMatching
            // 
            this.rdbSearchForeMatching.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rdbSearchForeMatching.Location = new System.Drawing.Point(0, 70);
            this.rdbSearchForeMatching.Name = "rdbSearchForeMatching";
            this.rdbSearchForeMatching.Size = new System.Drawing.Size(529, 24);
            this.rdbSearchForeMatching.TabIndex = 6;
            this.rdbSearchForeMatching.TabStop = true;
            this.rdbSearchForeMatching.Text = "Search for the  Records  matching current value";
            this.rdbSearchForeMatching.UseVisualStyleBackColor = true;
            // 
            // findwnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 142);
            this.Controls.Add(this.rdbSearchForeMatching);
            this.Controls.Add(this.rdbSearchEndsWith);
            this.Controls.Add(this.rdbSearchStartsWith);
            this.Controls.Add(this.txtSearched);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lstFields);
            this.Controls.Add(this.btnSearch);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "findwnd";
            this.Text = "Search";
            this.Load += new System.EventHandler(this.findwnd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListBox lstFields;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.TextBox txtSearched;
        private System.Windows.Forms.RadioButton rdbSearchStartsWith;
        private System.Windows.Forms.RadioButton rdbSearchEndsWith;
        private System.Windows.Forms.RadioButton rdbSearchForeMatching;
    }
}