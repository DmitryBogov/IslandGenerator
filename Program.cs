using System;
using Terrain;
using OpenSimplex;
using BitmapConvrerting;
using System.Drawing;
using System.IO;

namespace IslandGenerator
{

    class NoiseGeneratorWrapper: OpenSimplexNoise, INoiseGenerator
    {
        public NoiseGeneratorWrapper(long seed) : base(seed) { }
    }



    class Program
    {
        static void Main(string[] args)
        {
          


            /*       Console.Write("Roughness: ");
                   string roughness_str = Console.ReadLine();
                   roughness = Convert.ToDouble((string.IsNullOrEmpty(roughness_str) == true) ? "1" : roughness_str);

                   Console.Write("Noise: ");
                   string noiseR_str = Console.ReadLine();
                   noiseRoughness = Convert.ToDouble((string.IsNullOrEmpty(noiseR_str) == true) ? "1" : noiseR_str);*/

            if (Directory.Exists("Results"))
            {
                Directory.Delete("Results", true);

            }
            Directory.CreateDirectory("Results");



            for (double i = 0; i < 1; i+=0.1)
            {
                for (double j = 0; j < 1; j += 0.1)
                {
                    long seed = 123;
                    int size = 512;
                    double roughness = i;
                    double noiseRoughness = j;
                    string fileName = $"Results/Result_{size}_{Math.Round(roughness, 4)}_{Math.Round(noiseRoughness, 4)}";

                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }


                    DiamondSquare IslandGenerator = new DiamondSquare(
                        new NoiseGeneratorWrapper(seed),
                        size,
                        roughness,
                        noiseRoughness);

                    double[,] data = IslandGenerator.getData();
                    BitmapConverter.SaveBitmap(BitmapConverter.ConvertToBitmap(data,
                        (value) =>
                        {
                            if (value < roughness / 3)
                            {
                                return Color.Blue;
                            }
                            if (value > 1)
                            {
                                return Color.White;
                            }
                            int blackShade = Convert.ToInt32(255 * value);
                            
                            return Color.FromArgb(blackShade, blackShade, blackShade);
                        }), $"{fileName}.bmp");

                    Console.WriteLine($" Generated: {size}_{Math.Round(roughness, 4)}_{Math.Round(noiseRoughness, 4)}");
                    
                }
            }


           


            
        }
    }
}
