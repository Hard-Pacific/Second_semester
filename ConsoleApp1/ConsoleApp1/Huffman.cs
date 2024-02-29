using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HuffmanTest
{
    /// <summary>
    /// A class for representing the Huffman tree and encoding and decoding operations.
    /// </summary>
    public class HuffmanTree
    {
        private List<Node> nodes = new List<Node>();
        /// <summary>
        /// The root of the Huffman tree.
        /// </summary>
        public Node Root { get; set; }
        /// <summary>
        /// The frequency of characters in the source string.
        /// </summary>
        public Dictionary<char, int> Frequencies = new Dictionary<char, int>();
        /// <summary>
        /// Building a Huffman tree based on the source string.
        /// </summary>
        /// <param name="source">The source string for building the tree.</param>
        public void Build(string source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (!Frequencies.ContainsKey(source[i]))
                {
                    Frequencies.Add(source[i], 0);
                }

                Frequencies[source[i]]++;
            }

            foreach (KeyValuePair<char, int> symbol in Frequencies)
            {
                nodes.Add(new Node() { Symbol = symbol.Key, Frequency = symbol.Value });
            }

            while (nodes.Count > 1)
            {
                List<Node> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node>();

                if (orderedNodes.Count >= 2)
                {
                    List<Node> taken = orderedNodes.Take(2).ToList<Node>();
                    Node parent = new Node()
                    {
                        Symbol = '*',
                        Frequency = taken[0].Frequency + taken[1].Frequency,
                        Left = taken[0],
                        Right = taken[1]
                    };

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                this.Root = nodes.FirstOrDefault();

            }

        }
        /// <summary>
        /// Encoding of the source string.
        /// </summary>
        /// <param name="source">The source string for encoding.</param>
        /// <returns>A bitmap of encoded data.</returns>
        public BitArray Encode(string source)
        {
            List<bool> encodedSource = new List<bool>();

            for (int i = 0; i < source.Length; i++)
            {
                List<bool> encodedSymbol = this.Root.Traverse(source[i], new List<bool>());
                encodedSource.AddRange(encodedSymbol);
            }

            BitArray bits = new BitArray(encodedSource.ToArray());

            return bits;
        }
        /// <summary>
        /// Decoding a bit array.
        /// </summary>
        /// <param name="bits">Encoded data in the form of a bit array.</param>
        /// <returns>The decoded source string.</returns>
        public string Decode(BitArray bits)
        {
            Node current = this.Root;
            string decoded = "";

            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }

                if (IsLeaf(current))
                {
                    decoded += current.Symbol;
                    current = this.Root;
                }
            }

            return decoded;
        }
        /// <summary>
        /// Checking whether a node is a leaf in the tree.
        /// </summary>
        /// <param name="node">The node to check.</param>
        /// <returns>True if the node is a leaf, otherwise False.</returns>
        public bool IsLeaf(Node node)
        {
            return (node.Left == null && node.Right == null);
        }

    }
}