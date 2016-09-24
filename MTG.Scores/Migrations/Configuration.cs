namespace MTG.Scores.Migrations
{
  using Models;
  using System.Collections.Generic;
  using System.Data.Entity.Migrations;

  internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.MtgContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
      ContextKey = "MTG.Scores.DataAccess.MtgContext";
    }

    protected override void Seed(DataAccess.MtgContext context)
    {
      int numberOfPlayers = 10;

      var players = new List<Player>();

      for (int i = 0; i < numberOfPlayers; i++)
      {
        players.Add(new Player
        {
          Name = "Player " + i.ToString(),
        });
      }

      context.Players.AddOrUpdate(
        p => p.Name,
        players.ToArray());

      context.SaveChanges();

      var matches = new List<Match>();

      matches.Add(new Match { Player1Score = 2, Player2Score = 0, Player1ID = players[0].ID, Player2ID = players[1].ID });
      matches.Add(new Match { Player1Score = 2, Player2Score = 1, Player1ID = players[1].ID, Player2ID = players[2].ID });
      matches.Add(new Match { Player1Score = 2, Player2Score = 0, Player1ID = players[2].ID, Player2ID = players[3].ID });
      matches.Add(new Match { Player1Score = 0, Player2Score = 2, Player1ID = players[3].ID, Player2ID = players[4].ID });
      matches.Add(new Match { Player1Score = 1, Player2Score = 2, Player1ID = players[4].ID, Player2ID = players[5].ID });

      context.Matches.AddOrUpdate(
        m => new { m.Player1ID, m.Player2ID },
        matches.ToArray());

      context.SaveChanges();

      base.Seed(context);
    }
  }
}
