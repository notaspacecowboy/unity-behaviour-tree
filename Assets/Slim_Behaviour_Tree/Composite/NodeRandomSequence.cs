//=============================
//Author: Zack Yang 
//Created Date: 01/01/2021 16:56
//=============================
using System.Collections;
using System.Collections.Generic;
using SlimBehaviourTree;
using UnityEngine;
using Behaviour = UnityEngine.Behaviour;

namespace SlimBehaviourTree
{
    public class NodeRandomSequence : NodeComposite
    {
        // Start is called before the first frame update
        private int lastIndex;

        public NodeRandomSequence(string name, List<Behaviour> nodes) : base(name, nodes)
        {
            base.Type = "RandomSequence";
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

        protected override void OnEnter(BaseInput input)
        {
            Shuffle();
        }

        private void Shuffle()
        {
            Behaviour lastRunningNode = null;
            if (CurrentStatus == BehaviourStatus.Running)
            {
                lastRunningNode = Children[lastIndex];
            }

            for (int i = base.ChildrenCount; i >= 0; i--)
            {
                int toSwitch = Random.Range(0, i);
                Behaviour tmp = Children[i - 1];
                Children[i - 1] = Children[toSwitch];
                Children[toSwitch] = tmp;
            }

            if (lastRunningNode == null)
                return;

            for (int i = 0; i < base.ChildrenCount; i++)
            {
                if (Children[i] == lastRunningNode)
                {
                    Behaviour tmp = Children[i];
                    Children[i] = Children[0];
                    Children[0] = tmp;

                    break;
                }
            }
        }
    }
}
