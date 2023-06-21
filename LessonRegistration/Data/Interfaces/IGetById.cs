namespace LessonRegistration.Data.Interfaces
{
    public interface IGetById<Tout, Tid>
    {
        public Tout GetById(Tid id);
    }
}
