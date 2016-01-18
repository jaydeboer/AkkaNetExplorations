using Sample2.Models;

namespace Sample2.DataAccess
{
    public interface IMoveRequestRepository
    {
        MoveRequest Create(MoveRequest request);
    }
}