//=============================
//Author: Zack Yang 
//Created Date: 01/01/2021 17:48
//=============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimBehaviourTree
{
    public class NodeParellelAllFailed : NodeComposite
    {
        public NodeParellelAllFailed(string name, List<Behaviour> nodes) : base("ParellelAllFailed", nodes)
        {
        }

        protected override BehaviourStatus Execute(BaseInput input)
        {
            BehaviourStatus result;
            int count = 0;
            foreach (var child in _children)
            {
                result = child.Tick(input);

                if (result == BehaviourStatus.Success)
                {
                    CurrentStatus = BehaviourStatus.Failure;
                    return CurrentStatus;
                }

                if (result == BehaviourStatus.Failure)
                    count++;
            }

            if (count == base.ChildrenCount)
                return BehaviourStatus.Success;

            return BehaviourStatus.Running;
        }
    }
}
