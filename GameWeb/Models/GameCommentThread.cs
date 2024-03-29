﻿using System.Collections.Generic;

namespace GameWeb.Models
{
    public class GameCommentThread
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public List<GameComment> Comments { get; set; }
    }
}
