﻿using UnityEngine;
using System.Collections;

public class FruitCreater : MonoBehaviour {

    [SerializeField,TooltipAttribute("レーモンのprefabを入れてください")]
    GameObject lemon = null;

    [SerializeField, TooltipAttribute("アプモンのprefabを入れてください")]
    GameObject apple = null;

    [SerializeField, TooltipAttribute("モモンのprefabを入れてください")]
    GameObject peach = null;

    [SerializeField,Range(0,200), TooltipAttribute("レーモンの出す数")]
    int LEMON_NUM = 100;

    [SerializeField,Range(0,200), TooltipAttribute("アプモンの出す数")]
    int APPLE_NUM = 100;

    [SerializeField, Range(0,200),TooltipAttribute("モモンの出す数")]
    int PEACH_NUM = 100;

    HandController hand_controller_ = null;

    void Awake()
    {
        hand_controller_ = GameObject.Find("LeapHandController").GetComponent<HandController>();

        LemonCreate(LEMON_NUM);
        AppleCreate(APPLE_NUM);
        PeachCreate(PEACH_NUM);
    }

    void Update()
    {
    }

    public void LemonCreate(int lemon_num)
    {
        GameObject lemon_manager = GameObject.Find("LemonManager");
        for (int i = 0; i < lemon_num; ++i)
        {
            if (lemon == null) continue;
            GameObject game_object = Instantiate(lemon);
            game_object.transform.SetParent(lemon_manager.transform);
            game_object.name = lemon.name;
        }
    }

    public void AppleCreate(int apple_num)
    {
        GameObject apple_manager = GameObject.Find("AppleManager");
        for (int i = 0; i < apple_num; ++i)
        {
            if (apple == null) continue;
            GameObject game_object = Instantiate(apple);
            game_object.transform.SetParent(apple_manager.transform);
            game_object.name = apple.name;
        }
    }

    public void PeachCreate(int peach_num)
    {
        GameObject peach_manager = GameObject.Find("PeachManager");
        for (int i = 0; i < peach_num; ++i)
        {
            if (peach == null) continue;
            GameObject game_object = Instantiate(peach);
            game_object.transform.SetParent(peach_manager.transform);
            game_object.name = peach.name;
        }
    }
}
