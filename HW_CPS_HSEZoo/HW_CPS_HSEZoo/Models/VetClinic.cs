using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW_CPS_HSEZoo.Interfaces;


namespace HW_CPS_HSEZoo.Models
{
    /// <summary>
    /// Проверяет здоровье.
    /// </summary>
    public class VetClinic : IHealthAnalizer
    {
        private float _targetHealth;

        // Каждое пятое животное не будет взято в зоопарк.
        public VetClinic() { _targetHealth = 0.2f; }

        public VetClinic(float thealth)
        {
            _targetHealth = 0f <= thealth && thealth <= 1.0f ? thealth : 0.5f;
        }

        public bool AnalyzeHealth(IAlive? being)
        {
            if (being == null) return false;

            // Узнаем пройдет животное или нет.
            Random rnd = new Random();
            if (rnd.NextDouble() < _targetHealth) return false;
            return true;
        }
    }
}
