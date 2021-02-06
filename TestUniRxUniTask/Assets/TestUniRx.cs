using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UniRx;

public class TestUniRx : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //  FromCoroutine
        //  ê¨å˜ó·
        {
            Observable.FromCoroutine<int>(o => FuncCoroutineComplete(o))
                .Subscribe(
                    _ => Debug.Log($"{Time.frameCount} success : next : {_}"),
                    e => Debug.Log($"{Time.frameCount} success : {e}"),
                    () => Debug.Log($"{Time.frameCount} success : completed.")
                );
        }
        //  é∏îsó·
        {
            Observable.FromCoroutine<int>(o => FuncCoroutineFailuar(o))
                .Subscribe(
                    _ => Debug.Log($"{Time.frameCount} failuar : {_}"),
                    e => Debug.Log($"{Time.frameCount} failuar : {e}"),
                    () => Debug.Log($"{Time.frameCount} failuar : completed.")
                );
        }

    }

    private IEnumerator FuncCoroutineComplete(IObserver<int> observer)
    {
        observer.OnNext(0);

        yield return new WaitForSeconds(1.0f);

        observer.OnNext(1);

        yield return new WaitForSeconds(1.0f);

        observer.OnNext(2);
        observer.OnCompleted();
    }

    private IEnumerator FuncCoroutineFailuar(IObserver<int> observer)
    {
        observer.OnNext(0);

        yield return new WaitForSeconds(1.0f);

        observer.OnNext(1);

        yield return new WaitForSeconds(1.0f);

        observer.OnNext(2);
        observer.OnError(new Exception("example failuar"));
    }
}
