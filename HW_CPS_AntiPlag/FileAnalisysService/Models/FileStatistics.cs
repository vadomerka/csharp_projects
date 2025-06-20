﻿namespace FileAnaliseService.Models
{
    /// <summary>
    /// Статистика одного файла.
    /// </summary>
    public class FileStatistics
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public int ParagraphsCount { get; set; }
        public int WordsCount { get; set; }
        public int SymbolsCount { get; set; }
    }
}
