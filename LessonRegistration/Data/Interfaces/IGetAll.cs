using System.Collections.Generic;

namespace LessonRegistration.Data.Interfaces
{
    public interface IGetAll<T>
    {
        public IEnumerable<T> GetAll();
    }
}
