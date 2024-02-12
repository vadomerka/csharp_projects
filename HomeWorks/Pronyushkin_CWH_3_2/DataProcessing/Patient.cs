using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace DataProcessing
{
    /// <summary>
    /// Класс Player - объект, считываемый из json формата.
    /// </summary>
    public class Patient : IUpdate
    {
        public event EventHandler<EventTime>? Updated;

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

        // Свойства.
        [JsonPropertyName("patient_id")]
        public int PatientId { 
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
            }
        }
        [JsonPropertyName("age")]
        public int Age
        {
            get => _age;
            set
            {
                _age = value;
            }
        }
        [JsonPropertyName("gender")]
        public string Gender
        {
            get => _gender;
            set
            {
                _gender = value ?? "";
            }
        }
        [JsonPropertyName("diagnosis")]
        public string Diagnosis
        {
            get => _diagnosis;
            set
            {
                _diagnosis = value ?? "";
            }
        }
        [JsonPropertyName("heart_rate")]
        public int HeartRate
        {
            get => _heartRate;
            set
            {
                _heartRate = value;
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