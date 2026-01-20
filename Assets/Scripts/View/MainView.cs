using TTSDK;
using UnityEngine;
using UnityEngine.UI;

public class MainView : BaseView
{

    public Button BtnDailyReward;                   // 侧边栏

    public Button BtnServiceConversationLittle6;    // 小六客服
    public Button BtnServiceConversationIM;         // IM客服
    public Button BtnServiceConversation;           // 抖音平台客服

    public Button BtnOpenInput;                     // 打开输出框
    public Text InputText;                          // 输入显示文本

    public Button BtnSetRankScore;                  // 提交分数
    public Button BtnSetRankData;                   // 提交段位
    public Button BtnGetRankList;                   // 获得分数排行榜
    public Button BtnGetRankList2;                  // 获得段位排行榜

    public Button BtnRewardedVideoAd;       // 激励视频广告
    public Button BtnStartRecorde;          // 激励视频广告
    public Button BtnStopRecorde;           // 激励视频广告
    public Button BtnShare;                 // 激励视频广告



    void Awake()
    {
        // 侧边栏
        BtnDailyReward = transform.Find("BtnDailyReward").GetComponent<Button>();

        // 小六客服
        BtnServiceConversationLittle6 = transform.Find("BtnServiceConversationLittle6").GetComponent<Button>();
        // IM客服
        BtnServiceConversationIM = transform.Find("BtnServiceConversationIM").GetComponent<Button>();
        // 抖音平台客服
        BtnServiceConversation = transform.Find("BtnServiceConversation").GetComponent<Button>();

        // 打开输出框
        BtnOpenInput = transform.Find("BtnOpenInput").GetComponent<Button>();
        // 输入显示文本
        InputText = transform.Find("BtnOpenInput/InputText").GetComponent<Text>();

        // 提交分数
        BtnSetRankScore = transform.Find("BtnSetRankScore").GetComponent<Button>();
        // 提交段位
        BtnSetRankData = transform.Find("BtnSetRankData").GetComponent<Button>();
        // 获得分数排行榜
        BtnGetRankList = transform.Find("BtnGetRankList").GetComponent<Button>();
        // 获得段位排行榜
        BtnGetRankList2 = transform.Find("BtnGetRankList2").GetComponent<Button>();

        // 激励视频广告
        BtnRewardedVideoAd = transform.Find("BtnRewardedVideoAd").GetComponent<Button>();
        // 开始录制
        BtnStartRecorde = transform.Find("BtnStartRecorde").GetComponent<Button>();
        // 结束录制
        BtnStopRecorde = transform.Find("BtnStopRecorde").GetComponent<Button>();
        // 分享
        BtnShare = transform.Find("BtnShare").GetComponent<Button>();

    }

    private void OnEnable()
    {
        BtnDailyReward.onClick.AddListener(OnOpenClick);
        BtnServiceConversationLittle6.onClick.AddListener(OnServiceConversationLittle6Click);
        BtnServiceConversationIM.onClick.AddListener(OnServiceConversationIMClick);
        BtnServiceConversation.onClick.AddListener(OnServiceConversationClick);

        BtnOpenInput.onClick.AddListener(OnOpenInputClick);

        BtnSetRankScore.onClick.AddListener(SetRankDataScore);
        BtnSetRankData.onClick.AddListener(SetRankData);
        BtnGetRankList.onClick.AddListener(GetRankList);
        BtnGetRankList2.onClick.AddListener(GetRankList2);

        // 激励视频广告
        BtnRewardedVideoAd.onClick.AddListener(OnRewardedVideoAdClick);
        // 开始录制
        BtnStartRecorde.onClick.AddListener(OnStartRecordeClick);
        // 结束录制
        BtnStopRecorde.onClick.AddListener(OnStopRecordeClick);
        // 分享
        BtnShare.onClick.AddListener(OnShareClick);

        TT.OnKeyboardInput += OnKeyboardInput;
        TT.OnKeyboardConfirm += OnKeyboardConfirm;
        TT.OnKeyboardComplete += OnKeyboardComplete;

    }


