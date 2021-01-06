//=============================
//Author: Zack Yang 
//Created Date: 12/31/2020 11:23
//=============================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimBehaviourTree
{
    public class NodeAction : Behaviour
    {
        private readonly Func<BaseInput, BehaviourStatus> _action;
        public NodeAction(string name, Func<BaseInput, BehaviourStatus> action) : base(name, "Action")
        {
            _action = action;
        }

        protected override BehaviourStatus Execute(BaseInput input)
        {
            return _action(input);
        }
    }
}
