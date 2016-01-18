namespace  Sample2.DataAccess.Impl
{
    public class EFRepositoryFactory : IRepositoryFactory
    {
        public IMoveRequestRepository GetMoveRequestRepository()
        {
            return new EFMoveRequestRepository();
        }
    }
}