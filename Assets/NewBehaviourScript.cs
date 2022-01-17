using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject Object;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i< 4;i++)
        {
            GameObject @object = Instantiate(Object, new Vector3(0, 8+i, 0), Quaternion.identity);
            @object.transform.DOLocalMoveY(i, 2f);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
