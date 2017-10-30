using Oxide.Core.Libraries.Covalence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oxide.Plugins
{
    [Info("Refresh Animals", "moochannel", "1.0.0")]
    [Description("Kill animals to respawn frequently")]

    class RefreshAnimals : CovalencePlugin
    {
        #region Configuration

        bool PurgeOnLogin => GetConfig("PurgeOnLogin", true);

        protected override void LoadDefaultConfig()
        {
            Config["PurgeOnLogin"] = PurgeOnLogin;

            SaveConfig();
        }

        #endregion

        #region Localization

        protected override void LoadDefaultMessages()
        {
            lang.RegisterMessages(new Dictionary<string, string>
            {
                {"AdminOnly", "Auth level 2 required."},
                {"AdminAndModerator", "Auth level 1 or greater required."},
            }, this);
            lang.RegisterMessages(new Dictionary<string, string>
            {
                {"AdminOnly", "管理者権限が必要です"},
                {"AdminOrModerator", "管理者かモデレータ権限が必要です"},
           }, this, "ja");
        }

        #endregion

        #region Initialization

        void Loaded()
        {
#if !RUST
            throw new NotSupportedException($"This plugin does not support {(covalence.Game ?? "this game")}");
#endif

            LoadDefaultConfig();
            LoadDefaultMessages();
        }

        #endregion

        #region Refresh Animals On Logging in

        void OnPlayerInit(BasePlayer player)
        {
            if (PurgeOnLogin)
                PurgeAnimals();
        }

        #endregion

        #region Console Commands

        [Command("refreshanimals.list")]
        void AnimalsListCommand(IPlayer player, string command, string[] args)
        {
            if (args.Length != 0)
            {
                Puts("usage: refreshanimals.list");
                return;
            }
            if (!player.IsAdmin)
            {
                Puts(GetMessage("AdminOnly", null));
                return;
            }

            var animals = GetAllAnimals();
            Puts($"There are {animals.Count} animals.");
            foreach (var animal in animals)
            {
                Puts($"{animal.GetType()}: stopped?={animal.IsStopped}, nav_running?={animal.IsNavRunning()}");
            }
        }

        [Command("refreshanimals.stats")]
        void AnimalsStatsCommand(IPlayer player, string command, string[] args)
        {
            if (args.Length != 0)
            {
                Puts("usage: refreshanimals.stats");
                return;
            }
            if (!player.IsAdmin)
            {
                Puts(GetMessage("AdminOnly", null));
                return;
            }

            var animals = GetAllAnimals();
            var animalGroup = animals.Select(rec => new { AnimalType = rec.GetType() })
                                     .GroupBy(rec => rec.AnimalType )
                                     .Select(rec => new { AnimalType = rec.Key, Amount = rec.Count() })
                                     .OrderBy(rec => rec.AnimalType.ToString());
            Puts($"There are {animals.Count} animals.");
            foreach (var stat in animalGroup)
            {
                Puts($"{stat.AnimalType}: {stat.Amount}");
            }
        }

        [Command("refreshanimals.purge")]
        void AnimalsPurgeCommand(IPlayer player, string command, string[] args)
        {
            if (args.Length != 0)
            {
                Puts("usage: refreshanimals.purge");
                return;
            }
            if (!player.IsAdmin)
            {
                Puts(GetMessage("AdminOnly", null));
                return;
            }

            PurgeAnimals();
        }

        #endregion

        #region Animal Control Methods

        private List<BaseAnimalNPC> GetAllAnimals()
        {
            var animals = new List<BaseAnimalNPC>(BaseAnimalNPC.FindObjectsOfType<BaseAnimalNPC>());
            return animals;
        }

        private void PurgeAnimals()
        {
            var animals = GetAllAnimals();
            var purged = 0;
            var willPurgedAnimals = animals.Where(rec => rec.IsStopped && !rec.IsNavRunning());
            foreach (var animal in willPurgedAnimals)
            {
                animal.DieInstantly();
                purged++;
            }
            Puts($"{purged} animal(s) purged. (before: {animals.Count})");
        }

        #endregion

        #region Helper Methods

        private T GetConfig<T>(string name, T defaultValue)
        {
            if (Config[name] == null) return defaultValue;
            return (T)Convert.ChangeType(Config[name], typeof(T));
        }

        private string GetMessage(string key, string steamId = null) => lang.GetMessage(key, this, steamId);

        #endregion
    }
}
