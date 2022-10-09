using System;
using Terrain;
using OpenSimplex;
using BitmapConvrerting;
using System.Drawing;

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
            long seed = 0;
            int size = 128;
            double roughness = 1;
            double noiseRoughness = 1 ;

            DiamondSquare IslandGenerator = new DiamondSquare(
                new NoiseGeneratorWrapper(seed), 
                size, 
                roughness,
                noiseRoughness);

            double[,] data = IslandGenerator.getData() ;
            BitmapConverter.SaveBitmap(BitmapConverter.ConvertToBitmap(data,
                (value) =>
                {
                    if (value < roughness / 2)
                    {
                        return Color.Blue;
                    }
                    if (value > 1)
                    {
                        return Color.White;
                    }
                    int blackShade = Convert.ToInt32(255 * value);
                    return Color.FromArgb(blackShade, blackShade, blackShade);
                }), "Result.bmp");
            
        }
    }
}
