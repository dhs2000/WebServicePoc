using DomainModel;

using FluentNHibernate.Mapping;

namespace DataAccess
{
    public class ProjectItemMap : ClassMap<ProjectItem>
    {
        public ProjectItemMap()
        {
            this.Id(i => i.Id).Not.Nullable().GeneratedBy.Assigned();

            this.Map(i => i.Name).Not.Nullable().Length(255);

            this.References(i => i.Project).Not.Nullable().Column("ProjectId");

            this.Version(i => i.Version).UnsavedValue("0");

            this.Not.LazyLoad();

            this.DynamicUpdate();
        }
    }
}