using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantLibrary.DTO
{
    public class CustomerFeedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public byte Rating { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedDate { get; set; }
        public int RestaurantId { get; set; }
        public int? FeedbackResponseId { get; set; }

        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }
        [ForeignKey("FeedbackResponseId")]
        public virtual FeedbackResponse FeedbackResponse { get; set; }
    }
}
