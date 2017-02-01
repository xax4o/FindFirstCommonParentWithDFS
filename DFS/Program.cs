namespace DFS
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static HashSet<Node> usedNodes = new HashSet<Node>();
        public static Dictionary<string, Node> nodes = new Dictionary<string, Node>();

        public static void Main()
        {
            ReadInput();

            Console.Write("Enter node: ");
            var firstNode = Console.ReadLine();

            Console.Write("Enter node 2: ");
            var secondNode = Console.ReadLine();

            var rootNode = GetRootNode(nodes);
            var result = GetMinimumCommonNode(rootNode, firstNode, secondNode);
        
            Console.WriteLine("Searched node is: " + result.Name);
        }

        public static void ReadInput()
        {
            var N = int.Parse(Console.ReadLine());

            for (int i = 0; i < N; i++)
            {
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

        public static Node GetMinimumCommonNode(Node startNode, string firstSearchedString, string secondSearchedString)
        {
            int index = 0;

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

        public static Node GetRootNode(IDictionary<string, Node> nodes)
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
