using System;

namespace DDDExercice1.Domain
{
    class Boat: Vehicle 
    {
        public Boat(int id, string name, Place currentPlace): base(id, name, currentPlace) { }
        public override string Kind => "SHIP";
        protected override int Capacity => 4;
        protected override TimeSpan LoadingTime => TimeSpan.FromHours(1);
        protected override TimeSpan UnloadingTime => TimeSpan.FromHours(1);
    }
}
