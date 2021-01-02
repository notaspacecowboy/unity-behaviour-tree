//=============================
//Author: Zack Yang 
//Created Date: 12/31/2020 11:51
//=============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimBehaviourTree
{
    public abstract class NodeComposite : Behaviour
    {
        #region Public Properties

        public int ChildrenCount
        {
            get
            {
                return Children.Count;
            }
        }

        //tree properties
        public List<Behaviour> Children { get; private set; }

        #endregion

        #region Public Methods
        public NodeComposite(string name, List<Behaviour> nodes) : base(name)
        {
            base.Type = "Composite";
            this.Children = nodes;

            if (nodes == null)
            {
                this.Children = new List<Behaviour>();
            }
        }

        //tree operations

        public void AddChild(Behaviour node)
        {
            Children.Add(node);
            node.Parent = this;
        }

        public void RemoveChild(Behaviour node)
        {
            Children.Remove(node);
        }

        public void InsertChild(Behaviour node, Behaviour prevNode)
        {
            int i;
            for (i = 0; i < Children.Count; i++)
            {
                if (Children[i] == prevNode)
                    break;
            }

            if (i != Children.Count)
                i++;
            Children.Insert(i, node);
        }

        public void ReplaceChild(Behaviour oldNode, Behaviour newNode)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i] == oldNode)
                    Children[i] = newNode;
            }
        }

        public bool ContainsChild(Behaviour node)
        {
            return Children.Contains(node);
        }

        #endregion
    }
}
