using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing
{
    public delegate Patient ChangerDelegate(Patient p, string value);

    public static class PatientChanger
    {
        public static Patient PatientNameChange(Patient p, string value)
        {
            if (value == null) throw new ArgumentNullException();
            p.Name = value;
            return p;
        }

        public static Patient PatientAgeChange(Patient p, string value)
        {
            if (value == null) throw new ArgumentNullException();
            int age = 0;
            if (!int.TryParse(value, out age)) throw new ArgumentException();
            p.Age = age;
            return p;
        }

        public static Patient PatientGenderChange(Patient p, string value)
        {
            if (value == null) throw new ArgumentNullException();
            if (value.ToLower() == "male") p.Gender = "Male";
            if (value.ToLower() == "female") p.Gender = "Female";
            else p.Gender = "Other";
            return p;
        }

        public static Patient PatientDiagnosisChange(Patient p, string value)
        {
            if (value == null) throw new ArgumentNullException();
            p.Diagnosis = value;
            return p;
        }

        public static Patient PatientHeartRateChange(Patient p, string value)
        {
            if (value == null) throw new ArgumentNullException();
            int heartRate = 0;
            if (!int.TryParse(value, out heartRate)) throw new ArgumentException();
            p.HeartRate = heartRate;
            return p;
        }

        public static Patient PatientTemperatureChange(Patient p, string value)
        {
            if (value == null) throw new ArgumentNullException();
            double temperature = 0;
            if (!double.TryParse(value, out temperature)) throw new ArgumentException();
            p.Temperature = temperature;
            return p;
        }

        public static Patient PatientOxygenSaturationChange(Patient p, string value)
        {
            if (value == null) throw new ArgumentNullException();
            int oxygenSaturation = 0;
            if (!int.TryParse(value, out oxygenSaturation)) throw new ArgumentException();
            p.OxygenSaturation = oxygenSaturation;
            return p;
        }
    }
}
