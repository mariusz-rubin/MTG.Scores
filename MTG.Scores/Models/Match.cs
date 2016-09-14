using MTG.Scores.Infrastructure.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace MTG.Scores.Models
{
  public class Match : IValidatableObject
  {
    public int ID { get; set; }

    public int Player1ID { get; set; }

    [NotEqualTo("Player1ID", ErrorMessage = "Gracz 1 nie może być taki sam jak gracz 2")]
    public int Player2ID { get; set; }

    [DisplayName("Wynik gracza 1")]
    [Range(0, 2)]
    public int Player1Score { get; set; }

    [DisplayName("Wynik gracza 2")]
    [Range(0, 2)]
    public int Player2Score { get; set; }

    [InverseProperty("HomeMatches")]
    public virtual Player Player1 { get; set; }

    [InverseProperty("AwayMatches")]
    public virtual Player Player2 { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if((Player1Score != 2 && Player2Score != 2) || (Player1Score == Player2Score))
      {
        yield return new ValidationResult("Niepoprawny wynik");
      } 
    }
  }
}