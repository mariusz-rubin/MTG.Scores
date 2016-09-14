using MTG.Scores.DataAccess;
using MTG.Scores.Models.View;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;

namespace MTG.Scores.Controllers
{
  public class HomeController : Controller
  {
    private MtgContext db = new MtgContext();

    public ActionResult Index()
    {
      var matches = db.Matches.Include(m => m.Player1).Include(m => m.Player2).ToList();
      var players = db.Players.ToList();

      var rank = new List<RankingRecord>();

      foreach (var player in players)
      {
        var games = matches.Where(x => x.Player1ID == player.ID || x.Player2ID == player.ID);
        var wins = 
          games.Where(
            x =>
              x.Player1ID == player.ID
              ? x.Player1Score == 2 && x.Player1Score > x.Player2Score
              : x.Player2Score == 2 && x.Player2Score > x.Player1Score
          );
        rank.Add(new RankingRecord { Matches = games.Count(), Wins = wins.Count(), Name = player.Name } );
      }

      rank = rank.OrderByDescending(x => x.Wins).ToList();

      return View(rank);
    }
  }
}