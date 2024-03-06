using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantLibrary.DTOs
{
    public class FeedbackResponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Comments { get;set; }
        public DateTime CreatedDate { get; set; }
    }
}
