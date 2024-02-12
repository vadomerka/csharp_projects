using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing
{
    public static class PatientsProcessing
    {
        public static void DoctorSeparator(List<Patient> patients, Dictionary<int, Doctor> doctorList)
        {
            foreach (Patient p in patients)
            {
                for (int i = 0; i < p.Doctors.Count; i++)
                {
                    Doctor d = p.Doctors[i];
                    if (doctorList.TryGetValue(d.DoctorId, out Doctor? resDoc))
                    {
                        p.Doctors[i] = resDoc;
                    }
                    else
                    {
                        doctorList[d.DoctorId] = d;
                    }
                    p.Doctors[i].AddPatient(p);

                    bool removedDoctor = false;
                    for (int j = 0; j < i; j++)
                    {
                        if (p.Doctors[j].DoctorId == p.Doctors[i].DoctorId)
                        {
                            p.Doctors.RemoveAt(i);
                            removedDoctor = true;
                            i--;
                            break;
                        }
                    }

                    if (!removedDoctor)
                    {
                        p.Updated += p.Doctors[i].UpdateEventHandler;
                    }
                }
            }
        }
    }
}
