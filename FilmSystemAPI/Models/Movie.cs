using System;
using System.Collections.Generic;

namespace FilmSystemAPI.Models
{
    public partial class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Link { get; set; } = null!;
        public int MovieGenre { get; set; }
        public int Uploader { get; set; }

        public virtual Genres MovieGenreNavigation { get; set; } = null!;
        public virtual Person UploaderNavigation { get; set; } = null!;
    }
}
