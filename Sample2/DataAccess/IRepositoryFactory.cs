namespace Sample2.DataAccess
{
    public interface IRepositoryFactory
    {
        IMoveRequestRepository GetMoveRequestRepository();
    }
}