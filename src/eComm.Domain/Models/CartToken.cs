using System;

namespace eComm.Domain.Models
{
    public class CartToken
    {
        public CartToken(Guid tokenId, DateTime created)
        {
            TokenId = tokenId;
            Created = created;
        }

        public Guid TokenId { get; }
        public DateTime Created { get; }
        public DateTime Expires { get; } = DateTime.Now.AddDays(3);
    }
}