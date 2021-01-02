//=============================
//Author: Zack Yang 
//Created Date: 01/01/2021 16:30
//=============================
using System.Collections;
using System.Collections.Generic;
using SlimBehaviourTree;
using UnityEngine;

namespace SlimBehaviourTree
{
    public class NodeSequence : NodeComposite
    {
        private int lastIndex;

        public NodeSequence(string name, List<Behaviour> nodes) : base(name, nodes)
        {
            base.Type = "Sequence";
            lastIndex = 0;
        }

        protected override BehaviourStatus Execute(BaseInput input)
        {
            BehaviourStatus result;
            for (int i = lastIndex; i < base.ChildrenCount; i++)
            {
                result = base.Children[i].Tick(input);
                
                if (result == BehaviourStatus.Success)
                    continue;

                if (result == BehaviourStatus.Failure)
                {
                    CurrentStatus = BehaviourStatus.Failure;
                    return CurrentStatus;
                }

                if (result == BehaviourStatus.Running)
                {
                    lastIndex = i;
                    base.CurrentStatus = BehaviourStatus.Running;
                    return CurrentStatus;
                }
            }

            CurrentStatus = BehaviourStatus.Success;
            return CurrentStatus;
        }
    }

}