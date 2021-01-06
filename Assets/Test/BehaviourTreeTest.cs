//=============================
//Author: Zack Yang 
//Created Date: 01/04/2021 19:43
//=============================
using System.Collections;
using System.Collections.Generic;
using LitJson;
using SlimBehaviourTree;
using UnityEngine;

public class BehaviourTreeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BehaviourTree bt = new BehaviourTree("TestTree1");
        string jsonTxt = Resources.Load<TextAsset>("JsonTest1").text;
        bt.ReadData(jsonTxt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
