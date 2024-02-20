using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProcessing.Objects;

namespace DataProcessing.PatientProcessing
{
    /// <summary>
    /// Производит начальную обработку списка пациентов.
    /// </summary>
    public static class PatientsProcessing
    {
        /// <summary>
        /// Удаляет дубликаты докторов. Устанавливает отношение ассоциации между объектами.
        /// Создает словарь докторов. Подписывает докторов на обновление важных параметров.
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="doctorList"></param>
        public static void DoctorSeparator(List<Patient> patients, Dictionary<int, Doctor> doctorList)
        {
            foreach (Patient p in patients)
            {
                for (int i = 0; i < p.Doctors.Count; i++)
                {
                    Doctor d = p.Doctors[i];
                    // Если доктор уже записан, заменяем дубликат на ссылку.
                    if (doctorList.TryGetValue(d.DoctorId, out Doctor? resDoc))
                    {
                        p.Doctors[i] = resDoc;
                    }
                    // Если нет, создаем запись в словаре.
                    else
                    {
                        doctorList[d.DoctorId] = d;
                    }
                    // Добавляем пациента в словарь доктору.
                    p.Doctors[i].AddPatient(p);

                    // Убираем дубликаты докторов.
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
                    // Если доктор уникален, подписываем его на события показателей пациента.
                    if (!removedDoctor)
                    {
                        p.HeartRateUpdated += p.Doctors[i].UpdateEventHandler;
                        p.TemperatureUpdated += p.Doctors[i].UpdateEventHandler;
                        p.OxygenSaturationUpdated += p.Doctors[i].UpdateEventHandler;
                    }
                }
            }
        }
    }
}
