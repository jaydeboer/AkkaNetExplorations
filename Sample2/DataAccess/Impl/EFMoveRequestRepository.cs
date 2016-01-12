using System.Collections.Generic;
using Sample2.Models;

namespace Sample2.DataAccess.Impl
{
    public class EFMoveRequestRepository : IMoveRequestRepository
    {
        public MoveRequest Create(MoveRequest request)
        {
            _db.Add(request);
            request.Id = _db.Count;
            return request;
        }
        
        public EFMoveRequestRepository()
        {
            _db = new List<MoveRequest>();
        }

        private List<MoveRequest> _db;
    }
}