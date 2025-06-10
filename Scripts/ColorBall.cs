using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBall : MonoBehaviour
{
    // リセット用の初期位置
    Vector3 defaultPos = new Vector3();
    // Rigidbody
    Rigidbody rb = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        defaultPos = this.transform.localPosition;
    }
    /// <summary>
    /// リセット時の処理
    /// </summary>
    public void Reset()
    {
        gameObject.SetActive(true);
        // リジッドボディの速度を強制的に０にする
        rb.velocity = Vector3.zero;
        // リジッドボディの回転速度を強制的に０にする
        rb.angularVelocity = Vector3.zero;
        // 初期位置に戻す
        this.transform.localPosition = defaultPos;
    }
}
