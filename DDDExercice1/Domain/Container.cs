
namespace DDDExercice1.Domain
{

    public enum State
    {
        Stocked, Moving, Arrived
    }
    public class Container
    {
        private Place _currentPlace;

        public Container(int containerId, Place destinationPlace, Place currentPlace)
        {
            this.DestinationPlace = destinationPlace;
            this._currentPlace = currentPlace;
            this.Origin = currentPlace;

            this._currentPlace.UnloadContainer(this);
            this.Id = containerId;
        }

        public void MoveAt(Place currentPlace)
        {
            this._currentPlace = currentPlace;

            this.State = _currentPlace == DestinationPlace ? State.Arrived : State.Moving;
        }
        
        public int Id { get; }
        public Place DestinationPlace { get; }

        public Place Origin { get; }

        public State State
        {
            get;
            set;
        } = State.Stocked;
    }
}
