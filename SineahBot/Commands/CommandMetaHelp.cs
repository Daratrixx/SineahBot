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
                if (character.characterStatus == CharacterStatus.Trade)
                    character.Message(TradeHelpMessage);
                else
                    character.Message(HelpMessage);
            }
            else
            {
                character.Message(HelpMessage);
            }
        }

        public const string HelpMessage = @"
**HELP**
> - Type `look` to get a description of your surroundings, `look [object name]` to have a better description of an object.
> - Type `direction` to get a list of directions you can go to from where you are.
> - Type `move/go [direction]` to move.
> - Type `get [item name]` to pick up an item. It will then be in your inventory.
> - Type `drop [item name]` to drop and item from your inventory.
> - Type `lock [direction]` to lock the access. Some access might require an item to lock them.
> - Type `unlock [direction]` to unlock the access. Some access might require an item to unlock them.
> - Type `attack [target name]` to attack the a character or object.
> - Type `cast [spell name] on [target name]` to cast a spell on a specific target.
> - Type `cast [spell name]` to cast a spell without a target.
> - Type `say [speach]` to have your character talk out loud in the room.
> - Type `act [acting]` to have your character act in the room.
> - Type `trade [merchant name]` to start trading with a merchant. Type `leave` to stop trading.
> - Type `consume [item name]` to consume an item from your inventory or from the room.
> - Type `!information` to get some information about your character.
> - Type `!spells` to get the list of spells you can cast.
> - Type `!inventory` to get a list of items you own.
> - Type `!class [class name]` to get the description of a character class.
> - Type `!level` to level up.
> - [SOON] Type `!help [full command]` to have a detailed description of a command.
";

        public const string TradeHelpMessage = @"
**HELP** - TRADING
> - Type `trade [merchant name]` to start trading with a merchant. You can type just `trade` to start trading with a random merchant in the room.
> - Type `list` to get a list of possible transactions.
> - Type `buy [item name]` to buy an item.
> - Type `buy [amount] [item name]` to buy a certain amount of items.
> - Type `sell [item name]` to sell an item.
> - Type `sell [amount] [item name]` to sell several items. `[amount]` can be `all` or `*`, which will make you sell all your items of the specified type.
> - Type `leave` to stop trading.
";
    }
}
