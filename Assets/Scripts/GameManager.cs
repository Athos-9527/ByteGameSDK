using System;
using TTSDK.UNBridgeLib.LitJson;
using TTSDK;
using UnityEngine;
using StarkSDKSpace;

public class GameManager
{

    #region 检查每日奖励是否已经领取
    const string DailyRewardDateKey = "DailyRewardDate";
    /// <summary>
    /// 检查每日奖励是否已经领取
    /// </summary>
    /// <returns></returns>
    public static bool HasDailyRewardBeenClaimed()

    {
        var lastClaimDate = PlayerPrefs.GetString(DailyRewardDateKey);// localStorage.getItem('dailyRewardDate');

        var today = DateTime.Now.ToString("yyyy-MM-dd");// new Date().toISOString().split('T')[0]; // 获取当前日期并格式化为 YYYY-MM-DD
                                                        // 如果 lastClaimDate 存在且与今天相同，则表示用户今天已经领取过奖励
        Debug.Log($"lastClaimDate:{lastClaimDate}\tDateTimeNow:{today}");
        return lastClaimDate == today;
    }
    #endregion

    #region 保存每日奖励领取状态
    /// <summary>
    /// 保存每日奖励领取状态
    /// </summary>
    public static void SaveDailyRewardStatus()
    {
        var today = DateTime.Now.ToString("yyyy-MM-dd");// new Date().toISOString().split('T')[0]; // 获取当前日期并格式化为 YYYY-MM-DD
        PlayerPrefs.SetString(DailyRewardDateKey, today);

    }
    #endregion


    #region 客服

    /// <summary>
    /// 小 6 客服
    /// </summary>
    public static void TestOpenCustomerServiceConversation1()
    {
        Debug.Log("OpenCustomerService little 6");
        OpenCustomerServiceConversation(1);
    }

    /// <summary>
    /// 抖音IM 客服
    /// </summary>
    public static void TestOpenCustomerServiceConversation2()
    {
        Debug.Log("OpenCustomerService  IM");
        OpenCustomerServiceConversation(2);
    }

    /// <summary>
    /// 抖音IM 客服
    /// </summary>
    public static void TestOpenCustomerServiceConversation3()
    {
        Debug.Log("OpenCustomerService");
        OpenCustomerServiceConversation(3);
    }


    static void OpenCustomerServiceConversation(int type)
    {
        JsonData data = new JsonData
        {
            ["type"] = type
        };
        TT.OpenCustomerServiceConversation(data, (succ) =>
        {
            if (succ)
            {
                Debug.Log("succeed");
            } else
            {
                Debug.Log("fail");
            }
        });
    }

    #endregion


    #region 侧边栏检测

    public static void CheckScene(Action<bool> sccess)
    {
        TT.CheckScene(TTSideBar.SceneEnum.SideBar, b =>
        {
            Debug.Log("check scene 调用成功," + b);

            sccess?.Invoke(b);

            if (b)
            {
                Debug.Log("支持侧边栏");
            } else
            {
                Debug.Log("不支持侧边栏");
            }


        }, () =>
        {
            Debug.Log("check scene 接口调用结束的回调函数（调用成功、失败都会执行）");

        }, (errCode, errMsg) =>
        {

            Debug.Log($"check scene 接口调用失败的回调函数, errCode:{errCode}, errMsg:{errMsg}");
        });

    }

    #endregion


    #region 前往侧边栏

    public static void OnGotoSidebar()
    {
        var data = new JsonData
        {
            ["scene"] = "sidebar",
            // ["activityId"] = cacheActivityId,

        };
        TT.NavigateToScene(data, () =>
        {
            //Debug.Log("navigate to scene success");
            Debug.Log("跳转侧边栏成功回调");
        }, () =>
        {
            //Debug.Log("navigate to scene complete");
            Debug.Log("跳转侧边栏完成回调");
        }, (errCode, errMsg) =>
        {
            Debug.Log($"跳转侧边栏失败回调, errCode:{errCode}, errMsg:{errMsg}");
        });
    }

    #endregion


    #region 显示软键盘

    /// <summary>
    /// 显示软键盘
    /// </summary>
    public static void ShowKeyboard()
    {
        TT.ShowKeyboard(null, () =>
        {
            Debug.Log("show keyboard succeed");
        }, errMsg =>
        {
            Debug.LogError($"show keyboard failed, error: {errMsg}");
        });
    }

    #endregion


    #region 检测敏感词

    /// <summary>
    /// 检测敏感词
    /// </summary>
    /// <param name="value"></param>
    public static void CheckSensitive(string value)
    {
        TT.SensitiveWordCheck(value,
        result =>
        {
            Debug.Log(" 是否是敏感词:" + result);
        },
        errMsg =>
        {
            Debug.Log("检测失败，原因:" + errMsg);
        });
    }

    #endregion


    #region 替换敏感词

    /// <summary>
    /// 替换敏感词
    /// </summary>
    /// <param name="value"></param>
    /// <param name="sccess"></param>
    public static void ReplaceSensitive(string value, Action<string> sccess)
    {
        TT.ReplaceSensitiveWords(value, (code, msg, data) =>
        {
            Debug.Log($"返回code: {code}\n返回msg: {msg}\n返回data: {data?.ToJson()}");
            sccess?.Invoke(data["audit_content"].ToString());
        });
    }

    #endregion


    #region 检测登录

    /// <summary>
    /// 检测登录
    /// </summary>
    public static void CheckLogin()
    {
        TT.CheckSession(() =>
        {
            Debug.Log($"TestCheckSession success session");
        }, (errMsg) =>
        {
            Debug.Log($"TestCheckSession fail: {errMsg}");
            OnLogin();
        });
    }

