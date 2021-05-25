﻿using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static SineahBot.Tools.CharacterCreator;

namespace SineahBot.Tools
{
    public static class CharacterManager
    {

        public static int ExpMultiplier = 2;
        public static int GoldMultiplier = 2;
        public static int RegenerationInterval = 10;
        public static int AlteractionInterval = 2;
        static CharacterManager()
        {
            // start the character regeneration interval
            new MudInterval(RegenerationInterval, () =>
            {
                foreach (var c in characters)
                {
                    c.Value.Regenerate();
                }
            });

            // start the character alteration expiration interval
            new MudInterval(AlteractionInterval, () =>
            {
                foreach (var c in characters)
                {
                    c.Value.TickAlterations(AlteractionInterval);
                }
            });
        }
        public static Dictionary<Guid, Character> characters = new Dictionary<Guid, Character>();

        public static void LoadCharacters(IEnumerable<Character> characters)
        {
            foreach (var character in characters)
            {
                CharacterManager.characters[character.id] = character;
            }
        }
        public static Character GetCharacter(Guid idCharacter)
        {
            if (!characters.ContainsKey(idCharacter))
            {
                var character = Program.database.Characters.FirstOrDefault(x => x.id == idCharacter);
                if (character == null) throw new Exception($"Impossible to find character with id {idCharacter}");
                characters[idCharacter] = character;
                character.pronouns = character.pronouns; // update the properties
                ClassProgressionManager.ApplyClassProgressionForCharacter(character, true);
                LoadCharacterInventory(character);
                LoadCharacterEquipment(character);
                character.faction = FactionManager.CreatePlayerRepFaction();
                return character;
            }
            return characters[idCharacter];
        }

        public static void SaveLoadedCharacters()
        {
            foreach (var data in characters.Where(x => x.Value.agent is Player))
            {
                SaveCharacterInventory(data.Value);
                SaveCharacterEquipment(data.Value);
            }
        }

        public static void SaveCharacterInventory(Character character)
        {
            var items = Program.database.CharacterItems.AsQueryable().Where(x => x.idCharacter == character.id);
            Program.database.CharacterItems.RemoveRange(items);
            items = character.items.AsQueryable().Where(x => x.Key.permanant).Select(item => new CharacterItem()
            {
                id = Guid.NewGuid(),
                idCharacter = character.id,
                ItemName = item.Key.name,
                StackSize = item.Value
            });
            Program.database.CharacterItems.AddRange(items);
        }

        public static void SaveCharacterEquipment(Character character)
        {
            var equipment = Program.database.CharacterEquipment.AsQueryable().Where(x => x.idCharacter == character.id);
            Program.database.CharacterEquipment.RemoveRange(equipment);
            equipment = character.equipments.Values.AsQueryable().Where(x => x != null).Select(item => new CharacterEquipment()
            {
                id = Guid.NewGuid(),
                idCharacter = character.id,
                ItemName = item.name,
            });
            Program.database.CharacterEquipment.AddRange(equipment);
        }

        public static void LoadCharacterInventory(Character character)
        {
            var items = Program.database.CharacterItems.AsEnumerable().Where(x => x.idCharacter == character.id).ToList();
            var matchedItems = items.Select(x => new KeyValuePair<Item, int>(ItemManager.GetItem(x.ItemName), x.StackSize))
                .Where(x => x.Key != null)
                .Distinct()
                .ToList();
            character.items = new Dictionary<Item, int>(matchedItems);
        }

        public static void LoadCharacterEquipment(Character character)
        {
            var equipments = Program.database.CharacterEquipment.AsQueryable().Where(x => x.idCharacter == character.id).ToList();
            foreach (var equipment in equipments)
            {
                var item = character.FindInInventory(equipment.ItemName) as Equipment;
                if (item != null)
                {
                    character.Equip(item);
                }
            }
        }

        public static Character CreateCharacterForPlayer(CharacterCreationState state)
        {
            var character = new Character();
            character.agent = state.player;
            character.id = Guid.NewGuid();
            character.name = state.name;
            character.gender = state.gender;
            character.pronouns = state.pronouns;
            character.characterClass = state.characterClass;
            character.level = 1;
            character.experience = 0;
            character.gold = 20;
            ClassProgressionManager.ApplyClassProgressionForCharacter(character, true);
            if (ClassProgressionManager.IsPhysicalClass(character.characterClass))
            {
                character.AddToInventory(Data.Templates.Consumables.Bread, 3);
                character.AddToInventory(Data.Templates.Consumables.DriedMeat);
            }
            if (ClassProgressionManager.IsMagicalClass(character.characterClass))
            {
                character.AddToInventory(Data.Templates.Consumables.Bread, 3);
                character.AddToInventory(Data.Templates.Consumables.Water);
            }
            if (ClassProgressionManager.IsSecretClass(character.characterClass))
            {
                character.AddToInventory(Data.Templates.Consumables.Candy);
            }
            characters[character.id] = character;
            Program.database.Characters.Add(character);
            character.faction = FactionManager.CreatePlayerRepFaction();
            state.player.idCharacter = character.id;
            state.player.character = character;
            return character;
        }

        public static void DeletePlayerCharacter(Player player)
        {
            if (player == null) throw new Exception("Player cannot be null.");
            if (player.character == null || player.idCharacter == null) throw new Exception("This player doesn't have a character to be deleted.");
            var character = player.character;
            character.agent = null;
            characters.Remove(character.id);
            Program.database.CharacterEquipment.RemoveRange(Program.database.CharacterEquipment.AsQueryable().Where(x => x.idCharacter == character.id));
            Program.database.CharacterItems.RemoveRange(Program.database.CharacterItems.AsQueryable().Where(x => x.idCharacter == character.id));
            Program.database.Characters.Remove(character);
            player.idCharacter = null;
            player.character = null;
            player.playerStatus = PlayerStatus.CharacterCreation;
        }

        public static Character CreateTestCharacter()
        {
            var character = new Character()
            {
                characterStatus = CharacterStatus.Normal,
                id = Guid.NewGuid(),
                name = "test character",
                //description = "You notice the test character."
            };
            characters[character.id] = character;
            return character;
        }
    }
}
