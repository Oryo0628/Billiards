using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("落ちたボール：" + other.gameObject.name);
        // 落ちたボールを非アクティブにする
        other.gameObject.SetActive(false);
    }

}
