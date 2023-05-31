using LessonRegistration.Data;

namespace LessonRegistration.DTO
{
    public class InstituteShort : PostgreEntity, IDto<Data.Institute>
    {
        public string? Name { get; set; }
        public InstituteShort() { }
        public InstituteShort(Data.Institute institute)
        {
            Id = institute.Id;
            Name = institute.Name;
        }

        public virtual Data.Institute ToModel()
        {
            return new Data.Institute() { Name = this.Name!, Id = this.Id };
        }
    }
}
