using SineahBot.Data.Enums;
using SineahBot.Extensions;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SineahBot.Data
{
    public class NPC : Character
    {
        public Behaviour behaviour;

        public string npcName;
        public int npcStatus;
        public string shortDescription { get; set; }
        public string longDescription { get; set; }

        public string idSpawnRoom { get; set; }
        public bool elite;

        public Shop shop { get; private set; }
        public string knowledgeDefaultResponse = "I don't know anything about that.";
        public KnowledgeBase knowledgeBase = new KnowledgeBase();
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
        public NPC AddKnowledgeBase(KnowledgeBase knowledgeBase)
        {
            this.knowledgeBase.MergeKnowledge(knowledgeBase);
            return this;
        }
        public NPC RegisterKnowlede(IEnumerable<string> knowledges, string response)
        {
            knowledgeBase.SetKnowledge(knowledges, response);
            return this;
        }
        public NPC RegisterKnowlede(string knowledge, string response)
        {
            knowledgeBase.SetKnowledge(knowledge, response);
            return this;
        }
        public NPC CompileKnowlede()
        {
            knowledgeBase.CompileKnowledge();
            return this;
        }
        public NPC GenerateTraderKnowledge()
        {
            var selling = shop.GetBuyableEntries();
            var buying = shop.GetSellableEntries();
            if (selling.Count() == 0 && buying.Count() == 0)
            {
                return RegisterKnowlede(new string[] { "trade", "trades", "trading", "sell", "selling", "buy", "buying" }, "\"*I don't trade*\"");
            }
            var sellingMessage = $"I am selling {String.Join(", ", selling.Select(x => $"**{x.referenceItem.GetName()}**"))}.";
            var buyingMessage = $"I am buying {String.Join(", ", buying.Select(x => $"**{x.referenceItem.GetName()}**"))}.";
            if (selling.Count() > 0 && buying.Count() > 0)
            {
                RegisterKnowlede(new string[] { "trade", "trades", "trading" }, $"\"*{sellingMessage}\n{buyingMessage}*\"");
            }
            else
            {
                if (selling.Count() > 0)
                {
                    RegisterKnowlede(new string[] { "trade", "trades", "trading", "sell", "selling" }, $"\"*{sellingMessage}*\"");
                }
                else
                {
                    RegisterKnowlede(new string[] { "sell", "selling" }, $"\"*I don't sell anything.*\"");
                }
                if (buying.Count() > 0)
                {
                    RegisterKnowlede(new string[] { "trade", "trades", "trading", "buy", "buying" }, $"\"*{buyingMessage}*\"");
                }
                else
                {
                    RegisterKnowlede(new string[] { "buy", "buying" }, $"\"*I don't buy anything.*\"");
                }
            }
            return this;
        }
        public string GetKnowledgeResponse(string knowledge)
        {
            knowledge = knowledge.ToLower();
            if (knowledge.Is("rumor", "rumors"))
            {
                return $"\"**{BehaviourManager.GetRumorKnowledge(this)}**\"";
            }
            if (knowledgeBase.needCompile) // in case we forget to compile the knowledge, do it now
            {
                knowledgeBase.CompileKnowledge();
                Logging.Log($"Late knowledge compiling for NPC {name} ({npcName}).");
            }
            var answer = knowledgeBase.GetKnowledge(knowledge);
            if (answer != null)
            {
                return answer;
            }
            return knowledgeDefaultResponse;
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
            if (ratio < 45) return $"> {They} will be an impossible fight for you alone.";
            if (ratio < 90) return "> This would be a tough fight.";
            if (ratio > 110) return "> This should be an easy fight.";
            if (ratio > 155) return $"> You're going to beat {them} to death.";
            return $"> You seem evenly matched.";
        }

        public override void DamageHealth(int damageAmount, DamageType type, INamed source = null)
        {
            base.DamageHealth(damageAmount, type, source);
            if (!IsDead() && source != null && behaviour == null) // if alive, damage comes from somewhere, and no behaviour attached
            {
                new MudTimer(1, () =>
                {
                    if (!IsDead() && source is Entity e)
                    {
                        Commands.CommandCombatAttack.Attack(this, RoomManager.GetRoomById(currentRoomId), e);
                    }
                });
            }
        }

        public override void OnKilled(Entity killer = null)
        {
            BehaviourManager.SetActiveForNpc(this, false);
            if (shop != null) shop.CloseShop();
            base.OnKilled(killer);
            if (this.HasCharacterTag(CharacterTag.Summon))
            {
                // don't respawn a summoned unit and remove the behaviour
                BehaviourManager.RemoveNPC(this);
                return;
            }
            var respawnTime = elite ? 300 : 60;
            new MudTimer(respawnTime, () =>
            {
                health = baseHealth;
                mana = baseMana;
                var room = RoomManager.GetRoomById(idSpawnRoom);
                RoomManager.MoveToRoom(this, room);
                BehaviourManager.SetActiveForNpc(this, true);
            });
        }

        public override int GetWeaponDamage()
        {
            return (elite ? 10 : 5) + level * (elite ? 3 : 2);
        }

        public override int GetSpellPower()
        {
            return (elite ? 10 : 5) + level * (elite ? 3 : 2);
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
                baseSpells = baseSpells,
                alternativeNames = alternativeNames,
                characterClass = characterClass,
                characterStatus = characterStatus,
                agent = agent,
                items = new Dictionary<Item, int>(items),
                tags = new List<CharacterTag>(tags),
                knowledgeBase = new KnowledgeBase().MergeKnowledge(knowledgeBase),
                knowledgeDefaultResponse = knowledgeDefaultResponse
            };
        }
        public NPC SetResistances(params DamageType[] types)
        {
            resistances.AddRange(types);
            return this;
        }
        public NPC SetWeaknesses(params DamageType[] types)
        {
            weaknesses.AddRange(types);
            return this;
        }
        public NPC SetEquipment(params Equipment[] equipments)
        {
            foreach (var equipment in equipments)
            {
                AddToInventory(equipment);
                Equip(equipment);
            }
            return this;
        }
        public NPC AddInventory(Item item, int count = 1)
        {
            AddToInventory(item, count);
            return this;
        }
        public NPC SetFaction(Faction faction)
        {
            this.faction = faction;
            return this;
        }
        public NPC SetNPCName(string npcName)
        {
            if (!string.IsNullOrEmpty(this.npcName)) return this;
            this.npcName = npcName;
            alternativeNames = alternativeNames.Append(npcName).Append($"{name} {npcName}").ToArray();
            return this;
        }
        public override string ToString()
        {
            return $"NPC {GetName()} ({npcName})";
        }
    }
}
