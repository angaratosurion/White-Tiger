namespace White_Tiger_Managment
{
    partial class UserManagment
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSaveUsers = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grdUsers = new System.Windows.Forms.DataGridView();
            this.tabRoles = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSaveRoles = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.grdRoles = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabUsers.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsers)).BeginInit();
            this.tabRoles.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoles)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabUsers);
            this.tabControl1.Controls.Add(this.tabRoles);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(284, 262);
            this.tabControl1.TabIndex = 0;
            // 
            // tabUsers
            // 
            this.tabUsers.Controls.Add(this.panel1);
            this.tabUsers.Controls.Add(this.grdUsers);
            this.tabUsers.Location = new System.Drawing.Point(4, 22);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabUsers.Size = new System.Drawing.Size(276, 236);
            this.tabUsers.TabIndex = 0;
            this.tabUsers.Text = "Users";
            this.tabUsers.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 192);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(270, 41);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnSaveUsers);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(61, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(209, 41);
            this.panel3.TabIndex = 0;
            // 
            // btnSaveUsers
            // 
            this.btnSaveUsers.Location = new System.Drawing.Point(131, 13);
            this.btnSaveUsers.Name = "btnSaveUsers";
            this.btnSaveUsers.Size = new System.Drawing.Size(75, 23);
            this.btnSaveUsers.TabIndex = 1;
            this.btnSaveUsers.Text = "Save Users";
            this.btnSaveUsers.UseVisualStyleBackColor = true;
            this.btnSaveUsers.Click += new System.EventHandler(this.btnSaveUsers_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(50, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grdUsers
            // 
            this.grdUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUsers.Location = new System.Drawing.Point(3, 3);
            this.grdUsers.Name = "grdUsers";
            this.grdUsers.Size = new System.Drawing.Size(270, 230);
            this.grdUsers.TabIndex = 0;
            this.grdUsers.NewRowNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.grdUsers_NewRowNeeded);
            this.grdUsers.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.grdUsers_RowsAdded);
            // 
            // tabRoles
            // 
            this.tabRoles.Controls.Add(this.panel2);
            this.tabRoles.Controls.Add(this.grdRoles);
            this.tabRoles.Location = new System.Drawing.Point(4, 22);
            this.tabRoles.Name = "tabRoles";
            this.tabRoles.Padding = new System.Windows.Forms.Padding(3);
            this.tabRoles.Size = new System.Drawing.Size(276, 236);
            this.tabRoles.TabIndex = 1;
            this.tabRoles.Text = "Roles";
            this.tabRoles.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 192);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(270, 41);
            this.panel2.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnSaveRoles);
            this.panel4.Controls.Add(this.button3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(87, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(183, 41);
            this.panel4.TabIndex = 1;
            // 
            // btnSaveRoles
            // 
            this.btnSaveRoles.Location = new System.Drawing.Point(103, 13);
            this.btnSaveRoles.Name = "btnSaveRoles";
            this.btnSaveRoles.Size = new System.Drawing.Size(75, 23);
            this.btnSaveRoles.TabIndex = 3;
            this.btnSaveRoles.Text = "Save Roles";
            this.btnSaveRoles.UseVisualStyleBackColor = true;
            this.btnSaveRoles.Click += new System.EventHandler(this.btnSaveRoles_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(22, 15);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grdRoles
            // 
            this.grdRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRoles.Location = new System.Drawing.Point(3, 3);
            this.grdRoles.Name = "grdRoles";
            this.grdRoles.Size = new System.Drawing.Size(270, 230);
            this.grdRoles.TabIndex = 2;
            // 
            // UserManagment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tabControl1);
            this.Name = "UserManagment";
            this.Text = "UserManagment";
            this.Load += new System.EventHandler(this.UserManagment_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabUsers.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUsers)).EndInit();
            this.tabRoles.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRoles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.TabPage tabRoles;
        private System.Windows.Forms.DataGridView grdUsers;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView grdRoles;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSaveUsers;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnSaveRoles;
        private System.Windows.Forms.Button button3;
    }
}