using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Canoe.Models;

namespace Canoe.Data
{
    public class MvcCanoeContext : DbContext
    {
        public MvcCanoeContext(DbContextOptions<MvcCanoeContext> options)
            : base(options)
        {
        }
        
        public DbSet<PlayerCollection> GetPlayerCollection { get; set; }
        public DbSet<Player> GetPlayerList { get; set; }
        public DbSet<Cards> GetSetCardList { get; set; }
        public DbSet<Cards> GetCardList { get; set; }
        public DbSet<CardView> GetCardViews { get; set; }
        public DbSet<Classes> classes { get; set; }
        public DbSet<Colors> colors { get; set; }
        public DbSet<MatchTypes> matchtypes { get; set; }
        public DbSet<Rarity> rarity { get; set; }
        public DbSet<Sets> sets { get; set; }
        public DbSet<Tribes> tribes { get; set; }
        public DbSet<DeckFormats> deckformats { get; set; }
        


    }
}

