using DataProcessing.Objects;

namespace DataProcessing.DataProcessing
{
    // Делегат SorterDelegate для методов правил сортировки.
    public delegate int SorterDelegate(Patient x, Patient y);

    /// <summary>
    /// Класс DataSorter хранит правила сортировки.
    /// </summary>
    public static class DataSorter
    {
        /// <summary>
        /// Правило сортировки по свойству PatientId. Сортирует по величине.
        /// </summary>
        /// <param name="x">Пациент 1</param>
        /// <param name="y">Пациент 2</param>
        /// <returns>1, -1 или 0</returns>
        public static int PatientIdSort(Patient x, Patient y)
        {
            if (x == null || y == null) return 0;
            return x.PatientId.CompareTo(y.PatientId);
        }

        /// <summary>
        /// Правило сортировки по свойству Name. Сортирует в алфавитном порядке.
        /// </summary>
        /// <param name="x">Пациент 1</param>
        /// <param name="y">Пациент 2</param>
        /// <returns>1, -1 или 0</returns>
        public static int PatientNameSort(Patient x, Patient y)
        {
            if (x == null || y == null) return 0;
            return x.Name.CompareTo(y.Name);
        }

        /// <summary>
        /// Правило сортировки по свойству Age. Сортирует по величине.
        /// </summary>
        /// <param name="x">Пациент 1</param>
        /// <param name="y">Пациент 2</param>
        /// <returns>1, -1 или 0</returns>
        public static int PatientAgeSort(Patient x, Patient y)
        {
            if (x == null || y == null) return 0;
            return x.Age.CompareTo(y.Age);
        }

        /// <summary>
        /// Правило сортировки по свойству Gender. Сортирует в алфавитном порядке.
        /// </summary>
        /// <param name="x">Пациент 1</param>
        /// <param name="y">Пациент 2</param>
        /// <returns>1, -1 или 0</returns>
        public static int PatientGenderSort(Patient x, Patient y)
        {
            if (x == null || y == null) return 0;
            return x.Gender.CompareTo(y.Gender);
        }

        /// <summary>
        /// Правило сортировки по свойству Diagnosis. Сортирует в алфавитном порядке.
        /// </summary>
        /// <param name="x">Пациент 1</param>
        /// <param name="y">Пациент 2</param>
        /// <returns>1, -1 или 0</returns>
        public static int PatientDiagnosisSort(Patient x, Patient y)
        {
            return x.Diagnosis.Length.CompareTo(y.Diagnosis.Length);
        }

        /// <summary>
        /// Правило сортировки по свойству HeartRate. Сортирует по величине.
        /// </summary>
        /// <param name="x">Пациент 1</param>
        /// <param name="y">Пациент 2</param>
        /// <returns>1, -1 или 0</returns>
        public static int PatientHeartRateSort(Patient x, Patient y)
        {
            return x.HeartRate.CompareTo(y.HeartRate);
        }

        /// <summary>
        /// Правило сортировки по свойству Temperature. Сортирует по величине.
        /// </summary>
        /// <param name="x">Пациент 1</param>
        /// <param name="y">Пациент 2</param>
        /// <returns>1, -1 или 0</returns>
        public static int PatientTemperatureSort(Patient x, Patient y)
        {
            return x.Temperature.CompareTo(y.Temperature);
        }

        /// <summary>
        /// Правило сортировки по свойству OxygenSaturation. Сортирует по величине.
        /// </summary>
        /// <param name="x">Пациент 1</param>
        /// <param name="y">Пациент 2</param>
        /// <returns>1, -1 или 0</returns>
        public static int PatientOxygenSaturationSort(Patient x, Patient y)
        {
            return x.OxygenSaturation.CompareTo(y.OxygenSaturation);
        }

        /// <summary>
        /// Правило сортировки по свойству Doctors. Сортирует по длине списков.
        /// </summary>
        /// <param name="x">Пациент 1</param>
        /// <param name="y">Пациент 2</param>
        /// <returns>1, -1 или 0</returns>
        public static int PatientDoctorsSort(Patient x, Patient y)
        {
            return x.Doctors.Count.CompareTo(y.Doctors.Count);
        }
    }
}
