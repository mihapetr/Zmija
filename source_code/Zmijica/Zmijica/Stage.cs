using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zmijica
{
    internal class Stage
    {
        string[] design;
        public readonly int width;
        List<Point> pts;

        public Stage(int spec)
        {
            pts = new List<Point>();
            int height = 0; // provjera je li dizajn kvadratan

            switch (spec)
            {
                case 1:
                    design = new string[]
                    {
                        "####..####",
                        "#........#",
                        "#........#",
                        "#........#",
                        "..........",
                        "..........",
                        "#........#",
                        "#........#",
                        "#........#",
                        "####..####"
                    };
                    break;

                case 2:
                    design = new string[]
                    {
                        "#########..#########",
                        "#..................#",
                        "#..................#",
                        "#..................#",
                        "#.....########.....#",
                        "#........##........#",
                        "#........##........#",
                        "#........##.........",
                        ".........##.........",
                        "####............####",
                        "####............####",
                        ".........##.........",
                        "#........##........#",
                        "#........##........#",
                        "#........##........#",
                        "#.....########.....#",
                        "#..................#",
                        "#..................#",
                        "#..................#",
                        "#########..#########"
                    };
                    break;

                case 3:
                    design = new string[]
                    {
                        "####...#############",
                        "#..................#",
                        "#..................#",
                        "#..................#",
                        "#..................#",
                        "###############....#",
                        "#.............#....#",
                        "#.............#....#",
                        "#.............#....#",
                        "........#######.....",
                        "........#...........",
                        "........#...........",
                        "#######.#..........#",
                        "#.......#..........#",
                        "#.......#..........#",
                        "#.......############",
                        "#..................#",
                        "#..................#",
                        "#..................#",
                        "####...#############"
                    };
                    break;

                default:
                    throw new Exception($"Level broja {spec} ne postoji.");
            }
            width = design[0].Length;
            height = design.Length;
            if (width != height) throw new Exception("");

            // dekodiranje dizajna -> pts
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (design[j][i] == '#') pts.Add(new Point(i, j));
                }
            }
        }

        public List<Point> getWalls()
        {
            return pts;
        }
    }
}
