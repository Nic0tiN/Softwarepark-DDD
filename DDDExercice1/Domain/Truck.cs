using System;
using System.Collections.Generic;
using System.Text;

namespace DDDExercice1.Domain
{
    class Truck: Vehicle
    {
        public Truck(string name, Place currentPlace): base(name, currentPlace) {}
    }
}
