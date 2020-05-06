using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

{
    class Node
    {
        //Varianle declaration
        public int value;
        public Node left;
        public Node right;
        public Node parent; // added in a parent attribute
    }

    class BinarySearchTree
    {
        public Node insert(Node root, int v)
        {
            if (root == null)
            {
                root = new Node();
                root.value = v;
            }

            // insertion logic, if the value (v )is < root, insert to the root.left
            // otherwise it's >=, so insert to the right
            else if (v < root.value)
            {
                // when inserting a node, setting the current node as the parent
                root.left = insert(root.left, v);
                root.left.parent = root;
            }
            else
            {
                root.right = insert(root.right, v);
                root.right.parent = root;
            }
            return root;
        }

        public string Traverse(Node root)
        {
            //If the tree is empty
            if (root == null)
            {
                return "";
            }

            //Traversing from left to right 
            Console.WriteLine(root.value.ToString());
            Traverse(root.left);
            Traverse(root.right);
            return "";
        }

        //inOrder traversal
        public string inOrder(Node root)
        {
            if (root == null)
            {
                return "";
            }
            inOrder(root.left);
            Console.WriteLine(root.value.ToString());
            inOrder(root.right);
            return "";
        }
        
        //preOrder traversal
        public string preOrder(Node root)
        {
            if (root == null)
            {
                return "";
            }
            Console.WriteLine(root.value.ToString());
            preOrder(root.left);
            preOrder(root.right);
            return "";
        }

        //postOrder traversal
        public string postOrder(Node root)
        {
            if (root == null)
            {
                return "";
            }
            postOrder(root.left);
            postOrder(root.right);
            Console.WriteLine(root.value.ToString());
            return "";
        }

        //breadthFirst traversal
        public string breadthFirst(Node root)
        {
            string s = "";
            Queue<Node> q = new Queue<Node>();
            Node head;
            q.Enqueue(root);
            while (q.Count > 0)
            {
                root = q.Dequeue();
                s += root.ToString() + ", ";

                if (root.left != null)
                {
                    q.Enqueue(root.left);
                }

                if (root.right != null)
                {
                    q.Enqueue(root.right);
                }
            }
            return s;
        }

        
        public Node findsmallestbst(Node root)
        {
            if (root.left == null)
            {
                return root;
            }
            else
                return findsmallestbst(root.left);
        }

        public Node getsibling(Node root)
        {
            // if the node has a parent
            if (root.parent != null)
            {
                Node p = root.parent;
                //if node provided is left child, return right child
                if (p.left == root) { return p.right; }
                //if node provided is right child, return left child
                if (p.right == root) { return p.left; }
            }
            // if no sibling node is found
            return null;
        }

        public Node getuncle(Node root)
        {
            // check if the node has a parent
            if (root.parent != null)
            {
                // check if the nodes parent has a parent
                if (root.parent.parent != null)
                {
                    // call previously made getsibling on the parent of the node
                    return getsibling(root.parent);
                }
            }
            //if no parent or no grandparent, return null
            return null;
        }

    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Node root = null;
            BinarySearchTree bst = new BinarySearchTree();

            int[] testArray = { 23, 18, 12, 20, 44, 35, 52, 10, 49, 30 };

            Random rnd = new Random();
            Console.WriteLine("Elements to be inserted into the BST");
            for (int i = 0; i < 10; i++)
            {
                
                Console.WriteLine(testArray[i]);
            }

            for (int i = 0; i < 10; i++)
            {
                root = bst.insert(root, testArray[i]);
            }
            Console.WriteLine("Elements in the Tree, preorder. ");
          
            Console.WriteLine(bst.preOrder(root));
            Console.WriteLine("Elements in the Tree, in postorder. ");

            Console.WriteLine(bst.postOrder(root));
            Console.WriteLine("Elements in the Tree, in inorder.");

            Console.WriteLine(bst.inOrder(root));
            Console.Write("Smallest element in the tree: ");

            Console.WriteLine(bst.findsmallestbst(root).value.ToString());

            Node n = root.left.right;
            Console.WriteLine(string.Format("Sibling of {0} is {1}", n.value, bst.getsibling(n).value));
            n = root.left.left.left;
            Console.WriteLine(string.Format("aunt/uncle of {0} is {1}", n.value, bst.getuncle(n).value));
            Console.WriteLine();

        }
    }
}