    private void OnDisable()
    {
        BtnDailyReward.onClick.RemoveListener(OnOpenClick);
        BtnServiceConversationLittle6.onClick.RemoveListener(OnServiceConversationLittle6Click);
        BtnServiceConversationIM.onClick.RemoveListener(OnServiceConversationIMClick);
        BtnServiceConversation.onClick.RemoveListener(OnServiceConversationClick);
        BtnOpenInput.onClick.RemoveListener(OnOpenInputClick);

        BtnSetRankScore.onClick.RemoveListener(SetRankDataScore);
        BtnSetRankData.onClick.RemoveListener(SetRankData);
        BtnGetRankList.onClick.RemoveListener(GetRankList);
        BtnGetRankList2.onClick.RemoveListener(GetRankList2);


        // 激励视频广告
        BtnRewardedVideoAd.onClick.RemoveListener(OnRewardedVideoAdClick);
        // 开始录制
        BtnStartRecorde.onClick.RemoveListener(OnStartRecordeClick);
        // 结束录制
        BtnStopRecorde.onClick.RemoveListener(OnStopRecordeClick);
        // 分享
        BtnShare.onClick.RemoveListener(OnShareClick);

        TT.OnKeyboardInput -= OnKeyboardInput;
        TT.OnKeyboardConfirm -= OnKeyboardConfirm;
        TT.OnKeyboardComplete -= OnKeyboardComplete;
    }

    private void Start()
    {
        CheckScene();       // 检查侧边框显示
        CheckLogin();       // 检测登录


        if (TT.GetSystemInfo() != null)
        {
            DyAdManager.InitRewarded();
        }

        GameManager.InitRecorde();
    }

    // 打开奖励界面
    private void OnOpenClick()
    {
        UIManager.Instance.OpenPanel(UIType.DailyReward);
    }

    #region 侧边栏复访能力-检测


    // 入口奖励是否激活
    private void CheckScene()
    {
        GameManager.CheckScene((b) =>
        {
            BtnDailyReward.gameObject.SetActive(b);
        });
    }
    #endregion


    // IM客服
    private void OnServiceConversationIMClick()
    {
        GameManager.TestOpenCustomerServiceConversation2();
    }

    // 小6客服
    private void OnServiceConversationLittle6Click()
    {
        GameManager.TestOpenCustomerServiceConversation1();
    }

    // 抖音平台客服
    private void OnServiceConversationClick()
    {
        GameManager.TestOpenCustomerServiceConversation3();
    }

    // 打开输出键盘
    public void OnOpenInputClick()
    {
        GameManager.ShowKeyboard();
    }

    private void OnKeyboardInput(string value)
    {
        Debug.Log($"OnKeyboardInput: {value}");
        ReplaceSensitive(value);
    }
    private void OnKeyboardConfirm(string value)
    {
        Debug.Log($"OnKeyboardConfirm: {value}");
    }

    private void OnKeyboardComplete(string value)
    {
        Debug.Log($"OnKeyboardComplete: {value}");
    }

    // 替换敏感词
    public void ReplaceSensitive(string value)
    {
        GameManager.ReplaceSensitive(value, (txt) => { InputText.text = txt; });
    }

    // 检测登录
    private void CheckLogin()
    {
        GameManager.CheckLogin();
    }

    // 上传分数
    public void SetRankDataScore()
    {
        GameManager.SetRankData1();
    }

    // 上传段位
    public void SetRankData()
    {
        GameManager.SetRankData2();
    }

    // 获取排行榜列表，调用 API 后， 根据参数自动绘制游戏好友排行榜（ native UI ）。
    public void GetRankList()
    {
        GameManager.GetRankList1();
    }

    // 获取排行榜列表，调用 API 后， 根据参数自动绘制游戏好友排行榜（ native UI ）。
    public void GetRankList2()
    {
        GameManager.GetRankList2();
    }


    // 激励广告
    private void OnRewardedVideoAdClick()
    {
        DyAdManager.ShowNewReward();
    }

    // 开始录制
    private void OnStartRecordeClick()
    {
        GameManager.OnStartButtonTapped();
    }

    // 结束录制
    private void OnStopRecordeClick()
    {
        GameManager.OnStopButtonTapped();
    }


    // 分享
    private void OnShareClick()
    {
        GameManager.OnShare();
    }

}
