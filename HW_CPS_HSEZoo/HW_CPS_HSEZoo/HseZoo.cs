using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW_CPS_HSEZoo.Animals;

namespace HW_CPS_HSEZoo 
{
    /*
    class Animal
    class Herbo, 
    class Predator, 

    class Monkey, 
    class Rabbit, 
    class Tiger, 
    class Wolf
    */
    internal class HseZoo 
    {
        List<IAlive> _animalList = new List<IAlive>();

        public void Write() {
            _animalList.Add(new Wolf());
            
        }
    }
}