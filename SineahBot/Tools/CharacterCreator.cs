using SineahBot.Data;
using SineahBot.Data.Enums;
using SineahBot.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SineahBot.Tools
{
    public static partial class CharacterCreator
    {
        private static Dictionary<Player, CharacterCreationState> PlayerCharacterCreation = new Dictionary<Player, CharacterCreationState>();

        public static void ParsePlayerCharacterCreationInput(Player p, string input)
        {
            CharacterCreationState state;
            if (!PlayerCharacterCreation.TryGetValue(p, out state)) // if the player doesn't have a character being created, initiate the creation
            {
                state = PlayerCharacterCreation[p] = new CharacterCreationState() { Player = p };
                p.Message($@"You are about to start your adventure.");
                state.Player.Message(CreationSteps[0].GetPrompt(state));
                return;
            }

            if (CreationSteps[state.CurrentStep].TryParseInput(state, input))
            {
                ++state.CurrentStep; // advence to next state
            }

            if (state.CurrentStep == CreationSteps.Length) // if we reach the end of the creation process
            {
                var character = CharacterManager.CreateCharacterForPlayer(state); // create character
                character.currentRoomId = RoomManager.GetSpawnRoomId(character.characterAncestry); // get spawn room
                p.Message($@"You are now ready to walk the world. Type **!help** to learn how to play. Farewell for now, mortal."); // display informations
                RoomManager.GetRoomById(character.currentRoomId).AddToRoom(character); // add player to room, will display room description
                p.playerStatus = PlayerStatus.InCharacter; // indicate that the player is now in character and further commands must go in that pipeline
                PlayerCharacterCreation.Remove(p); // forget the creation state for the player
                return;
            }

            if (state.CurrentStep >= 0) // if the step is still within bounds
            {
                state.Player.Message(CreationSteps[state.CurrentStep].GetPrompt(state));
            }
        }

        // name
        private static CharacterCreationStep InputName = new CharacterCreationStep("What will be your characters **name**? (*type anything*)",
        "^(.+)$",
        (state, match) => { state.Name = match.Groups[0].Value; },
        null);
        private static CharacterCreationStep ConfirmName = new CharacterCreationStepValidation<string>((state) => state.Name);

        // gender
        private static CharacterCreationStep InputGender = new CharacterCreationStep("What **gender** will your character identify as? (*type anything*)",
        "^(.+)$",
        (state, match) => { state.Gender = match.Groups[0].Value; },
        null);
        private static CharacterCreationStep ConfirmGender = new CharacterCreationStepValidation<string>((state) => state.Gender);

        // pronouns
        public static readonly string[] PossiblePronouns = new string[] {
            "they/them/theirs/their/themselves",
            "she/her/hers/her/herself",
            "he/him/his/his/himself"
        };
        private static string GetPossiblePronounsList()
        {
            return String.Join("\n", PossiblePronouns.Select((x, i) => $"[**{i + 1}**] - {x}"));
        }
        private static string GetPossiblePronounsValues()
        {
            return String.Join("|", PossiblePronouns.Select((x, i) => $"{i + 1}"));
        }
        private static CharacterCreationStep InputPronouns = new CharacterCreationStep($"What pronouns does your character want to be refered as (*type a number*)?\n{GetPossiblePronounsList()}",
        $"^({GetPossiblePronounsValues()})$",
        (state, match) => { state.Pronouns = PossiblePronouns[int.Parse(match.Groups[0].Value) - 1]; },
        (state, input) => { state.Player.Message("Invalid value, please input one of the possible numbers."); });
        private static CharacterCreationStep ConfirmPronons = new CharacterCreationStepValidation<string>((state) => state.Pronouns);

        // ancestry
        private static CharacterAncestry[] PossibleAncestries = { CharacterAncestry.Human, CharacterAncestry.Kobold };
        private static string GetPossibleAncestryList()
        {
            return String.Join("/", PossibleAncestries);
        }
        private static string GetPossibleAncestryValues()
        {
            return String.Join("|", PossibleAncestries);
        }
        private static CharacterCreationStep InputAncestry = new CharacterCreationStep($"What will be your characters **ancestry**? (*type an ancestry name such as [{GetPossibleAncestryList()}]*)",
        $"^({GetPossibleAncestryValues()})$",
        (state, match) => { state.CharacterAncestry = Enum.Parse<CharacterAncestry>(match.Groups[1].Value); },
        null);
        private static CharacterCreationStep ConfirmAncestry = new CharacterCreationStepValidation<CharacterAncestry>((state) => state.CharacterAncestry);

        // class
        private static string GetPossibleClassList()
        {
            return String.Join("/", CharacterClassManager.starterClass.Select(x => $"**{x}**"));
        }
        private static string GetPossibleClassValues()
        {
            return String.Join("|", CharacterClassManager.starterClass.Union(CharacterClassManager.secretClass));
        }
        private static CharacterCreationStep InputClass = new CharacterCreationStep($"What will be your characters starting **class**? (*type a class name such as [{GetPossibleClassList()}]*)",
        $"^({GetPossibleClassValues()})$",
        (state, match) => { state.CharacterClass = Enum.Parse<CharacterClass>(match.Groups[1].Value.Capitalize()); },
        (state, input) => { state.Player.Message("Invalid value, please input one of the possible values."); });
        private static CharacterCreationStep ConfirmClass = new CharacterCreationStepValidation<CharacterClass>((state) => state.CharacterClass);

        private static CharacterCreationStep[] CreationSteps = new CharacterCreationStep[]
        {
            InputName,
            ConfirmName,
            InputGender,
            ConfirmGender,
            InputPronouns,
            ConfirmPronons,
            InputAncestry,
            ConfirmAncestry,
            InputClass,
            ConfirmClass,
        };
    }
}
