using Microsoft.Xna.Framework;
using StoneShard_Mono.Content.Rooms;
using StoneShard_Mono.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace StoneShard_Mono.Utils
{
    public class WayFinder
    {
        public Func<Vector2, bool> _reachable;

        public Vector2 _start;

        public Vector2 _end;

        public Room _room;

        public WayFinder(Func<Vector2, bool> reachable, Vector2 start, Vector2 end, Room room)
        {
            _reachable = reachable;
            _start = start;
            _end = end;
            _room = room;
        }

        public List<Vector2> FindPath()
        {
            var closedSet = new HashSet<Vector2>();

            var openSet = new HashSet<Vector2>() { _start };

            var parent = new Dictionary<Vector2, Vector2>();

            var gScore = new Dictionary<Vector2, float>();

            var hScore = new Dictionary<Vector2, float>();

            for (int x = 0; x < _room.TileMapSize.X; x++)
            {
                for (int y = 0; y < _room.TileMapSize.Y; y++)
                {
                    gScore.Add(new(x, y), int.MaxValue);
                    hScore.Add(new(x, y), int.MaxValue);
                }
            }

            gScore[_start] = 0;
            hScore[_start] = HeuristicCostEstimate(_start, _end);

            while (openSet.Count > 0)
            {
                var current = hScore
                .Where(item => openSet.Contains(item.Key))
                .OrderBy(item => item.Value)
                .First().Key;
                if (current == _end)
                {
                    return ReconstructPath(parent, current);
                }

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (Vector2 neighbor in current.Neighbors())
                {
                    if (closedSet.Contains(neighbor))
                    {
                        continue;
                    }
                    if (neighbor.X < 0 || neighbor.X >= _room.TileMapSize.X || neighbor.Y < 0 || neighbor.Y >= _room.TileMapSize.Y)
                    {
                        continue;
                    }
                    if (!_reachable(neighbor))
                    {
                        closedSet.Add(neighbor);
                        continue;
                    }

                    float tentativeGScore = gScore[current] + Vector2.Distance(current, neighbor);

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                    else if (tentativeGScore >= gScore[neighbor]) continue;

                    parent[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    hScore[neighbor] = gScore[neighbor] + (HeuristicCostEstimate(neighbor, _end));
                }
            }

            return new();
        }

        private float HeuristicCostEstimate(Vector2 start, Vector2 end)
        {
            return Math.Abs(end.X - start.X) + Math.Abs(end.Y - start.Y);
        }

        private List<Vector2> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 goal)
        {
            Vector2 current = goal;
            var totalPath = new List<Vector2> { goal };
            while (cameFrom.ContainsKey(current))
            {
                Vector2 old = current;
                current = cameFrom[current];
                totalPath.Insert(0, current);
                cameFrom.Remove(old);
            }
            totalPath.RemoveAt(0);
            return totalPath;
        }
    }
}
