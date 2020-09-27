using Microsoft.EntityFrameworkCore.Query;
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
        public string knowledgeDefaultResponse = "I don't know anything about that.";
        public Dictionary<string, string> knowledgeBase = new Dictionary<string, string>();
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
        public NPC SetKnowladgeDefaultResponse(string defaultResponse)
        {
            return this;
        }
        public NPC RegisterKnowlede(IEnumerable<string> knowledges, string response)
        {
            foreach (var knowledge in knowledges)
            {
                knowledgeBase[knowledge.ToLower()] = response;
                if (knowledge.Contains(" "))
                    knowledgeBase[knowledge.ToLower().Replace(" ", "")] = response;
            }
            return this;
        }
        public NPC RegisterKnowlede(string knowledge, string response)
        {
            knowledgeBase[knowledge.ToLower()] = response;
            if (knowledge.Contains(" "))
                knowledgeBase[knowledge.ToLower().Replace(" ", "")] = response;
            return this;
        }
        public string GetKnowledgeResponse(string knowledge)
        {
            knowledge = knowledge.ToLower().Replace(" ", "");
            if (!knowledgeBase.ContainsKey(knowledge))
            {
                if (knowledgeDefaultResponse == null) return null;
                return $"**{GetName()}**: \"*{knowledgeDefaultResponse}*\"";
            }
            return $"**{GetName()}** - *{knowledge}*\n{knowledgeBase[knowledge]}";
        }

        public override string GetFullDescription(IAgent agent = null)
        {
            return $"*{longDescription}*" +
            $"\n> {GetStateDescription(agent)}" +
            $"\n> {GetPowerDescription(agent as Character)} " +
            $"\n> {GetAlterationDescription(agent as Character)} ";
        }

        public override string GetPowerDescription(Character character)
        {
            if (character == null) return "";
            var ratio = (character.level * 10000) / (level * 100 * (elite ? 2 : 1));
            if (ratio < 45) return "> They will be an impossible fight for you alone.";
            if (ratio < 90) return "> This would be a tough fight.";
            if (ratio > 110) return "> This should be an easy fight.";
            if (ratio > 155) return "> You're going to beat them to death.";
            return $"> You seem evenly matched.";
        }

        public override void DamageHealth(int damageAmount, INamed source = null)
        {
            base.DamageHealth(damageAmount, source);
            if (!IsDead() && source != null)
            {
                new MudTimer(1, () =>
                {
                    if (!IsDead() && source != null)
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
            var respawnTime = elite ? 300 : 60;
            new MudTimer(respawnTime, () =>
            {
                health = baseHealth;
                mana = baseMana;
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
                baseHealth = baseHealth,
                health = health,
                baseMana = baseMana,
                mana = mana,
                spells = spells,
                alternativeNames = alternativeNames,
                characterClass = characterClass,
                characterStatus = characterStatus,
                agent = agent,
                tags = new List<CharacterTag>(tags),
                knowledgeBase = new Dictionary<string, string>(knowledgeBase),
                knowledgeDefaultResponse = knowledgeDefaultResponse
            };
        }
        public NPC Equipment(Equipment equipment)
        {
            base.Equip(equipment);
            return this;
        }
    }
}
