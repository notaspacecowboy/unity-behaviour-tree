//=============================
//Author: Zack Yang 
//Created Date: 12/29/2020 11:51
//=============================
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace SlimBehaviourTree
{
    public interface IBehaviour
    {
        string Name { get; }
        BehaviourStatus CurrentStatus { get; }
        BehaviourStatus Tick(BaseInput input);
    }
}
