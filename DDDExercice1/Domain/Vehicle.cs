using System;
using System.Collections.Generic;
using System.Linq;
using DDDExercice1.Application;
using DDDExercice1.Domain.Event;

namespace DDDExercice1.Domain
{
    public abstract class Vehicle
    {
        public abstract string Kind { get; }
        protected abstract int Capacity { get; }
        protected abstract TimeSpan LoadingTime { get; }
        protected abstract TimeSpan UnloadingTime { get; }

        public enum Action  
        {
            Ready, Loading, Moving, Arriving, Unloading, Returning
        }

        private readonly string _name;
        private Place _currentPlace;
        private Place _destinationPlace;
        private Action _action;
        private List<Container> _containers = new List<Container>();
        private TimeSpan _travelingTime;
        private Route _route;
        private int Id;

        public event EventHandler<TransportEvent> TransportEventHandler;

        protected Vehicle(int id, string name, Place defaultPlace)
        {
            this._name = name;
            this._currentPlace = defaultPlace;
            this.Id = id;
        }

        public Action State
        {
            get => this._action;
            set
            {
                TransportEvent eEvent;
                switch (value)
                {
                    case Action.Loading:
                        eEvent = new TransportEvent(TransportEvent.EventType.Load, (int)WorldApplication.CurrentTime.TotalHours, Id, this,
                            _currentPlace, this._destinationPlace, _containers.ToArray());
                        eEvent.duration = (int)this.LoadingTime.TotalHours;
                        TransportEventHandler?.Invoke(this, eEvent);
                        break;
                    case Action.Returning:
                    case Action.Moving:
                         eEvent = new TransportEvent(TransportEvent.EventType.Depart, (int)WorldApplication.CurrentTime.TotalHours, this.Id, this, this._currentPlace, this._destinationPlace, this._containers.ToArray());
                        TransportEventHandler?.Invoke(this, eEvent);
                       break;
                    case Action.Unloading:
                        eEvent = new TransportEvent(TransportEvent.EventType.Arrive, (int)WorldApplication.CurrentTime.TotalHours, this.Id, this, this._currentPlace, this._containers.ToArray());
                        TransportEventHandler?.Invoke(this, eEvent);
                        
                        eEvent = new TransportEvent(TransportEvent.EventType.Unload, (int)WorldApplication.CurrentTime.TotalHours, Id, this,
                            _currentPlace, _containers.ToArray());
                        eEvent.duration = (int)this.UnloadingTime.TotalHours;
                        TransportEventHandler?.Invoke(this, eEvent);
                        break;
                    case Action.Ready: 
                        eEvent = new TransportEvent(TransportEvent.EventType.Arrive, (int)WorldApplication.CurrentTime.TotalHours, this.Id, this, this._currentPlace, this._containers.ToArray());
                        TransportEventHandler?.Invoke(this, eEvent);
                        break;
                }
                this._action = value;
            }
        }

        public void Load()
        {
            if (this.State != Action.Ready)
            {
                return;
            }

            Container container = null;
            while (this._containers.Count < this.Capacity && (container = this._currentPlace.LoadContainer()) != null)
            {
                this._containers.Add(container);
            }

            if (this._containers.Count == 0)
            {
                return;
            }  

            this._route = WorldApplication.RouteFinder(this._currentPlace, this._containers.First().DestinationPlace);
            this._destinationPlace = _route.UnloadingPlace;

            this.State = Action.Loading;
            if (LoadingTime > TimeSpan.Zero)
            {
                this._travelingTime = this.LoadingTime;
            }
            else
            {
                this.State = Action.Moving;
                this._travelingTime = this._route.TravelingTime;
            }
        }

        public void Move(TimeSpan elapsedTime)
        {
            if (this.State == Action.Ready)
            {
                return;
            }

            this._travelingTime -= elapsedTime;

            if (this._travelingTime <= TimeSpan.Zero)
            {
                if (this.State == Action.Loading)
                {
                    this._travelingTime = this._route.TravelingTime;
                    this.State = Action.Moving;

                }
                else if (this.State == Action.Moving)
                {
                    this._currentPlace = this._route.UnloadingPlace;
                    this._travelingTime = this.UnloadingTime;
                    this.State = Action.Unloading;
                } 
                else if (this.State == Action.Returning)
                {
                    this._currentPlace = this._route.LoadingPlace;
                    this._route = null;
                    this._containers.Clear();
                    this.State = Action.Ready;
                }
            }
        }

        public void Unload(TimeSpan elapsedTime)
        {
            if (this.State != Action.Unloading)
            {
                return;
            }

            if (this._travelingTime <= TimeSpan.Zero)
            {
                foreach (var container in this._containers)
                {
                    this._destinationPlace.UnloadContainer(container);
                }

                this._travelingTime = this._route.TravelingTime;
                this._destinationPlace = this._route.LoadingPlace;

                this._containers.Clear();
                this.State = Action.Returning;
            }
        }
    }
}
