﻿using Microsoft.EntityFrameworkCore.Query;
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
        public NPC RegisterKnowlede(string knowledge, string response)
        {
            knowledgeBase[knowledge.ToLower().Replace(" ", "")] = response;
            return this;
        }
        public string GetKnowledgeResponse(string knowledge)
        {
            knowledge = knowledge.ToLower().Replace(" ","");
            if (!knowledgeBase.ContainsKey(knowledge)) return $"*{GetName()}*: I don't know anything about that.";
            return $"*{GetName()}*: **{knowledge}**... {knowledgeBase[knowledge]}";
        }

        public override string GetFullDescription(IAgent agent = null)
        {
            return $"*{longDescription}*" +
            $"\n> {GetStateDescription(agent)}" +
            $"\n> {GetPowerDescription(agent as Character)} " +
            $"\n> {GetAlterationDescription(agent as Character)} ";
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
                tags = new List<CharacterTag>(tags)
            };
        }
        public NPC Equipment(Equipment equipment)
        {
            base.Equip(equipment);
            return this;
        }
    }
}
