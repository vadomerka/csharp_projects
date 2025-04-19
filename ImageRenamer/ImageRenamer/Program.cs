using System;


public class CustomSearcher
{
    public static List<string> GetDirectories(string path, string searchPattern = "*",
        SearchOption searchOption = SearchOption.AllDirectories)
    {
        if (searchOption == SearchOption.TopDirectoryOnly)
            return Directory.GetDirectories(path, searchPattern).ToList();

        var directories = new List<string>(GetDirectories(path, searchPattern));

        for (var i = 0; i < directories.Count; i++)
            directories.AddRange(GetDirectories(directories[i], searchPattern));

        return directories;
    }

    private static List<string> GetDirectories(string path, string searchPattern)
    {
        try
        {
            return Directory.GetDirectories(path, searchPattern).ToList();
        }
        catch (UnauthorizedAccessException)
        {
            return new List<string>();
        }
    }
}


class Program
{
    static int Main(string[] args)
    {
        Console.WriteLine("Введите путь к папке для переименования.");
        string? folderPath = Console.ReadLine();
        if (folderPath == null)
        {
            Console.WriteLine("Неверный ввод.");
        }
        int fileIndex = 0;
        if (File.Exists(folderPath)) {
            Console.WriteLine("Неверный ввод.");
        }

        var directories = CustomSearcher.GetDirectories(folderPath);
        directories.Insert(0, folderPath);
        for (int i = 0; i < directories.Count(); i++)
        {
            var files = Directory.GetFiles(directories[i]);
            // List<string>. files
            var sortedFiles = new DirectoryInfo(directories[i]).GetFiles()
                                                  .OrderBy(f => f.LastWriteTime)
                                                  .ToList();
            for (int j = 0; j < sortedFiles.Count(); j++)
            {
                if (sortedFiles[j].Exists) {
                    // string changeTime = File.GetLastWriteTime(files[j]).ToString();
                    string ext = sortedFiles[j].Extension;
                    string newName = Path.Combine(directories[i], "zp_" + fileIndex + ext);
                    sortedFiles[j].MoveTo(newName);
                    fileIndex++;
                }
                Console.WriteLine("f--" + sortedFiles[j].ToString());
            }
            Console.WriteLine("d-" + directories[i]);
        }
        return 0;
    }
}