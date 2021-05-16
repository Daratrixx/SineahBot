using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Tools
{
    public static class CharacterCreator
    {
        private static Dictionary<Player, CharacterCreationState> playerCharacterCreation = new Dictionary<Player, CharacterCreationState>();

        public static void ParsePlayerCharacterCreationInput(Player p, string input)
        {
            CharacterCreationState state;
            if (!playerCharacterCreation.TryGetValue(p, out state)) // if the player doesn't have a character being created, initiate the creation
            {
                state = playerCharacterCreation[p] = new CharacterCreationState() { player = p };
                p.Message($@"You are about to start your adventure.");
                state.player.Message(creationSteps[0].Prompt(state));
                return;
            }

            if (creationSteps[state.currentStep].TryParseInput(state, input))
            {
                ++state.currentStep; // advence to next state
            }

            if (state.currentStep == creationSteps.Length) // if we reach the end of the creation process
            {
                var character = CharacterManager.CreateCharacterForPlayer(state); // create character
                character.currentRoomId = RoomManager.GetSpawnRoomId(); // get spawn room
                p.Message($@"You are now ready to walk the world. Type **!help** to learn how to play. Farewell for now, mortal."); // display informations
                RoomManager.GetRoom(character.currentRoomId).AddToRoom(character); // add player to room, will display room description
                p.playerStatus = PlayerStatus.InCharacter; // indicate that the player is now in character and further commands must go in that pipeline
                playerCharacterCreation.Remove(p); // forget the creation state for the player
                return;
            }

            if (state.currentStep >= 0) // if the step is still within bounds
            {
                state.player.Message(creationSteps[state.currentStep].Prompt(state));
            }
        }

        private static CharacterCreationStep InputName = new CharacterCreationStep("What will be your characters **name**? (*type anything*)",
        "^(.+)$",
        (state, match) => { state.name = match.Groups[0].Value; },
        null);
        private static CharacterCreationStep ConfirmName = new CharacterCreationStepValidation<string>((state) => state.name);

        private static CharacterCreationStep InputGender = new CharacterCreationStep("What **gender** will your character identify as? (*type anything*)",
        "^(.+)$",
        (state, match) => { state.gender = match.Groups[0].Value; },
        null);
        private static CharacterCreationStep ConfirmGender = new CharacterCreationStepValidation<string>((state) => state.gender);

        private static string[] possiblePronouns = new string[] {
            "they/them/theirs/their/themselves",
            "she/her/hers/her/herself",
            "he/him/his/his/himself"
        };
        private static string GetPossiblePronounsList()
        {
            return String.Join("\n", possiblePronouns.Select((x, i) => $"[**{i + 1}**] - {x}"));
        }
        private static string GetPossiblePronounsValues()
        {
            return String.Join("|", possiblePronouns.Select((x, i) => $"{i + 1}"));
        }
        private static CharacterCreationStep InputPronouns = new CharacterCreationStep($"What pronouns does your character want to be refered as (*type a number*)?\n{GetPossiblePronounsList()}",
        $"^({GetPossiblePronounsValues()})$",
        (state, match) => { state.pronouns = possiblePronouns[int.Parse(match.Groups[0].Value) - 1]; },
        (state, input) => { state.player.Message("Invalid value, please input one of the possible numbers."); });
        private static CharacterCreationStep ConfirmPronons = new CharacterCreationStepValidation<string>((state) => state.pronouns);

        private static string GetPossibleClassList()
        {
            return String.Join("/", ClassProgressionManager.starterClass.Select(x => $"**{x}**"));
        }
        private static string GetPossibleClassValues()
        {
            return String.Join("|", ClassProgressionManager.starterClass.Union(ClassProgressionManager.secretClass));
        }
        private static CharacterCreationStep InputClass = new CharacterCreationStep($"What will be your characters starting **class**? (*type a class name such as [{GetPossibleClassList()}]*)",
        $"^({GetPossibleClassValues()})$",
        (state, match) => { state.characterClass = Enum.Parse<CharacterClass>(match.Groups[1].Value); },
        (state, input) => { state.player.Message("Invalid value, please input one of the possible values."); });
        private static CharacterCreationStep ConfirmClass = new CharacterCreationStepValidation<CharacterClass>((state) => state.characterClass);


        private static CharacterCreationStep[] creationSteps = new CharacterCreationStep[]
        {
            InputName,
            ConfirmName,
            InputGender,
            ConfirmGender,
            InputPronouns,
            ConfirmPronons,
            InputClass,
            ConfirmClass,
        };

        public class CharacterCreationStep
        {
            protected string prompt;
            protected Regex expectedInput;
            protected Action<CharacterCreationState, Match> onStepSuccess;
            protected Action<CharacterCreationState, string> onStepError;
            public CharacterCreationStep(string prompt, string expectedInput, Action<CharacterCreationState, Match> onStepSuccess, Action<CharacterCreationState, string> onStepError)
            {
                this.prompt = prompt;
                this.expectedInput = new Regex(expectedInput, RegexOptions.IgnoreCase);
                this.onStepSuccess = onStepSuccess;
                this.onStepError = onStepError;
            }
            protected CharacterCreationStep(string expectedInput)
            {
                this.expectedInput = new Regex(expectedInput, RegexOptions.IgnoreCase);
            }

            public virtual string Prompt(CharacterCreationState state)
            {
                return prompt;
            }

            public virtual bool TryParseInput(CharacterCreationState state, string input)
            {
                var match = expectedInput.Match(input);
                if (match.Success)
                {
                    onStepSuccess?.Invoke(state, match);
                    return true;
                }
                onStepError?.Invoke(state, input);
                return false;
            }
        }

        public class CharacterCreationStepValidation<T> : CharacterCreationStep
        {
            public PropertyInfo value;
            public CharacterCreationStepValidation(Expression<Func<CharacterCreationState, T>> valuePropertyExpr) : base("^(y|yes)|(n|no)$")
            {
                if (valuePropertyExpr != null)
                {
                    var expr = (MemberExpression)valuePropertyExpr.Body;
                    value = (PropertyInfo)expr.Member;
                }

                onStepSuccess = OnSuccess;
                onStepError = OnError;
            }
            private T GetValue(CharacterCreationState state)
            {
                return (T)value.GetValue(state);
            }
            private void SetValue(CharacterCreationState state, T val)
            {
                value.SetValue(state, val);
            }
            public override string Prompt(CharacterCreationState state)
            {
                return $@"""{GetValue(state)}""... is that correct? [**y**es/**n**o]";
            }
            private void OnSuccess(CharacterCreationState state, Match match)
            {
                if (match.Groups[1].Value != "")
                {
                    ++state.currentStep;
                }
                else
                {
                    this.SetValue(state, default(T));
                    --state.currentStep;
                }
            }
            private void OnError(CharacterCreationState state, string input)
            {
                state.player.Message($@"Invalid input. Expected: y/yes/n/no");
            }
            public override bool TryParseInput(CharacterCreationState state, string input)
            {
                base.TryParseInput(state, input);
                return false;
            }
        }

        public class CharacterCreationState
        {
            public Player player;
            public int currentStep = 0;

            public string name { get; set; }
            public string gender { get; set; }
            public string pronouns { get; set; }
            public CharacterClass characterClass { get; set; }
        }
    }
}
