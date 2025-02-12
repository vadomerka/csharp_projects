using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW_CPS_HSEZoo.Inventory.Animals;
using HW_CPS_HSEZoo.Interfaces;


namespace HW_CPS_HSEZoo
{
    internal class VetClinic : IHealthAnalizer
    {
        private float _targetHealth;

        public VetClinic() { _targetHealth = 0.5f; }

        public VetClinic(float thealth) 
        {
            _targetHealth = (0f <= thealth && thealth <= 1.0f) ? thealth : 0.5f;
        }

        public bool AnalyzeHealth(IAlive? being) {
            // If being is null
            if (being == null) return false;

            // Getting reliable data.
            Random rnd = new Random();
            if (rnd.NextDouble() < _targetHealth) return false;
            return true;
        }
    }
}
