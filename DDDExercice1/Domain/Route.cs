using System;
using System.Collections.Generic;
using System.Text;

namespace DDDExercice1.Domain
{
    public class Route
    {
        private readonly Place _loadingPlace;
        private readonly Place _unloadingPlace;
        private readonly TimeSpan _travelingTime;

        public Route(Place loadingPlace, Place unloadingPlace, TimeSpan travelingTime)
        {
            this._loadingPlace = loadingPlace;
            this._unloadingPlace = unloadingPlace;
            this._travelingTime = travelingTime;
        }

        public Place LoadingPlace => _loadingPlace;
        public Place UnloadingPlace => _unloadingPlace;
        public TimeSpan TravelingTime => _travelingTime;

        public bool IsEqual(Place loadingPlace, Place unloadingPlace)
        {
            return (this._loadingPlace == loadingPlace && this._unloadingPlace == unloadingPlace);
        }

    }
}
