//=============================
//Author: Zack Yang 
//Created Date: 12/31/2020 11:34
//=============================

using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace SlimBehaviourTree
{
    public class NodeCondition : Behaviour
    {
        private readonly Func<BaseInput, bool> _condition;
        public NodeCondition(string name, Func<BaseInput, bool> condition) : base(name, "Condition")
        {
            _condition = condition;
        }

        protected override BehaviourStatus Execute(BaseInput input)
        {
            bool result = _condition(input);
            if (result)
                return BehaviourStatus.Success;
            else
                return BehaviourStatus.Failure;
        }
    }
}
