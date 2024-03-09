using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantLibrary.DTO
{
    public class Restaurant
    {
        public Restaurant()
        {
            this.Feedbacks = new HashSet<CustomerFeedback>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<CustomerFeedback> Feedbacks { get; set; }
    }
}
