using System;
using System.Collections.Generic;
using System.Linq;
using DDDExercice1.Domain;

namespace DDDExercice1
{
    public class Application
    {
        private static Dictionary<(Place, Place), Route> Routing = new Dictionary<(Place, Place), Route>();
        private Dictionary<string, Place> Locations = new Dictionary<string, Place>();
        private HashSet<Container> Containers = new HashSet<Container>();
        private HashSet<Vehicle> Vehicles = new HashSet<Vehicle>();
        private TimeSpan _currentTime;

        public Application(IEnumerable<string>containersDestination)
        {
            Place factory = new Place("Factory");
            Place port = new Place("Port");
            Place locationA = new Place("A");
            Place locationB = new Place("B");

            Locations.Add(factory.Name, factory);
            Locations.Add(port.Name, port);
            Locations.Add(locationA.Name, locationA);
            Locations.Add(locationB.Name, locationB);

            Routing.Add((factory, locationA), new Route(factory, port, TimeSpan.FromHours(1)));
            Routing.Add((port, locationA), new Route(port, locationA, TimeSpan.FromHours(4)));
            Routing.Add((factory, locationB), new Route(factory, locationB, TimeSpan.FromHours(5)));

            Truck truck1 = new Truck("Truck 1", factory);
            Truck truck2 = new Truck("Truck 2", factory);
            Boat boat1 = new Boat("Boat 1", port);

            this.Vehicles.Add(truck1);
            this.Vehicles.Add(truck2);
            this.Vehicles.Add(boat1);

            foreach (var containerDestination in containersDestination)
            {
                this.Containers.Add(new Container(this.Locations[containerDestination], factory));
            }
        }

        public void Delivery()
        {
            var deliveryTime = new TimeSpan();
            while (!this.AreAllContainersDelivered())
            {
                foreach (var vehicle in this.Vehicles)
                {
                    vehicle.Load();
                }

                TimeSpan elapsedTime = TimeSpan.FromHours(1);
                this._currentTime += elapsedTime;
                // Find available vehicle and load container
                foreach (var vehicle in this.Vehicles)
                {
                    vehicle.Move(elapsedTime);
                }

                foreach (var vehicle in this.Vehicles)
                {
                    vehicle.Unload();
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

        public TimeSpan CurrentTime => _currentTime;
    }
}
