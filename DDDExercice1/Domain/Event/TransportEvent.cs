#nullable enable
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DDDExercice1.Domain.Event
{
    public class TransportEvent: EventArgs
    {
        public enum EventType
        {
            Depart, Load, Arrive, Unload
        }

        public string @event { get; }
        public int time { get; }
        public int? duration { get; set; } = null;
        public int transport_id { get; }
        public string kind { get; }
        public string location { get; }
        public string? destination { get; }
        public CargoEvent[]? cargo { get; } = null;

        public TransportEvent(EventType @event, int time, int transportId, Vehicle vehicle, Place location, Place destination, Container[] containers)
        {
            this.destination = destination.Name.ToUpper();
            this.@event = @event.ToString().ToUpper();
            this.time = time;
            transport_id = transportId;
            this.kind = vehicle.Kind.ToUpper();
            this.location = location.Name.ToUpper();
            this.cargo = this.ContainersToCargoEvents(containers);
        }

        public TransportEvent(EventType @event, int time, int transportId, Vehicle vehicle, Place location, Container[] containers)
        {
            this.@event = @event.ToString().ToUpper();
            this.time = time;
            transport_id = transportId;
            this.kind = vehicle.Kind.ToUpper();
            this.location = location.Name.ToUpper();
            this.cargo = this.ContainersToCargoEvents(containers);
        }

        private CargoEvent[]? ContainersToCargoEvents(Container[] containers)
        {
            if (containers.Length == 0)
            {
                return null;
            }

            var events = new CargoEvent[containers.Length];
            for (int key = 0; key < containers.Length; key++)
            {
                events[key] = new CargoEvent(containers[key].Id, containers[key].DestinationPlace,
                    containers[key].Origin);
            }

            return events;
        }
    }
}
