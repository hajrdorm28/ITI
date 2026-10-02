using System;

namespace lab6_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region binding
            Console.WriteLine("Early Binding:");
            Animal_Early AE = new Dog_Early();
            AE.Speak();
            
            Dog_Early DE = new Dog_Early();
            DE.Speak();

            Console.WriteLine("\nLate Binding:");
            Animal_Late AL = new Dog_Late();
            AL.Speak();

            Dog_Late DL = new Dog_Late();
            DL.Speak();
            Console.WriteLine("\n---------------------------------------------------\n");
            #endregion

            #region Interfaces
            Console.WriteLine("AudioPlayer:");
            IPlayable audio = new AudioPlayer();
            Console.Write("  audio.Play() => "); audio.Play();
            Console.Write("  audio.Play() => "); audio.Play(); // warning
                             
            Console.Write("  audio.Pause() => "); audio.Pause();
            Console.Write("  audio.Pause() => "); audio.Pause(); // warning


            Console.WriteLine("\nVideoPlayer:");
            IPlayable video = new VideoPlayer();
            Console.Write("  video.Pause() => "); video.Pause(); // warning
            Console.Write("  video.Play() => "); video.Play();
                             
            Console.Write("  video.Play() => "); video.Play(); // warning
            Console.Write("  video.Pause() => "); video.Pause();
            Console.WriteLine("\n---------------------------------------------------\n");
            #endregion

            #region Abstract
            Sphere sphere = new Sphere(3);
            Cube cube = new Cube(2.5);
            Cylinder cylinder = new Cylinder(2, 5);

            Console.WriteLine("Sphere:");
            Console.WriteLine("  Volume = " + sphere.GetVolume().ToString("F2"));
            Console.WriteLine("  Surface Area = " + sphere.GetSurfaceArea().ToString("F2"));
            Console.WriteLine();

            Console.WriteLine("Cube:");
            Console.WriteLine("  Volume = " + cube.GetVolume().ToString("F2"));
            Console.WriteLine("  Surface Area = " + cube.GetSurfaceArea().ToString("F2"));
            Console.WriteLine();

            Console.WriteLine("Cylinder:");
            Console.WriteLine("  Volume = " + cylinder.GetVolume().ToString("F2"));
            Console.WriteLine("  Surface Area = " + cylinder.GetSurfaceArea().ToString("F2"));
            #endregion
        }
    }
}