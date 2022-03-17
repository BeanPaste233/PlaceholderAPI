using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TerrariaApi.Server;
using TShockAPI;

namespace PlaceholderAPI
{
    [ApiVersion(2,1)]
    public class PlaceholderAPI : TerrariaPlugin
    {
        public PlaceholderAPI(Terraria.Main game) : base(game){ }
        public override string Name => "PlaceholderAPI";
        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public override string Author => "豆沙";
        public override string Description => "一款通用占位符插件";
        public static PlaceholderAPI Instance { get { return instance; } }
        private static PlaceholderAPI instance;
        public PlaceholderManager placeholderManager=new PlaceholderManager();
        public override void Initialize()
        {
            instance = this;
            placeholderManager.Register("{player}");
            Hooks.PreGetText += OnGetText;
            Commands.ChatCommands.Add(new Command("test",test,"ptest"));
        }

        private void test(CommandArgs args)
        {
            string text = "{player} 's placeholderAPI is testing.";
            TShock.Log.ConsoleInfo(placeholderManager.GetText(text,args.Player));
        }

        private void OnGetText(Hooks.GetTextArgs args)
        {
            args.List["{player}"] = "BeanPasteTest";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Hooks.PreGetText -= OnGetText;
            }
            base.Dispose(disposing);
        }
    }
}
