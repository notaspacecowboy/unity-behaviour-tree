//=============================
//Author: Zack Yang 
//Created Date: 01/01/2021 18:04
//=============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimBehaviourTree
{
    public abstract class NodeDecorator : Behaviour
    {
        protected Behaviour _child;

        public NodeDecorator(string name, string type, Behaviour child) : base(name, type)
        {
            SetChild(child);
        }

        public void SetChild(Behaviour child)
        {
            child.ParentTree = this.ParentTree;
            child.Data = this.Data;
            _child = child;
        }
    }
}
