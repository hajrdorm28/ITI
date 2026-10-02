using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab6_1
{
    public interface IPlayable
    {
        void Play();
        void Pause();

    }
    public class AudioPlayer() : IPlayable
    {
        private bool isPlaying = false;
        public void Play()
        {
            if (isPlaying)
            {
                Console.WriteLine("Warning! audio already playing");
                return;
            }
            isPlaying = true;
            Console.WriteLine("Audio is now playing");
        }
        public void Pause()
        {
            if (!isPlaying)
            {
                Console.WriteLine("Warning! audio already paused");
                return;
            }
            isPlaying = false;
            Console.WriteLine("Audio is now paused");
        }
    }

    public class VideoPlayer() : IPlayable 
    {
        private bool isPlaying = false;
        public void Play()
        {
            if (isPlaying)
            {
                Console.WriteLine("Warning! video already playing");
                return;
            }
            isPlaying = true;
            Console.WriteLine("Video is now playing");
        }
        public void Pause()
        {
            if (!isPlaying)
            {
                Console.WriteLine("Warning! video already paused");
                return;
            }
            isPlaying = false;
            Console.WriteLine("Video is now paused");
        }
    }
}
