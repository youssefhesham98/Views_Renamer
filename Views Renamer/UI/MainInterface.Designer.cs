namespace Views_Renamer.UI
{
    partial class MainInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainInterface));
            this.arc = new System.Windows.Forms.Button();
            this.str = new System.Windows.Forms.Button();
            this.elec = new System.Windows.Forms.Button();
            this.hvac = new System.Windows.Forms.Button();
            this.ff = new System.Windows.Forms.Button();
            this.pb = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.edecs = new System.Windows.Forms.PictureBox();
            this.infra = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edecs)).BeginInit();
            this.SuspendLayout();
            // 
            // arc
            // 
            this.arc.Location = new System.Drawing.Point(12, 93);
            this.arc.Name = "arc";
            this.arc.Size = new System.Drawing.Size(75, 25);
            this.arc.TabIndex = 1;
            this.arc.Text = "Architecture";
            this.arc.UseVisualStyleBackColor = true;
            this.arc.Click += new System.EventHandler(this.arc_Click);
            // 
            // str
            // 
            this.str.Location = new System.Drawing.Point(93, 93);
            this.str.Name = "str";
            this.str.Size = new System.Drawing.Size(75, 25);
            this.str.TabIndex = 1;
            this.str.Text = "Structure";
            this.str.UseVisualStyleBackColor = true;
            this.str.Click += new System.EventHandler(this.str_Click);
            // 
            // elec
            // 
            this.elec.Location = new System.Drawing.Point(174, 93);
            this.elec.Name = "elec";
            this.elec.Size = new System.Drawing.Size(75, 25);
            this.elec.TabIndex = 4;
            this.elec.Text = "Electric";
            this.elec.UseVisualStyleBackColor = true;
            this.elec.Click += new System.EventHandler(this.elec_Click);
            // 
            // hvac
            // 
            this.hvac.Location = new System.Drawing.Point(255, 93);
            this.hvac.Name = "hvac";
            this.hvac.Size = new System.Drawing.Size(75, 25);
            this.hvac.TabIndex = 5;
            this.hvac.Text = "HVAC";
            this.hvac.UseVisualStyleBackColor = true;
            this.hvac.Click += new System.EventHandler(this.hvac_Click);
            // 
            // ff
            // 
            this.ff.Location = new System.Drawing.Point(336, 93);
            this.ff.Name = "ff";
            this.ff.Size = new System.Drawing.Size(75, 25);
            this.ff.TabIndex = 6;
            this.ff.Text = "Fire Fighting";
            this.ff.UseVisualStyleBackColor = true;
            this.ff.Click += new System.EventHandler(this.ff_Click);
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(417, 93);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(75, 25);
            this.pb.TabIndex = 7;
            this.pb.Text = "Plumbing";
            this.pb.UseVisualStyleBackColor = true;
            this.pb.Click += new System.EventHandler(this.pb_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Views_Renamer.Properties.Resources.VIEWS_RENAMER;
            this.pictureBox1.Location = new System.Drawing.Point(-16, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(570, 75);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // edecs
            // 
            this.edecs.BackColor = System.Drawing.Color.Transparent;
            this.edecs.Image = global::Views_Renamer.Properties.Resources.EDECS;
            this.edecs.Location = new System.Drawing.Point(682, 87);
            this.edecs.Name = "edecs";
            this.edecs.Size = new System.Drawing.Size(129, 50);
            this.edecs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.edecs.TabIndex = 2;
            this.edecs.TabStop = false;
            this.edecs.Click += new System.EventHandler(this.edecs_Click);
            // 
            // infra
            // 
            this.infra.Location = new System.Drawing.Point(498, 93);
            this.infra.Name = "infra";
            this.infra.Size = new System.Drawing.Size(75, 25);
            this.infra.TabIndex = 8;
            this.infra.Text = "Infrastructure";
            this.infra.UseVisualStyleBackColor = true;
            this.infra.Click += new System.EventHandler(this.infra_Click);
            // 
            // MainInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 138);
            this.Controls.Add(this.infra);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.ff);
            this.Controls.Add(this.hvac);
            this.Controls.Add(this.elec);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.edecs);
            this.Controls.Add(this.str);
            this.Controls.Add(this.arc);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(101)))), ((int)(((byte)(96)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainInterface";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Views Renamer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edecs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button arc;
        private System.Windows.Forms.Button str;
        private System.Windows.Forms.PictureBox edecs;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button elec;
        private System.Windows.Forms.Button hvac;
        private System.Windows.Forms.Button ff;
        private System.Windows.Forms.Button pb;
        private System.Windows.Forms.Button infra;
    }
}