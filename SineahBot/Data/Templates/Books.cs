using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data.Templates
{
    public static class Books
    {
        public static Display SineahHistoryPart1 = new Display("The history of Sineah (Pt.1)", new string[] { "the history sineah part 1",
        "history sineah part 1", "sineah part 1",
        "history sineah pt 1", "sineah pt 1",
        "history sineah 1", "sineah 1" ,
        "history of sineah",
        "history sineah", "sineah" })
        {
            description = @"""The history of Sineah (Pt.1)"" thrones here.",
            content = $@"> The history is not that long and not so complicated but I don't have time to write it down for the test :v Shouldn't get up 3 hours before it starts uh ?
",
        };
        public static Display PurgeTheUndead = new Display("Purge the undead", new string[] { "purge undead",
        "purgetheundead", "purgeundead",
        "purge", "undead" })
        {
            description = @"""Purge the undead"" thrones here.",
            content = $@"> **Undead** are a known but not so common menace. The best course of action when facing the undead is to let a servitor of the gods smite them back to their grave.
> However, if you can't afford to wait for a priest, here's a few points to help you stay alive:
> - Undead are very vulnerable to fire. Most will burn to a crisp within a minute once set ablaze.
> - Undead cannot get sick, poisoned, weakened, taunted, frenzied, nor stunned.
> - If you are able to use any kind of healing magic, using it on undead will actually hurt them.
> There are multiple types of undead creatures, listed below from less dangerous to most dangerous:
> - Skeleton
> - Zombi
> - Ghost
> - Ghoul
> - Banshee
> - Lich
> None of the aformentioned undead creature can be defeated by a non-veteran adventurer.
",
        };
        public static Display CityCritters = new Display("City critters", new string[] { "citycritters",
        "critters" })
        {
            description = @"""Critters"" thrones here.",
            content = $@"> Many critters populate our cities. Here's a rundown on what you can epxect to encounter:
> - **Rats** are a fairly common pest that you will find in all the cities of the world. They are most likely to live in the sewers. Careful, some of them might carry dangerous diseases.
",
        };
    }
}
