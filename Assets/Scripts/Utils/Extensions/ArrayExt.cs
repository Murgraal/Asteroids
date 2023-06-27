using System.Collections.Generic;

namespace Utils.Extensions
{
    public static class ArrayExt
    {
        public static List<T[,]> HalveX<T>(this T[,] original)
        {
            var result = new List<T[,]>();
        
            var xSize = original.GetLength(0);
            var ySize = original.GetLength(1);
        
            var halfSizeX = xSize / 2;

            for (int i = 0; i < 2; i++)
            {
                result.Add(new T[halfSizeX,ySize]);
            }
        
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (x < halfSizeX)
                    {
                        result[0][x, y] = original[x, y];
                    }
                    else
                    {
                        result[1][x - halfSizeX, y] = original[x, y];
                    }
                }
            }
        
            return result;
        }
    
        public static List<T[,]> HalveY<T>(this T[,] original)
        {
            var result = new List<T[,]>();
        
            var xSize = original.GetLength(0);
            var ySize = original.GetLength(1);
        
            var halfSizeY = ySize / 2;

            for (int i = 0; i < 2; i++)
            {
                result.Add(new T[xSize,halfSizeY]);
            }
        
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (y < halfSizeY)
                    {
                        result[0][x, y] = original[x, y];
                    }
                    else
                    {
                        result[1][x, y - halfSizeY] = original[x, y];
                    }
                }
            }
        
            return result;
        }
    }
}