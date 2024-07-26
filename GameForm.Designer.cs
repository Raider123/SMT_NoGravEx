namespace NoGravEx_Entwurf2
{
    partial class NoGravEx_V2
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoGravEx_V2));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cdown_timer = new System.Windows.Forms.Timer(this.components);
            this.cdown_label = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsEMGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noFeedbackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.game_settings_visual_only = new System.Windows.Forms.ToolStripMenuItem();
            this.game_settings_audio_only = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsEMGToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openWaveplusFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseSensorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.store_emg_timer = new System.Windows.Forms.Timer(this.components);
            this.game_timer = new System.Windows.Forms.Timer(this.components);
            this.meas_button = new System.Windows.Forms.Button();
            this.meas_label = new System.Windows.Forms.Label();
            this.meas_progress_bar = new System.Windows.Forms.ProgressBar();
            this.start_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.curr_sens_label = new System.Windows.Forms.Label();
            this.choice_btn1 = new System.Windows.Forms.Button();
            this.choice_btn2 = new System.Windows.Forms.Button();
            this.choice_btn3 = new System.Windows.Forms.Button();
            this.choice_btn4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(320, 800);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 50);
            this.panel1.TabIndex = 0;
            this.panel1.BackColorChanged += new System.EventHandler(this.Panel1_BackColorChanged);
            this.panel1.Move += new System.EventHandler(this.Panel1_Move);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Calibri", 49.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(300, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 101);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ziel: 0";
            // 
            // cdown_timer
            // 
            this.cdown_timer.Interval = 1000;
            this.cdown_timer.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // cdown_label
            // 
            this.cdown_label.AutoSize = true;
            this.cdown_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cdown_label.ForeColor = System.Drawing.Color.Yellow;
            this.cdown_label.Location = new System.Drawing.Point(12, 22);
            this.cdown_label.Name = "cdown_label";
            this.cdown_label.Size = new System.Drawing.Size(124, 135);
            this.cdown_label.TabIndex = 2;
            this.cdown_label.Text = "0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(35, 34);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 113);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsEMGToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripSeparator1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(212, 58);
            // 
            // settingsEMGToolStripMenuItem
            // 
            this.settingsEMGToolStripMenuItem.Name = "settingsEMGToolStripMenuItem";
            this.settingsEMGToolStripMenuItem.Size = new System.Drawing.Size(211, 24);
            this.settingsEMGToolStripMenuItem.Text = "Settings EMG";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(211, 24);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(208, 6);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameSettingsToolStripMenuItem,
            this.settingsEMGToolStripMenuItem1,
            this.startToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1181, 28);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameSettingsToolStripMenuItem
            // 
            this.gameSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartToolStripMenuItem,
            this.soundToolStripMenuItem,
            this.noFeedbackToolStripMenuItem});
            this.gameSettingsToolStripMenuItem.Name = "gameSettingsToolStripMenuItem";
            this.gameSettingsToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.gameSettingsToolStripMenuItem.Text = "Game Settings";
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // soundToolStripMenuItem
            // 
            this.soundToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onOffToolStripMenuItem});
            this.soundToolStripMenuItem.Name = "soundToolStripMenuItem";
            this.soundToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.soundToolStripMenuItem.Text = "Sound";
            // 
            // onOffToolStripMenuItem
            // 
            this.onOffToolStripMenuItem.Name = "onOffToolStripMenuItem";
            this.onOffToolStripMenuItem.Size = new System.Drawing.Size(138, 26);
            this.onOffToolStripMenuItem.Text = "On/Off";
            this.onOffToolStripMenuItem.Click += new System.EventHandler(this.onOffToolStripMenuItem_Click);
            // 
            // noFeedbackToolStripMenuItem
            // 
            this.noFeedbackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.game_settings_visual_only,
            this.game_settings_audio_only});
            this.noFeedbackToolStripMenuItem.Name = "noFeedbackToolStripMenuItem";
            this.noFeedbackToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.noFeedbackToolStripMenuItem.Text = "No Feedback";
            // 
            // game_settings_visual_only
            // 
            this.game_settings_visual_only.Name = "game_settings_visual_only";
            this.game_settings_visual_only.Size = new System.Drawing.Size(164, 26);
            this.game_settings_visual_only.Text = "Visual only";
            this.game_settings_visual_only.Click += new System.EventHandler(this.visual_feedback_only);
            // 
            // game_settings_audio_only
            // 
            this.game_settings_audio_only.Name = "game_settings_audio_only";
            this.game_settings_audio_only.Size = new System.Drawing.Size(164, 26);
            this.game_settings_audio_only.Text = "Audio only";
            this.game_settings_audio_only.Click += new System.EventHandler(this.audio_feedback_only);
            // 
            // settingsEMGToolStripMenuItem1
            // 
            this.settingsEMGToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openWaveplusFormToolStripMenuItem,
            this.chooseSensorToolStripMenuItem});
            this.settingsEMGToolStripMenuItem1.Name = "settingsEMGToolStripMenuItem1";
            this.settingsEMGToolStripMenuItem1.Size = new System.Drawing.Size(111, 24);
            this.settingsEMGToolStripMenuItem1.Text = "EMG Settings";
            // 
            // openWaveplusFormToolStripMenuItem
            // 
            this.openWaveplusFormToolStripMenuItem.Name = "openWaveplusFormToolStripMenuItem";
            this.openWaveplusFormToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.openWaveplusFormToolStripMenuItem.Text = "Open Waveplus Form";
            this.openWaveplusFormToolStripMenuItem.Click += new System.EventHandler(this.openWaveplusFormToolStripMenuItem_Click);
            // 
            // chooseSensorToolStripMenuItem
            // 
            this.chooseSensorToolStripMenuItem.Name = "chooseSensorToolStripMenuItem";
            this.chooseSensorToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.chooseSensorToolStripMenuItem.Text = "Choose Sensor";
            this.chooseSensorToolStripMenuItem.Click += new System.EventHandler(this.chooseSensorToolStripMenuItem_Click_1);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(54, 24);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // store_emg_timer
            // 
            this.store_emg_timer.Interval = 10;
            this.store_emg_timer.Tick += new System.EventHandler(this.store_emg_timer_Tick);
            // 
            // game_timer
            // 
            this.game_timer.Interval = 10;
            this.game_timer.Tick += new System.EventHandler(this.game_timer_Tick);
            // 
            // meas_button
            // 
            this.meas_button.BackColor = System.Drawing.Color.Brown;
            this.meas_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.meas_button.Font = new System.Drawing.Font("Calibri", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.meas_button.ForeColor = System.Drawing.Color.White;
            this.meas_button.Location = new System.Drawing.Point(349, 601);
            this.meas_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.meas_button.Name = "meas_button";
            this.meas_button.Size = new System.Drawing.Size(300, 100);
            this.meas_button.TabIndex = 10;
            this.meas_button.Text = "Start";
            this.meas_button.UseVisualStyleBackColor = false;
            this.meas_button.Click += new System.EventHandler(this.meas_button_Click);
            // 
            // meas_label
            // 
            this.meas_label.AutoSize = true;
            this.meas_label.Font = new System.Drawing.Font("Calibri", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.meas_label.ForeColor = System.Drawing.Color.White;
            this.meas_label.Location = new System.Drawing.Point(320, 233);
            this.meas_label.Name = "meas_label";
            this.meas_label.Size = new System.Drawing.Size(667, 97);
            this.meas_label.TabIndex = 11;
            this.meas_label.Text = "Maximum Strength";
            this.meas_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // meas_progress_bar
            // 
            this.meas_progress_bar.Location = new System.Drawing.Point(549, 421);
            this.meas_progress_bar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.meas_progress_bar.Name = "meas_progress_bar";
            this.meas_progress_bar.Size = new System.Drawing.Size(200, 59);
            this.meas_progress_bar.TabIndex = 12;
            // 
            // start_button
            // 
            this.start_button.BackColor = System.Drawing.Color.Orange;
            this.start_button.Enabled = false;
            this.start_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.start_button.Font = new System.Drawing.Font("Calibri", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.start_button.ForeColor = System.Drawing.Color.White;
            this.start_button.Location = new System.Drawing.Point(700, 601);
            this.start_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(300, 100);
            this.start_button.TabIndex = 10;
            this.start_button.Text = "Press to Start Game";
            this.start_button.UseVisualStyleBackColor = false;
            this.start_button.Visible = false;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Calibri", 64.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(980, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(269, 133);
            this.label3.TabIndex = 14;
            this.label3.Text = "MVC";
            // 
            // curr_sens_label
            // 
            this.curr_sens_label.AutoSize = true;
            this.curr_sens_label.BackColor = System.Drawing.Color.Transparent;
            this.curr_sens_label.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.curr_sens_label.ForeColor = System.Drawing.Color.White;
            this.curr_sens_label.Location = new System.Drawing.Point(12, 693);
            this.curr_sens_label.Name = "curr_sens_label";
            this.curr_sens_label.Size = new System.Drawing.Size(155, 28);
            this.curr_sens_label.TabIndex = 15;
            this.curr_sens_label.Text = "Current sensor:";
            // 
            // choice_btn1
            // 
            this.choice_btn1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.choice_btn1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.choice_btn1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.choice_btn1.ForeColor = System.Drawing.Color.White;
            this.choice_btn1.Location = new System.Drawing.Point(280, 400);
            this.choice_btn1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.choice_btn1.Name = "choice_btn1";
            this.choice_btn1.Size = new System.Drawing.Size(200, 165);
            this.choice_btn1.TabIndex = 16;
            this.choice_btn1.Text = "Visual \r\n+ \r\nAudio\r\n\r\n\r\n\r\n\r\n";
            this.choice_btn1.UseVisualStyleBackColor = false;
            this.choice_btn1.Visible = false;
            this.choice_btn1.Click += new System.EventHandler(this.choice_btn1_Click);
            // 
            // choice_btn2
            // 
            this.choice_btn2.BackColor = System.Drawing.Color.DimGray;
            this.choice_btn2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.choice_btn2.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.choice_btn2.ForeColor = System.Drawing.Color.White;
            this.choice_btn2.Location = new System.Drawing.Point(511, 400);
            this.choice_btn2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.choice_btn2.Name = "choice_btn2";
            this.choice_btn2.Size = new System.Drawing.Size(200, 165);
            this.choice_btn2.TabIndex = 17;
            this.choice_btn2.Text = "Visual Only";
            this.choice_btn2.UseVisualStyleBackColor = false;
            this.choice_btn2.Visible = false;
            this.choice_btn2.Click += new System.EventHandler(this.choice_btn2_Click);
            // 
            // choice_btn3
            // 
            this.choice_btn3.BackColor = System.Drawing.Color.DimGray;
            this.choice_btn3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.choice_btn3.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.choice_btn3.ForeColor = System.Drawing.Color.White;
            this.choice_btn3.Location = new System.Drawing.Point(971, 400);
            this.choice_btn3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.choice_btn3.Name = "choice_btn3";
            this.choice_btn3.Size = new System.Drawing.Size(200, 165);
            this.choice_btn3.TabIndex = 18;
            this.choice_btn3.Text = "Audio Only";
            this.choice_btn3.UseVisualStyleBackColor = false;
            this.choice_btn3.Visible = false;
            this.choice_btn3.Click += new System.EventHandler(this.choice_btn3_Click);
            // 
            // choice_btn4
            // 
            this.choice_btn4.BackColor = System.Drawing.Color.DimGray;
            this.choice_btn4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.choice_btn4.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.choice_btn4.ForeColor = System.Drawing.Color.White;
            this.choice_btn4.Location = new System.Drawing.Point(739, 400);
            this.choice_btn4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.choice_btn4.Name = "choice_btn4";
            this.choice_btn4.Size = new System.Drawing.Size(200, 165);
            this.choice_btn4.TabIndex = 19;
            this.choice_btn4.Text = "Normal\r\n+ \r\nMoving Bar\r\n\r\n";
            this.choice_btn4.UseVisualStyleBackColor = false;
            this.choice_btn4.Visible = false;
            this.choice_btn4.Click += new System.EventHandler(this.choice_btn4_Click);
            // 
            // NoGravEx_V2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1181, 730);
            this.Controls.Add(this.choice_btn4);
            this.Controls.Add(this.choice_btn3);
            this.Controls.Add(this.choice_btn2);
            this.Controls.Add(this.choice_btn1);
            this.Controls.Add(this.curr_sens_label);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.meas_progress_bar);
            this.Controls.Add(this.meas_label);
            this.Controls.Add(this.meas_button);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cdown_label);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "NoGravEx_V2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NoGravEx";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NoGravEx_V2_FormClosed);
            this.Load += new System.EventHandler(this.NoGravEx_V2_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NoGravEx_V2_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer cdown_timer;
        private System.Windows.Forms.Label cdown_label;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsEMGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsEMGToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openWaveplusFormToolStripMenuItem;
        private System.Windows.Forms.Timer store_emg_timer;
        private System.Windows.Forms.Timer game_timer;
        private System.Windows.Forms.ToolStripMenuItem gameSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem soundToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onOffToolStripMenuItem;
        private System.Windows.Forms.Button meas_button;
        private System.Windows.Forms.Label meas_label;
        private System.Windows.Forms.ProgressBar meas_progress_bar;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label curr_sens_label;
        private System.Windows.Forms.ToolStripMenuItem chooseSensorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noFeedbackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem game_settings_visual_only;
        private System.Windows.Forms.ToolStripMenuItem game_settings_audio_only;
        private System.Windows.Forms.Button choice_btn1;
        private System.Windows.Forms.Button choice_btn2;
        private System.Windows.Forms.Button choice_btn3;
        private System.Windows.Forms.Button choice_btn4;
    }
}

