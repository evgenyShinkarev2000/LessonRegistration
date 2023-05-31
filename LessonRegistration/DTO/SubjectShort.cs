using LessonRegistration.Data;

namespace LessonRegistration.DTO
{
    public class SubjectShort : PostgreEntity, IDto<Data.Subject>
    {
        public int Capacity { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public SubjectShort() { }
        public SubjectShort(Data.Subject subject)
        {
            this.Id = subject.Id;
            this.Capacity = subject.Capacity;
            this.Name = subject.Name;
            this.Description = subject.Description;
        }
        public virtual Data.Subject ToModel()
        {
            return new Data.Subject()
            {
                Id = this.Id,
                Capacity = this.Capacity,
                Name = this.Name!,
                Description = this.Description!
            };
        }
    }
}
