using UnityEngine;
using System.Collections;
using System;


[Serializable]
public class RequestObject : MonoBehaviour
{
    public string CallingApplicationName;
    public string DestinationAppName;

    public Vector3 TargetPosition;

    void Start()
    {
        StartCoroutine(LerpPosition(TargetPosition, 5));
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
  

    // Update is called once per frame
    void Update()
    {
     if(transform.position == TargetPosition){
         DestroyImmediate(gameObject);
     }
    }
}
