using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Models
{
    public sealed class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
