using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data.Path
{
    public interface INeighbor<Neighbor> where Neighbor: INeighbor<Neighbor>
    {
        public bool IsNeighbor(Neighbor b);
    }
}
