using MTG.Scores.DataAccess;
using MTG.Scores.Models.View;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace MTG.Scores.Controllers
{
  public class HomeController : Controller
  {
    private MtgContext db = new MtgContext();

    public ActionResult Index()
    {
      var players = db.Players.ToList();

      var rank = new List<RankingRecord>();

      foreach (var player in players)
      {
        var games = player.HomeMatches.Count + player.AwayMatches.Count;

        var homeWins = player.HomeMatches.Count(x => x.Player1Score == 2 && x.Player1Score > x.Player2Score);
        var awayWins = player.AwayMatches.Count(x => x.Player2Score == 2 && x.Player2Score > x.Player1Score);
        var wonMatches = homeWins + awayWins;
        var lostMatches = games - wonMatches;

        var wonHomePoints = player.HomeMatches.Sum(x => GetMatchPoints(x.Player1Score, x.Player2Score));
        var wonAwayPoints = player.AwayMatches.Sum(x => GetMatchPoints(x.Player2Score, x.Player1Score));
        var wonPoints = wonHomePoints + wonAwayPoints;

        var wonSets = player.HomeMatches.Sum(x => x.Player1Score) + player.AwayMatches.Sum(x => x.Player2Score);
        var lostSets = player.HomeMatches.Sum(x => x.Player2Score) + player.AwayMatches.Sum(x => x.Player1Score);

        rank.Add(
          new RankingRecord {
            Name = player.Name,
            WonPoints = wonPoints,
            WonSets = wonSets,
            LostSets = lostSets,
            Matches = games,
            WonMatches = wonMatches,
            LostMatches = lostMatches,
          });
      }

      rank = rank.OrderByDescending(x => x.WonPoints)
                 .ThenByDescending(x => x.WonSets)
                 .ThenBy(x => x.Name)
                 .ToList();

      return View(rank);
    }

    private static int GetMatchPoints(int wonSets, int lostSets)
    {
      if (wonSets == 2)
      {
        return lostSets == 0 ? 3 : 2;
      }

      return wonSets;
    }
  }
}