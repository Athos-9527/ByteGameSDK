using System.Collections;
using TTSDK;
using UnityEngine;

public class LoadingView : MonoBehaviour
{

    void Start()
    {
        TT.InitSDK((errorCode, errorEnv) =>
        {
            Debug.Log($"ErrorCode: {errorCode},,ErrorEnv: {errorEnv}");

            if (errorCode == 0)
            {
                StartCoroutine(OnEnrtyMain());
            }

        });
    }

    private IEnumerator OnEnrtyMain()
    {
        yield return new WaitForSeconds(2f);
        UIManager.Instance.OpenPanel(UIType.Main);
    }
}
