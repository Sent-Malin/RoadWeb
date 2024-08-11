using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadWebs.Roads;

public class StraightRails : IRoad
{
    public Roads LeftWay { get; private set; }
    public Roads TopWay { get; private set; }
    public Roads RightWay { get; private set; }
    public Roads BottomWay { get; private set; }
    public string Symbol { get; private set; }
    public bool Station { get; private set; }
    
    private string[] _variants;
    private int _indexVar;

    public StraightRails()
    {
        Symbol = "─";
        _variants = new string[] { "─", "│" };
        _indexVar = 0;
        LeftWay = Roads.Rails;
        TopWay = Roads.None;
        RightWay = Roads.Rails;
        BottomWay = Roads.None;
        Station = false;
    }

    public string Rotate()
    {
        _indexVar++;
        if (_indexVar == _variants.Length)
            _indexVar = 0;

        Symbol = _variants[_indexVar];

        

        LeftWay = Roads.None;
        TopWay = Roads.Rails;
        RightWay = Roads.None;
        BottomWay = Roads.Rails;

        return Symbol;
    }
}
