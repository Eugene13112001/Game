using Microsoft.AspNetCore.Mvc;
using WebApplication34.Models;
using Newtonsoft.Json;
using System.Text.Json;
namespace WebApplication34.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        

        private readonly ILogger<WeatherForecastController> _logger;
        ApplicationContext context;
        public WeatherForecastController(ILogger<WeatherForecastController> logger , ApplicationContext context)
        {
            this.context = context;

            _logger = logger;
        }

        
        [HttpPost, ActionName("AddGame")]
        public bool AddGame(string Name , string Creator, string[] View)
        {
            try
            {
                this.context.AddGame(Name, Creator, View);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        
        [HttpPost, ActionName("AddView")]
        public bool AddView(string Name)
        {
            try
            {
                this.context.AddView(Name);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpDelete, ActionName("DeleteView")]
        public bool DeleteView(int id)
        {
            try
            {
                this.context.DeleteView(id);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpDelete, ActionName("DeleteGame")]
        public bool DeleteGame(int id)
        {
            try
            {
                this.context.DeleteGame(id);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPut, ActionName("ChangeGame")]
        public bool ChangeGame(int id , string Name, string Creator, string[] View)
        {
            try
            {
                this.context.ChangeGame(id, Name, Creator, View);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPut, ActionName("ChangeView")]
        public bool ChangeView(int id, string Name)
        {
            try
            {
                this.context.ChangeView(id, Name);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpGet, ActionName("GetGameAll")]
        public List<GameView> GetGameAll()
        {
            return this.context.GetAllGames();
          
        }
        [HttpGet, ActionName("GetGameBy")]
        public string GetGameBy(string name)
        {

            GameView[] a = this.context.GetGames(name).ToArray();
            string h = JsonConvert.SerializeObject(a, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return h;

        }
        [HttpGet, ActionName("GetViews")]
        public List<ViewModel> GetViews()
        {
            
            return this.context.GetAllViews();
        }

    }
}