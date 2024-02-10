using ConsoleApp1;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class MyFileWriter
{
    public static void Main(string[] args)
    {
        int[] atest = ArrayProcessing.CreateArray(20);
        Console.WriteLine(ArrayProcessing.AsString(atest));
    }
}