namespace DFS
{
    using System.Collections.Generic;

    public class Node
    {
        private IList<Node> children;

        public Node(string name)
        {
            this.Name = name;
            this.HasParent = false;
            this.children = new List<Node>();
        }

        public string Name { get; private set; }

        public bool HasParent { get; set; }

        public void AddChild(Node node)
        {
            this.children.Add(node);
        }

        public Node GetChild(int index)
        {
            return this.children[index];
        }

        public int NumberOfChildren
        {
            get
            {
                return this.children.Count;
            }
        }
    }
}
