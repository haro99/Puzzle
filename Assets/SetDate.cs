using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クッキーの位置データ
/// </summary>
public class SetDate : MonoBehaviour
{
    public GameManager GameManager;
    public int x, y, number;

    private void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    private void OnMouseDown()
    {
        GameManager.Check(this.gameObject);
    }
}
