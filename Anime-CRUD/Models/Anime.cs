using System;
using System.Collections.Generic;

namespace Anime_CRUD.Models
{
    public partial class Anime
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int Capitulos { get; set; }
        public string Autor { get; set; } = null!;
    }
}
