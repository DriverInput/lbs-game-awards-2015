using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace game
{
    class Map
    {
        //private int[,] map;
        public float this[int index1, int index2] { get { return MapArray[index1, index2]; } set { MapArray[index1, index2] = value; } }
        private float[,] MapArray { get; set; }

        public int X { get; private set; }
        public int Y { get; private set; }

        public Map(int X, int Y, float[,] map)
        {
            this.X = X;
            this.Y = Y;
            this.MapArray = map;
        }

        public Map()
        {
            MapArray = new float[0,0];
            X = 0;
            Y = 0;
        }

        public int[,] ToInt()
        {
            int[,] Output = new int[X , Y];
            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    Output[x, y] = (int)MapArray[x, y];
                }
            }
            return Output;        
        }
        public new string ToString() // overrides object.ToString complitly //by useing the "new" keyword
        {
            string temp = null;
            for (int y = 0; y < Y ; y++)
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
        //public int GetData() {} // get numbers saved in the varibels
        public int GetData(int x, int y, int position = 0)
        {
            try
            {
                return Convert.ToInt32(MapArray[x, y].ToString()[position + 2].ToString());
            }
            catch (Exception)
            {
                return 0;
            }                 
        }
        public int GetData(int x, int y, int startposition, int endposition)
        {
            try
            {
                string Temp = null;
                for (int i = startposition + 2/*to not cont tex "3." */; i <= endposition + 2; i++)
                {
                    Temp += MapArray[x, y].ToString()[i];
                }
                return Convert.ToInt32(Temp);
            }
            catch (Exception)
            {
                return 0;            
            }
        }

    }
    class MapLoader
    {

        static int arraySize_X;
        static int arraySize_Y;
        static double[,] array_int;
        static string[,] array_string;

        static List<string> stringList = new List<string>();
        public static Map LoadMap(string locationName)
        {
            readMap(locationName);
            getAndSaveSize();
            InitalizeArray();
            stringListToArrey();
            ConvertStringToInt();
            Map map = new Map(arraySize_X,arraySize_Y,Convertsadsadsa());
            resetVaribels();
            return map;
        }
        public static float[,] Convertsadsadsa()
        {
            float[,] dsafdsgfs = new float[array_int.GetLength(0),array_int.GetLength(1)];
            for (int x = 0; x < array_int.GetLength(0); x++)
            {
                for (int y = 0; y < array_int.GetLength(1); y++)
                {
                    dsafdsgfs[x, y] = Convert.ToSingle(array_int[x, y]);
                }
            }
            return dsafdsgfs;        
        }
        public static void UnloadMap(Map map, string location)
        {
            using (StreamWriter sw = new StreamWriter(location))
            {
                sw.Write(map.ToString());
            }
        }        
        private static void resetVaribels()
        {
            array_int = new double[0, 0];
            array_string = new string[0, 0];
            arraySize_X = 0;
            arraySize_Y = 0;
            stringList = new List<string>();
        }
        private static void readMap(string locationName)
        {
            using (StreamReader sr = new StreamReader(locationName))
            {
                while (!(!(!(!(true.Equals(!(!(!(!(!(false.Equals(true))))) == true ? true.Equals(!(!(true))) : false)))/*Potatis*/))) == true && (true && !false) || true)
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
        }
        private static void getAndSaveSize()
        {
            arraySize_Y = stringList.Count - 1; //set arraysize
            foreach (char item in stringList[0])
            {
                if (item.Equals(','))
                {
                    arraySize_X++;
                }
            }
        }
        private static void InitalizeArray()
        {
            array_int = new double[arraySize_X, arraySize_Y];

            array_string = new string[arraySize_X, arraySize_Y];
        }
        private static void stringListToArrey()
        {
            
            for (int Temp_Y = 0; Temp_Y < arraySize_Y; Temp_Y++)
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
                        array_string[tempX, Temp_Y] = TempString;
                        TempString = null;
                        tempX++;
                    }
                }
            }
        }
        private static void ConvertStringToInt()
        {
            for (int Y = 0; Y < arraySize_Y; Y++)
            {
                for (int X = 0; X < arraySize_X; X++)
                {
                    if (array_string[X, Y] != null)
                    {
                        array_int[X, Y] = Convert.ToDouble(array_string[X, Y].Replace('.', ',')); // O.o 
                    }
                }
            }
        }
       
    }
}

