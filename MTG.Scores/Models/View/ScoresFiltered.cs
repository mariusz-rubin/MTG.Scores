using System.Collections.Generic;

namespace MTG.Scores.Models.View
{
  public class ScoresFiltered
  {
    public int SelectedPlayerId { get; set; }

    public IEnumerable<Score> Scores{ get; set; }
  }
}