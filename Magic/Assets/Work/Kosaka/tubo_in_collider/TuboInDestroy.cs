﻿using UnityEngine;
using System.Collections;


public class TuboInDestroy : MonoBehaviour
{
    [SerializeField, Tooltip("鍋に入ってから消えるまでの時間(単位：秒)")]
    float DESTROY_TIME = 0.0f;

    [SerializeField, Range(0, 10), Tooltip("ラッシュイベント時の鍋に入っているアプモンの数")]
    int MAX_APPLE_NUM = 5;

    [SerializeField, Range(0, 10), Tooltip("ラッシュイベント時の鍋に入っているレーモンの数")]
    int MAX_LEMON_NUM = 5;

    //それぞれのくだモンの鍋に入った(消した)数
    int lemon_count_ = 0;
    int apumon_count_ = 0;
    bool is_in_momon_ = false;

    bool is_in_dorian_ = false;

    //くだモンの名前
    const string LEMON_NAME = "le-mon";
    const string APUMON_NAME = "apumon";
    const string MOMON_NAME = "momon";

    const string JAMAMON_NAME = "jamamon";
    const string DORIANBOM_NAME = "dorianbom_red";

    RushEventer rush_eventer_ = null;

    BoxCollider lid_;

    //-----------------------------------------------------------------

    //それぞれのくだモンの鍋に入った(消した)数のゲッター
    public int GetLemonCount() { return lemon_count_; }
    public int GetApumonCount() { return apumon_count_; }
    public bool GetMomonCount() { return is_in_momon_; }
    public int GetKudamonCount()
    {
        var kudamon_add = lemon_count_ + apumon_count_;
        return kudamon_add;
    }
    public bool IsInDorain { get { return is_in_dorian_; } }

    void Start()
    {
        rush_eventer_ = FindObjectOfType<RushEventer>();
        lid_ = GameObject.Find("Lid").GetComponent<BoxCollider>();
    }

    public void Update()
    {
        //蓋のコライダーを作る
        if (GetKudamonCount() >= 10)
        {
            lid_.isTrigger = false;
        }

        //ジェスチャーしたらコライダーを消す
        // TODO : 条件を[サークルのジェスチャーを取得]に変える
        if (true)
        {
            lid_.isTrigger = true;
        }

        RushEvent();
    }

    //鍋の中のTrigger判定
    void OnTriggerEnter(Collider other)
    {

        //それぞれのくだモンを「消す処理」と「カウント処理」（と「入ったものを出力」するためのデバッグ）
        if (other.name == LEMON_NAME)
        {
            Destroy(other.gameObject, DESTROY_TIME);
            lemon_count_++;
            //Debug.Log(" Lemon Destroy " + lemon_count);
        }
        else if (other.name == APUMON_NAME)
        {
            Destroy(other.gameObject, DESTROY_TIME);
            apumon_count_++;
            //Debug.Log(" Apple Destroy " + apumon_count);
        }
        else if (other.name == MOMON_NAME)
        {
            Destroy(other.gameObject, DESTROY_TIME);
            is_in_momon_ = true;
            //Debug.Log(" Peach Destroy " + momon_count);
        }
        else if (other.name == JAMAMON_NAME)
        {
            Destroy(other.gameObject, DESTROY_TIME);
            JamamonFlyOut();
        }
        else if (other.name == DORIANBOM_NAME)
        {
            Destroy(other.gameObject);
            is_in_dorian_ = true;
        }
    }

    void RushEvent()
    {
        if (!rush_eventer_.IsStart) return;
        lemon_count_ = MAX_LEMON_NUM;
        apumon_count_ = MAX_APPLE_NUM;
    }

    public void ResetCount()
    {
        lemon_count_ = 0;
        apumon_count_ = 0;
    }

    public void ResetMomon()
    {
        is_in_momon_ = false;
    }

    public void ResetDorian()
    {
        is_in_dorian_ = false;
    }

    void JamamonFlyOut()
    {
        var kudamon_manager = GameObject.Find("FruitManager")
            .GetComponent<FruitCreater>();

        for (var apple_num = 0; apple_num < apumon_count_; apple_num++)
        {
            var apple = kudamon_manager.AppleCreate();
            apple.transform.position = transform.position + new Vector3(0.0f, 2.0f, 0.0f);
            apple.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-10.0f, 10.0f), 30.0f, Random.Range(-10.0f, 10.0f));
        }

        for (var lemon_num = 0; lemon_num < lemon_count_; lemon_num++)
        {
            var lemon = kudamon_manager.LemonCreate();
            lemon.transform.position = transform.position + new Vector3(0.0f, 2.0f, 0.0f);
            lemon.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-10.0f, 10.0f), 30.0f, Random.Range(-10.0f, 10.0f));
        }
        lemon_count_ = 0;
        apumon_count_ = 0;
    }
}