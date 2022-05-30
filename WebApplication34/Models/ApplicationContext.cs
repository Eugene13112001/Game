
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
namespace WebApplication34.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Zapis> Zapis { get; set; }
        public DbSet<View> View { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            if (!this.Games.Any<Game>())
            {
                this.AddView("RPG");
                this.AddView("Strategy");
                this.AddView("Shooting");
                this.AddGame("Crysis", "Studio1", new string[] { "Shooting" });
                this.AddGame("Witcher", "Studio2", new string[] { "RPG", "Strategy" });
                this.AddGame("Noire", "Studio3", new string[] { "RPG", "Shooting" });
            }
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
      
        public List<GameView> GetGames(string view)
        {
            View v = this.FindView(view);
            List<Zapis> list = this.Zapis.Where(o => o.ViewId == v.Id).ToList<Zapis>();
            List<GameView> games = new List<GameView>();
            foreach (Zapis z in list)
            {
                Game game = this.FindGameById(z.GameId);
                games.Add(new GameView { Id = game.Id, Name = game.Name, Creator = game.Creator});
            }
            var users = games.Join(this.Zapis,
        u => u.Id,
        c => c.GameId,
        (u, c) => new
        {
            Id = u.Id,
            Name = u.Name,
            Creator = u.Creator,
            ViewId = c.ViewId
        }); ;
            var list2 = users.Join(this.View,
        u => u.ViewId,
        c => c.Id,
        (u, c) => new
        {
            Id = u.Id,
            Name = u.Name,
            Creator = u.Creator,
            View = c.Name
        }); ;
            var results = from p in list2
                          group p.View by p.Id into g
                          select new { Id = g.Key, Views = g.ToList() };
            var final =
    from person in games
    join pet in results on person.Id equals pet.Id into gj
    from subpet in gj.DefaultIfEmpty()
    select new
    {
        Id = person.Id,
        Name = person.Name,
        Creator = person.Creator,
        View = subpet.Views
    };
            List<GameView> viewsgames = new List<GameView>();
            foreach (var i in final)
                viewsgames.Add(new GameView { Id = i.Id, Name = i.Name, Creator = i.Creator, Views = i.View.ToArray() });
            return new HashSet<GameView>(viewsgames).ToList();
          

        }
        public List<GameView> GetAllGames()
        {
                var users = this.Games.Join(this.Zapis,
           u => u.Id, 
           c => c.GameId, 
           (u, c) => new 
           {
               Id = u.Id,
               Name = u.Name,
               Creator = u.Creator,
               ViewId = c.ViewId
           });;

            var games = users.Join(this.View, 
          u => u.ViewId, 
          c => c.Id, 
          (u, c) => new 
          {
              Id = u.Id,
              Name = u.Name,
              Creator = u.Creator,
              View = c.Name
          }); ;
            var results = from p in games
                          group p.View by p.Id into g
                          select new { Id = g.Key, Views = g.ToList() };
            var final =
     from person in this.Games
     join pet in results on person.Id equals pet.Id into gj
     from subpet in gj.DefaultIfEmpty()
     select new
     {
         Id = person.Id,
         Name = person.Name,
         Creator = person.Creator,
         View = subpet.Views
     };
            List<GameView> viewsgames = new List<GameView>();
            foreach (var i in final)
                viewsgames.Add(new GameView { Id = i.Id, Name = i.Name, Creator = i.Creator, Views = i.View.ToArray() });
            return  new HashSet<GameView>(viewsgames).ToList();
        }
        public List<ViewModel> GetAllViews()
        {
            List<ViewModel> a = new List<ViewModel>();
            foreach (var view in this.View.ToList<View>())
                a.Add(new ViewModel { Id = view.Id, Name = view.Name });
            return a;
            
        }
        public void AddGame(string name, string creator , string[]  id)
        {
            Game game = new Game { Name = name, Creator = creator };
            this.Games.Add(game);
            this.SaveChanges();
            
            foreach (string i in id)          
                this.Zapis.Add(new Zapis { GameId = game.Id, ViewId = this.FindView(i).Id });
            this.SaveChanges();



        }
        public void AddView(string name)
        {
            this.View.Add(new View { Name = name });
            this.SaveChanges();

        }
        public void ChangeView(int id, string name)
        {
            View view = this.View
            .Where(o => o.Id == id)
            .FirstOrDefault();
            view.Name = name;
            this.SaveChanges();

        }
        public View FindViewById(int id)
        {
            return this.View.Where(o => o.Id == id).FirstOrDefault();
        }
        public View FindView(string name)
        {
            return this.View.Where(o => o.Name == name).FirstOrDefault();
        }
        public Game FindGame(string name)
        {
            return this.Games.Where(o => o.Name == name).FirstOrDefault();
        }
        public Game FindGameById(int id)
        {
            return this.Games.Where(o => o.Id == id).FirstOrDefault();
        }
        public void DeleteGameByName(string name)
        {
            Game game = this.FindGame(name);
            this.DeleteZapisAll(game);
            this.Games.Remove(game);
            this.SaveChanges();
        }
        public void DeleteGame(int id)
        {
            Game game = this.FindGameById(id);
            this.DeleteZapisAll(game);
            this.Games.Remove(game);
            this.SaveChanges();
        }
        public void DeleteViewByName(string name)
        {
            this.View.Remove(this.FindView(name));
            this.SaveChanges();
        }
        public void DeleteView(int id)
        {
            this.View.Remove(this.FindViewById(id));
            this.SaveChanges();
        }
        public void DeleteZapisAll(Game game)
        {
            List<Zapis> list = this.Zapis.Where(o => o.GameId == game.Id).ToList<Zapis>();
            foreach (Zapis zap in list)
                this.Zapis.Remove(zap);
            this.SaveChanges();
        }
      
        public void ChangeGame(int id , string name, string creator, string[]  vievs)
        {
            Game game = this.FindGameById(id);
            this.DeleteZapisAll(game);
            game.Name = name;
            game.Creator = creator;
            foreach (string i in vievs)
                this.Zapis.Add(new Zapis { GameId = game.Id, ViewId = this.FindView(i).Id });
            this.SaveChanges();

        }
    }
}
