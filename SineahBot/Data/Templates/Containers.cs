using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data.Templates
{
    public static class Containers
    {
        public static Container Cabinet = new Container("Cabinet", new string[] { })
        {
            description = "A large cabinet rests against the wall.",
            details = ""
        };
        public static Container Chest = new Container("Chest", new string[] { })
        {
            description = "A mundane chest lays on the floor.",
            details = "This is a chest like any other. Perfect to stash anyway anything, really."
        };
        public static Container IronChest = new Container("Iron chest", new string[] { "ironchest", "chest" })
        {
            description = "A solid, iron-reinforced chest lays on the floor.",
            details = "This is a very sturdy chest, reinforced with several iron plating. It will be almost impossible to break this open without damaging whatever is stached inside."
        };
        public static Container Desk = new Container("Desk", new string[] { })
        {
            description = "A sturdy desk rests against the wall.",
            details = "This desk is made out of solid wood. It offers a large workspace, as well as several drawers and compartments to store items."
        };


        public static Container CreateContainerFromCharacter(Character character)
        {
            var output = new Container($"{character.GetName()}'s remains", new string[] { character.GetName(), $"{character.GetName()}s remains", $"{character.GetName()} remains", "remains", "body", "corpse" })
            {
                description = $"{character.GetName()}'s remains lay here.",
                details = $"{character.GetName()} died and here are their remains."
            };
            foreach (var entry in character.items)
                output.items.Add(entry.Key, entry.Value);

            return output;
        }

    }
}
