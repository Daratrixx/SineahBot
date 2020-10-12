using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data.Templates
{
    public static class Books
    {
        public static Display SineahHistory = Display.Book("History of Sineah", "A very old looking books, labelled \"*History of Sineah*\" by __Bishop Ernaldz__",
        // page 1
        $"> The story of Sineah emerges from the story of __Freinort__. __Freinort__ was a small community of pagan worshipers. " +
        $"They built shrines and temples to honor their primitive deities, and originally were a positive force in the region, helping nearby villages with their knowledge, magic, and miracles. " +
        $"Their warriors were protecting the land from local monsters, and they even pushed back the kobold hordes for decades, all on their own.",
        // page 2
        "> Over time, their beliefs shifted, and their cult took a darker turn. Outdoor shrines went vacant as more and more underground rooms were dug, scaring the earth they once honored. " +
        "The memory of their own ancestors became more important than the balance of nature. The __Freinort artefacts__ were twisted. Soon, they were able to harness the power of the dead. " +
        "And as they were becoming even more powerful, the region gradually fell into chaos. " +
        "It all peaked when they started filling their __ossuary__ with the bones of massacred inhabitants from the nearby villages, planning on using them to raise an army of the dead.",
        // page 3
        "> That's when the glorious northern __Holy Church__ intervened. They fought against their undead minions and slayed their unholy cultists. " +
        "Their underground rooms were buried, their altars shattered, and their faith was no more. " +
        "To restore balance in the region, and save the remaining humans from chaos, they erected a large fortress or the fuming rubbles of __Freinort__. " +
        "This fortress expanded over time, and became the great city-state of Sineah, keeping peace in this part of the world, and holding the front against the ever-growing kobold hordes."
        );

        public static Display PurgeTheUndead = Display.Book("Purge the undead", "",
        $@"> **Undead** are a known but not so common menace. The best course of action when facing the undead is to let a servitor of the gods smite them back to their grave.
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
");

        public static Display CityCritters = Display.Book("City critters", "",
        $@"> Many critters populate our cities. Here's a rundown on what you can epxect to encounter:
> - **Rats** are a fairly common pest that you will find in all the cities of the world. They are most likely to live in the sewers. Careful, some of them might carry dangerous diseases.
");
        public static Display Gods = Display.Book("Gods and deities", "",
        $@"> Soon™");
        public static Display SineahRulers = Display.Book("Sineah rulers", "",
        $@"> Soon™");
        public static Display ListarothGeography = Display.Book("Listaroth Geography", "",
        $@"> Soon™");
    }
}
