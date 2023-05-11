using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FilmSystemAPI.Models
{
    public partial class Genres
    {
        public Genres()
        {
            Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
