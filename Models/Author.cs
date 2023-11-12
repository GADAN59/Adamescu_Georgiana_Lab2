using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
//using System.Security.Policy;

namespace Adamescu_Georgiana_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Book>? Books { get; set; } //navigation property

        [Display(Name = "Author")]
        public string FullName => $"{FirstName} {LastName}";
    }
}
