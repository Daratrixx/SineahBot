using System;
using System.Text.RegularExpressions;

namespace SineahBot.Tools
{
    public static partial class CharacterCreator
    {
        public class CharacterCreationStep
        {
            protected string Prompt;
            protected Regex ExpectedInput;
            protected Action<CharacterCreationState, Match> OnStepSuccess;
            protected Action<CharacterCreationState, string> OnStepError;
            public CharacterCreationStep(string prompt, string expectedInput, Action<CharacterCreationState, Match> onStepSuccess, Action<CharacterCreationState, string> onStepError)
            {
                this.Prompt = prompt;
                this.ExpectedInput = new Regex(expectedInput, RegexOptions.IgnoreCase);
                this.OnStepSuccess = onStepSuccess;
                this.OnStepError = onStepError;
            }
            protected CharacterCreationStep(string expectedInput)
            {
                this.ExpectedInput = new Regex(expectedInput, RegexOptions.IgnoreCase);
            }

            public virtual string GetPrompt(CharacterCreationState state)
            {
                return Prompt;
            }

            public virtual bool TryParseInput(CharacterCreationState state, string input)
            {
                var match = ExpectedInput.Match(input);
                if (match.Success)
                {
                    OnStepSuccess?.Invoke(state, match);
                    return true;
                }
                OnStepError?.Invoke(state, input);
                return false;
            }
        }
    }
}
