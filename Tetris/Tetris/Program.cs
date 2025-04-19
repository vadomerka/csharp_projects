using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    static void Main()
    {
        DateTime date;
        string userDateTime = Console.ReadLine();

        while (!DateTime.TryParseExact(userDateTime, "yyyy-MM-dd hh-mm-ss",
                                       CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal,
                                       out date))
        {
            userDateTime = Console.ReadLine();
        };
        Console.WriteLine(date);
        Console.ReadLine();
    }
}
