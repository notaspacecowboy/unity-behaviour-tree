//=============================
//Author: Zack Yang 
//Created Date: 01/04/2021 20:18
//=============================
using System.Collections;
using System.Collections.Generic;
using SlimBehaviourTree;
using UnityEngine;

public class TreeBuilderTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region Selector test

        BehaviourTree tree = TreeBuilder.Init("testTree1", "JsonTest1")
                                .Selector().Do("TestAction1", TestAction1)
                                           .Do("TestAction2", TestAction2)
                                           .Condition("ConditionTest1", TestCondition1)
                                .End()
                                .Build();

        tree.Run(new BaseInput());

        #endregion

        #region Sequence test

        tree = TreeBuilder.Init("testTree1", "JsonTest1")
                .Sequence().Do("TestAction1", TestAction1)
                           .Do("TestAction2", TestAction2)
                           .Condition("ConditionTest1", TestCondition1)
                .End()
                .Build();

        tree.Run(new BaseInput());

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    BehaviourStatus TestAction1(BaseInput input)
    {
        Debug.Log("TestAction1 passed");
        return BehaviourStatus.Success;
    }

    BehaviourStatus TestAction2(BaseInput input)
    {
        Debug.Log("TestAction2 passed");
        return BehaviourStatus.Failure;
    }

    bool TestCondition1(BaseInput input)
    {
        Debug.Log("TestCondition1 passed");
        return true;
    }
}
