using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FilmSystemAPI.Models
{
    public partial class Person
    {
        public Person()
        {
            Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
