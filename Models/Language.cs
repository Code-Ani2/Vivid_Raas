using System;
using System.Collections.Generic;

namespace VividRasV2.Models
{
    public partial class Language
    {
        public Language()
        {
            Movies = new HashSet<Movie>();
            TelevisionDramas = new HashSet<TelevisionDrama>();
            WebSeries = new HashSet<WebSeries>();
        }

        public int LangId { get; set; }
        public string LangName { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<TelevisionDrama> TelevisionDramas { get; set; }
        public virtual ICollection<WebSeries> WebSeries { get; set; }
    }
}
