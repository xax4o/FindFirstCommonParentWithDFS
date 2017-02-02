namespace DFS
{
    using System;
    using System.Collections.Generic;

    public class Startup
    {
        public static HashSet<Node> usedNodes = new HashSet<Node>();
        public static Dictionary<string, Node> nodes = new Dictionary<string, Node>();

        public static void Main()
        {
            ReadInput();

            Console.Write("Enter the two nodes in the format /FirstNode SecondNode/: ");
            var searchedNodes = Console.ReadLine().Split(' ');

            var firstNode = searchedNodes[0];
            var secondNode = searchedNodes[1];

            var rootNode = GetRootNode();
            var result = GetFirstCommonNode(rootNode, firstNode, secondNode);

            Console.WriteLine("The Root parrent of {0} and {1} is {2} ", firstNode, secondNode, result.Name);
        }

        public static void ReadInput()
        {
            Console.Write("Enter number of tree nodes: ");
            var N = int.Parse(Console.ReadLine());

            for (int i = 0; i < N; i++)
            {
                Console.Write("Enter tree node {0} in the format /Parrent Child/: ", i + 1);
                var connection = Console.ReadLine().Split(' ');

                var parent = connection[0];
                var child = connection[1];

                Node parentNode;
                Node childNode;

                if (nodes.ContainsKey(parent))
                {
                    parentNode = nodes[parent];
                }
                else
                {
                    parentNode = new Node(parent);
                    nodes.Add(parent, parentNode);
                }

                if (nodes.ContainsKey(child))
                {
                    childNode = nodes[child];
                }
                else
                {
                    childNode = new Node(child);
                    nodes.Add(child, childNode);
                }

                parentNode.AddChild(childNode);
                childNode.HasParent = true;
            }
        }

        public static Node GetFirstCommonNode(Node startNode, string firstSearchedString, string secondSearchedString)
        {
            var index = 0;

            while (true)
            {
                if (index < startNode.NumberOfChildren)
                {
                    DFS(startNode.GetChild(index));
                }
                else
                {
                    throw new ArgumentException("No such child");
                }

                if (usedNodes.Contains(nodes[firstSearchedString]) && usedNodes.Contains(nodes[secondSearchedString]))
                {
                    startNode = startNode.GetChild(index);
                    usedNodes.Clear();
                    index = -1;
                }
                else if (usedNodes.Contains(nodes[firstSearchedString]) || usedNodes.Contains(nodes[secondSearchedString]))
                {
                    return startNode;
                }
                else
                {
                    usedNodes.Clear();
                }

                index++;
            }

            throw new ArgumentException("No Common node");
        }

        public static Node GetRootNode()
        {
            foreach (var node in nodes)
            {
                if (!node.Value.HasParent)
                {
                    return node.Value;
                }
            }

            throw new ArgumentException("There is no root node");
        }

        public static void DFS(Node startNode)
        {
            usedNodes.Add(startNode);

            for (int i = 0; i < startNode.NumberOfChildren; i++)
            {
                DFS(startNode.GetChild(i));
            }
        }
    }
}
