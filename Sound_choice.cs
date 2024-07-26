using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Waveplus_Daq_Example
{
    public class Sound_choice
    {
        public MediaPlayer p1, p2, p3, p4, p5, p6, p7, p8;

        public string file1 = Path.Combine(Environment.CurrentDirectory, @"sound_files\", "cinematic-transition-sound-effect.mp3");
        public string file2 = Path.Combine(Environment.CurrentDirectory, @"sound_files\", "hand-bell-ringing-sound.mp3");
        public string file3 = Path.Combine(Environment.CurrentDirectory, @"sound_files\", "samba-beat-118-bpm.mp3");
        public string file4 = Path.Combine(Environment.CurrentDirectory, @"sound_files\", "cartoon-clock-ticking-sound-effect.mp3");
        public string file5 = Path.Combine(Environment.CurrentDirectory, @"sound_files\", "bubble-sound-effect.mp3");

        public string file6 = Path.Combine(Environment.CurrentDirectory, @"sound_files\", "Harfe.m4a");
        public string file7 = Path.Combine(Environment.CurrentDirectory, @"sound_files\", "Glocke.m4a");
        public string file8 = Path.Combine(Environment.CurrentDirectory, @"sound_files\", "Bongos.m4a");

        public Sound_choice()
        {
            // Construct the sound via the player
            p1 = new MediaPlayer();
            p2 = new MediaPlayer();
            p3 = new MediaPlayer();
            p4 = new MediaPlayer();

            p1.Open(new Uri(file1));
            p2.Open(new Uri(file2));
            p3.Open(new Uri(file3));
            p4.Open(new Uri(file4));
            
        }

        public void play_successful(int goal)
        {
            if(goal == 1 || goal == 5 || goal == 10)
            {
                p5 = new MediaPlayer();
                p5.Open(new Uri(file5));
                p5.Play();
            }
        }

        public void speak_goal(int goal)
        {

            if (goal == 1)
            {
                p6 = new MediaPlayer();
                p6.Open(new Uri(file6));
                p6.Play();
            }
            else if(goal == 5)
            {
                p7 = new MediaPlayer();
                p7.Open(new Uri(file7));
                p7.Play();

            }
            else if(goal == 10)
            {
                p8 = new MediaPlayer();
                p8.Open(new Uri(file8));
                p8.Play();

            }
        }

        public void play_sound(int goal)
        {
            if(goal == 1)
            {
                p1.Play();
            }else if (goal == 5)
            {
                p2.Play();
            }else if(goal == 10)
            {
                p3.Play();
            }else if(goal == 0)
            {
                p4.Play();
            }

        }

        public void stop_sound()
        {
            p1.Stop();
            p2.Stop();
            p3.Stop();
            p4.Stop();
        }


    }
}
