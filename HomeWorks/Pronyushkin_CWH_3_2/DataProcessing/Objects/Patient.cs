using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using DataProcessing.EventProcessing;

namespace DataProcessing.Objects
{
    /// <summary>
    /// Класс Доктор. Находится в отношении Ассоциации с Пациентами.
    /// </summary>
    public class Patient : IUpdate
    {
        public event EventHandler<EventTime>? Updated;
        public event EventHandler<EventTime>? HeartRateUpdated;
        public event EventHandler<EventTime>? TemperatureUpdated;
        public event EventHandler<EventTime>? OxygenSaturationUpdated;

        private int _patientId;
        private string _name;
        private int _age;
        private string _gender;
        private string _diagnosis;
        private int _heartRate;
        private double _temperature;
        private int _oxygenSaturation;
        private List<Doctor> _doctors;

        public Patient()
        {
            _name = "";
            _gender = "";
            _diagnosis = "";
            _doctors = new List<Doctor>();
        }

        public Patient(int patientId, string name, int age, string gender,
                        string diagnosis, int heartRate, double temperature,
                        int oxygenSaturation, List<Doctor> doctors)
        {
            _patientId = patientId;
            _name = name ?? "";
            _age = age;
            _gender = gender ?? "";
            _diagnosis = diagnosis ?? "";
            _heartRate = heartRate;
            _temperature = temperature;
            _oxygenSaturation = oxygenSaturation;
            _doctors = doctors ?? new List<Doctor>();
        }

        // Свойства для JsonSerializer. При изменении вызывают событие.
        [JsonPropertyName("patient_id")]
        public int PatientId
        {
            get => _patientId;
            set => _patientId = value;
        }
        [JsonPropertyName("name")]
        public string Name
        {
            get => _name;
            set
            {
                _name = value ?? "";
                Updated?.Invoke(this, new EventTime());
            }
        }
        [JsonPropertyName("age")]
        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                Updated?.Invoke(this, new EventTime());
            }
        }
        [JsonPropertyName("gender")]
        public string Gender
        {
            get => _gender;
            set
            {
                _gender = value ?? "";
                Updated?.Invoke(this, new EventTime());
            }
        }
        [JsonPropertyName("diagnosis")]
        public string Diagnosis
        {
            get => _diagnosis;
            set
            {
                _diagnosis = value ?? "";
                Updated?.Invoke(this, new EventTime());
            }
        }
        // Жизненно важные свойства вызывают дополнительные события для докторов.
        [JsonPropertyName("heart_rate")]
        public int HeartRate
        {
            get => _heartRate;
            set
            {
                _heartRate = value;
                HeartRateUpdated?.Invoke(this, new EventTime());
                Updated?.Invoke(this, new EventTime());
            }
        }
        [JsonPropertyName("temperature")]
        public double Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                TemperatureUpdated?.Invoke(this, new EventTime());
                Updated?.Invoke(this, new EventTime());
            }
        }
        [JsonPropertyName("oxygen_saturation")]
        public int OxygenSaturation
        {
            get => _oxygenSaturation;
            set
            {
                _oxygenSaturation = value;
                OxygenSaturationUpdated?.Invoke(this, new EventTime());
                Updated?.Invoke(this, new EventTime());
            }
        }
        [JsonPropertyName("doctors")]
        public List<Doctor> Doctors
        {
            get => _doctors;
            set
            {
                _doctors = value ?? new List<Doctor>();
                Updated?.Invoke(this, new EventTime());
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{_patientId} {_name}");
            foreach (Doctor d in _doctors)
            {
                sb.Append($" {d.DoctorId}");
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