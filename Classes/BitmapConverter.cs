using System;
using System.Drawing;

namespace BitmapConvrerting
{
    static class BitmapConverter
    {
        
        static public Bitmap ConvertToBitmap(double[,] data, Func<double, Color> DoubleToColorFunc) 
        {

            if (data.GetLength(0) != data.GetLength(1))
            {
                throw new Exception("Array is not the correct size");
            }
            int size = data.GetLength(0);

            Bitmap result = new Bitmap(size, size);

            for (int x = 0; x < size; x++ )
                for (int y = 0; y < size; y++ )
                {
                    result.SetPixel(x, y, DoubleToColorFunc(data[x, y]));
                }
            return result;
        }
        static public void SaveBitmap(Bitmap image, string fileName)
        {
            image.Save(fileName);
        }
  
    }
}
