using System;
using TTSDK;
using UnityEngine;


/// <summary>
/// 定义一个管理微信广告的类
/// </summary>
public class DyAdManager
{

    #region 激励视频广告

    // 激励广告对象
    static TTRewardedVideoAd _newReward;
    /// <summary>
    /// 用户看完广告的回调事件
    /// </summary>
    public static event Action<bool> ShowRewardEvent;

    static string _rewardedID2 = "702lidh2e1k7gbqa3q";

    /// <summary>
    /// 初始化激励广告
    /// </summary>
    public static void InitRewarded()
    {
        try
        {
            // 创建激励广告实例
            _newReward = TT.CreateRewardedVideoAd(new CreateRewardedVideoAdParam() { AdUnitId = _rewardedID2 }); //  WX.CreateRewardedVideoAd(new WXCreateRewardedVideoAdParam()
            if (_newReward == null)
            {
                Debug.LogError("激励视频创建失败");
            } else
            {

                Debug.Log("激励视频创建成功");
                // 注册广告事件
                _newReward.OnClose += _newReward_OnClose;
                _newReward.OnError += _newReward_OnError;
                _newReward.OnLoad += _newReward_OnLoad;
            }
        } catch (Exception ex)
        {
            Debug.Log("初始化激励广告:" + ex.StackTrace);
        }
    }


    private static void _newReward_OnLoad()
    {
        Debug.Log("激励视频加载成功");
    }

    private static void _newReward_OnError(int code, string message)
    {
        Debug.Log($"激励视频错误 errorCode: {code}\terrorMessage:{message}");
    }

    private static void _newReward_OnClose(bool isEnded, int count)
    {
        //if (GameManager.Instance.GameState == GameState.暂停)
        //{
        //    GameManager.Instance.GameState = GameState.游戏中;
        //}
        Debug.Log($"激励视频关闭 ended: {isEnded}, count: {count}");
        if (isEnded)
        {
            // 用户看完了广告，发放奖励
            Debug.Log("用户看完了广告，需要发放奖励");
        } else
        {
            Debug.Log("用户没有看完广告，没有奖励");
        }
        // 触发回调事件
        ShowRewardEvent?.Invoke(isEnded);
    }

    /// <summary>
    /// 显示激励广告
    /// </summary>
    public static void ShowNewReward()
    {
        _newReward.Show();
    }


    #endregion

}
