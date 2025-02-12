using System;
using System.Collections.Generic;

namespace VividRasV2.Models
{
    public partial class Movie
    {
        public int MovieId { get; set; }
        public int LangId { get; set; }
        public int GenreId { get; set; }
        public int ContentTypeId { get; set; }
        public string MovieName { get; set; } = null!;
        public string ReleaseYear { get; set; } = null!;
        public string? Duration { get; set; }

        public virtual ContentType ContentType { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
        public virtual Language Lang { get; set; } = null!;
    }
}



