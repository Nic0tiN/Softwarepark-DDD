using System;
using System.Collections.Generic;
using System.Linq;
using DDDExercice1.Domain;
using DDDExercice1.Domain.Event;

namespace DDDExercice1.Application
{
    public class WorldApplication
    {
        private static Dictionary<(Place, Place), Route> Routing = new Dictionary<(Place, Place), Route>();
        private Dictionary<string, Place> Locations = new Dictionary<string, Place>();
        private HashSet<Container> Containers = new HashSet<Container>();
        private HashSet<Vehicle> Vehicles = new HashSet<Vehicle>();
        private static TimeSpan _currentTime;
        public EventHandler<TransportEvent> TransportEventHandler;

        public WorldApplication(IEnumerable<string>containersDestination)
        {
            _currentTime = new TimeSpan(0);
            Place factory = new Place("Factory");
            Place port = new Place("Port");
            Place locationA = new Place("A");
            Place locationB = new Place("B");

            Locations.Add(factory.Name, factory);
            Locations.Add(port.Name, port);
            Locations.Add(locationA.Name, locationA);
            Locations.Add(locationB.Name, locationB);

            Routing.Add((factory, locationA), new Route(factory, port, TimeSpan.FromHours(1)));
            Routing.Add((port, locationA), new Route(port, locationA, TimeSpan.FromHours(6)));
            Routing.Add((factory, locationB), new Route(factory, locationB, TimeSpan.FromHours(5)));

            Truck truck1 = new Truck(0, "Truck 1", factory);
            Truck truck2 = new Truck(1, "Truck 2", factory);
            Boat boat1 = new Boat(2, "Boat 1", port);
            truck1.TransportEventHandler += WorldApplicationOnTransportEventHandler;
            truck2.TransportEventHandler += WorldApplicationOnTransportEventHandler;
            boat1.TransportEventHandler += WorldApplicationOnTransportEventHandler;

            this.Vehicles.Add(truck1);
            this.Vehicles.Add(truck2);
            this.Vehicles.Add(boat1);

            int i = 0;
            foreach (var containerDestination in containersDestination)
            {
                this.Containers.Add(new Container(i++, this.Locations[containerDestination], factory));
            }
        }

        private void WorldApplicationOnTransportEventHandler(object sender, TransportEvent e)
        {
            TransportEventHandler?.Invoke(sender, e);
        }

        public void Delivery()
        {
            while (!(this.AreAllContainersDelivered() && AreAllVehiclesReady()))
            {
                foreach (var vehicle in this.Vehicles)
                {
                    vehicle.Load();
                }

                TimeSpan elapsedTime = TimeSpan.FromHours(1);
                _currentTime += elapsedTime;

                foreach (var vehicle in this.Vehicles)
                {
                    vehicle.Move(elapsedTime);
                }

                foreach (var vehicle in this.Vehicles)
                {
                    vehicle.Unload(elapsedTime);
                }
            }
        }

        public static Route RouteFinder(Place loadingPlace, Place unloadingPlace)
        {
            return Routing[(loadingPlace, unloadingPlace)];
        }

        private bool AreAllContainersDelivered()
        {
            return this.Containers.All(c => c.State == State.Arrived);
        }

        private bool AreAllVehiclesReady()
        {
            return this.Vehicles.All(c => c.State == Vehicle.Action.Ready);
        }

        public static TimeSpan CurrentTime => _currentTime;
    }
}
