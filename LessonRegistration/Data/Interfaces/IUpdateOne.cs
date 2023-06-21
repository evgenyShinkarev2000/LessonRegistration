namespace LessonRegistration.Data.Interfaces
{
    public interface IUpdateOne<T>
    {
        public T Update(T item);
    }
}
