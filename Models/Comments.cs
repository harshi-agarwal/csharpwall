using System.ComponentModel.DataAnnotations;
using System;
namespace theWall.Models
{
   
 public class Comment : BaseEntity
 {
     
  [Key]
  public int id { get; set; }
  public int user_id{get;set;}
  public int message_id{get;set;}
  
  public Message message{get;set;}
  [Required]
  [MinLength(2)]
  public string context { get; set; }
  public DateTime created_at {get;set;}
  public DateTime updated_at {get;set;}
  public User  user{get;set;}
   }
}