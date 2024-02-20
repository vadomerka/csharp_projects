using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataProcessing.EventProcessing;

namespace DataProcessing.Objects
{
    /// <summary>
    /// Класс Доктор. Находится в отношении Ассоциации с Пациентами.
    /// </summary>
    public class Doctor : IUpdate
    {
        public event EventHandler<EventTime>? Updated;

        private int _doctorId;
        private string _name;
        private int _appointmentCount;
        private List<Patient> _patients;
        private List<int> _appointPatientIds;

        public Doctor()
        {
            _name = string.Empty;
            _patients = new List<Patient>();
            _appointPatientIds = new List<int>();
        }

        public Doctor(int doctorId, string? name, int appointmentCount)
        {
            _doctorId = doctorId;
            _name = name ?? "";
            _appointmentCount = appointmentCount;
            _patients = new List<Patient>();
            _appointPatientIds = new List<int>();
        }

        // Свойства для JsonSerializer. При изменении вызывают событие.
        [JsonPropertyName("doctor_id")]
        public int DoctorId
        {
            get => _doctorId;
            set
            {
                _doctorId = value;
                Updated?.Invoke(this, new EventTime());
            }
        }
        [JsonPropertyName("name")]
        public string? Name
        {
            get => _name;
            set
            {
                _name = value ?? "";
                Updated?.Invoke(this, new EventTime());
            }
        }
        [JsonPropertyName("appointment_count")]
        public int AppointmentCount
        {
            get => _appointmentCount;
            set
            {
                _appointmentCount = value;
                Updated?.Invoke(this, new EventTime());
            }
        }
        [JsonIgnore]
        public List<Patient> Patients
        {
            get => _patients;
            set
            {
                _patients = value ?? new List<Patient>();
                Updated?.Invoke(this, new EventTime());
            }
        }

        /// <summary>
        /// Метод проверяет жизненно важные показатели пациента.
        /// </summary>
        /// <param name="obj">Пациент</param>
        /// <param name="ev">Время изменения показателей</param>
        /// <exception cref="ArgumentNullException">Вызывается при пустом объекте</exception>
        /// <exception cref="ArgumentException">Вызывается если объект не пациент</exception>
        public void UpdateEventHandler(object? obj, EventTime ev)
        {
            if (obj == null) throw new ArgumentNullException();
            if (!(obj is Patient)) throw new ArgumentException();

            Patient p = (Patient)obj;
            if (p.HeartRate < 60 || 100 < p.HeartRate ||
                p.Temperature < 36 || 38 < p.Temperature ||
                p.OxygenSaturation < 95 || 100 < p.OxygenSaturation)
            {
                // Если пациент еще не записан на прием - записываем.
                if (!HasAppointment(p))
                {
                    AppointmentCount++;
                    _appointPatientIds.Add(p.PatientId);
                }
            }
            else
            {
                // Если пациент здоров, и записан на прием - отписываем.
                if (HasAppointment(p))
                {
                    AppointmentCount--;
                    _appointPatientIds.Remove(p.PatientId);
                }
            }        }
        /// <summary>
        /// Вспомогательный метод. Проверяет записан ли пациент с этим id на прием.
        /// </summary>
        /// <param name="patient">Пациент</param>
        /// <returns>true если записан на прием, иначе false</returns>
        private bool HasAppointment(Patient patient)
        {
            for (int i = 0; i < _appointPatientIds.Count; i++)
            {
                if (_appointPatientIds[i] == patient.PatientId)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Вспомогательный метод. Проверяет есть ли пациент с этим id в списке доктора.
        /// </summary>
        /// <param name="patient">пациент</param>
        /// <returns>true если есть, иначе false</returns>
        private bool IsAssigned(Patient patient)
        {
            for (int i = 0; i < _patients.Count; i++)
            {
                if (_patients[i].PatientId == patient.PatientId)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Добавляет пациента в список к доктору, если его там нет.
        /// </summary>
        /// <param name="patient">Пациент</param>
        public void AddPatient(Patient patient)
        {
            if (!IsAssigned(patient))
                _patients.Add(patient);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{_doctorId} {_name}");
            foreach (Patient p in _patients)
            {
                sb.Append($" {p.PatientId}");
            }
            return sb.ToString();
        }

        public string ToJSON()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            return JsonSerializer.Serialize(this, options);
        }
    }
}
