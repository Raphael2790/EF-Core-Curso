using System.Collections.Generic;

namespace AprendendoEntityFramework.MusicContext.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        public int Ano { get; set; }
        public string Nome { get; set; }
        public string Link { get; set; }

        public virtual ICollection<Music> Musics { get; set; }
    }
}