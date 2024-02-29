using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HuffmanTest
{
    public class Node
    {
        /// <summary>
        /// The symbol of the node of the Huffman tree.
        /// The frequency of the node symbol.
        /// The right branch of the node.
        /// The left branch of the node.
        /// </summary>
        public char Symbol { get; set; }
        public int Frequency { get; set; }
        public Node Right { get; set; }
        public Node Left { get; set; }

        /// <summary>
        /// A recursive method for traversing the Huffman tree and searching for a symbol.
        /// </summary>
        /// <param name="symbol">The desired symbol.</param>
        /// <param name="data">The path to the symbol (a list of Boolean values).</param>
        /// <returns>The path to the symbol in the form of a list of Boolean values.</returns>
        public List<bool> Traverse(char symbol, List<bool> data)
        {
            // Leaf
            if (Right == null && Left == null)
            {
                if (symbol.Equals(this.Symbol))
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                List<bool> left = null;
                List<bool> right = null;

                if (Left != null)
                {
                    List<bool> leftPath = new List<bool>();
                    leftPath.AddRange(data);
                    leftPath.Add(false);

                    left = Left.Traverse(symbol, leftPath);
                }

                if (Right != null)
                {
                    List<bool> rightPath = new List<bool>();
                    rightPath.AddRange(data);
                    rightPath.Add(true);
                    right = Right.Traverse(symbol, rightPath);
                }

                if (left != null)
                {
                    return left;
                }
                else
                {
                    return right;
                }
            }
        }
    }
}