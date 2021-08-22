using System;
using System.Collections.Generic;
using System.Text;

namespace DDDExercice1.Domain
{
    public enum State
    {
        Stocked, Moving, Arrived
    }
    public class Container
    {

        private readonly Place _destinationPlace;
        private Place _currentPlace;

        public Container(Place destinationPlace, Place currentPlace)
        {
            this._destinationPlace = destinationPlace;
            this._currentPlace = currentPlace;

            this._currentPlace.UnloadContainer(this);
        }

        public void MoveAt(Place currentPlace)
        {
            this._currentPlace = currentPlace;

            this.State = _currentPlace == _destinationPlace ? State.Arrived : State.Moving;
        }

        public Place DestinationPlace => this._destinationPlace;

        public State State
        {
            get;
            set;
        } = State.Stocked;
    }
}
