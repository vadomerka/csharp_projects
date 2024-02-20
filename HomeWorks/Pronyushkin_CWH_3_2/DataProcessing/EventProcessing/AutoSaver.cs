using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProcessing.DataProcessing;
using DataProcessing.Objects;

namespace DataProcessing.EventProcessing
{
    /// <summary>
    /// Класс для автоматического сохранения при изменении данных.
    /// </summary>
    public class AutoSaver
    {
        private List<Patient> _patients = new List<Patient>();
        private string _fPath = string.Empty;
        private DateTime _lastEventTime = DateTime.MinValue;
        // Поля для минимального и максимального времени между сохранениями.
        // При некоторых изменениях активируются события сразу у нескольких объектов.
        // Чтобы облегчить нагрузку на файловую систему, я ввожу ограничение.
        private TimeSpan _maxInterval = TimeSpan.FromSeconds(15);
        private TimeSpan _minInterval = TimeSpan.FromSeconds(1);

        public AutoSaver() { }

        public AutoSaver(List<Patient>? patients, string? fPath)
        {
            if (patients == null || fPath == null) throw new ArgumentNullException();
            _patients = patients;
            _fPath = GenerateSaveName(fPath);
        }

        /// <summary>
        /// Вспомогательный метод. Создает пусть для авто сохранения.
        /// </summary>
        /// <param name="path">Изначальный путь</param>
        /// <returns></returns>
        private string GenerateSaveName(string path)
        {
            string result = string.Empty;
            if (path.Contains("."))
            {
                path = string.Join(".", path.Split('.')[..^1]);
            }
            result = path + "_tmp.json";
            return result;
        }

        /// <summary>
        /// Делает авто сохранение, если изменения произошли в нужном интервале.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public void UpdateSave(object? obj, EventTime e)
        {
            if (e.Time - _lastEventTime >= _minInterval &&
                e.Time - _lastEventTime <= _maxInterval)
            {
                DataWriter.Write(_patients, _fPath);
            }
            _lastEventTime = e.Time;
        }
    }
}
