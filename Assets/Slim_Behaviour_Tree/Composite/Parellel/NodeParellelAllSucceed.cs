//=============================
//Author: Zack Yang 
//Created Date: 01/01/2021 17:55
//=============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimBehaviourTree
{
    public class NodeParellelAllSucceed : NodeComposite
    {
        public NodeParellelAllSucceed() : base("NodeParellelAllSucceed")
        {
        }

        protected override BehaviourStatus Execute(BaseInput input)
        {
            BehaviourStatus result;
            int count = 0;
            foreach (var child in _children)
            {
                result = child.Tick(input);

                if (result == BehaviourStatus.Failure)
                {
                    CurrentStatus = BehaviourStatus.Failure;
                    return CurrentStatus;
                }

                if (result == BehaviourStatus.Success)
                    count++;
            }

            if (count == base.ChildrenCount)
                return BehaviourStatus.Success;

            return BehaviourStatus.Running;
        }
    }
}

