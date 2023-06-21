namespace LessonRegistration.Data.Models
{
    public class Person : PostgreEntity
    {
        public string FirstName { get; set; } = default!;
        public string SecondName { get; set; } = default!;
        public string Patronymic { get; set; } = default!;
    }
}
