using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace GraphStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            Node Root = new Node("Eleja");
            //Console.WriteLine(Root.FindNode("Eleja").title);
            Root.FindNode("Eleja").AddEdgeNode(22 , "Jelgava");
            //Console.WriteLine(Root.FindNode("Jelgava").title);
            Root.FindNode("Jelgava").AddEdgeNode(6, "Ozolnieki");
            //Console.WriteLine(Root.FindNode("Ozolnieki").title);
            Root.FindNode("Ozolnieki").AddEdge(7, "Jelgava");
            //Console.WriteLine(Root.FindNode("Ozolnieki").title);
            Root.FindNode("Jelgava").AddEdgeNode(30, "Dobele");

            Console.WriteLine(Root.FindNode("Ozolnieki").title);
            Console.WriteLine(Root.FindNode("Dobele").title);

            //Console.WriteLine(Root.FindPrice("Dobele"));

            Root.FindNode("Dobele").AddEdgeNode(89, "Olaine");

            Root.FindNode("Olaine").AddEdgeNode(100, "Bauska");

            Console.WriteLine(Root.FindPrice("Bauska"));

            Console.WriteLine(Root.FindNode("Bauska").title);

            //Console.WriteLine(Root.FindNode("Eleja").title);
            //Console.WriteLine(Root.FindNode("Jelgava").title);
            //Root.FindNode("Dobele").AddEdge(31, "Jelgava");
            //Root.FindNode("Jelgava").AddEdgeNode(48, "Riga");
            //Root.FindNode("Riga").AddEdge(45, "Jelgava");

        }
    }

    class Edge
    {
        public int Weight { get; set; }
        public Node Destination;

        public Edge(int Weight, Node Destination)
        {
            this.Weight = Weight;
            this.Destination = Destination;
        }
    }
    
    class Node
    {
        public string title;
        public List<Edge> edges;

        public Node(string title)
        {
            edges = new List<Edge>();
            this.title = title;
        }

        public void AddEdge(int Weight, string destName)
        {
            edges.Add(new Edge(Weight, FindNode(destName)));
        }

        public void AddEdgeNode(int Weight, string destName)
        {
            edges.Add(new Edge(Weight, new Node(destName)));
        }

        public bool CheckVisited(List<int> visited)
        {
            foreach (int item in visited)
            {
                if (item == this.GetHashCode())
                {
                    return true;
                }
            }
            
            return false;
        }

        public Node FindNode(string title, List<int> visited = null)
        {
            if (visited == null)
            {
                visited = new List<int>();
            }

            visited.Add(this.GetHashCode());

            if (this.title == title)
            {
                return this;
            }

            Node searchNode = null;

            foreach (Edge edge in edges)
            {
                if (edge != null)
                {
                    if (edge.Destination != null)
                    {
                        if (!edge.Destination.CheckVisited(visited))
                        {
                            //Console.WriteLine($"Searching in {this.title} trying to find {title} in {edge.Destination.title}");
                            searchNode = edge.Destination.FindNode(title, visited);
                        }
                    }
                }

                if (searchNode != null)
                {
                    if (searchNode.title == title)
                    {
                        return searchNode;
                    }
                }
            }
            
            return null;
        }

        public int FindPrice(string title, List<int> visited = null, int weight = 0)
        {
            int price = 0;

            if (visited == null)
            {
                visited = new List<int>();
            }

            visited.Add(this.GetHashCode());

            price += weight;

            if (this.title == title)
            {
                return price;
            }

            foreach (Edge edge in edges)
            {
                if (edge != null)
                {
                    if (edge.Destination != null)
                    {
                        if (!edge.Destination.CheckVisited(visited))
                        {
                            //Console.WriteLine($"Searching in {this.title} trying to find {title} in {edge.Destination.title}");
                            price += edge.Destination.FindPrice(title, visited, edge.Weight);
                        }
                    }
                }
            }

            Console.WriteLine("Total price is "+ price);
            return price;
        }
    }
}
