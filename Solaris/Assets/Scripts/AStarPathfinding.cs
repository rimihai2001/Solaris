using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding
{
    private class Node
    {
        public Vector3 Position;
        public float GScore;
        public float FScore;
        public Node CameFrom;

        public Node(Vector3 position)
        {
            Position = position;
            GScore = float.MaxValue;
            FScore = float.MaxValue;
        }
    }

    public static List<Vector3> FindPath(Vector3 start, Vector3[] asteroids)
    {
        var openSet = new List<Node>();
        var closedSet = new List<Node>();
        var startNode = new Node(start);
        startNode.GScore = 0;
        startNode.FScore = Heuristic(start, asteroids);
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            var current = GetNodeWithLowestFScore(openSet);

            if (AllAsteroidsHit(current.Position, asteroids))
            {
                return ReconstructPath(current);
            }

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (var neighbor in GetNeighbors(current.Position))
            {
                if (closedSet.Contains(neighbor))
                {
                    continue;
                }

                var tentativeGScore = current.GScore + Vector3.Distance(current.Position, neighbor.Position);

                if (!openSet.Contains(neighbor))
                {
                    openSet.Add(neighbor);
                }
                else if (tentativeGScore >= neighbor.GScore)
                {
                    continue;
                }

                neighbor.CameFrom = current;
                neighbor.GScore = tentativeGScore;
                neighbor.FScore = tentativeGScore + Heuristic(neighbor.Position, asteroids);
            }
        }

        return null;
    }

    private static bool AllAsteroidsHit(Vector3 position, Vector3[] asteroids)
    {
        foreach (var asteroid in asteroids)
        {
            if (Vector3.Distance(position, asteroid) > 1)
            {
                return false;
            }
        }

        return true;
    }

    private static float Heuristic(Vector3 position, Vector3[] asteroids)
    {
        float maxDistance = 0;

        foreach (var asteroid in asteroids)
        {
            var distance = Vector3.Distance(position, asteroid);

            if (distance > maxDistance)
            {
                maxDistance = distance;
            }
        }

        return maxDistance;
    }

    private static List<Node> GetNeighbors(Vector3 position)
    {
        var neighbors = new List<Node>();
        neighbors.Add(new Node(position + Vector3.forward));
        neighbors.Add(new Node(position + Vector3.back));
        neighbors.Add(new Node(position + Vector3.left));
        neighbors.Add(new Node(position + Vector3.right));
        neighbors.Add(new Node(position + Vector3.up));
        neighbors.Add(new Node(position + Vector3.down));
        return neighbors;
    }

    private static Node GetNodeWithLowestFScore(List<Node> nodes)
    {
        var lowestScore = float.MaxValue;
        Node lowestNode = null;

        foreach (var node in nodes)
        {
            if (node.FScore < lowestScore)
            {
                lowestScore = node.FScore;
                lowestNode = node;
            }
        }

        return lowestNode;
    }

    private static List<Vector3> ReconstructPath(Node node)
    {
        var path = new List<Vector3>();
        var current = node;

        while (current != null)
        {
            path.Add(current.Position);
            current = current.CameFrom;
        }

        path.Reverse();
        return path;
    }
}
