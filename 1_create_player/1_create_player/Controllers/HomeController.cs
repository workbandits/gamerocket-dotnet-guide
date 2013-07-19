using GameRocket;
using System.Web.Mvc;

namespace GameRocketGuide.Controllers
{
    public class Constants
    {
        public static GameRocketGateway Gateway = new GameRocketGateway
        {
            Environment = GameRocket.Environment.PRODUCTION,
            Apikey = "your_apikey",
            Secretkey = "your_secretkey"
        };
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {    
            return View();
        }

        [HttpPost]
        public ActionResult CreatePlayer(FormCollection collection)
        {
            PlayerRequest request = new PlayerRequest
            {
                Name = collection["name"],
                Locale = collection["locale"]
            };

            Result<Player> result = Constants.Gateway.Player.Create(request);

            if (result.IsSuccess())
            {
                Player player = result.Target;
                ViewData["PlayerId"] = player.Id;
            }
            else
            {
                ViewData["Message"] = result.Error;
            }

            return View();
        }
    }
}
