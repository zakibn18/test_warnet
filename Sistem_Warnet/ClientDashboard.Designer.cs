namespace Sistem_Warnet
{
    partial class ClientDashboard
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
            this.lblSisaWaktu = new System.Windows.Forms.Label();
            this.lblInfoPC = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblSisaWaktu
            // 
            this.lblSisaWaktu.AutoSize = true;
            this.lblSisaWaktu.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSisaWaktu.Location = new System.Drawing.Point(556, 9);
            this.lblSisaWaktu.Name = "lblSisaWaktu";
            this.lblSisaWaktu.Size = new System.Drawing.Size(170, 33);
            this.lblSisaWaktu.TabIndex = 0;
            this.lblSisaWaktu.Text = "Sisa Waktu:";
            this.lblSisaWaktu.Click += new System.EventHandler(this.lblSisaWaktu_Click);
            // 
            // lblInfoPC
            // 
            this.lblInfoPC.AutoSize = true;
            this.lblInfoPC.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoPC.Location = new System.Drawing.Point(556, 65);
            this.lblInfoPC.Name = "lblInfoPC";
            this.lblInfoPC.Size = new System.Drawing.Size(71, 33);
            this.lblInfoPC.TabIndex = 1;
            this.lblInfoPC.Text = "PC: ";
            this.lblInfoPC.Click += new System.EventHandler(this.lblInfoPC_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.IndianRed;
            this.btnLogout.Location = new System.Drawing.Point(643, 365);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(145, 73);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // ClientDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblInfoPC);
            this.Controls.Add(this.lblSisaWaktu);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.Name = "ClientDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ClientDashboard";
            this.Load += new System.EventHandler(this.ClientDashboard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSisaWaktu;
        private System.Windows.Forms.Label lblInfoPC;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Timer timer1;
    }
}