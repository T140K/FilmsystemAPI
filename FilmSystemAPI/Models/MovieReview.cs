using System;
using System.Collections.Generic;

namespace FilmSystemAPI.Models
{
    public partial class MovieReview
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Review { get; set; }
        public int ReviewPerson { get; set; }
        public int Movie { get; set; }
        
        public virtual Movie MovieNavigation { get; set; } = null!;
        public virtual Person ReviewPersonNavigation { get; set; } = null!;
    }
}
