using System;

namespace Players.Service.Domain
{
    public class PlayerGroup
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }
    }
}
