using System;
using System.Collections.Generic;

namespace VividRasV2.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Movies = new HashSet<Movie>();
            TelevisionDramas = new HashSet<TelevisionDrama>();
            WebSeries = new HashSet<WebSeries>();
        }

        public int GenreId { get; set; }
        public string GenreName { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<TelevisionDrama> TelevisionDramas { get; set; }
        public virtual ICollection<WebSeries> WebSeries { get; set; }
    }
}
