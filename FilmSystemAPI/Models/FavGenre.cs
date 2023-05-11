using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FilmSystemAPI.Models
{
    public partial class FavGenre
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public int Genre { get; set; }
        public virtual Genres GenreNavigation { get; set; } = null!;
        public virtual Person PersonNavigation { get; set; } = null!;
    }
}
