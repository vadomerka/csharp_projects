using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProcessing.Objects;

namespace DataProcessing.PatientProcessing
{
    // Делегат ChangerDelegate для методов правил изменения пациента.
    public delegate Patient ChangerDelegate(Patient p, string value);

    /// <summary>
    /// Класс PatientChanger хранит правила изменения пациента.
    /// </summary>
    public static class PatientChanger
    {
        /// <summary>
        /// Метод изменения поля Name пациента.
        /// </summary>
        /// <param name="p">Пациент</param>
        /// <param name="value">Новое значение</param>
        /// <returns>Возвращает ссылку на пациента</returns>
        /// <exception cref="ArgumentNullException">При null значениях</exception>
        public static Patient PatientNameChange(Patient p, string value)
        {
            if (p == null || value == null) throw new ArgumentNullException();
            p.Name = value;
            return p;
        }

        /// <summary>
        /// Метод изменения поля Age пациента.
        /// </summary>
        /// <param name="p">Пациент</param>
        /// <param name="value">Новое значение</param>
        /// <returns>Возвращает ссылку на пациента</returns>
        /// <exception cref="ArgumentNullException">При null значениях</exception>
        public static Patient PatientAgeChange(Patient p, string value)
        {
            if (p == null || value == null) throw new ArgumentNullException();
            int age = 0;
            if (!int.TryParse(value, out age)) throw new ArgumentException();
            p.Age = age;
            return p;
        }

        /// <summary>
        /// Метод изменения поля Gender пациента.
        /// </summary>
        /// <param name="p">Пациент</param>
        /// <param name="value">Новое значение</param>
        /// <returns>Возвращает ссылку на пациента</returns>
        /// <exception cref="ArgumentNullException">При null значениях</exception>
        public static Patient PatientGenderChange(Patient p, string value)
        {
            if (p == null || value == null) throw new ArgumentNullException();
            if (value.ToLower() == "male") p.Gender = "Male";
            if (value.ToLower() == "female") p.Gender = "Female";
            else p.Gender = "Other";
            return p;
        }

        /// <summary>
        /// Метод изменения поля Diagnosis пациента.
        /// </summary>
        /// <param name="p">Пациент</param>
        /// <param name="value">Новое значение</param>
        /// <returns>Возвращает ссылку на пациента</returns>
        /// <exception cref="ArgumentNullException">При null значениях</exception>
        public static Patient PatientDiagnosisChange(Patient p, string value)
        {
            if (p == null || value == null) throw new ArgumentNullException();
            p.Diagnosis = value;
            return p;
        }

        /// <summary>
        /// Метод изменения поля HeartRate пациента.
        /// </summary>
        /// <param name="p">Пациент</param>
        /// <param name="value">Новое значение</param>
        /// <returns>Возвращает ссылку на пациента</returns>
        /// <exception cref="ArgumentNullException">При null значениях</exception>
        /// <exception cref="ArgumentException">При не целом значении</exception>
        public static Patient PatientHeartRateChange(Patient p, string value)
        {
            if (p == null || value == null) throw new ArgumentNullException();
            int heartRate = 0;
            if (!int.TryParse(value, out heartRate)) throw new ArgumentException();
            p.HeartRate = heartRate;
            return p;
        }

        /// <summary>
        /// Метод изменения поля Temperature пациента.
        /// </summary>
        /// <param name="p">Пациент</param>
        /// <param name="value">Новое значение</param>
        /// <returns>Возвращает ссылку на пациента</returns>
        /// <exception cref="ArgumentNullException">При null значениях</exception>
        /// <exception cref="ArgumentException">При не вещественном значении</exception>
        public static Patient PatientTemperatureChange(Patient p, string value)
        {
            if (p == null || value == null) throw new ArgumentNullException();
            double temperature = 0;
            if (!double.TryParse(value, out temperature)) throw new ArgumentException();
            p.Temperature = temperature;
            return p;
        }

        /// <summary>
        /// Метод изменения поля OxygenSaturation пациента.
        /// </summary>
        /// <param name="p">Пациент</param>
        /// <param name="value">Новое значение</param>
        /// <returns>Возвращает ссылку на пациента</returns>
        /// <exception cref="ArgumentNullException">При null значениях</exception>
        /// <exception cref="ArgumentException">При не целом значении</exception>
        public static Patient PatientOxygenSaturationChange(Patient p, string value)
        {
            if (p == null || value == null) throw new ArgumentNullException();
            int oxygenSaturation = 0;
            if (!int.TryParse(value, out oxygenSaturation)) throw new ArgumentException();
            p.OxygenSaturation = oxygenSaturation;
            return p;
        }
    }
}
