// -----------------------------------------------------------------------
// <copyright file="TileMap.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace StrategyRPG.TileEngine
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Represents a map of tiles.
    /// </summary>
    public class TileMap
    {
        private Texture2D _mouseMap;

        /// <summary>
        /// Gets the rows.
        /// </summary>
        public List<MapRow> Rows { get; private set; }

        /// <summary>
        /// Gets the width of the map.
        /// </summary>
        public int MapWidth { get; private set; }

        /// <summary>
        /// Gets the height of the map.
        /// </summary>
        public int MapHeight { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TileMap"/> class.
        /// </summary>
        /// <param name="mouseMap">The mouse map.</param>
        public TileMap(Texture2D mouseMap) : this(null, null, null, mouseMap) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TileMap&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="mapWidth">Width of the map.</param>
        /// <param name="mapHeight">Height of the map.</param>
        /// <param name="mapData">The map data.</param>
        /// <param name="mouseMap">The mouse map.</param>
        public TileMap(int? mapWidth, int? mapHeight, MapRow[] mapData, Texture2D mouseMap)
        {
            _mouseMap = mouseMap;

            Rows = new List<MapRow>();

            if (mapWidth.HasValue)
            { MapWidth = mapWidth.Value; }
            else
            { MapWidth = 25; }

            if (mapHeight.HasValue)
            { MapHeight = mapHeight.Value; }
            else { MapHeight = 40; }

            List<int> _dirtCells = new List<int>
            {
                0, 1, 0, 1, 0, 1, 0, 1,0, 1, 0, 1,  6,
            };

            Random _rand = new Random();

            for (int y = 0; y < MapHeight; y++)
            {
                MapRow thisRow = new MapRow();
                for (int x = 0; x < MapWidth; x++)
                {
                    thisRow.Columns.Add(new MapCell { TileID = _dirtCells.OrderBy(d => _rand.Next()).First() });
                }
                Rows.Add(thisRow);
            }

            // Create Sample Map Data

            if (mapData == null)
            {
                GenerateMapData();

                XElement xmlData = new XElement("Asset",
                    new XAttribute("Type", typeof(MapRow[]).FullName),
                    from r in Rows
                    where Rows.IndexOf(r) < 20
                    select new XElement("Item",
                        new XElement("Columns",
                            from c in r.Columns
                            where r.Columns.IndexOf(c) < 10
                            select new XElement("Item",
                                new XElement("TileID", c.TileID),
                                new XElement("HeightTiles",
                                    String.Join(Environment.NewLine, c.HeightTiles.Select(d => d.ToString()).ToArray())
                                ),
                                new XElement("TopperTiles",
                                    String.Join(Environment.NewLine, c.TopperTiles.Select(d => d.ToString()).ToArray())
                                )
                            )
                        )
                    )
                );

                // Generate Map Data
                var stringData = xmlData.ToString();
            }
            else
            {
                // Load Map Data from XML

                for (int i = 0; i < mapData.Count(); i++)
                {
                    for (int j = 0; j < mapData[i].Columns.Count; j++)
                    {
                        Rows[i].Columns[j].TileID = mapData[i].Columns[j].TileID;
                        Rows[i].Columns[j].HeightTiles = mapData[i].Columns[j].HeightTiles;
                        Rows[i].Columns[j].TopperTiles = mapData[i].Columns[j].TopperTiles;
                    }
                }
            }
        }

        private void GenerateMapData()
        {
            Rows[0].Columns[3].TileID = 3;
            Rows[0].Columns[4].TileID = 3;
            Rows[0].Columns[5].TileID = 1;
            Rows[0].Columns[6].TileID = 1;
            Rows[0].Columns[7].TileID = 1;

            Rows[1].Columns[3].TileID = 3;
            Rows[1].Columns[4].TileID = 1;
            Rows[1].Columns[5].TileID = 1;
            Rows[1].Columns[6].TileID = 1;
            Rows[1].Columns[7].TileID = 1;

            Rows[2].Columns[2].TileID = 3;
            Rows[2].Columns[3].TileID = 1;
            Rows[2].Columns[4].TileID = 1;
            Rows[2].Columns[5].TileID = 1;
            Rows[2].Columns[6].TileID = 1;
            Rows[2].Columns[7].TileID = 1;

            Rows[3].Columns[2].TileID = 3;
            Rows[3].Columns[3].TileID = 1;
            Rows[3].Columns[4].TileID = 1;
            Rows[3].Columns[5].TileID = 2;
            Rows[3].Columns[6].TileID = 2;
            Rows[3].Columns[7].TileID = 2;

            Rows[4].Columns[2].TileID = 3;
            Rows[4].Columns[3].TileID = 1;
            Rows[4].Columns[4].TileID = 1;
            Rows[4].Columns[5].TileID = 2;
            Rows[4].Columns[6].TileID = 2;
            Rows[4].Columns[7].TileID = 2;

            Rows[5].Columns[2].TileID = 3;
            Rows[5].Columns[3].TileID = 1;
            Rows[5].Columns[4].TileID = 1;
            Rows[5].Columns[5].TileID = 2;
            Rows[5].Columns[6].TileID = 2;
            Rows[5].Columns[7].TileID = 2;

            Rows[16].Columns[4].AddHeightTile(54);

            Rows[17].Columns[3].AddHeightTile(54);

            Rows[15].Columns[3].AddHeightTile(54);
            Rows[16].Columns[3].AddHeightTile(53);

            Rows[15].Columns[4].AddHeightTile(54);
            Rows[15].Columns[4].AddHeightTile(54);
            Rows[15].Columns[4].AddHeightTile(51);

            Rows[18].Columns[3].AddHeightTile(51);
            Rows[19].Columns[3].AddHeightTile(50);
            Rows[18].Columns[4].AddHeightTile(55);

            Rows[14].Columns[4].AddHeightTile(54);

            Rows[14].Columns[5].AddHeightTile(62);
            Rows[14].Columns[5].AddHeightTile(61);
            Rows[14].Columns[5].AddHeightTile(63);

            Rows[17].Columns[4].AddTopperTile(114);
            Rows[16].Columns[5].AddTopperTile(115);
            Rows[14].Columns[4].AddTopperTile(125);
            Rows[15].Columns[5].AddTopperTile(91);
            Rows[16].Columns[6].AddTopperTile(94);            
        }

        /// <summary>
        /// Worlds to map cell.
        /// </summary>
        /// <param name="worldPoint">The world point.</param>
        /// <param name="localPoint">The local point.</param>
        /// <returns></returns>
        public Point WorldToMapCell(Point worldPoint, out Point localPoint)
        {
            Point mapCell = new Point(
               (int)(worldPoint.X / _mouseMap.Width),
               ((int)(worldPoint.Y / _mouseMap.Height)) * 2
               );

            int localPointX = worldPoint.X % _mouseMap.Width;
            int localPointY = worldPoint.Y % _mouseMap.Height;

            int dx = 0;
            int dy = 0;

            uint[] myUint = new uint[1];

            if (new Rectangle(0, 0, _mouseMap.Width, _mouseMap.Height).Contains(localPointX, localPointY))
            {
                _mouseMap.GetData(0, new Rectangle(localPointX, localPointY, 1, 1), myUint, 0, 1);

                if (myUint[0] == 0xFF0000FF) // Red
                {
                    dx = -1;
                    dy = -1;
                    localPointX = localPointX + (_mouseMap.Width / 2);
                    localPointY = localPointY + (_mouseMap.Height / 2);
                }

                if (myUint[0] == 0xFF00FF00) // Green
                {
                    dx = -1;
                    localPointX = localPointX + (_mouseMap.Width / 2);
                    dy = 1;
                    localPointY = localPointY - (_mouseMap.Height / 2);
                }

                if (myUint[0] == 0xFF00FFFF) // Yellow
                {
                    dy = -1;
                    localPointX = localPointX - (_mouseMap.Width / 2);
                    localPointY = localPointY + (_mouseMap.Height / 2);
                }

                if (myUint[0] == 0xFFFF0000) // Blue
                {
                    dy = +1;
                    localPointX = localPointX - (_mouseMap.Width / 2);
                    localPointY = localPointY - (_mouseMap.Height / 2);
                }
            }

            mapCell.X += dx;
            mapCell.Y += dy - 2;

            localPoint = new Point(localPointX, localPointY);

            return mapCell;
        }

        /// <summary>
        /// Worlds to map cell.
        /// </summary>
        /// <param name="worldPoint">The world point.</param>
        /// <returns></returns>
        public Point WorldToMapCell(Point worldPoint)
        {
            Point dummy;
            return WorldToMapCell(worldPoint, out dummy);
        }
    }
}
