using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{
    List<PathNode> openPathNodes = new List<PathNode>();
    List<PathNode> closedPathNodes = new List<PathNode>();

    [SerializeField] float targetNodeTreshold;

    internal void MoveAlongPath(List<Vector3> path)
    {
        if (path.Count > 0)
        {
            StartCoroutine(MoveCoroutine(path));
        }
    }

    private IEnumerator MoveCoroutine(List<Vector3> path)
    {
        foreach (Vector3 node in path)
        {
            while (Vector3.Distance(transform.position, node) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, node, 0.01f);
                yield return null;
            }
        }
    }

    internal List<Vector3> FindPath(PathNode startNode, PathNode targetNode)
    {
        openPathNodes.Clear();
        closedPathNodes.Clear();

        openPathNodes.Add(startNode);


        while (openPathNodes.Count > 0)
        {
            PathNode current = openPathNodes[0];

            for (int i = 0; i < openPathNodes.Count; i++)
            {
                if (openPathNodes[i].FCost < current.FCost ||
                    openPathNodes[i].FCost == current.FCost && openPathNodes[i].hCost < current.hCost)
                {
                    current = openPathNodes[i];
                }
            }

            openPathNodes.Remove(current);
            closedPathNodes.Add(current);

            if (MathF.Abs(current.location.magnitude - targetNode.location.magnitude) < targetNodeTreshold)
            {
                targetNode = current;
                return ReconstructPath(startNode, targetNode);
            }

            foreach (PathNode neighbor in GetNeighbors(current))
            {
                if (closedPathNodes.Contains(neighbor))
                    continue;

                float movementCostToNeighbor = current.gCost + GetDistance(current, neighbor);

                if (movementCostToNeighbor < neighbor.gCost || !openPathNodes.Contains(neighbor))
                {
                    neighbor.gCost = movementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = current;

                    if (!openPathNodes.Contains(neighbor))
                        openPathNodes.Add(neighbor);
                }
            }
        }

        return null;
    }

    private float GetDistance(PathNode current, PathNode neighbor)
    {
        return Vector3.Distance(current.location, neighbor.location);
    }

    private List<PathNode> GetNeighbors(PathNode current)
    {
        List<PathNode> neighbors = new List<PathNode>();

        Vector3[] directions =
        {
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, -1),
            new Vector3(1, 0, 1),
            new Vector3(-1, 0, 1),
            new Vector3(1, 0, -1),
            new Vector3(-1, 0, -1)
        };

        foreach (Vector3 direction in directions)
        {
            Vector3 neighborPosition = current.location + direction;

            if (!IsNeighborAGameObject(current, direction))
            {
                PathNode neighborNode = new PathNode(neighborPosition);
                neighbors.Add(neighborNode);
            }
        }

        return neighbors;
    }

    private bool IsNeighborAGameObject(PathNode current, Vector3 direction)
    {
        return Physics.Raycast(current.location, direction, 1.0f);
    }

    private List<Vector3> ReconstructPath(PathNode startNode, PathNode targetNode)
    {
        List<Vector3> path = new List<Vector3>();
        PathNode current = targetNode;

        while (current != startNode)
        {
            path.Add(current.location);
            current = current.parent;
        }

        path.Reverse();
        return path;
    }
}
