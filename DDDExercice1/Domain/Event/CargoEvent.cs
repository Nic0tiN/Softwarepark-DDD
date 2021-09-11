using System;

namespace DDDExercice1.Domain.Event
{
    public class CargoEvent : EventArgs
    {
        public int cargo_id { get; }
        public string destination { get; }
        public string origin { get; }
        public CargoEvent(int cargo_id, Place destination, Place origin)
        {
            this.cargo_id = cargo_id;
            this.destination = destination.Name.ToUpper();
            this.origin = origin.Name.ToUpper();
        }
    }
}
