using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandMetaHelp : Command
    {
        public CommandMetaHelp()
        {
            commandRegex = new Regex(@"^(!help)( .+)?$", RegexOptions.IgnoreCase);
        }

        public override void Run(Character character, Room room)
        {
            //var entity = agent as Entity;
            var targetName = GetArgument(2);

            if (String.IsNullOrWhiteSpace(targetName))
            {
            switch(character.characterStatus) {
                    case CharacterStatus.Trade:
                        character.Message(TradeHelpMessage);
                        break;
                    case CharacterStatus.Search:
                        character.Message(SearchHelpMessage);
                        break;
                    default:
                        character.Message(GetDefaultHelpMessage());
                        break;
                }
            }
            else
            {
                character.Message(GetHelpMessage(targetName.ToLower()));
            }
        }

        public const string TradeHelpMessage = @"
**HELP** - TRADING
> - Type `trade [merchant name]` to start trading with a merchant. You can type just `trade` to start trading with a random merchant in the room.
> - Type `list` to get a list of possible transactions.
> - Type `buy [item name]` to buy an item.
> - Type `buy [amount] [item name]` to buy a certain amount of items.
> - Type `sell [item name]` to sell an item.
> - Type `sell [amount] [item name]` to sell several items. `[amount]` can be `all` or `*`, which will make you sell all your items of the specified type.
> - Type `leave` to stop trading.
> While in the trading mode, you cannot **move**, **attack**, **cast** spells, **equip** or **unequip** gear, **get** or **drop** items, **lock** or **unlock** access, but you will automatically quit the trade if you are attacked.
";

        public const string SearchHelpMessage = @"
**HELP** - SEARCHING
> - Type `look`, `list` or `l` to get a list of all items present in the container.
> - Type `get [item name]` or `pick up [item name]` to retrieve an item from the container.
> - Type `get [amount] [item name]` or `pick up [amount] [item name]` to retrieve several items from the container.
> - Type `stash [item name]`, `drop [item name]`,`s [item name]` or ` d [item name]` to stash an item from your inventory.
> - Type `stash [amount] [item name]`, `drop [amount] [item name]`,`s [amount] [item name]` or `d [amount] [item name]` to stash several item from your inventory.
> - Type `leave` or `out` to stop searching.
> While in the searching mode, you cannot **move**, **attack**, **cast** spells, **lock** or **unlock** access, but you will automatically quit searching if you are attacked.
";

        public static string GetHelpMessage(string command = null)
        {
            var output = GetCommandHelpMessage(command);
            if (output == null) output = GetConceptHelpMessage(command);
            if (output == null) output = $"Unkown command or concept \"**`{command}`**\"";
            return output;
        }
        public static string GetCommandHelpMessage(string command = null)
        {
            if (string.IsNullOrWhiteSpace(command) || command == "help" || command == "!help") return GetHelpMessage();
            if (CommandDescription.ContainsKey(command))
                return $"**HELP** - `{command}`\n{CommandDescription[command]}";
            return null;
        }
        public static string GetConceptHelpMessage(string concept = null)
        {
            if (string.IsNullOrWhiteSpace(concept) || concept == "help" || concept == "!help") return GetHelpMessage();
            if (ConceptDescription.ContainsKey(concept))
                return $"**HELP** - `{concept}`\n{ConceptDescription[concept]}";
            return null;
        }
        public static string GetDefaultHelpMessage()
        {
            return $"**HELP**\n> List of existing commands:" +
            $"\n> {string.Join(", ", CommandDescription.Keys)}" +
            $"\n Type `!help [command name]` to have a detailed description of a command." +
            $"\n> List of other concepts:" +
            $"\n> {string.Join(", ", ConceptDescription.Keys)}" +
            $"\n Type `!help [concept name]` to have a detailed description of a concept.";
        }

        public static readonly Dictionary<string, string> CommandDescription = new Dictionary<string, string>()
        {
            { "look", @"> *(alt: `l`)*
> - `look` will display a description of your surroundings.
> The description will be in two part: first *room description* which describes the room, and then after the > symbol is the list of all entities you can interact with
> - `look [entity name]` will display a better description of a specific entity.
> Looking at a sign will tell you what is written on it. Looking at a character will give you details about their status and power.
" },
            { "direction", @"> *(alt: `dir`)*
> - `direction` will display a list of directions you can go to from where you are
> The directions are West, North, West, South, Up, Down, In, Out.
> Whenever you are using a direction in a command, you can shorten `west` to `w`, `north` to `n`, `east` to `e`, and `south to s` (example: `e` to move east, `flee n` to flee north)
" },
            { "move", @"> *(alt: `go`)*
> - `move [direction]` to move in the specified direction.
> You can type only `[direction name]` to move (example: `up` to move up)
> You cannot move while trading or fighting.
" },
            { "get", @"> *(alt: `g`)*
> - `get [item name]` to pick up an item. It will then be in your inventory.
> You cannot get items while trading.
" },
            { "drop", @"> *(alt: `d`)*
> - `drop [item name]` to drop an item from your inventory.
> You cannot drop items while trading.
" },
            { "lock", @"> - `lock [direction]` to lock the access.
> It is impossible to walk through a locked access.
> All access cannot be locked.
> Some access might require an item to lock them.
> You cannot lock an access while trading or fighting.
" },
            { "unlock", @"> - `unlock [direction]` to unlock the access.
> Some access might require an item to unlock them.
> You cannot unlock an access while trading or fighting.
" },
            { "attack", @"> *(alt: `atk`, `hit`, `strike`)*
> - `attack [target name]` to attack the character.
> Dealing any amount of damage to a character will trigger a fight.
" },
            { "cast", @"> - `cast [spell name] on [target name]` to cast a spell on a specific character.
> Some spells need a target but can be self cast. In that case, the target can be omitted to cast it on yourself (see below)
> - `cast [spell name]` to cast a spell on yourself/around you.
> Dealing any amount of damage to a character will trigger a fight.
" },
            { "consume", @"> - `consume [item name]` to consume an item.
> You can consume an item that is not in your inventory if it's in the same room as you.
> Items cannot be consumed in combat unless specified otherwise (typing `!inventory` or `look [item name]` will tell you if an item can be used in combat)
" },
            { "say", @"> *(alt: `""`)*
> - `say [message]` to have your character talk out loud in the room.
> Some characters or words may be filtered.
" },
            { "act", @"> *(alt: `*`)*
> - `act [acting]` to have your character act in the room.
> Some characters or words may be filtered.
" },
            { "read", @"> - `read [display name]` to read the content of a display.
> Displays can be signs or books.
> - `read [book name] p [page number]` to read a specific page from a book.
" },
            { "trade", @"> - `trade [merchant name]` to start trading with a merchant. Type `leave` to stop trading.
> While in trading mode, you have access to the following commands:
> - Type `list` or `l` to get a list of possible transactions.
> - Type `buy [item name]` to buy an item.
> - Type `buy [amount] [item name]` to buy a certain amount of items.
> - Type `sell [item name]` to sell an item.
> - Type `sell [amount] [item name]` to sell several items. `[amount]` can be `all` or `*`, which will make you sell all your items of the specified type.
> - Type `leave` or `out` to stop trading.
> While in the trading mode, you cannot **move**, **attack**, **cast** spells, **equip** or **unequip** gear, **get** or **drop** items, **lock** or **unlock** access, but you will automatically quit the trade if you are attacked.
" },
            { "ask", @"> - `ask [character name] about [subject]` to get information about a subject.
> You can ask about a lot of general information to most character.
> Some NPC dialogue and some books will have some underlined text called __key words__.
> Asking the right NPC about the right __key word__ will lead you to new discoveries.
" },
            { "search", @"> - `search [container name]` to search and see what items a container is holding.
> Containers can be different kind of furniture such as chests, cabinets, desks...
> Corpse and remains are container, and can be searched in the same way.
> While searching, the following commands are available:
> - Type `look`, `list` or `l` to get a list of all items present in the container.
> - Type `get [item name]` or `pick up [item name]` to retrieve an item from the container.
> - Type `get [amount] [item name]` or `pick up [amount] [item name]` to retrieve several items from the container.
> - Type `stash [item name]`, `drop [item name]`,`s [item name]` or ` d [item name]` to stash an item from your inventory.
> - Type `stash [amount] [item name]`, `drop [amount] [item name]`,`s [amount] [item name]` or `d [amount] [item name]` to stash several item from your inventory.
> - Type `leave` or `out` to stop searching.
> While in the searching mode, you cannot **move**, **attack**, **cast** spells, **lock** or **unlock** access, but you will automatically quit searching if you are attacked.
" },
            { "!information", @"*(alt: `!i`)*
> - `!information` to get some information about your character.
" },
            { "!spells", @"> - `!spells` to get the list of spells you can cast.
> - `!spells [spell name]` to get a detailed description of a spell you know.
" },
            { "!inventory", @"> *(alt: `!inv`)*
> - `!inventory` to get a list of items you own, as well as your gold.
" },
            { "!class", @"> - `!class [class name]` to get the description of a character class.
" },
            { "!level", @"> - `!level` to attempt to level up.
> You can't level up if you don't have enough experience.
> The higher level you are, the less experience you earn from combat.
> Some class can switch to another class upon reaching a new level. You will be prompted to choose which cless you wish to level up as.
" },
        };
        public static readonly Dictionary<string, string> ConceptDescription = new Dictionary<string, string>()
        {
            { "trading", @"> While in trading mode, you have access to the following commands:
> - Type `list` or `l` to get a list of possible transactions.
> Transactions are grouped into two categories: ""will buy from you"" and ""will sell to you""
> - Type `buy [item name]` to buy an item from the trader.
> You can buy anything from the ""will sell to you"" category.
> - Type `buy [amount] [item name]` to buy a certain amount of items.
> - Type `sell [item name]` to sell an item to the trader.
> You can only sell items that are listed in ""will buy from you"" category.
> - Type `sell [amount] [item name]` to sell several items. `[amount]` can be `all` or `*`, which will make you sell all your items of the specified type.
> - Type `leave` or `out` to stop trading.
> While in the trading mode, you cannot **move**, **attack**, **cast** spells, **equip** or **unequip** gear, **get** or **drop** items, **lock** or **unlock** access, but you will automatically quit the trade if you are attacked.
" },
            { "searching", @"> While in searching mode, you have access to the following commands:
> - Type `list` or `l` to get a list of items stashed in the container you're searching into.
> - Type `get [item name]` to get an item stack from the container.
> - Type `get [amount] [item name]` to get a desired amount of items from the container. `[amount]` can be `all` or `*`, which will make you get all the items of the specified typ from the container.
> - Type `store [item name]` to stash an item into the container.
> - Type `store [amount] [item name]` to stash several items at once. `[amount]` can be `all` or `*`, which will make you stash all your items of the specified type.
> - Type `leave` or `out` to stop searching.
> While in the searching mode, you cannot **move**, **attack**, **cast** spells, **equip** or **unequip** gear, **get** or **drop** items, **lock** or **unlock** access, but you will automatically quit the search if you are attacked.
" },
            { "combat", @"> Dealing or receiving any amount of damage will put you into fight mode.
> While fighting, you cannot move, equip or unequip gear, lock or unlock access.
> You will remain in combat as long as every character you damaged/that damaged you are still fighting.
> You can use the **flee [direction]** command to flee the room and leave combat.
> Leaving combat will prevent you from getting eny reward from the combat you left.
" },
        };

    }
}
