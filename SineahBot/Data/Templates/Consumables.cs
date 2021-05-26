using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Templates
{
    public static class Consumables
    {
        // health
        public static Consumable Bread = new Consumable("Bread", new string[] { "piece of bread" })
        {
            description = "There's a piece of bread.",
            details = "Consuming this will restore a little bit of health.",
            OnConsumed = (character) => { character.RestoreHealth(10, Bread); }
        };
        public static Consumable DriedMeat = new Consumable("Dried meat", new string[] { "dmeat", "meat" })
        {
            description = "There's some dried meat.",
            details = "Consuming this will restore some health.",
            OnConsumed = (character) => { character.RestoreHealth(30, DriedMeat); }
        };
        public static Consumable FourWindsBlanquette = new Consumable("Blanquette", new string[] { "blqt" })
        {
            description = "There's a blanquette.",
            details = "Four Wind's blanquette, renoun dish of the inn. Consuming this will restore a decent amount of health.",
            OnConsumed = (character) => { character.RestoreHealth(40, DriedMeat); }
        };
        public static Consumable HealingHerbs = new Consumable("Healing herbs", new string[] { "healingh", "hherbs", "healh", "herbs", "hh" })
        {
            description = "There's some healing herbs.",
            details = "Consuming this will restore a decent amount of health.",
            OnConsumed = (character) => { character.RestoreHealth(50, HealingHerbs); }
        };
        public static Consumable HealthPotion = new Consumable("Health potion", new string[] { "health pot", "healthpot", "hpot" })
        {
            description = "There's a healing potion.",
            details = "Consuming this will restore a lot of health.",
            combatConsumable = true,
            OnConsumed = (character) => { character.RestoreHealth(70, HealthPotion); }
        };

        // mana
        public static Consumable Water = new Consumable("Water", new string[] { "bottle of water", "water bottle", "waterbottle" })
        {
            description = "Here's a bottle of water.",
            details = "Consuming this will restore a little bit of mana.",
            OnConsumed = (character) => { character.RestoreMana(5, Water); }
        };
        public static Consumable Beer = new Consumable("Beer", new string[] { "mug of beer", "beer mug", "beermug," })
        {
            description = "Here's a mug of beer.",
            details = "Consuming this will restore some mana.",
            OnConsumed = (character) => { character.RestoreMana(15, Beer); }
        };
        public static Consumable Wine = new Consumable("Wine", new string[] { "bottle of wine", "wine bottle", "winebottle" })
        {
            description = "Here's a bottle of wine.",
            details = "Consuming this will restore a decent amount of mana.",
            OnConsumed = (character) => { character.RestoreMana(25, Wine); }
        };
        public static Consumable ManaPotion = new Consumable("Mana potion", new string[] { "mana pot", "manapot", "mpot" })
        {
            description = "Here's a mana potion.",
            details = "Consuming this will restore a lot of mana.",
            combatConsumable = true,
            OnConsumed = (character) => { character.RestoreMana(35, ManaPotion); }
        };

        // other
        public static Consumable Antidote = new Consumable("Antidote", new string[] { })
        {
            description = "There's an antidote.",
            details = "Consuming this will eliminate all trace of poison from your body.",
            combatConsumable = false,
            OnConsumed = (character) => { character.RemoveAlteration(AlterationType.Poisoned); }
        };
        public static Consumable AntiBurnCream = new Consumable("Anti-burn cream", new string[] { "antiburncream", "burncream", "burn cream", "anti burn cream" })
        {
            description = "There's some anti-burn cream.",
            details = "Consuming this will heal your burns.",
            combatConsumable = false,
            OnConsumed = (character) => { character.RemoveAlteration(AlterationType.Burnt); }
        };
        public static Consumable Medicine = new Consumable("Medicine", new string[] { "meds" })
        {
            description = "There's some medicine.",
            details = "Consuming this will heal you from common sickness.",
            combatConsumable = false,
            OnConsumed = (character) => { character.RemoveAlteration(AlterationType.Sickness); }
        };
        public static Consumable Quill = new Consumable("Quill", new string[] { })
        {
            description = "There's a Quill.",
            details = "Using this will allow you to leave a message in the room you are currently situated.",
            combatConsumable = false,
            OnConsumed = (c) =>
            {
                c.Message("The next message you send will be written in the room for all to see (replaces any previously written message).");
                c.RegisterMessageBypass((character, room, message) =>
                {
                    var display = room.displays.Where(x => x is Display.PlayerMessage).FirstOrDefault(x => (x as Display.PlayerMessage).idWritter == character.id) as Display.PlayerMessage;
                    if (display != null)
                    {
                        room.RemoveFromRoom(display);
                        character.messages.RemoveAll(x => x.idRoom == room.id);
                    }
                    message = CensorManager.FilterMessage(message);
                    room.AddToRoom(new Display.PlayerMessage(character, message), false);
                    character.messages.Add(new CharacterMessage() { idCharacter = character.id, idRoom = room.id });
                    c.Message($"You wrote the message: \"{message}\"");
                    room.DescribeAction($"{character.GetName()} just left a message here.", character);
                },
                (character, room) =>
                {
                    c.AddToInventory(Quill);
                    c.Message($"You recovered your Quill.");
                });
            },
        };

        // mixte
        public static Consumable Candy = new Consumable("Candy", new string[] { })
        {
            description = "Here's some candy.",
            details = "Consuming this will restore a little bit of health and mana.",
            OnConsumed = (character) => { character.RestoreHealth(10, Candy); character.RestoreMana(5, Candy); }
        };
        public static Consumable Stimulants = new Consumable("Stimulants", new string[] { })
        {
            description = "Here's some stimulants.",
            details = "Consuming this will restore a little bit of health and some mana.",
            combatConsumable = true,
            OnConsumed = (character) => { character.RestoreHealth(10, Stimulants); character.RestoreMana(15, Stimulants); }
        };
    }
}
