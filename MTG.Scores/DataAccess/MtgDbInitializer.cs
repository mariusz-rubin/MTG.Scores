using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using MTG.Scores.Models;
using System.Collections.Generic;

namespace MTG.Scores.DataAccess
{
  public class MtgDbInitializer : DropCreateDatabaseAlways<MtgContext>
  {
    protected override void Seed(MtgContext context)
    {
      int numberOfPlayers = 10;

      var players = new List<Player>();

      for (int i = 0; i < numberOfPlayers; i++)
      {
        players.Add(new Player
        {
          ID = 1,
          Name = "Player " + i.ToString(),
        });
      }

      players.ForEach(p => context.Players.Add(p));
      context.SaveChanges();

      var matches = new List<Match>();

      matches.Add(new Match { ID = 1, Player1Score = 2, Player2Score = 0, Player1 = players[0], Player2 = players[1] });
      matches.Add(new Match { ID = 2, Player1Score = 2, Player2Score = 1, Player1 = players[1], Player2 = players[2] });
      matches.Add(new Match { ID = 3, Player1Score = 2, Player2Score = 0, Player1 = players[2], Player2 = players[3] });
      matches.Add(new Match { ID = 4, Player1Score = 0, Player2Score = 2, Player1 = players[3], Player2 = players[4] });
      matches.Add(new Match { ID = 5, Player1Score = 1, Player2Score = 2, Player1 = players[4], Player2 = players[5] });

      matches.ForEach(m => context.Matches.Add(m));
      context.SaveChanges();

      base.Seed(context);
    }
  }
}