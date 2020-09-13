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

        public Guid idSpawnRoom { get; set; }

        public override string GetShortDescription(IAgent agent = null)
        {
            return shortDescription;
        }

        public override string GetFullDescription(IAgent agent = null)
        {
            return longDescription;
        }

        public override bool OnDamage(int damageAmount, INamed source = null)
        {
            var output = base.OnDamage(damageAmount, source);
            if (!output)
            {
                new MudTimer(1, () =>
                {
                    if (health > 0)
                    {
                        CommandManager.ParseInCharacterMessage(this, $"atk {source.GetName()}", RoomManager.GetRoom(currentRoomId));
                        Player.CommitPlayerMessageBuffers().Wait();
                    }
                });
            }
            return output;
        }

        public override void OnKilled(IAgent agent = null)
        {
            base.OnKilled(agent);
            new MudTimer(30, () =>
            {
                health = maxHealth;
                mana = maxMana;
                var room = RoomManager.GetRoom(idSpawnRoom);
                RoomManager.MoveToRoom(this, room);
                Player.CommitPlayerMessageBuffers().Wait();
            });
        }
    }
}
