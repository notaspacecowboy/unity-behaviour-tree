//=============================
//Author: Zack Yang 
//Created Date: 01/04/2021 18:04
//=============================
using System.Collections;
using System.Collections.Generic;
using SlimBehaviourTree;
using UnityEngine;

public class BlackboardTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Blackboard b = new Blackboard();
        b.SetValue("test1", 234);
        b.SetValue("test2", "yangzheng");
        b.SetValue("test3", false);

        //b.Print();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
