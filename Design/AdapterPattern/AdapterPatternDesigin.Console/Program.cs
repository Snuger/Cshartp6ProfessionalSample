using System;

namespace AdapterPatternDesigin.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AudioPlayer audioPlayer = new AudioPlayer();
            System.Console.WriteLine(audioPlayer.Play("mp4","三天打鱼"));
            System.Console.WriteLine(audioPlayer.Play("mp4", "一天到晚游泳的鱼"));
            System.Console.WriteLine(audioPlayer.Play("v1c", "你可好啊"));
             System.Console.ReadLine();
        }
    }
}
