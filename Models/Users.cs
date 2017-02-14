using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
namespace theWall.Models
{
    public abstract class BaseEntity {}
 public class User : BaseEntity
 {
    //  public User()
    //  {
    //      quotes = new List<Quote>();
    //  }
  [Key]
  public int id { get; set; }
  [Required]
  [MinLength(2)]
  [RegularExpression("^[a-zA-z]+$")]
  public string f_name { get; set; }
  [Required]
  [MinLength(2)]
  [RegularExpression("^[a-zA-z]+$")]
  public string l_name { get; set; }
  [Required]
  [EmailAddress]
  public string email { get; set; }
  [Required]
  [MinLength(8)]
  public string password{ get; set; }
  [Required]
  [MinLength(8)]
  [Compare("password",ErrorMessage="password do not match")]
  public string cpassword{ get; set; }
  public DateTime Created_at { get;set; }
  public DateTime Updated_at { get;set; }
//   public ICollection<Quote> quotes { get; set; }
 }
}