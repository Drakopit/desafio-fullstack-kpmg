using System;

namespace desafio_fullstack.Domain
{
    public class LeaderBoard : Entity
    {
        public long PlayerId { get; set; }
        public long Balance { get; set; }
        public DateTime LastUpdateDate { get; set; }

        public LeaderBoard() {}

    }
}
