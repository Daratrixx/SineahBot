using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SineahBot.Data
{
    public class Entity : DataItem, IInteractable
    {
        public Guid currentRoomId;
        public void OnInteracted(IAgent agent, Interaction interaction)
        {
            /*switch (interaction)
            {
                case Interaction.Look:
                    if (this is IObservable) (this as IObservable).OnObserved(agent);
                    else agent.Message("Impossible to look at entity");
                    break;
                case Interaction.Attack:
                    if (this is IAttackable) (this as IAttackable).OnAttacked(agent);
                    else agent.Message("Impossible to attack at entity");
                    break;
                case Interaction.Carry:
                    if (this is ICarryable) (this as ICarryable).OnCarried(agent);
                    else agent.Message("Impossible to carry at entity");
                    break;
                case Interaction.Consume:
                    if (this is IConsumable) (this as IConsumable).OnConsumed(agent);
                    else agent.Message("Impossible to consume at entity");
                    break;
                case Interaction.Drop:
                    if (this is IDropable) (this as IDropable).OnDropped(agent);
                    else agent.Message("Impossible to drop at entity");
                    break;
                case Interaction.Use:
                    if (this is IUsable) (this as IUsable).OnUsed(agent);
                    else agent.Message("Impossible to use at entity");
                    break;
                case Interaction.Equip:
                    if (this is IEquipable) (this as IEquipable).OnEquipped(agent);
                    else agent.Message("Impossible to equip at entity");
                    break;
                default:break;
            }*/
        }
    }
}
