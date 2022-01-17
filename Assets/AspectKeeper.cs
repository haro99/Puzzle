using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectKeeper : MonoBehaviour
{
    [SerializeField]
    private Camera targetCamera; //対象とするカメラ

    [SerializeField]
    private Vector2 aspectVec; //目的解像度

    // Start is called before the first frame update
    void Start()
    {
        var screenAspect = Screen.width / (float)Screen.height; //画面のアスペクト比
        var targetAspect = aspectVec.x / aspectVec.y; //目的のアスペクト比

        var magRate = targetAspect / screenAspect; //目的アスペクト比にするための倍率
        Debug.Log(magRate);
        var size = targetCamera.orthographicSize;
        targetCamera.orthographicSize = size * magRate;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
