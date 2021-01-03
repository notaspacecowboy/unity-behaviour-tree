//=============================
//Author: Zack Yang 
//Created Date: 12/31/2020 11:23
//=============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimBehaviourTree
{
    public abstract class NodeAction : Behaviour
    {
        public NodeAction(string name) : base(name, "Action")
        {
        }
    }
}
