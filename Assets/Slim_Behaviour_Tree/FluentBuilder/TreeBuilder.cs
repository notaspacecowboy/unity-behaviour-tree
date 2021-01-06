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
        public static TreeBuilder Init(string name, string jsonFilePath)
        {
            return new TreeBuilder(name, jsonFilePath);
        }

        public BehaviourTree Build()
        {
            if (_nodes.Count != 0)
            {
                throw new InvalidOperationException("Failed to build the tree! Nodes not fulfilled yet");
            }
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

        public TreeBuilder Do(string name, Func<BaseInput, BehaviourStatus> action)
        {
            NodeAction node = new NodeAction(name, action);
            return PushLeaf(node);
        }

        public TreeBuilder Condition(string name, Func<BaseInput, bool> condition)
        {
            NodeCondition node = new NodeCondition(name, condition);
            return this.PushLeaf(node);
        }

        public TreeBuilder Selector()
        {
            NodeSelector node = new NodeSelector();
            return PushInternal(node);
        }

        public TreeBuilder Sequence()
        {
            NodeSequence node = new NodeSequence();
            return PushInternal(node);
        }

        public TreeBuilder RandomSelector()
        {
            NodeRandomSelector node = new NodeRandomSelector();
            return PushInternal(node);
        }

        public TreeBuilder RandomSequence()
        {
            NodeRandomSequence node = new NodeRandomSequence();
            return PushInternal(node);
        }

        public TreeBuilder ParellelAllFailed()
        {
            NodeParellelAllFailed node = new NodeParellelAllFailed();
            return PushInternal(node);
        }

        public TreeBuilder ParellelAllSucceed()
        {
            NodeParellelAllSucceed node = new NodeParellelAllSucceed();
            return PushInternal(node);
        }

        #region Private Variables

        private BehaviourTree _currentTree;
        private Stack<Behaviour> _nodes;

        #endregion

        #region Private Methods

        private TreeBuilder(string name, string jsonFilePath)
        {
            _nodes = new Stack<Behaviour>();

            _currentTree = new BehaviourTree(name);
            string jsonTxt = Resources.Load<TextAsset>(jsonFilePath).text;
            _currentTree.ReadData(jsonTxt);
        }

        private TreeBuilder PushInternal(Behaviour current)
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

        #endregion
    }
}
