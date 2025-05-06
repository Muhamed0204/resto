using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Table
    {
        public int Id { get; set; }

        public int? RestaurantId { get; set; } // Внешний ключ — необязательный

        [Display(Name = "Table Number")]
        public int? Number { get; set; }

        [Range(1, 20)]
        [Display(Name = "Seats")]
        public int? Seats { get; set; }

        [StringLength(100)]
        public string? Location { get; set; }

        public string? Status { get; set; } // Free, Reserved, Occupied

        // Навигационное свойство — необязательное, но с new()
        public Restaurant? Restaurant { get; set; } = null;
    }
}
