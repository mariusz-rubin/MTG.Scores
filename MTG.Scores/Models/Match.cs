using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTG.Scores.Models
{
  public class Match
  {
    public int ID { get; set; }

    public int? Player1ID { get; set; }

    public int? Player2ID { get; set; }

    [DisplayName("Wynik gracza 1")]
    [Range(0, 2)]
    public int Player1Score { get; set; }

    [DisplayName("Wynik gracza 2")]
    [Range(0, 2)]
    public int Player2Score { get; set; }

    public virtual Player Player1 { get; set; }

    public virtual Player Player2 { get; set; }
  }
}