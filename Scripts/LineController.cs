using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineController : MonoBehaviour
{
    Image image;
    RectTransform rectTransform;
    Vector3 mousePos = new Vector3();


    void Start()
    {
        image = GetComponent<Image>();
        this.rectTransform = image.GetComponent<RectTransform>();
    }

    void Update()
    {
        // マウスクリック開始時
            if(Input.GetMouseButtonDown(0) == true)
            {
                // 開始位置の保管
                mousePos = Input.mousePosition;
                // 方向線の表示
                Debug.Log("クリック開始");
            }

            // マウスクリック中
            if(Input.GetMouseButton(0) == true)
            {
                // 現在の位置を随時保管
                Vector3 pos = Input.mousePosition;
                // 角度の算出
                Vector3 def = mousePos - pos;
                float rad = Mathf.Atan2(def.x, def.y);
                float angle = rad * Mathf.Rad2Deg;
                Vector3 rot = new Vector3(0, angle, 0);
                Quaternion qua = Quaternion.Euler(rot);

                // 方向線の位置角度を設定
                rectTransform.localScale = new Vector3(rad, this.rectTransform.localScale.y, this.rectTransform.localScale.z);
                
            }

             
            
    }

}
