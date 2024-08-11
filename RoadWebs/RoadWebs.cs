using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadWebs;

public class RoadWebs
{
    private Cell[][] field;

    private Cell[][] dices;

    private Cell[] cellsToBuild;

    private int _roundNow;

    private int _activeCellNumber;

    private int _activeCellX;

    private int _activeCellY;

    public RoadWebs()
    {
        field = [
            GetSystemRowCell(),
            [new Cell("[]") { system = true },  new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]") { system = true }],
            [GetSystemHorizontalStraightRail(), new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]"), GetSystemHorizontalStraightRail()],
            [new Cell("[]") { system = true },  new Cell("[]"), new Cell("[]"), new Cell("{}"), new Cell("{}"), new Cell("{}"), new Cell("[]"), new Cell("[]"), new Cell("[]") { system = true }],
            [GetSystemHorizontalStraightRoad(), new Cell("[]"), new Cell("[]"), new Cell("{}"), new Cell("{}"), new Cell("{}"), new Cell("[]"), new Cell("[]"), GetSystemHorizontalStraightRoad()],
            [new Cell("[]") { system = true },  new Cell("[]"), new Cell("[]"), new Cell("{}"), new Cell("{}"), new Cell("{}"), new Cell("[]"), new Cell("[]"), new Cell("[]") { system = true }],
            [GetSystemHorizontalStraightRail(), new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]"), GetSystemHorizontalStraightRail()],
            [new Cell("[]") { system = true },  new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]"), new Cell("[]") { system = true }],
            GetSystemRowCell(),
        ];

        _roundNow = 1;
        _activeCellNumber = 0;

        dices = GetRoadsDices();
    }

    public string SetActiveRoad(int number)
    {
        _activeCellNumber = number;
        return cellsToBuild[_activeCellNumber].symbol;
    }

    public string TrySetActiveSection(int x, int y)
    {
        return String.Empty;
    }

    public bool TryBuildRoad(int x, int y, out string error)
    {
        if (cellsToBuild[_activeCellNumber].isBuilded)
        {
            error = "Активная дорога уже построена";
            return false;
        }

        //check place


        if (field[x][y].symbol != "{}" && field[x][y].symbol != "[]")
        {
            if (field[x][y].roundNumber != _roundNow)
            {
                error = "Место строительства занято неразрушимой дорогой";
                return false;
            }
            field[x][y].isBuilded = false;
        }

        cellsToBuild[_activeCellNumber].isBuilded = true;
        field[x][y] = cellsToBuild[_activeCellNumber];
        
        error = string.Empty;
        return true;
    }



    public string Rotate()
    {
        return cellsToBuild[_activeCellNumber].Rotate();
    }

    private Cell[][] GetRoadsDices()
    {
        return [
            [new Cell() { symbol = "─", variants = ["─", "│"], station = false, leftWay = Roads.Roads.Rails, topWay = Roads.Roads.None, rightWay = Roads.Roads.Rails, bottomWay = Roads.Roads.None }],
            [new Cell() { symbol = "┌", variants = ["┌", "┐", "┘", "└"], station = false, leftWay = Roads.Roads.None, topWay = Roads.Roads.None, rightWay = Roads.Roads.Rails, bottomWay = Roads.Roads.Rails }],
            [new Cell() { symbol = "║", variants = ["║", "═"], station = false, leftWay = Roads.Roads.None, topWay = Roads.Roads.Road, rightWay = Roads.Roads.None, bottomWay = Roads.Roads.Road }],
            [new Cell() { symbol = "╔", variants = ["╔", "╗", "╝", "╚"], station = false, leftWay = Roads.Roads.None, topWay = Roads.Roads.None, rightWay = Roads.Roads.Road, bottomWay = Roads.Roads.Road }],
            [new Cell() { symbol = "╠", variants = ["╠", "╦", "╣", "╩"], station = false, leftWay = Roads.Roads.None, topWay = Roads.Roads.Road, rightWay = Roads.Roads.Road, bottomWay = Roads.Roads.Road }],
            [new Cell() { symbol = "├", variants = ["├", "┬", "┤", "┴"], station = false, leftWay = Roads.Roads.None, topWay = Roads.Roads.Rails, rightWay = Roads.Roads.Rails, bottomWay = Roads.Roads.Rails }]
            ]; 
    }

    private Cell[] GetSystemRowCell()
    {
        return [
                new Cell("[]") { system = true },
                new Cell("[]") { system = true },
                new Cell("║") { system = true, leftWay = Roads.Roads.None, topWay = Roads.Roads.Road, rightWay = Roads.Roads.None, bottomWay = Roads.Roads.Road },
                new Cell("[]") { system = true },
                new Cell("│") { system = true, leftWay = Roads.Roads.None, topWay = Roads.Roads.Rails, rightWay = Roads.Roads.None, bottomWay = Roads.Roads.Rails },
                new Cell("[]") { system = true },
                new Cell("║") { system = true, leftWay = Roads.Roads.None, topWay = Roads.Roads.Road, rightWay = Roads.Roads.None, bottomWay = Roads.Roads.Road },
                new Cell("[]") { system = true },
                new Cell("[]") { system = true },
            ];
    }

    private Cell GetSystemHorizontalStraightRail()
    {
        return new Cell() 
        { 
            symbol = "─", 
            variants = ["─", "│"], 
            system = true, 
            station = false, 
            leftWay = Roads.Roads.Rails, 
            topWay = Roads.Roads.None, 
            rightWay = Roads.Roads.Rails, 
            bottomWay = Roads.Roads.None 
        };
    }

    private Cell GetSystemHorizontalStraightRoad()
    {
        return new Cell()
        {
            symbol = "═",
            variants = ["║", "═"],
            system = true,
            station = false,
            leftWay = Roads.Roads.Road,
            topWay = Roads.Roads.None,
            rightWay = Roads.Roads.Road,
            bottomWay = Roads.Roads.None
        };
    }

    private class Cell
    {
        public string[] variants;
        public string symbol;
        public int roundNumber;
        public bool isBuilded;
        public bool station;
        public bool system = false;
        public Roads.Roads leftWay = Roads.Roads.None;
        public Roads.Roads topWay = Roads.Roads.None;
        public Roads.Roads rightWay = Roads.Roads.None;
        public Roads.Roads bottomWay = Roads.Roads.None;
        public int indexVar;

        public Cell()
        {
            symbol = "[]";
            isBuilded = false;
            variants = new string[0];
            this.station = false;
            indexVar = 0;
        }
        public Cell(string symbol)
        {
            this.symbol = symbol;
            isBuilded = false;
            variants = new string[0];
            this.station = false;
            indexVar = 0;
        }

        public string Rotate()
        {
            indexVar++;
            if (indexVar == variants.Length)
                indexVar = 0;

            symbol = variants[indexVar];

            Roads.Roads container = leftWay;

            leftWay = bottomWay;
            bottomWay = rightWay;
            rightWay = topWay;
            topWay = container;

            return symbol;
        }
    }
}
