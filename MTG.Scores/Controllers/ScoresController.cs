using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MTG.Scores.DataAccess;
using MTG.Scores.Models;
using System.Collections.Generic;

namespace MTG.Scores.Controllers
{
  public class ScoresController : Controller
  {
    private MtgContext db = new MtgContext();

    // GET: Matches
    public ActionResult Index()
    {
      ViewBag.Players = CreatePlayerSelectListItems();

      return View();
    }

    public ActionResult PopulateScores(int? playerId = null)
    {
      var matches = db.Matches.Include(m => m.Player1).Include(m => m.Player2);

      if (playerId == null)
      {
        return PartialView("ScoresTable", matches);
      }

      var scoresFiltered = new Models.View.ScoresFiltered() { SelectedPlayerId = playerId.Value };

      scoresFiltered.Scores =
        matches
        .Where(x => x.Player1ID == playerId || x.Player2ID == playerId)
        .Select(y => new Models.View.Score { Match = y})
        .ToList();

      foreach (var score in scoresFiltered.Scores)
      {
        score.Winner = IsWinner(score.Match, playerId.Value);
      }

      return PartialView("ScoresTableFiltered", scoresFiltered);
    }

    // GET: Matches/Create
    public ActionResult Create()
    {
      ViewBag.Player1ID = new SelectList(db.Players, "ID", "Name");
      ViewBag.Player2ID = new SelectList(db.Players, "ID", "Name");
      return View();
    }

    // POST: Matches/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "ID,Player1ID,Player2ID,Player1Score,Player2Score")] Match match)
    {
      if (ModelState.IsValid)
      {
        db.Matches.Add(match);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      ViewBag.Player1ID = new SelectList(db.Players, "ID", "Name", match.Player1ID);
      ViewBag.Player2ID = new SelectList(db.Players, "ID", "Name", match.Player2ID);
      return View(match);
    }

    // GET: Matches/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Match match = db.Matches.Find(id);
      if (match == null)
      {
        return HttpNotFound();
      }
      ViewBag.Player1ID = new SelectList(db.Players, "ID", "Name", match.Player1ID);
      ViewBag.Player2ID = new SelectList(db.Players, "ID", "Name", match.Player2ID);
      return View(match);
    }

    // POST: Matches/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "ID,Player1ID,Player2ID,Player1Score,Player2Score")] Match match)
    {
      if (ModelState.IsValid)
      {
        db.Entry(match).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      ViewBag.Player1ID = new SelectList(db.Players, "ID", "Name", match.Player1ID);
      ViewBag.Player2ID = new SelectList(db.Players, "ID", "Name", match.Player2ID);
      return View(match);
    }

    // GET: Matches/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Match match = db.Matches.Find(id);
      if (match == null)
      {
        return HttpNotFound();
      }
      return View(match);
    }

    // POST: Matches/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Match match = db.Matches.Find(id);
      db.Matches.Remove(match);
      db.SaveChanges();
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    private bool IsWinner(Match y, int playerId)
    {
      if (y.Player1Score == 2 && playerId == y.Player1ID
        || y.Player2Score == 2 && playerId == y.Player2ID)
      {
        return true;
      }

      return false;
    }

    private dynamic CreatePlayerSelectListItems()
    {
      var playersFilter = new List<SelectListItem>();
      playersFilter.Add(new SelectListItem { Text = "Wszyscy gracze ", Value = null });
      playersFilter.AddRange(db.Players.Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() }));

      return playersFilter;
    }
  }
}
