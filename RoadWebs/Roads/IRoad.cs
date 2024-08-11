using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadWebs.Roads;

public interface IRoad
{
    string Symbol { get; }
    Roads LeftWay { get; }
    Roads TopWay{ get; }
    Roads RightWay { get; }
    Roads BottomWay { get; }
    bool Station { get; }
    string Rotate();
}

public enum Roads
{
    Rails,
    Road,
    None
}