using System;
using System.Collections.Generic;
using System.Text;

namespace DDDExercice1.Domain
{
    class Boat: Vehicle 
    {
        public Boat(string name, Place currentPlace): base(name, currentPlace) { }
    }
}
