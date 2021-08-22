using System;
using System.Collections.Generic;
using System.Text;

namespace DDDExercice1.Domain
{
    abstract class Vehicle
    {
        public enum Action  
        {
            Ready, Moving, Unloading, Returning
        }

        private readonly string _name;
        private Place _currentPlace;
        private Place _destinationPlace;
        private Action _action;
        private Container _container;
        private TimeSpan _travelingTime;
        private Route _route;

        protected Vehicle(string name, Place defaultPlace)
        {
            this._name = name;
            this._currentPlace = defaultPlace;
        }

        public string Name => this._name;
        private Action State
        {
            get { return this._action; }
            set { this._action = value; }
        }

        public Place CurrentPlace
        {
            get => this._currentPlace;
            set => this._currentPlace = value;
        }

        public void Load()
        {
            if (this.State != Action.Ready)
            {
                return;
            }

            _container = this._currentPlace.LoadContainer();
            if (_container == null)
            {
                return;
            }

            this._route = Application.RouteFinder(this._currentPlace, this._container.DestinationPlace);
            this._destinationPlace = _route.UnloadingPlace;
            this.State = Action.Moving;
            this._travelingTime = _route.TravelingTime;
        }

        internal void Move(TimeSpan elapsedTime)
        {
            if (this.State != Action.Returning && this.State != Action.Moving)
            {
                return;
            }

            this._travelingTime -= elapsedTime;

            if (this._travelingTime <= TimeSpan.Zero)
            {
                if (this.State == Action.Moving)
                {
                    this._currentPlace = this._route.UnloadingPlace;
                    this.State = Action.Unloading;
                } else if (this.State == Action.Returning)
                {
                    this.State = Action.Ready;
                    this._currentPlace = this._route.LoadingPlace;
                    this._route = null;
                    this._container = null;
                }
            }
        }

        public void Unload()
        {
            if (this.State != Action.Unloading)
            {
                return;
            }

            this._destinationPlace.UnloadContainer(this._container);
            this.State = Action.Returning;
            this._travelingTime = this._route.TravelingTime;
            this._container = null;
            this._destinationPlace = null;
        }
    }
}
