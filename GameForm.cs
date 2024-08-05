using System;
using System.Collections;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Waveplus.DaqExample;
using Waveplus_Daq_Example;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace NoGravEx_Entwurf2
{
    public partial class NoGravEx_V2 : Form
    {
        // The EMG example program
        private WaveplusDaqExampleForm wdaq;

        // Strings used during the measurement state
        private string max_str = "Maximum Strength";
        private string relax_str = "Relax";
        private string curr_sensor_str = "Current sensor: ";
        // Specify the filepath for the emg-samples
        public string filePath;

        // Choose the standard sensor upon startup
        private int current_sensor = 1;

        // Relative variables for the pillar etc.
        private int side_length, upper_point_x, upper_point_y;
        // Maximum Game Sum and Array Length (goals to be achieved); Goal variables
        private int curr_goal;
        private int goal_arr_length = 9;
        private int[] goal_arr;
        private int num_of_success = 0; // Counts the times, there has been success
        private int passed_time = 0;
        // Current position of the white panel in the pillar (1, 5, 10)
        private int current_pos;
        // Countdown Settings
        private int c_seconds = 2; // Right now, the time is more like 3s, which can be adjusted
        // Measuring duration
        private int meas_time_sec = 3;
        private int num_c_ticks;
        // Game Duration
        private int game_duration_sec = 10; // no real function, because the game loop is infinite
        private int num_g_ticks;

        // Constants that need to be stored
        public double maximum_contraction_uV = 0;
        public double relaxed_contraction_uV = 0;
        // Arraylist to store data dynamically for the average of the emg samples
        public ArrayList emg_array_list;
        public double average_value = 0;
        // For the moving average of the emg samples during the game we create an instance of MovingAverage
        public MovingAverage simple_ma;

        // For storing the data files in a sample (.emg_txt file)
        public StringBuilder emg_txt;

        //In order to deactivate one label in the infinite_mode
        public bool infinite_mode = false;


        /// <summary>
        /// We implement two states for now:
        /// 1. Measuring State - MVC, resting contraction
        /// 2. Game State - pillar, realtime data streaming
        /// </summary>
        public enum programStates
        {
            meas_state,
            game_state,
            choice_state,
            game_over_state,
            audio_only_state,
            visual_only_state_1, // only the pillar is shown
            visual_only_state_2 // just like the game_state but the white pillar/goal_label only appears when in correct area
        }

        public programStates current_state = programStates.meas_state;

        // Implements the sound player for the three states (excluding zero)
        public Sound_choice musicplayer;

        public bool sound_enabled = false;

        private bool no_feedback_state = false;

        public NoGravEx_V2()
        {
            InitializeComponent();

            this.Height = 800;
            this.Width = 1000;

            this.WindowState = FormWindowState.Maximized;

            /**
             *     Functions related to the game state
             */
            // Can be modified - Constants for the side lenghts for relative scaling
            this.side_length = this.Height / 5;
            this.upper_point_x = this.Width / 2;
            this.upper_point_y = this.Height / 8;

            // Determines how often data is called during the realtime emg_sample streaming.
            this.num_g_ticks = this.game_duration_sec * 1000 / 10;

            // Determines how many samples (ticks/calls) are taken for the MVC/relaxed - average
            this.num_c_ticks = this.meas_time_sec * 1000 / 10;

            // For implementing the curr_goal, that needs to be achieved
            this.create_game_queue(goal_arr_length);

            //////
           // for(int i = 0; i < goal_arr.Length; i++)
           // {
          //      Debug.WriteLine($"goal_arr[{i}] = {goal_arr[i]}");
            //}
            ///////

            // Set the position of the cursor once in the beginning
            this.Set_cursor_pos();

            // Set the label position once in the beginning
            this.Set_label_pos();


            // Refresh the curr_goal once in the beginning
            this.Refresh_goal();

            // Reset the countdown timer in the beginning
            this.Reset_countdown();

            // Hide the initial picture box
            this.Hide_picturebox();

            // Setting the filepath
            this.set_file_path();

            // Saving the emg-samples in a .emg_txt file
            emg_txt = new StringBuilder();
            var newLine = string.Format("[uV]");
            emg_txt.AppendLine(newLine);

            // Implementing the moving average
            /** 100ms for the time-interval, ideal: 40
             * 10ms for time-interval, ideal:?
             * */
            this.simple_ma = new MovingAverage(k: 100); // very variant at 80; very ideal at 180

            // Creating a new sound_music player class
            this.musicplayer = new Sound_choice();

            /**
              *     Functions related to the meas state
              */
            // Progress Bar settings
            this.meas_progress_bar.SetState(1); // Assigns a different style to the progressbar (cf. static main class)
            this.meas_progress_bar.Maximum = this.num_c_ticks; // Progessbar updates after each sample is stored
            this.meas_progress_bar.Step = 1;

            // Adjust the settings according to the current program state (meas_state)
            this.change_program_state();

            // Extremly important for the programs purpose (extremly simple integration)
            wdaq = new WaveplusDaqExampleForm();
            wdaq.Show(); // Make the waveplus form visible

            /**
             * Edit here to change the standard sensor (on startup)
             */
            // Choose the standard current sensor
            // The standard sensor is 0
            this.change_sensor(this.current_sensor);
            this.curr_sens_label.Location = new Point(10, this.Height - 100);


            // ***** Updating the full-screen ****** 
            this.Resize += new EventHandler(Form_Resize);
            UpdateLayout(); // Initiale Layout-Einstellung
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            this.Invalidate(); // Fordert eine Neuzeichnung an

            //Change the bar positions
            this.upper_point_x = this.Width / 2;
            this.upper_point_y = this.Height / 4;

            //Change all elements
            UpdateLayout();

            label1.Location = new Point(label1.Location.X - 20, 100);
            panel1.Location = new Point(panel1.Location.X + 5, panel1.Location.Y);
        }

        private void UpdateLayout()
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Define the elements to be centered as a group
            Control[] controlsToCenter = new Control[]
            {
        panel1, label1, cdown_label, pictureBox1, meas_button, meas_label,
        meas_progress_bar, start_button, label3, curr_sens_label,
        choice_btn1, choice_btn2, choice_btn3, choice_btn4
            };

            // Calculate the total width and height of the group
            int totalWidth = controlsToCenter.Max(c => c.Right) - controlsToCenter.Min(c => c.Left);
            int totalHeight = controlsToCenter.Max(c => c.Bottom) - controlsToCenter.Min(c => c.Top);

            // Calculate the top-left position for the group
            int startX = (formWidth - totalWidth) / 2;
            int startY = (formHeight - totalHeight) / 2;

            // Calculate offsets for each element and set their new positions
            int offsetX = startX - controlsToCenter.Min(c => c.Left);
            int offsetY = startY - controlsToCenter.Min(c => c.Top);

            foreach (var control in controlsToCenter)
            {
                control.Location = new Point(control.Left + offsetX, control.Top + offsetY);
            }
        }

        /// <summary>
        /// Change the selected sensor by simply pressing a number key. There are 10 sensor in total.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoGravEx_V2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                chooseSensorToolStripMenuItem_Click_1(sender, e);
            }
        }

        /// <summary>
        /// Changes the sensor in the waveplus device.
        /// </summary>
        /// <param name="sensor_num"></param>
        private void change_sensor(int sensor_num)
        {
            if (wdaq != null)
            {
                wdaq.current_sensor = sensor_num;

                curr_sens_label.Text = curr_sensor_str + " " + sensor_num;
            }
        }

        private void set_file_path()
        {
            DateTime now = DateTime.Now;

            string timestamp = now.ToString("yyyyMMdd_HHmmss");

            string name_of_file = $"SAMPLES_{timestamp}.txt";

            this.filePath = Path.Combine(Environment.CurrentDirectory, @"emg_sample_files\", name_of_file);
        }

        /// <summary>
        /// Defines the methods and attributes in the meas and game state.
        /// </summary>
        private void change_program_state()
        {
            this.Refresh();

            if (this.current_state == programStates.meas_state)
            {
                this.no_feedback_state = false;
                this.sound_enabled = false;

                panel1.Visible = false;
                pictureBox1.Visible = false;
                label1.Visible = false;
                label3.Visible = false;

                start_button.Visible = false;
                start_button.Enabled = false;
                meas_button.Visible = true;
                meas_label.Visible = true;
                meas_progress_bar.Visible = true;

                cdown_label.Visible = true;

                choice_btn1.Visible = false;
                choice_btn2.Visible = false;
                choice_btn3.Visible = false;
                choice_btn4.Visible = false;

                if (maximum_contraction_uV == 0 && relaxed_contraction_uV == 0)
                {
                    meas_button.Text = "Start";
                }
                else
                {
                    meas_button.Text = "Repeat";
                }

                // Activate the meas_button after 6 seconds idle
                meas_button.Enabled = false;

                meas_label.Text = max_str;
                curr_goal = 3;

                // Timer after startup (6 seconds) to delete the idle values of the waveplus memory
                c_seconds = 6;
                cdown_timer.Start();
            }
            else if (this.current_state == programStates.game_state) // visual and auditive support
            {
                this.no_feedback_state = false;
                this.sound_enabled = true;

                panel1.Visible = true;
                pictureBox1.Visible = true;
                label1.Visible = true;
                if (infinite_mode == false)
                {
                    label3.Visible = true;
                }
                
                meas_button.Visible = false;
                meas_button.Enabled = false;
                meas_label.Visible = false;
                meas_progress_bar.Visible = false;
                start_button.Visible = false;
                start_button.Enabled = false;

                cdown_label.Visible = true;

                choice_btn1.Visible = false;
                choice_btn2.Visible = false;
                choice_btn3.Visible = false;
                choice_btn4.Visible = false;

                Hide_picturebox();
                Refresh_goal();
                Reset_countdown();

                if (this.sound_enabled)
                {
                    musicplayer.speak_goal(curr_goal);
                }
                emg_txt.AppendLine("NEXT-GOAL: " + curr_goal.ToString());

            }
            else if (this.current_state == programStates.audio_only_state) // auditive support only (when the white bar is registered in the corresponding place)
            {
                this.no_feedback_state = true;
                this.sound_enabled = true;

                panel1.Visible = false;
                pictureBox1.Visible = false;
                label1.Visible = false;
                label3.Visible = false;

                meas_button.Visible = false;
                meas_button.Enabled = false;
                meas_label.Visible = false;
                meas_progress_bar.Visible = false;
                start_button.Visible = false;
                start_button.Enabled = false;

                cdown_label.Visible = false;

                choice_btn1.Visible = false;
                choice_btn2.Visible = false;
                choice_btn3.Visible = false;
                choice_btn4.Visible = false;


                //Because the first goal to be reached is not a number, but 0, we need to adjust the bar to start in the middle
                //because the no-feedback-mode only fires when the former 0 state (from visual+audio) is reached.
                //Thus in order to differentiate, we need to begin a bit higher on the pillar for the countdown to start.
                Reset_goal();
                panel1.Location = new Point(panel1.Location.X, upper_point_y + 2 * side_length);
                Hide_picturebox();
                Reset_countdown();

                if (this.sound_enabled)
                {
                    musicplayer.speak_goal(curr_goal);
                }
                emg_txt.AppendLine("NEXT-GOAL: " + curr_goal.ToString());

            }
            else if (this.current_state == programStates.visual_only_state_1) // visual support (only the pillar) the whole time
            {
                this.no_feedback_state = true;

                panel1.Visible = false;
                pictureBox1.Visible = false;
                label1.Visible = true;
                label3.Visible = false;

                meas_button.Visible = false;
                meas_button.Enabled = false;
                meas_label.Visible = false;
                meas_progress_bar.Visible = false;
                start_button.Visible = false;
                start_button.Enabled = false;

                cdown_label.Visible = false;

                choice_btn1.Visible = false;
                choice_btn2.Visible = false;
                choice_btn3.Visible = false;
                choice_btn4.Visible = false;

                //Because the first goal to be reached is not a number, but 0, we need to adjust the bar to start in the middle
                //because the no-feedback-mode only fires when the former 0 state (from visual+audio) is reached.
                //Thus in order to differentiate, we need to begin a bit higher on the pillar for the countdown to start.
                Reset_goal();
                panel1.Location = new Point(panel1.Location.X, upper_point_y + 2 * side_length);
                Hide_picturebox();
                Reset_countdown();

                if (this.sound_enabled)
                {
                    musicplayer.speak_goal(curr_goal);
                }
                emg_txt.AppendLine("NEXT-GOAL: " + curr_goal.ToString());
            }
            else if(this.current_state == programStates.visual_only_state_2)
            {
                this.no_feedback_state = false;
                this.sound_enabled = true;

                panel1.Visible = false; // white bar is only visible in the correct zones (at the start not visible)
                pictureBox1.Visible = true;
                label1.Visible = true;
                if (infinite_mode == false)
                {
                    label3.Visible = true;
                }

                meas_button.Visible = false;
                meas_button.Enabled = false;
                meas_label.Visible = false;
                meas_progress_bar.Visible = false;
                start_button.Visible = false;
                start_button.Enabled = false;

                cdown_label.Visible = true;

                choice_btn1.Visible = false;
                choice_btn2.Visible = false;
                choice_btn3.Visible = false;
                choice_btn4.Visible = false;

                Hide_picturebox();
                Refresh_goal();
                Reset_countdown();

                if (this.sound_enabled)
                {
                    musicplayer.speak_goal(curr_goal);
                }
                emg_txt.AppendLine("NEXT-GOAL: " + curr_goal.ToString());

            }
            else if (this.current_state == programStates.choice_state)
            {
                this.no_feedback_state = false;
                panel1.Visible = false;
                pictureBox1.Visible = false;
                label1.Visible = false;
                label3.Visible = false;
                meas_button.Visible = false;
                meas_button.Enabled = false;
                meas_progress_bar.Visible = false;
                start_button.Visible = false;
                start_button.Enabled = false;
                cdown_label.Visible = false;

                // Display three buttons
                meas_label.Visible = true;
                meas_label.Text = "Game Mode";

                choice_btn1.Visible = true;
                choice_btn2.Visible = true;
                choice_btn3.Visible = true;
                choice_btn4.Visible = true;
            }
            else if (this.current_state == programStates.game_over_state)
            {
                this.no_feedback_state = false;
                this.sound_enabled = false;
                panel1.Visible = false;
                pictureBox1.Visible = false;
                label1.Visible = false;
                label3.Visible = false;
                meas_button.Visible = false;
                meas_button.Enabled = false;
                meas_label.Visible = true;
                meas_progress_bar.Visible = false;
                start_button.Visible = false;
                start_button.Enabled = false;
                cdown_label.Visible = false;
                choice_btn1.Visible = false;
                choice_btn2.Visible = false;
                choice_btn3.Visible = false;
                choice_btn4.Visible = false;

                meas_label.Text = "Succesful - The End";


                // Finish the .txt document by appending all the emg-samples
                File.WriteAllText(filePath, emg_txt.ToString());

                game_timer.Stop();
                cdown_timer.Stop();
            }
        }

        /// <summary>
        /// Creates an array which contains a random sequence of 1, 5, 10 values based on the array length and the maximum array sum
        /// 1. Maximum Sum and Array Length given
        /// 2. Fill the Array equally with 1s, 5s and 10s
        /// 3. Shuffle the entire Array, so it stays the same
        /// </summary>
        /// <param name="sum"></param>
        private void create_game_queue(int arr_length)
        {
            this.goal_arr = new int[arr_length];
            int temp = (int)Math.Ceiling((decimal)arr_length / 3);

            int i;
            for (i = 0; i < temp; i++)
            {
                this.goal_arr[i] = 1;
            }
            int z;
            for (z = temp; z < 2 * temp; z++)
            {
                this.goal_arr[z] = 5;
            }
            int y;
            for (y = 2 * temp; y < 3 * temp; y++)
            {
                this.goal_arr[y] = 10;
            }

            // Shuffling the array
            Random rnd = new Random();
            this.goal_arr = goal_arr.OrderBy(x => rnd.Next()).ToArray();
        }

        /// <summary>
        /// 
        /// Super class for drawing the game on the canvas
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g2d = e.Graphics;

            if (this.current_state == programStates.game_state || 
                this.current_state == programStates.visual_only_state_1 || 
                this.current_state == programStates.visual_only_state_2)
            {
                this.Draw_pillar(g2d);
                this.Draw_pillar_text(g2d);
            }
        }

        /// <summary>
        /// 
        /// Event-triggered panel - if the panel moves, it triggers a positional check function which in turn triggers a countdown
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel1_Move(object sender, System.EventArgs e)
        {
            Check_position();
        }

        /// <summary>
        /// 
        /// The purpose of Timer1_Tick is implemented for the countdown timer. 
        /// After a time_interval set by the countdown in the GameForm settings (here 1 second).
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            // Write the countdown time to the text label
            cdown_label.Text = c_seconds--.ToString();

            if (c_seconds < 0 )
            {
                if (no_feedback_state)
                {
                    if (curr_goal != 0)
                    {
                        num_of_success += 1;
                    }
                    
                    if(num_of_success < goal_arr_length)
                    {
                        Refresh_goal();
                        Reset_countdown();

                        if (sound_enabled)
                        {
                            musicplayer.speak_goal(curr_goal);
                        }
                        emg_txt.AppendLine("NEXT-GOAL: " + curr_goal.ToString());
                    }

                    // After a successful countdown sequence
                    cdown_timer.Stop();
                }
                else
                {
                    Show_succesful();
                    if (sound_enabled)
                    {
                        musicplayer.play_successful(curr_goal);
                    }

                    if (curr_goal == 0)
                    {
                        if (num_of_success < goal_arr_length)
                        {
                            Refresh_goal();
                            Reset_countdown();

                            if (sound_enabled)
                            {
                                musicplayer.speak_goal(curr_goal);
                            }
                            emg_txt.AppendLine("NEXT-GOAL: " + curr_goal.ToString());
                        }
                    }
                    else if (curr_goal == -1)
                    {
                        meas_button.Enabled = true;
                        meas_label.Text = relax_str;
                        meas_progress_bar.Value = 0;
                        Hide_picturebox();
                        cdown_label.Text = "";

                    }
                    else if (curr_goal == -2)
                    {

                        meas_button.Enabled = true;
                        meas_button.Text = "Repeat";

                        start_button.Visible = true;
                        start_button.Enabled = true;
                    }
                    else if (curr_goal == 3)
                    {
                        meas_button.Enabled = true;
                    }
                    else
                    {
                        emg_txt.AppendLine("SUCCES AT Goal: " + curr_goal.ToString());
                        num_of_success += 1;

                        Reset_goal();
                    }

                    // After a successful countdown sequence
                    cdown_timer.Stop();
                }
            }

        }

        /// <summary>
        /// 
        /// Due to our event-triggered architecture, once the positional function changes the color,
        /// then we can initiate a countdown timer (c_down_timer) or so far initiate a timer for the measuring of the MVC or relaxed value
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel1_BackColorChanged(object sender, EventArgs e)
        {
            // If the color of the cursor changes to red, we want to start the timer
            // If the color changes to white, we want the countdown label to reset
            if (panel1.BackColor == Color.Red)
            {
                cdown_timer.Start();
                if (sound_enabled && no_feedback_state == false)
                {
                    musicplayer.play_sound(curr_goal);
                }

                // activate the panel when visual_only_state_2 is chosen (deactivated at the start, starts with timer)
                if (this.current_state == programStates.visual_only_state_2)
                {
                    panel1.Visible = true;
                }
            }
            else
            {
                cdown_timer.Stop();
                if (sound_enabled && no_feedback_state == false)
                {
                    musicplayer.stop_sound();
                }
                Reset_countdown();
                Hide_picturebox();

                // deactivate the panel when visual_only_state_2 is chosen (after the timer stops)
                if (this.current_state == programStates.visual_only_state_2)
                {
                    panel1.Visible = false;
                }
            }

        }

        /// <summary>
        /// 
        /// Checks the position of the panel (moving white bar) and initates a color change upon activation.
        /// 
        /// </summary>
        private void Check_position()
        {
            int y_pos = panel1.Location.Y;

            if (no_feedback_state)
            {
                if (y_pos > upper_point_y + 3 * side_length && y_pos + panel1.Size.Height < upper_point_y + 4 * side_length)
                {
                    // Wert 0
                    panel1.BackColor = Color.Red;
                }
                else
                {
                    panel1.BackColor = Color.White;
                }
            }
            else
            {
                if (y_pos > upper_point_y && y_pos + panel1.Size.Height < upper_point_y + side_length && curr_goal == 10)
                {
                    // Wert 10
                    panel1.BackColor = Color.Red;
                }
                else if (y_pos > upper_point_y + side_length && y_pos + panel1.Size.Height < upper_point_y + 2 * side_length && curr_goal == 5)
                {
                    // Wert 5
                    panel1.BackColor = Color.Red;
                }
                else if (y_pos > upper_point_y + 2 * side_length && y_pos + panel1.Size.Height < upper_point_y + 3 * side_length && curr_goal == 1)
                {
                    // Wert 1
                    panel1.BackColor = Color.Red;
                }
                else if (y_pos > upper_point_y + 3 * side_length && y_pos + panel1.Size.Height < upper_point_y + 4 * side_length && curr_goal == 0)
                {
                    // Wert 0
                    panel1.BackColor = Color.Red;
                }
                else
                {
                    panel1.BackColor = Color.White;
                }
            }
        }

        /// <summary>
        /// 
        /// Static Function that draws the pillar including all rectangles based on the actual window height and size. 
        /// 
        /// </summary>
        /// <param name="g"></param>
        private void Draw_pillar(Graphics g)
        {

            Brush b1 = new SolidBrush(Color.Yellow);
            Brush b2 = new SolidBrush(Color.Violet);
            Brush b3 = new SolidBrush(Color.Green);
            Brush b4 = new SolidBrush(Color.Blue);

            Brush sb1 = new SolidBrush(Color.Orange);
            Brush sb2 = new SolidBrush(Color.DarkViolet);
            Brush sb3 = new SolidBrush(Color.DarkGreen);

            // Declare the array size of the rectangle
            Rectangle[] stand_rect = new Rectangle[4];
            Rectangle[] sub_stand_rect = new Rectangle[3];

            for (int i = 0; i < stand_rect.Length; i++)
            {
                int x_pos = this.upper_point_x - side_length / 2;
                int y_pos = (this.upper_point_y) + side_length * i;
                stand_rect[i] = new Rectangle(x_pos, y_pos, side_length, side_length);
            }

            for (int i = 0; i < sub_stand_rect.Length; i++)
            {
                // side length of the smaller sub squares
                int sub_side_length = side_length / 2;
                int sub_x_pos = this.upper_point_x - side_length / 2;
                int sub_y_pos = (this.upper_point_y + sub_side_length / 2) + sub_side_length * 2 * i;
                //The sub squares are not regular "squares", thus it needs to be side_length
                sub_stand_rect[i] = new Rectangle(sub_x_pos, sub_y_pos, side_length, sub_side_length);
            }
            // Creating the rectangles for our pillar
            g.FillRectangle(b1, stand_rect[0]);
            g.FillRectangle(b2, stand_rect[1]);
            g.FillRectangle(b3, stand_rect[2]);
            g.FillRectangle(b4, stand_rect[3]);

            // Creating the sub - rectangles for our pillar
            g.FillRectangle(sb1, sub_stand_rect[0]);
            g.FillRectangle(sb2, sub_stand_rect[1]);
            g.FillRectangle(sb3, sub_stand_rect[2]);

        }

        private void Draw_pillar_text(Graphics g)
        {
            int[] goals = { 10, 5, 1, 0 };
            Brush white_brush = new SolidBrush(Color.White);

            for (int i = 0; i < 4; i++)
            {
                int x_pos = upper_point_x - side_length - 30;
                int y_pos = this.upper_point_y + 165 * i + 35;
                if (goals[i] < 10)
                {
                    g.DrawString(goals[i].ToString(), new Font("Arial", 60), white_brush, new Point(x_pos + 30, y_pos));
                }
                else
                {
                    g.DrawString(goals[i].ToString(), new Font("Arial", 60), white_brush, new Point(x_pos, y_pos));
                }
            }

        }

        /// <summary>
        /// Sets the beginning position of the panel, i.e. the cursor (white bar).
        /// </summary>
        private void Set_cursor_pos()
        {
            panel1.Location = new Point(upper_point_x - panel1.Size.Width / 2, upper_point_y + 4 * side_length - 25);
        }

        /// <summary>
        /// Sets the beginning position for the label that includes the numbers for the curr_goal.
        /// </summary>
        private void Set_label_pos()
        {
            this.label1.Location = new Point(upper_point_x - label1.Size.Width / 2, 15);
        }

        /// <summary>
        /// Changes the curr_goal to a new curr_goal with a curr_goal value: 0, 1, 5, 10
        /// </summary>
        private void Refresh_goal()
        {
           if(num_of_success < goal_arr_length)
            {
                curr_goal = goal_arr[num_of_success];

                label1.Text = "Ziel: " + curr_goal;
            }
        }

        /// <summary>
        /// Sets the original curr_goal to 0 for a relaxed value measurement.
        /// </summary>
        private void Reset_goal()
        {
            curr_goal = 0;
            label1.Text = "Ziel: " + curr_goal;
        }

        /// <summary>
        /// Resets the countdown variable (counts the number of times a tick is called) and
        /// resets the label Text before a new curr_goal is written to it.
        /// </summary>
        private void Reset_countdown()
        {
            cdown_label.Text = "";
            c_seconds = 2;
        }

        /// <summary>
        ///  Shows the waveplus-form (that has already been initiated with the start of the application).
        ///  Room for improvement in the future (for example embedded into the actual game form).
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openWaveplusFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wdaq != null)
            {
                wdaq.Show();
                wdaq.BringToFront();
            }
        }

        /// <summary>
        /// Replaces the label box for the countdown with a succesful green checkmark in case of an succesful attempt.
        /// </summary>
        private void Show_succesful()
        {
            pictureBox1.Visible = true;
        }

        /// <summary>
        /// Method to change the input emg sensor from the waveplus device (maybe visible Display?)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chooseSensorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Do you want to repeat the measurement?";
            const string caption = "Repeat Measurement";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
        }


        /// <summary>
        /// For the maximum value measurement, we store the actual sample per Timer call (Tick) and decrease
        /// the num_c_ticks value
        /// 
        /// The sample is stored in an dynamic arraylist and the average is calculated while the timer is ongoing.
        /// When the timer terminates (num_c_ticks < 0), the average maximum value is expressed using label 2 (for now).
        /// 
        /// For now, the Timer variable operates at 100ms (Time interval that the waveplus device provides data, can be adjusted in the
        /// device but must also be adjusted here) and the measuring timeframe is 3 seconds.
        /// Thus this Timer call is called 30 times (3000/100 = 30). This can of course be adjusted.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void store_emg_timer_Tick(object sender, EventArgs e)
        {
            num_c_ticks--;

            // Update the progressbar for visual feedback
            this.meas_progress_bar.PerformStep();

            // Read the current emg_sample
            double raw_emg_sample = wdaq.current_emg_sample;

            // Saving the emg sample in the .emg_txt document
            emg_txt.AppendLine(raw_emg_sample.ToString());

            // Absolute value of the emg sample and multiply it by 10
            double curr_emg_sample = Math.Abs(raw_emg_sample);

            emg_array_list.Add(curr_emg_sample);

            //Test-Line calculates the average after every tick and prints it to the meas label
            object[] emg_arr = emg_array_list.ToArray();
            double emg_average = calculate_average_emg(emg_arr);

            //this.maximum_contraction_uV = emg_average;
            label3.Text = emg_average.ToString();

            if (num_c_ticks < 0)
            {
                // If the curr_goal is -1, we want it to store the emg sample average into the maximum_contraction_uV value and vice versa
                if (curr_goal == -1)
                {
                    this.maximum_contraction_uV = emg_average;

                }
                else if (curr_goal == -2)
                {
                    this.relaxed_contraction_uV = emg_average;
                }

                // Finish the .emg_txt document by appending all the emg-samples
                if (curr_goal == -1)
                {
                    emg_txt.AppendLine("Maximum-Measurement End");

                }
                else if (curr_goal == -2)
                {
                    emg_txt.AppendLine("Relaxed-Measurement End");
                }
                File.WriteAllText(filePath, emg_txt.ToString());

                // The store_emg_timer method needs to be the last command or the tick terminates here!
                store_emg_timer.Stop();

            }
        }

        /// <summary>
        /// This method creates the measuring timer and resets the num_c_ticks value and resets the array list for a new measurement.
        /// </summary>
        private void create_emg_timer()
        {
            if (wdaq != null)
            {
                emg_array_list = new ArrayList();
                num_c_ticks = meas_time_sec * 1000 / 10;
                average_value = 0;

                store_emg_timer.Start();
            }
        }

        /// <summary>
        /// Method hides the succesful checkmark box for a succesful attempt.
        /// </summary>
        private void Hide_picturebox()
        {
            pictureBox1.Visible = false;
        }

        /// <summary>
        /// Starts the countdown Timer with the specified Time (3 seconds).
        /// Starts the EMG Timer to collect and average the emg data
        /// 
        /// // wdaq start measurement needs some time to collect the data (enter a stable state), thus
                // we would need to start a countdown / have a pause before the measuring begins
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void meas_button_Click(object sender, EventArgs e)
        {

            // In the Timer.Tick Function the relax measuremnt automatically begins after the max measurement
            if (maximum_contraction_uV != 0 && relaxed_contraction_uV != 0)
            {
                // Clear the constants
                maximum_contraction_uV = 0;
                relaxed_contraction_uV = 0;

                // Disable the start button of the game
                start_button.Enabled = false;
                start_button.Visible = false;

                const string message = "Do you want to repeat the measurement?";
                const string caption = "Repeat Measurement";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);


                if (result == DialogResult.No)
                {
                    return;
                }
                else
                {
                    start_MVC_measurement();
                }
            }
            else if (maximum_contraction_uV != 0 && relaxed_contraction_uV == 0)
            {
                start_relax_measurement();
            }
            else
            {
                start_MVC_measurement();
            }
        }

        private void start_MVC_measurement()
        {
            // Deactivate the button during the measurement:
            meas_button.Enabled = false;

            curr_goal = -1;
            meas_label.Text = max_str;

            Hide_picturebox();
            meas_progress_bar.Value = 0;
            c_seconds = meas_time_sec + 1;

            cdown_timer.Start();
            create_emg_timer();
        }

        private void start_relax_measurement()
        {
            meas_button.Enabled = false;

            curr_goal = -2;
            meas_label.Text = relax_str;

            Hide_picturebox();
            meas_progress_bar.Value = 0;

            c_seconds = meas_time_sec + 1;

            cdown_timer.Start();
            create_emg_timer();

        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Creating a new game queue for a new session (new queue, num_of_success, new goal)
            this.create_game_queue(goal_arr_length);
            this.num_of_success = 0;
            this.Refresh_goal();

            // Reset the moving average
            this.simple_ma = new MovingAverage(k: 100); // very variant at 80; very ideal at 180

            // Creates a new .txt document for storing the measured values
            this.set_file_path();

            emg_txt = new StringBuilder();
            var newLine = string.Format("[uV]");
            emg_txt.AppendLine(newLine);

            // Reset the cursor position 
            this.Set_cursor_pos();

            // Reset the time passed
            this.passed_time = 0;

            // Restarts the Measuring Process at the Waveplus Device
            wdaq.StopCaptureButton_Click(sender, e);
            wdaq.StartCaptureButton_Click(sender, e);

            // Restart the game timer
            num_g_ticks = game_duration_sec * 1000 / 10;
            game_timer.Start();

            this.current_state = programStates.choice_state;
            this.change_program_state();
        }

        /// <summary>
        /// After the Start - Button initiates the game state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (maximum_contraction_uV != 0)
            {
                current_state = programStates.game_state;
                change_program_state();

                num_g_ticks = game_duration_sec * 1000 / 10;
                game_timer.Start();
            }
        }

        private void start_button_Click(object sender, EventArgs e)
        {
            if (maximum_contraction_uV != 0 && relaxed_contraction_uV != 0)
            {
                current_state = programStates.choice_state;
                change_program_state();
                this.Refresh(); // Refresh the canvas itself

                num_g_ticks = game_duration_sec * 1000 / 10;
                game_timer.Start();
            }
        }

        private void NoGravEx_V2_Load(object sender, EventArgs e)
        {
            // Initiates the Measuring Process at the Waveplus Device
            //Deactivated, because it leads to equal values in the beginning (errors)
            wdaq.StartCaptureButton_Click(sender, e);

            //Ask for the sensor input
            chooseSensorToolStripMenuItem_Click_1(sender, e);
        }

        private void NoGravEx_V2_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Finish the .txt document by appending all the emg-samples
            File.WriteAllText(filePath, emg_txt.ToString());

            // Stop existing timers
            game_timer.Stop();
            cdown_timer.Stop();

            // Stop the EMG-Data aquisition when the form is closed
            wdaq.StopCaptureButton_Click(sender, e);
        }

        private void chooseSensorToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            int new_sensor = Convert.ToInt32(Interaction.InputBox("Welcher Sensor wird benutzt?", "Sensor auswählen", "", this.Width / 2, this.Height / 2));
            if (new_sensor > 0 && new_sensor < 17) //from 0 to 16
            {
                this.change_sensor(new_sensor);
            }
        }

        private void onOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sound_enabled == true)
            {
                sound_enabled = false;
            }
            else if (sound_enabled == false)
            {
                sound_enabled = true;
            }
        }

        /// <summary>
        /// Deactivates the visual feedback on the screen (white bar does not move); neither does 
        /// the position check function work the same way (here only a positional check for the relaxed state - 0 - is made.
        /// Whether the participant has achieved success is only visible by the collected emg data. (cf. emg_sample files in the working directory).
        /// 
        /// In this Mode, the sound feedback is disabled as well.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void visual_feedback_only(object sender, EventArgs e) // for visual only state 1 only!!
        {
            if (no_feedback_state)
            {
                this.current_state = programStates.game_state;
                this.change_program_state();
            }
            else
            {
                this.current_state = programStates.visual_only_state_1;
                this.change_program_state();
            }
        }

        /// <summary>
        /// Additionally to the no visual feedback mode, the only auditive feedback mode does contain the audio, but completely neglects the visual component.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void audio_feedback_only(object sender, EventArgs e)
        {
            if (no_feedback_state)
            {
                this.current_state = programStates.game_state;
                this.change_program_state();
            }
            else
            {
                this.current_state = programStates.audio_only_state;
                this.change_program_state();
            }
        }

        private void limited_loop(object sender, EventArgs e)
        {
            goal_arr_length = 9;
            infinite_mode = false;
            infinity_image.Visible = false;

            // Creating a new game queue for a new session (new queue, num_of_success, new goal)
            this.create_game_queue(goal_arr_length);
            this.num_of_success = 0;
            this.Refresh_goal();

            // Reset the moving average
            this.simple_ma = new MovingAverage(k: 100); // very variant at 80; very ideal at 180

            // Creates a new .txt document for storing the measured values
            this.set_file_path();

            emg_txt = new StringBuilder();
            var newLine = string.Format("[uV]");
            emg_txt.AppendLine(newLine);

            // Reset the cursor position 
            this.Set_cursor_pos();

            // Reset the time passed
            this.passed_time = 0;

            // Restarts the Measuring Process at the Waveplus Device
            wdaq.StopCaptureButton_Click(sender, e);
            wdaq.StartCaptureButton_Click(sender, e);

            // Restart the game timer
            num_g_ticks = game_duration_sec * 1000 / 10;
            game_timer.Start();
        }

        private void infinite_loop(object sender, EventArgs e)
        {
            goal_arr_length = 90000;
            infinite_mode = true;
            label3.Visible = false;

            infinity_image.Location = new Point(label3.Location.X, label3.Location.Y);
            infinity_image.Visible = true;

            // Creating a new game queue for a new session (new queue, num_of_success, new goal)
            this.create_game_queue(goal_arr_length);
            this.num_of_success = 0;
            this.Refresh_goal();

            // Reset the moving average
            this.simple_ma = new MovingAverage(k: 100); // very variant at 80; very ideal at 180

            // Creates a new .txt document for storing the measured values
            this.set_file_path();

            emg_txt = new StringBuilder();
            var newLine = string.Format("[uV]");
            emg_txt.AppendLine(newLine);

            // Reset the cursor position 
            this.Set_cursor_pos();

            // Reset the time passed
            this.passed_time = 0;

            // Restarts the Measuring Process at the Waveplus Device
            wdaq.StopCaptureButton_Click(sender, e);
            wdaq.StartCaptureButton_Click(sender, e);

            // Restart the game timer
            num_g_ticks = game_duration_sec * 1000 / 10;
            game_timer.Start();
        }


        private void choice_btn1_Click(object sender, EventArgs e)
        {
            current_state = programStates.game_state;
            change_program_state();
        }

        private void choice_btn2_Click(object sender, EventArgs e)
        {
            current_state = programStates.visual_only_state_1;
            change_program_state();
        }

        private void choice_btn3_Click(object sender, EventArgs e)
        {
            current_state = programStates.audio_only_state;
            change_program_state();
        }

        private void choice_btn4_Click(object sender, EventArgs e)
        {
            current_state = programStates.visual_only_state_2;
            change_program_state();
        }

        /// <summary>
        /// Reads the current emg sample per Tick from the waveplus device into curr_emg_sample.
        /// 
        /// 1. If the curr_emg sample is bigger than the reference maximum_contrcation_uV, then it is 
        ///    capped (maximum value is taken). Is not very scientific, because peaks of this size can be smoothed out using
        ///    some kind of high or lowpass filter.
        /// 2. We have so far implemented a method that passes the percentage of the input emg_signal voltage to the height of the pillar
        ///    that means that depending on the height of the pillar, the panel (white bar) is moved by the percentage of the input emg_voltage.
        ///   
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void game_timer_Tick(object sender, EventArgs e)
        {
            // Decrease by one game tick
            num_g_ticks--;

            // Increase the time counter (ms)
            passed_time += 10;

            // Check the position of the panel at the beginning of each tick
            int y_pos = panel1.Location.Y;
            if (y_pos > upper_point_y && y_pos + panel1.Size.Height < upper_point_y + side_length)
            {
                // Wert 10
                current_pos = 10;
            }
            else if (y_pos > upper_point_y + side_length && y_pos + panel1.Size.Height < upper_point_y + 2 * side_length)
            {
                // Wert 5
                current_pos = 5;
            }
            else if (y_pos > upper_point_y + 2 * side_length && y_pos + panel1.Size.Height < upper_point_y + 3 * side_length)
            {
                // Wert 1
                current_pos = 1;
            }
            else if (y_pos > upper_point_y + 3 * side_length && y_pos + panel1.Size.Height < upper_point_y + 4 * side_length)
            {
                // Wert 0
                current_pos = 0;
            }

            // Read the current emg_sample
            double raw_emg_sample = wdaq.current_emg_sample;

            // Saving the emg sample in the .emg_txt document
            var newLine = string.Format(current_pos.ToString() + " " + raw_emg_sample.ToString() + " " + passed_time.ToString() + "ms");
            emg_txt.AppendLine(newLine);

            // Absolute value of the emg sample and multiply it by 10
            double curr_emg_sample = Math.Abs(raw_emg_sample);

            // Subtract the relaxed contraction to observe the change of the muscular activity in mV
            // This means that 0 does not correlate to less than 10% current overall activity but muscular activity
            //curr_emg_sample -= relaxed_contraction_uV;

            if (curr_emg_sample > maximum_contraction_uV)
            { // caps the value at 100%
                curr_emg_sample = maximum_contraction_uV;
            }
            else if (curr_emg_sample < 0)
            {
                curr_emg_sample = 0;
            }

            // Use the simple moving average algorithm to perform noise reduction
            double simply_average_sample = simple_ma.Update(curr_emg_sample); // Check the significance of the int conversion! (int)

            // The input force goes up to 85%, so not 100%. Thus we have simply_averaged_sample be normalized to
            // 85% of maximum contraction:
            double normalized_mvc = 0.6 * this.maximum_contraction_uV;

            double cont_percent = (double)Decimal.Divide((decimal)simply_average_sample, (decimal)normalized_mvc);

            // If cont_percent is bigger than 1, than we cap it to 1 (measurement was incorrectly done)
            if (cont_percent > 1)
            {
                cont_percent = 1;
            }

            // Eins zu Eins Übertragung des derzeitigen Prozentwertes auf die Säule
            int new_panel_y = upper_point_y + 4 * side_length - (int)(4 * side_length * cont_percent);
            if (cont_percent < 0.2)
            {
                panel1.Location = new Point(panel1.Location.X, upper_point_y + 4 * side_length - 2 * panel1.Size.Height);
            }
            else
            {
                panel1.Location = new Point(panel1.Location.X, new_panel_y);
            }

            // Display the achieved sum as a string
            label3.Text = num_of_success.ToString() + "/" + goal_arr.Length.ToString();

            if(num_of_success < goal_arr_length)
            {
                // We do not decrease the game loop (infinite loop), until the object has been reached
                num_g_ticks++;
            }
            else
            {
                //Game Endscreen Message
                this.current_state = programStates.game_over_state;
                this.change_program_state();
            }
        }

        /// <summary>
        /// Method that is crucial for generating the average of an object element.
        /// 
        /// Used in generating the average emg sample - value for the maximum/relaxed contraction and
        /// in the future for the moving average of the realtime emg sample streaming.

        /// </summary>
        /// <param name="emg_obj"></param>
        /// <returns></returns>
        private double calculate_average_emg(object[] emg_obj)
        {
            if (emg_obj != null)
            {
                foreach (double sample_value in emg_obj)
                {
                    average_value += Convert.ToDouble(sample_value);
                }
                if (average_value > 0)
                {
                    average_value = average_value / emg_obj.Length;
                }
            }
            return average_value;
        }
    }
}

