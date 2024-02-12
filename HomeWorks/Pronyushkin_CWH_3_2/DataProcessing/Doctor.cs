using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataProcessing
{
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

        [JsonPropertyName("doctor_id")]
        public int DoctorId
        {
            get => _doctorId;
            set => _doctorId = value;
        }
        [JsonPropertyName("name")]
        public string? Name
        {
            get => _name;
            set => _name = value ?? "";
        }
        [JsonPropertyName("appointment_count")]
        public int AppointmentCount
        {
            get => _appointmentCount;
            set => _appointmentCount = value;
        }
        [JsonIgnore]
        public List<Patient> Patients
        {
            get => _patients;
            set => _patients = value ?? new List<Patient>();
        }

        public void UpdateEventHandler(object? obj, EventTime ev)
        {
            if (obj == null) throw new ArgumentNullException();
            if (!(obj is Patient)) throw new ArgumentException();

            Patient p = (Patient)obj;
            if ((p.HeartRate < 60 || 100 < p.HeartRate) ||
                (p.Temperature < 36 || 38 < p.Temperature) ||
                (p.OxygenSaturation < 95 || 100 < p.OxygenSaturation))
            {
                if (!HasAppointment(p))
                {
                    this.AppointmentCount++;
                    _appointPatientIds.Add(p.PatientId);
                }
            }
            else
            {
                if (HasAppointment(p))
                {
                    this.AppointmentCount--;
                    _appointPatientIds.Remove(p.PatientId);
                }
            }
            Console.WriteLine($"{DoctorId}: appointment count {this.AppointmentCount}");        }

        public bool HasAppointment(Patient patient)
        {
            for (int i = 0; i < _appointPatientIds.Count; i++)
            {
                if (_appointPatientIds[i] == patient.PatientId)
                    return true;
            }
            return false;
        }

        private bool IsAssigned(Patient patient)
        {
            for (int i = 0; i < _patients.Count; i++)
            {
                if (_patients[i].PatientId == patient.PatientId)
                    return true;
            }
            return false;
        }

        public void AddPatient(Patient patient)
        {
            if (!this.IsAssigned(patient))
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
