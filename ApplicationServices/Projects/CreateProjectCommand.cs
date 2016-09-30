using ApplicationServices.Common;

namespace ApplicationServices.Projects
{
    public class CreateProjectCommand : ICommand
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
    }
}