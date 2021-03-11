using System;
using System.Collections.Generic;
using System.Text;

namespace AprendendoEntityFramework.MusicContext.Models
{
    public class Music
    {
        public long MusicId { get; set; }
        public string  Name { get; set; }

        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
    }
}
