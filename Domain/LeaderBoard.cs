using System;

namespace desafio_fullstack.Domain
{
    public class LeaderBoard : Entity
    {
        public long PlayerId { get; private set; }
        public long Balance { get; private set; }
        public DateTime LastUpdateDate { get; private set; }

        public LeaderBoard(long playerId, long balance, DateTime lastUpdateDate)
        {
            PlayerId = playerId;
            Balance = balance;
            LastUpdateDate = lastUpdateDate;
        }

    }
}
