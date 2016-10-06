using System;

namespace ApplicationServices.Projects
{
    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public long ItemsCount { get; set; }        
    }
}