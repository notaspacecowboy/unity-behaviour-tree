//=============================
//Author: Zack Yang 
//Created Date: 01/02/2021 21:20
//=============================
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace SlimBehaviourTree
{
    public class BehaviourTree
    {
        private string _treeName;
        private Behaviour _root;
        private Blackboard _data;

        public BehaviourTree(string name)
        {
            _treeName = name;
            _root = null;   
            _data = new Blackboard();
        }
        public BehaviourTree(string name, Behaviour root)
        {
            _treeName = name;
            _data = new Blackboard();

            SetRoot(root);
        }

        public void SetRoot(Behaviour root)
        {
            _root = root;
            _root.Data = this._data;
        }

        public void Run(BaseInput input)
        {
            _root.Tick(input);
        }
    }
}
