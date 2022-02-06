namespace SineahBot.Data.Enums
{
    public enum AlterationType
    {
        Burning, // get damaged over time
        Burnt, // get reduced healing
        Bleeding, // get damaged over time
        Poisoned, // get reduced mana regen
        Sickness, // get damaged over time
        Weakened, // deal reduced damage
        Blind, // can't see other entities, room descriptions, or directions
        Deaf, // can't hear character talking, or access being locked/unlocked
        Frenzied, // will attack a random target every few seconds
        Taunted, // will attack the taunting target every few seconds
        Sleeping, // can't perceive anything nor act, regenerates faster
        Stunned, // can't perceive anything nor act
        Corroded, // reduced armor

        Invisible, // can't be seen by other characters
        Shrouded, // hides identity
        Warded, // get reduced magic damage and heal
        Amplified, // deal increased magic damage and heal
        Hardened, // get reduced physical damage
        Empowered, // deal increased physical damage
    }
}
