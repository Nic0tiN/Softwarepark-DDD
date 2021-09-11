using System.Collections.Generic;

namespace DDDExercice1.Domain
{
    public class Place
    {
        private List<Container> Containers = new List<Container>();
        public Place(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public void UnloadContainer(Container container)
        {
            this.Containers.Add(container);
            container.MoveAt(this);
        }

        public Container LoadContainer()
        {
            if (this.Containers.Count == 0)
            {
                return null;
            }

            var container = this.Containers[0];
            this.Containers.RemoveAt(0);

            return container;
        }
    }
}
