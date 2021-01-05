using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebSigurnost.Models
{
    public class KomentarContext : DbContext
    {
        public KomentarContext(DbContextOptions<KomentarContext> opcije) : base(opcije)
        {

        }
        public DbSet<Komentar> Komentari { get; set; }
    }
}
