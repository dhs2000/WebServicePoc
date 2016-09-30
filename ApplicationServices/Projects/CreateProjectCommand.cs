using MediatR;

namespace ApplicationServices.Projects
{
    public class CreateProjectCommand : IAsyncRequest
    {
        public CreateProjectCommand(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        protected CreateProjectCommand()
        {
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public override string ToString()
        {
            return $"{nameof(this.Id)}: {this.Id}, {nameof(this.Name)}: {this.Name}";
        }
    }
}