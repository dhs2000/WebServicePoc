using DomainModel;

using FluentNHibernate.Mapping;

namespace DataAccess.Mappings
{
    public class FileMap : ClassMap<File>
    {
        public FileMap()
        {
            this.Table("Files");

            this.Id(i => i.Id).Not.Nullable().GeneratedBy.Assigned();

            this.Map(i => i.Name).Not.Nullable().Length(255);

            this.Version(i => i.RootRevision).UnsavedValue("0");

            this.DynamicUpdate();
        }
    }
}