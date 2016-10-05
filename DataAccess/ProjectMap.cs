using DomainModel;

using FluentNHibernate.Mapping;

namespace DataAccess
{
    public class ProjectMap : ClassMap<Project>
    {
        public ProjectMap()
        {
            this.Id(i => i.Id).Not.Nullable();

            this.Map(i => i.Name).Not.Nullable().Length(255);

            this.HasMany<ProjectItem>(i => i.Items)
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField()
                .NotFound.Exception()
                .Fetch.Join()
                .AsBag()
                .Inverse();

            this.Map(i => i.RootRevision)
                .OptimisticLock();

            this.Not.LazyLoad();
        }
    }
}