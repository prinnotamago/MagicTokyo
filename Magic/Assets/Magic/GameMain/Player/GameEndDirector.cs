﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameEndDirector : NetworkBehaviour 
{
    float count_ = 0;

    bool is_start_ = false;
    public bool IsStart { get { return is_start_; } }

    const int FRUIT_NUM_ANNOUNCE_TIME = 2;
    const int CHANGE_SCENE_TIME = 2;


    Text text_ = null;

    [SerializeField
   , TooltipAttribute("ここに「Ike3ParticleManager」prefabを入れてください\n(プログラマー用)")]
    private GameObject particle_manager_;

    [SerializeField
    , TooltipAttribute("表示させたいタイムアップのパーティクルを入れてください")]
    private ParticleSystem particle_;

    [ClientRpc]
    public void RpcFinishLocal()
    {
        GameObject particle_manager = GameObject.Find(particle_manager_.name);
        ParticleSystem game_object = Instantiate(particle_);
        game_object.transform.SetParent(particle_manager.transform);
        game_object.transform.position = new Vector3(0.0f, 3.0f, 1.5f);
        game_object.name = particle_.name;
    }

    void Start()
    {
        //text_ = GameObject.Find("EndText").GetComponent<Text>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        if (!is_start_) return;
        if (count_ == 0.0f)
        {
            RpcFinishLocal();
            //text_.enabled = true;
        }
        count_ += Time.deltaTime;

        if (FRUIT_NUM_ANNOUNCE_TIME == (int)count_)
        {
            //　総数表示
        }


        if (count_ < CHANGE_SCENE_TIME) return;
        if (!isServer) return;
        var fruit_counter = GetComponent<FruitCounter>();
        var local_fruit_num = fruit_counter.FruitNum;
        var remote_fruit_num = fruit_counter.RemoteFruitNum;
        string result = "";

        if (local_fruit_num == remote_fruit_num)
        {
            result = "draw";
        }
        else if (local_fruit_num > remote_fruit_num)
        {
            result = "win";
        }
        else
        {
            result = "lose";
        }
        FindObjectOfType<ScoreSaver>().FruitNum = local_fruit_num;
        NetworkManager.singleton.StopHost();
        Application.LoadLevel(result);
    }

    [ClientRpc]
    public void RpcTellClientStart()
    {
        is_start_ = true;
    }
}
