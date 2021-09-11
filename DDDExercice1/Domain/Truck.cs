using System;

namespace DDDExercice1.Domain
{
    class Truck: Vehicle
    {
        public Truck(int id, string name, Place currentPlace): base(id, name, currentPlace) {}
        public override string Kind => "TRUCK";
        protected override int Capacity => 1;
        protected override TimeSpan LoadingTime => TimeSpan.Zero;
        protected override TimeSpan UnloadingTime => TimeSpan.Zero;
    }
}