    static string Code;
    static string AnonymousCode;
    // 登录
    private static void OnLogin()
    {
        var force = true;
        TT.Login((code, anonymousCode, isLogin) =>
        {
            Code = code;
            AnonymousCode = anonymousCode;
            Debug.Log($"TestLogin: force:{force},code:{code},anonymousCode:{anonymousCode},isLogin:{isLogin}");
        }, (errMsg) =>
        {
            Debug.Log($"TestLogin: force:{force},{errMsg}");
        }, force);

    }


    #endregion


    #region 成绩上传


    /// <summary>
    /// 上传分数
    /// </summary>
    public static void SetRankData1()
    {
        SetRankData(0, "100", 0);
    }

    /// <summary>
    /// 上传段位
    /// </summary>
    public static void SetRankData2()
    {
        SetRankData(1, "秩序白银", 2);
    }

    public static void SetRankData(int dataType, string value, int priority, string zoneId = "default")
    {
        var paramJson = new JsonData
        {
            ["dataType"] = dataType, // 成绩为数字类型
            ["value"] = value,  // 用户得分
            ["priority"] = priority,   // dataType 为数字类型，不需要权重，直接传0
            ["zoneId"] = zoneId,
        };
        Debug.Log($"SetImRankData param:{paramJson.ToJson()}");
        TT.SetImRankData(paramJson, (b, s) =>
        {
            if (b)
            {
                Debug.Log($"SetImRankScore: {b}");
            } else
            {
                Debug.Log($"SetImRankScore: {b}");
            }
        });
    }

    #endregion


    #region 排行榜信息


    /// <summary>
    /// 分数排名
    /// </summary>
    public static void GetRankList1()
    {
        GetRankList("day", 0, "all", "", "", "default");
    }

    /// <summary>
    /// 段位排名
    /// </summary>
    public static void GetRankList2()
    {
        GetRankList("day", 1, "all", "", "", "default");
    }


    public static void GetRankList(string rankType, int dataType, string relationType, string suffix, string rankTitle, string zoneId)
    {
        var paramJson = new JsonData
        {
            ["rankType"] = rankType,
            ["dataType"] = dataType,
            ["relationType"] = relationType,
            ["suffix"] = suffix,
            ["rankTitle"] = rankTitle,
            ["zoneId"] = zoneId,
        };
        Debug.Log($"GetImRankList param:{paramJson.ToJson()}");
        TT.GetImRankList(paramJson, (b, s) =>
        {
            if (b)
            {
                Debug.Log("GetImRankList");
            } else
            {
                Debug.Log("GetImRankList 2");
            }
        });
    }

    #endregion


    static TTGameRecorder m_TTGameRecorder;

    public static void InitRecorde()
    {
        m_TTGameRecorder = TT.GetGameRecorder();
    }

    // 开始录屏
    public static void OnStartButtonTapped()
    {
        if (m_TTGameRecorder.GetVideoRecordState() != TTGameRecorder.VideoRecordState.RECORD_STARTED)
        {
            m_TTGameRecorder.Start(true,
                600,
                OnRecordStart,
                OnRecordError,
                OnRecordTimeout);
        } else
        {
            Debug.Log("Recorder is started");
        }

        void OnRecordStart()
        {
            Debug.Log("OnRecordStart");
        }
        void OnRecordError(int errCode, string errMsg)
        {
            Debug.Log($"OnRecordError - errCode: {errCode}, errMsg: {errMsg}");
        }
        void OnRecordTimeout(string videoPath)
        {
            Debug.Log($"OnRecordTimeout - videoPath: {videoPath}, video duration: {m_TTGameRecorder.GetRecordDuration() / 1000.0f} s");
        }

    }

    // 结束录屏
    public static void OnStopButtonTapped()
    {
        m_TTGameRecorder.Stop(OnRecordComplete, OnRecordError);

        void OnRecordComplete(string videoPath)
        {
            VideoPath = videoPath;
            Debug.Log($"OnRecordComplete - videoPath: {videoPath}, video duration: {m_TTGameRecorder.GetRecordDuration() / 1000.0f} s");
        }

        void OnRecordError(int errCode, string errMsg)
        {
            Debug.Log($"OnRecordError - errCode: {errCode}, errMsg: {errMsg}");
        }

    }


    #region 分享

    static string VideoPath;

    public static void OnShare()
    {
        // 通用分享
        JsonData shareJson = new JsonData();
        shareJson["channel"] = "video";
        shareJson["title"] = "Some Title";
        shareJson["extra"] = new JsonData();
        shareJson["extra"]["videoPath"] = VideoPath;//录屏分享的话，路径是 OnRecordComplete 拿到的路径
        JsonData videoTopics = new JsonData();
        videoTopics.SetJsonType(JsonType.Array);
        videoTopics.Add("Some Topic1");
        videoTopics.Add("Some Topic2");
        shareJson["extra"]["videoTopics"] = videoTopics;
        shareJson["extra"]["hashtag_list"] = videoTopics;

        Debug.Log($"ShareAppMessageBtnClicked jsonData: {shareJson.ToJson()}");
        TT.ShareAppMessage(shareJson, (data) =>
        {
            Debug.Log($"ShareAppMessage success: {data}");
        },
            (errMsg) =>
            {
                Debug.Log($"ShareAppMessage failed: {errMsg}");
            },
            () =>
            {
                Debug.Log($"ShareAppMessage cancel");
            });
    }


    #endregion

}
