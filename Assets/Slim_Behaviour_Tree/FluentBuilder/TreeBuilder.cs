//=============================
//Author: Zack Yang 
//Created Date: 01/04/2021 17:17
//=============================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using NUnit.Framework;
using TreeEditor;

namespace SlimBehaviourTree
{
    public class TreeBuilder
    {
        private BehaviourTree _currentTree;
        private Stack<Behaviour> _nodes;
        private TreeBuilder(string name, string jsonFilePath)
        {
            _currentTree = new BehaviourTree(name);
            string jsonTxt = Resources.Load<TextAsset>(jsonFilePath).text;
            _currentTree.ReadData(jsonTxt);
        }
        public static TreeBuilder Init(string name, string jsonFilePath)
        {
            return new TreeBuilder(name, jsonFilePath);
        }

        public BehaviourTree Build()
        {
            return _currentTree;
        }

        public TreeBuilder End()
        {
            if (_nodes.Count == 0)
            {
                throw new InvalidOperationException("Cannot end the tree building process now");
            }

            _nodes.Pop();

            return this;
        }

        private TreeBuilder PushComposite(Behaviour current)
        {
            if (_nodes.Count == 0 && _currentTree.GetRoot() != null)
            {
                throw new InvalidOperationException("Cannot add new nodes to behaviour tree.");
            }

            if (_nodes.Count == 0)
            {
                _currentTree.SetRoot(current);
                _nodes.Push(current);
            }
            else
            {
                Behaviour parent = _nodes.Peek();
                if (parent.Type == "Composite")
                {
                    (parent as NodeComposite).AddChild(current);
                    _nodes.Push(current);
                }
                else if (parent.Type == "Decorator")
                {
                    (parent as NodeDecorator).SetChild(current);
                    _nodes.Pop();
                    _nodes.Push(current);
                }
            }

            return this;
        }

        private TreeBuilder PushLeaf(Behaviour current)
        {
            if (_nodes.Count == 0 && _currentTree.GetRoot() != null)
            {
                throw new InvalidOperationException("Cannot add new nodes to behaviour tree.");
            }

            if (_nodes.Count == 0)
            {
                _currentTree.SetRoot(current);
            }
            else
            {
                Behaviour parent = _nodes.Peek();
                if (parent.Type == "Composite")
                {
                    (parent as NodeComposite).AddChild(current);
                }
                else if (parent.Type == "Decorator")
                {
                    (parent as NodeDecorator).SetChild(current);
                    _nodes.Pop();
                }
            }

            return this;
        }
    }
}
