using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public Utils.Graph graph;

    private Utils.Node enemyNode;
    private Utils.Node playerNode;
    public float speed =2;
    public List<Transform> listOfNodePos; 

    void Start() {
        //enemyNode = FindClosestNode(transform.position);
        //playerNode = FindClosestNode(player.position);
        enemyNode= new Utils.Node(transform.position);
        playerNode = new Utils.Node(player.transform.position);
        graph= new Utils.Graph();
        for(int i=0;i<listOfNodePos.Count;i++){
           graph.AddNode(listOfNodePos[i].position);
        }
        graph.nodes.Add(enemyNode);
        graph.nodes.Add(playerNode);
    }
    public void ConnectNodes(List<Utils.Node> nodes,Utils.Graph graph) {
        for (int i = 0; i < nodes.Count; i++) {
            for (int j = i + 1; j < nodes.Count; j++) {
                Utils.Node nodeA = nodes[i];
                Utils.Node nodeB = nodes[j];

                if (nodeA != nodeB) {
                    float distance = Vector3.Distance(nodeA.position, nodeB.position);
                    graph.AddEdge(nodeA,nodeB,distance);
                }
            }
        }
    }

    void Update() {
        enemyNode.position=transform.position;
        playerNode.position= player.transform.position;
        ConnectNodes(graph.nodes,graph);
        List<Utils.Node> path = Dijkstra(enemyNode, playerNode);

        if (path != null) {
            MoveAlongPath(path);
        }
        if(Input.GetKeyDown(KeyCode.F)){

        }
        Debug.Log(FindClosestNode(player.position).position);
    }

    private Utils.Node FindClosestNode(Vector3 position) {
        Utils.Node closestNode = null;
        float closestDistance = Mathf.Infinity;

        foreach (Utils.Node node in graph.nodes) {
            float distance = Vector3.Distance(node.position, position);
            if (distance < closestDistance) {
                closestNode = node;
                closestDistance = distance;
            }
        }

        return closestNode;
    }

    private List<Utils.Node> Dijkstra(Utils.Node start, Utils.Node end) {
        graph.ClearNodes();

        start.distance = 0;
        PriorityQueue<Utils.Node> queue = new PriorityQueue<Utils.Node>();
        queue.Enqueue(start, start.distance);

        while (queue.Count > 0) {
            Utils.Node node = queue.Dequeue();

            if (node == end) {
                return GetPath(end);
            }

            if (node.visited) {
                continue;
            }

            node.visited = true;

            foreach (Utils.Edge edge in node.edges) {
                float distance = node.distance + edge.weight;

                if (distance < edge.end.distance) {
                    edge.end.distance = distance;
                    edge.end.previous = node;

                    if (!edge.end.visited) {
                        queue.Enqueue(edge.end, distance);
                    }
                }
            }
        }

        return null;
    }

    private List<Utils.Node> GetPath(Utils.Node end) {
        List<Utils.Node> path = new List<Utils.Node>();

        while (end.previous != null) {
            path.Add(end);
            end = end.previous;
        }

        path.Reverse();

        return path;
    }
    private void MoveAlongPath(List<Utils.Node> path) {
        if (path.Count > 0) {
            // Get the direction to the first node in the path
            Vector3 direction = (path[0].position - transform.position).normalized;

            // Move the enemy towards the first node in the path
            transform.position += direction * speed * Time.deltaTime;

            // If the enemy has reached the first node in the path, remove it from the path
            if (Vector3.Distance(transform.position, path[0].position) < 0.1f) {
                path.RemoveAt(0);
            }
        }
    }
    public class PriorityQueue<T>
{
    private List<KeyValuePair<T, float>> elements = new List<KeyValuePair<T, float>>();

    public int Count { get { return elements.Count; } }

    public void Enqueue(T item, float priority)
    {
        elements.Add(new KeyValuePair<T, float>(item, priority));
    }

    public T Dequeue()
    {
        int bestIndex = 0;

        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].Value < elements[bestIndex].Value)
            {
                bestIndex = i;
            }
        }

        T bestItem = elements[bestIndex].Key;
        elements.RemoveAt(bestIndex);
        return bestItem;
    }
}

}
