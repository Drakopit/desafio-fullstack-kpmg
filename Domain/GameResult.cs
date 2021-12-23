using System;

namespace desafio_fullstack.Domain
{
    public class GameResult : Entity
    {
        public long PlayerId { get; set; }
        public long GameId { get; set; }
        public long Win { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
