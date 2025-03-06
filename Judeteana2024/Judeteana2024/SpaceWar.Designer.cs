namespace Judeteana2024
{
    partial class SpaceWar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpaceWar));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.asteroid = new System.Windows.Forms.PictureBox();
            this.bonus = new System.Windows.Forms.PictureBox();
            this.inamic = new System.Windows.Forms.PictureBox();
            this.nava = new System.Windows.Forms.PictureBox();
            this.end = new System.Windows.Forms.PictureBox();
            this.pauza = new System.Windows.Forms.PictureBox();
            this.start = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MesajViteza = new System.Windows.Forms.Timer(this.components);
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.timerSecunde = new System.Windows.Forms.Timer(this.components);
            this.timer30mili = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.asteroid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bonus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inamic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nava)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.end)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pauza)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.start)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(859, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Scor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(862, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Vieti";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(913, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(913, 88);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 4;
            // 
            // asteroid
            // 
            this.asteroid.Image = global::Judeteana2024.Properties.Resources.viata;
            this.asteroid.Location = new System.Drawing.Point(562, 402);
            this.asteroid.Name = "asteroid";
            this.asteroid.Size = new System.Drawing.Size(45, 38);
            this.asteroid.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.asteroid.TabIndex = 12;
            this.asteroid.TabStop = false;
            // 
            // bonus
            // 
            this.bonus.Image = global::Judeteana2024.Properties.Resources.asteroid;
            this.bonus.Location = new System.Drawing.Point(572, 273);
            this.bonus.Name = "bonus";
            this.bonus.Size = new System.Drawing.Size(35, 39);
            this.bonus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bonus.TabIndex = 11;
            this.bonus.TabStop = false;
            // 
            // inamic
            // 
            this.inamic.Image = global::Judeteana2024.Properties.Resources.inamic;
            this.inamic.Location = new System.Drawing.Point(562, 131);
            this.inamic.Name = "inamic";
            this.inamic.Size = new System.Drawing.Size(100, 50);
            this.inamic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.inamic.TabIndex = 9;
            this.inamic.TabStop = false;
            // 
            // nava
            // 
            this.nava.Image = global::Judeteana2024.Properties.Resources.navaMove;
            this.nava.Location = new System.Drawing.Point(40, 149);
            this.nava.Name = "nava";
            this.nava.Size = new System.Drawing.Size(100, 50);
            this.nava.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.nava.TabIndex = 8;
            this.nava.TabStop = false;
            // 
            // end
            // 
            this.end.Image = global::Judeteana2024.Properties.Resources.Stop;
            this.end.Location = new System.Drawing.Point(1029, 150);
            this.end.Name = "end";
            this.end.Size = new System.Drawing.Size(50, 50);
            this.end.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.end.TabIndex = 7;
            this.end.TabStop = false;
            this.end.Click += new System.EventHandler(this.end_Click);
            // 
            // pauza
            // 
            this.pauza.Image = global::Judeteana2024.Properties.Resources.Pauza;
            this.pauza.Location = new System.Drawing.Point(943, 150);
            this.pauza.Name = "pauza";
            this.pauza.Size = new System.Drawing.Size(50, 50);
            this.pauza.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pauza.TabIndex = 6;
            this.pauza.TabStop = false;
            this.pauza.Click += new System.EventHandler(this.pauza_Click);
            // 
            // start
            // 
            this.start.Image = global::Judeteana2024.Properties.Resources.Start;
            this.start.Location = new System.Drawing.Point(858, 150);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(50, 50);
            this.start.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.start.TabIndex = 5;
            this.start.TabStop = false;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 500);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(858, 273);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 13;
            this.textBox3.Visible = false;
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(862, 351);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(177, 89);
            this.axWindowsMediaPlayer1.TabIndex = 14;
            // 
            // timerSecunde
            // 
            this.timerSecunde.Interval = 1000;
            this.timerSecunde.Tick += new System.EventHandler(this.timerSecunde_Tick);
            // 
            // timer30mili
            // 
            this.timer30mili.Interval = 30;
            this.timer30mili.Tick += new System.EventHandler(this.timer30mili_Tick);
            // 
            // SpaceWar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 560);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.asteroid);
            this.Controls.Add(this.bonus);
            this.Controls.Add(this.inamic);
            this.Controls.Add(this.nava);
            this.Controls.Add(this.end);
            this.Controls.Add(this.pauza);
            this.Controls.Add(this.start);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "SpaceWar";
            this.Text = "SpaceWar";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SpaceWar_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.asteroid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bonus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inamic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nava)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.end)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pauza)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.start)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.PictureBox start;
        private System.Windows.Forms.PictureBox pauza;
        private System.Windows.Forms.PictureBox end;
        private System.Windows.Forms.PictureBox nava;
        private System.Windows.Forms.PictureBox inamic;
        private System.Windows.Forms.PictureBox bonus;
        private System.Windows.Forms.PictureBox asteroid;
        private System.Windows.Forms.Timer MesajViteza;
        private System.Windows.Forms.TextBox textBox3;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.Timer timerSecunde;
        private System.Windows.Forms.Timer timer30mili;
    }
}