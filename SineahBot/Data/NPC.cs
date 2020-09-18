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
        public bool elite;

        public Shop shop { get; private set; }
        public NPC RegisterShop(Shop shop)
        {
            this.shop = shop;
            shop.RegisterOwner(this);
            return this;
        }
        public override string GetShortDescription(IAgent agent = null)
        {
            return shortDescription;
        }

        public override string GetFullDescription(IAgent agent = null)
        {
            return $"*{longDescription}* \n> {GetStateDescription(agent)}";
        }

        public override void OnDamage(int damageAmount, Entity source = null)
        {
            base.OnDamage(damageAmount, source);
            if (!IsDead())
            {
                new MudTimer(1, () =>
                {
                    if (!IsDead())
                    {
                        CommandManager.ParseInCharacterMessage(this, $"atk {source.GetName()}", RoomManager.GetRoom(currentRoomId));
                    }
                });
            }
        }

        public override void OnKilled(Entity killer = null)
        {
            if (shop != null) shop.CloseShop();
            base.OnKilled(killer);
            new MudTimer(30, () =>
            {
                health = maxHealth;
                mana = maxMana;
                var room = RoomManager.GetRoom(idSpawnRoom);
                RoomManager.MoveToRoom(this, room);
            });
        }

        public override int GetWeaponDamage()
        {
            return level * (elite ? 3 : 2);
        }

        public override int GetSpellPower()
        {
            return level * (elite ? 3 : 2);
        }


        public NPC Clone()
        {
            return new NPC()
            {
                id = Guid.NewGuid(),
                name = name,
                shortDescription = shortDescription,
                longDescription = longDescription,
                elite = elite,
                level = level,
                experience = experience,
                gold = gold,
                maxHealth = maxHealth,
                health = health,
                maxMana = maxMana,
                mana = mana,
                spells = spells,
                alternativeNames = alternativeNames,
                characterClass = characterClass,
                characterStatus = characterStatus,
                agent = agent
            };
        }
    }
}
