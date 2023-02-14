using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public class Node {
        public Vector3 position;
        public List<Edge> edges;
        public float distance;
        public bool visited;
        public Node previous;

        public Node(Vector3 position) {
            this.position = position;
            this.edges = new List<Edge>();
            this.distance = Mathf.Infinity;
            this.visited = false;
            this.previous = null;
        }
    }

    public class Edge {
        public Node start;
        public Node end;
        public float weight;

        public Edge(Node start, Node end, float weight) {
            this.start = start;
            this.end = end;
            this.weight = weight;
        }
    }
    public class Graph {
        public List<Node> nodes;

        public Graph() {
            this.nodes = new List<Node>();
        }

        public void AddNode(Vector3 position) {
            nodes.Add(new Node(position));
        }

        public void AddEdge(Node start, Node end, float weight) {
            start.edges.Add(new Edge(start, end, weight));
        }

        public void ClearNodes() {
            foreach (Node node in nodes) {
                node.distance = Mathf.Infinity;
                node.visited = false;
                node.previous = null;
            }
        }
    }
}
