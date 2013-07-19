using GameRocket;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameRocketGuide.Controllers
{
    public class Constants
    {
        public static GameRocketGateway Gateway = new GameRocketGateway
        {
            Environment = GameRocket.Environment.PRODUCTION,
            Apikey = "use_your_apikey",
            Secretkey = "use_your_secretkey"
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

        [HttpGet]
        public ActionResult RunAction()
        {
            ActionRequest request = new ActionRequest
            {
                Player = "<use_player_id>",
                Parameters = new Dictionary<string, string>()
                    {
                        {"name", Request.QueryString["name"]}
                    }
            };

            Result<Dictionary<string, object>> result = Constants.Gateway.Action.Run("hello-world", request);

            if (result.IsSuccess())
            {
                Dictionary<string, object> data = (Dictionary<string, object>)result.Target["data"];
                ViewData["Hello"] = data["hello"];
            }
            else
            {
                ViewData["Message"] = result.Error;
            }

            return View();
        }

        [HttpGet]
        public ActionResult UnlockContent()
        {
            PurchaseRequest request = new PurchaseRequest
            {
                Player = "<use_player_id>"
            };

            Result<Dictionary<string, object>> result = Constants.Gateway.Purchase.Buy("unlock-content", request);

            if (result.IsSuccess())
            {
                Dictionary<string, object> data = (Dictionary<string, object>)result.Target["data"];
                ViewData["Success"] = data["message"];
            }
            else
            {
                ViewData["Message"] = result.Error;
            }

            return View();
        }
    }
}
