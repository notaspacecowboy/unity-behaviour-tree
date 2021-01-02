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
        public Behaviour Child { get; protected set; }

        public NodeDecorator(string name, Behaviour child) : base(name)
        {
            base.Type = "Decorator";
            Child = child;
        }
    }
}
