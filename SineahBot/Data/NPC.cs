using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SineahBot.Data
{
    public class NPC : Character
    {
        public string shortDescription { get; set; }
        public string longDescription { get; set; }
        public string[] alternativeNames { get; set; }

        public override string GetShortDescription(IAgent agent = null)
        {
            return shortDescription;
        }

        public override string GetFullDescription(IAgent agent = null)
        {
            return longDescription;
        }

        public override void OnKilled(IAgent agent = null)
        {
            base.OnKilled(agent);
            if (agent != null)
            {
                int rewardExp = this.experience + ClassProgressionManager.ExperienceForNextLevel(this.level) / 10;
                if (agent is Player)
                {
                    var player = agent as Player;
                    player.character.experience += (rewardExp * this.level) / player.character.level;
                    player.character.gold += this.gold;
                }
                if (agent is Character)
                {
                    var character = agent as Character;
                    character.experience += (rewardExp * this.level) / character.level;
                    character.gold += this.gold;
                }
            }
        }
    }
}
