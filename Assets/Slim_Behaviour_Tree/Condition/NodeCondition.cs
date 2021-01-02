//=============================
//Author: Zack Yang 
//Created Date: 12/31/2020 11:34
//=============================
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace SlimBehaviourTree
{
    public abstract class NodeCondition : Behaviour
    {
        public NodeCondition(string name) : base(name)
        {
            base.Type = "Condition";
        }
    }
}
