using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;

public class TestUniTask : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AsyncFunc().Forget();
        Debug.Log($"{Time.frameCount}: After Forget.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async UniTaskVoid AsyncFunc()
    {
        Debug.Log($"{Time.frameCount}: Start.");

        await UniTask.Yield(PlayerLoopTiming.Update);
        
        await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate);

        Debug.Log($"{Time.frameCount}: Done Yield.");

        int num = 3;
        while (num > 0)
        {
            var wait_sec = Random();
            Debug.Log($"{Time.frameCount}: {Time.realtimeSinceStartup}: Start Wait. {wait_sec}");
            await Wait(wait_sec).ToUniTask();
            Debug.Log($"{Time.frameCount}: {Time.realtimeSinceStartup}: Done Wait.");
            --num;
        }

        Debug.Log($"{Time.frameCount}: Finish AsynFunc.");
    }

    private float Random()
    {
        var count = 10000000;
        while (count > 0)
        {
            --count;
        }
        //return UnityEngine.Random.Range(2, 5);
        return 0.0f;
    }

    IEnumerator Wait(float sec)
    {
        yield return new WaitForSeconds(sec);
    }
}
