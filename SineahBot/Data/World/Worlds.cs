
using SineahBot.Tools;

namespace SineahBot.Data.World
{
    public static class Worlds
    {
        public static void LoadWorlds()
        {
            Sineah.LoadWorld();
            NerajiDesert.LoadWorld();
            Roads.LoadWorld();

            // after all the world parts have been loaded
            PathBuilder.BuildGraphs();
        }
    }
}
