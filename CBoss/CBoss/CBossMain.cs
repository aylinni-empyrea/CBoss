using System;
using System.IO;
using System.Collections.Generic;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.DB;
using System.Reflection;

namespace CBoss
{
    [ApiVersion(2, 1)]
    public class CBoss : TerrariaPlugin
    {
        #region TerrariaPlugin

        public override Version Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version; }
        }

        public override string Author
        {
            get { return "Creamed"; }
        }

        public override string Description
        {
            get { return "Region npc spawner"; }
        }

        public override string Name
        {
            get { return "CBoss"; }
        }

        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
            }
            base.Dispose(disposing);
        }

        public CBoss(Main game)
            : base(game)
        {
        }

        #endregion

        #region OnInitialize

        private static void OnInitialize(EventArgs args)
        {
            Commands.ChatCommands.Add(new Command("cboss.root", CBossCommands.CBossCommand, "cboss")
            {
                HelpText =
                    "Toggles automatic boss spawns; Reloads the configuration; Lists bosses and minions spawned by the plugin"
            });
        }

        #endregion
    }
}