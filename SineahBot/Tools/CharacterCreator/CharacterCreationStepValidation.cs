using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SineahBot.Tools
{
    public static partial class CharacterCreator
    {
        public class CharacterCreationStepValidation<T> : CharacterCreationStep
        {
            public PropertyInfo Value;
            public CharacterCreationStepValidation(Expression<Func<CharacterCreationState, T>> valuePropertyExpr) : base("^(y|yes)|(n|no)$")
            {
                if (valuePropertyExpr != null)
                {
                    var expr = (MemberExpression)valuePropertyExpr.Body;
                    Value = (PropertyInfo)expr.Member;
                }

                OnStepSuccess = OnSuccess;
                OnStepError = OnError;
            }
            private T GetValue(CharacterCreationState state)
            {
                return (T)Value.GetValue(state);
            }
            private void SetValue(CharacterCreationState state, T val)
            {
                Value.SetValue(state, val);
            }
            public override string GetPrompt(CharacterCreationState state)
            {
                return $@"""{GetValue(state)}""... is that correct? [**y**es/**n**o]";
            }
            private void OnSuccess(CharacterCreationState state, Match match)
            {
                if (match.Groups[1].Value != "")
                {
                    ++state.CurrentStep;
                }
                else
                {
                    this.SetValue(state, default(T));
                    --state.CurrentStep;
                }
            }
            private void OnError(CharacterCreationState state, string input)
            {
                state.Player.Message($@"Invalid input. Expected: y/yes/n/no");
            }
            public override bool TryParseInput(CharacterCreationState state, string input)
            {
                base.TryParseInput(state, input);
                return false;
            }
        }
    }
}
