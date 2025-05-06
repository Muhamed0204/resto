using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        public string Название { get; set; }

        [Required(ErrorMessage = "Адрес обязателен")]
        public string Адрес { get; set; }

        [Phone(ErrorMessage = "Неверный формат телефона")]
        public string Телефон { get; set; }

        public string ЧасыРаботы { get; set; }
        public ICollection<Table>? Tables { get; set; } = new List<Table>();
    }
}
