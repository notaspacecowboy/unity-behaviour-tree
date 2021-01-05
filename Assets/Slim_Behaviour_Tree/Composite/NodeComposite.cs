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
                return _children.Count;
            }
        }
        #endregion

        #region Public Methods
        public NodeComposite(string name, List<Behaviour> nodes) : base(name, "Composite")
        {
            this.InitChildren(nodes);
        }

        //tree operations

        public void AddChild(Behaviour node)
        {
            node.ParentTree = this.ParentTree;
            node.Data = this.Data;
            _children.Add(node);
        }

        protected void RemoveChild(Behaviour node)
        {
            _children.Remove(node);
        }

        public void InsertChild(Behaviour node, Behaviour prevNode)
        {
            node.ParentTree = this.ParentTree;
            node.Data = this.Data;

            int i;
            for (i = 0; i < _children.Count; i++)
            {
                if (_children[i] == prevNode)
                    break;
            }

            if (i != _children.Count)
                i++;

            _children.Insert(i, node);
        }

        public void ReplaceChild(Behaviour oldNode, Behaviour newNode)
        {
            newNode.ParentTree = this.ParentTree;
            newNode.Data = this.Data;

            for (int i = 0; i < _children.Count; i++)
            {
                if (_children[i] == oldNode)
                    _children[i] = newNode;
            }
        }

        public bool ContainsChild(Behaviour node)
        {
            return _children.Contains(node);
        }
        #endregion

        #region Private Instance Variables
        //tree properties
        protected List<Behaviour> _children;
        #endregion

        #region Private Methods
        private void InitChildren(List<Behaviour> nodes)
        {
            this._children = new List<Behaviour>();

            foreach (var child in nodes)
            {
                AddChild(child);
            }
        }

        #endregion
    }
}
