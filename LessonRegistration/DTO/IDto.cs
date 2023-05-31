namespace LessonRegistration.DTO
{
    public interface IDto<TModel>
    {
        public TModel ToModel();
    }
}
