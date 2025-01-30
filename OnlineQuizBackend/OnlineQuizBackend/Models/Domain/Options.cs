using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuizBackend.Models.Domain
{
    public class Options
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OptionId { get; set; }
        public string OptionText { get; set; }

        public int QuestionId { get; set; }
        public Questions Question { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
