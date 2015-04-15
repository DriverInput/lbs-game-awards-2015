using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Czaplicki.Utility
{
    class Map
    {
        public double this[int index1, int index2] { get { return MapArray[index1, index2]; } set { MapArray[index1, index2] = value; } }
        private double[,] MapArray { get; set; }

        public int X { get; private set; }
        public int Y { get; private set; }

        public Map(int X, int Y, double[,] map)
        {
            this.X = X;
            this.Y = Y;
            this.MapArray = map;
        }
        public Map(Vector2 XY)
        {
            this.X = (int)XY.X;
            this.Y = (int)XY.Y;
            this.MapArray = new double[X, Y];
        }
        public Map()
        {
            MapArray = new double[0, 0];
            X = 0;
            Y = 0;
        }
        public int[,] ToInt()
        {
            int[,] Output = new int[X, Y];
            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    Output[x, y] = (int)(MapArray[x, y]);
                }
            }
            return Output;
        }
        public new string ToString() // overrides object.ToString complitly //by useing the "new" keyword
        {
            string temp = null;
            for (int y = 0; y < Y; y++) // waired
            {
                for (int x = 0; true; x++)
                {
                    try
                    {
                        temp += MapArray[x, y].ToString();
                        temp += ",";
                    }
                    catch
                    {
                        break;
                    }
                }
                temp += "\r\n";
            }
            return temp;
        }
    }
    class MapLoader
    {
        public static Map LoadMap(string location)
        {

            string[,] stringArray;
            double[,] doubleArray;

            List<string> stringList = new List<string>();
            int X = 0, Y = 0;


            using (StreamReader sr = new StreamReader(location))
            {
                while (true)
                {
                    string Temp = sr.ReadLine();
                    if (Temp != null)
                    {
                        stringList.Add(Temp);
                    }
                    else
                    {
                        break;
                    }
                }
            }


            Y = stringList.Count;//-1; //set arraysize
            foreach (char item in stringList[0])
            {
                if (item.Equals(','))
                {
                    X++;
                }
            }


            stringArray = new string[X, Y];
            doubleArray = new double[X, Y];


            for (int Temp_Y = 0; Temp_Y < Y; Temp_Y++)
            {
                int tempX = 0;
                string TempString = null;
                for (int i = 0; i < stringList[Temp_Y].Length; i++)
                {
                    if (stringList[Temp_Y][i] != ',')
                    {
                        TempString += stringList[Temp_Y][i];
                    }
                    else
                    { // byta
                        stringArray[tempX, Temp_Y] = TempString;
                        TempString = null;
                        tempX++;
                    }
                }
            }

            for (int TempX = 0; TempX < X; TempX++)
            {
                for (int TempY = 0; TempY < Y; TempY++)
                {
                    if (stringArray[TempX, TempY] != null)
                    {
                        doubleArray[TempX, TempY] = Convert.ToDouble(stringArray[TempX, TempY]);//.Replace('.',',')); // O.o 
                    }
                }
            }

            return new Map(X, Y, doubleArray);

        }
        public static void UnloadMap(Map map, string location)
        {
            using (StreamWriter sw = new StreamWriter(location))
            {
                sw.Write(map.ToString());
            }
        }



    }
}
