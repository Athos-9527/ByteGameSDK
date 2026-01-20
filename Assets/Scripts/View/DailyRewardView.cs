using System.Collections.Generic;
using TTSDK;
using UnityEngine;
using UnityEngine.UI;


public class DailyRewardView : BaseView
{

    public Button BtnClose;
    public Button BtnEntry;
    public Text TxtEntry;

    private bool isFromSidebar = false; //状态，表示是否从侧边栏进入
    private bool isReward = false; // 是否有奖励




    void Awake()
    {
        BtnClose = transform.Find("BtnClose").GetComponent<Button>();
        BtnEntry = transform.Find("BtnEntry").GetComponent<Button>();
        TxtEntry = BtnEntry.transform.Find("TxtEntry").GetComponent<Text>();
    }


    private void OnEnable()
    {
        BtnClose.onClick.AddListener(OnCloseClick);
        BtnEntry.onClick.AddListener(OnGotoSidebarClick);
        TxtEntry.text = GameManager.HasDailyRewardBeenClaimed() ? "已领取" : "进入侧边栏";
        isFromSidebar = false;
        isReward = false;

        TT.GetAppLifeCycle().OnShow += StartController_OnShow;
    }

    private void OnDisable()
    {
        BtnClose.onClick.RemoveListener(OnCloseClick);
        BtnEntry.onClick.RemoveListener(OnGotoSidebarClick);

        TT.GetAppLifeCycle().OnShow -= StartController_OnShow;
    }


    void OnCloseClick()
    {
        UIManager.Instance.ClosePanel(UIType.DailyReward);
    }

    private void OnGotoSidebarClick()
    {
        if (isReward)
        {
            // 领取奖励
            TxtEntry.text = "已领取";
            GameManager.SaveDailyRewardStatus();
        } else if (!GameManager.HasDailyRewardBeenClaimed())
        {
            // 前往侧边栏
            OnGotoSidebar();
        }
    }

    private void StartController_OnShow(Dictionary<string, object> param)
    {

        //判断用户是否是从侧边栏进来的
        Debug.Log(param);

        foreach (var item in param)
        {
            Debug.Log($"显示回调 key:{item.Key}\tvalue:{item.Value}");
        }
        //  console.log("res.launch_from" + res.launch_from);
        //  console.log("res.location" + res.location);
        this.isFromSidebar = (param["launchFrom"].ToString() == "homepage" && param["location"].ToString() == "sidebar_card");
        //if (param.ContainsKey("launch_from") && param.ContainsKey("location"))
        if (this.isFromSidebar)
        {
            Debug.Log("从侧边栏进来的");
            TxtEntry.text = "领取奖励";
            isReward = true;

            // 在游戏开始时或用户尝试领取奖励时调用
            if (GameManager.HasDailyRewardBeenClaimed())
            {
                isReward = false;
                TxtEntry.text = "已领取";
                //领取过奖励了。
            }
        } else
        {
            //否则反之
            Debug.Log("正常进来的");
            TxtEntry.text = "前往侧边栏";

        }

    }

    /// <summary>
    /// 前往侧边栏
    /// </summary>
    public void OnGotoSidebar()
    {
        GameManager.OnGotoSidebar();
    }

}
