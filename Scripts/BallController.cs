using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    // イベント宣言
    public event System.Action OnGameOver;
    public event System.Action OnGameClear;

    // MainBall
    [SerializeField] private GameObject mainBall = null;
    // 打つ力
    [SerializeField] private float power = 0.1f;
    // 方向表示用オブジェクトのトランスフォーム
    [SerializeField] private Transform arrow = null;
    [SerializeField] private Image image = null;
    // ボールリスト
    [SerializeField] private List<ColorBall> ballList = new List<ColorBall>();
    public List<ColorBall> BallList => ballList;
    public GameObject MainBall => mainBall;

    // マウス位置保管用
    Vector3 mousePos = new Vector3();
    // MainBall:Rigidbody
    Rigidbody mainrb = null;
    // リセットのためのメインボールの初期値の保管
    Vector3 mainBallDefaultPos = new Vector3();

    void Start ()
    {
        // MainBallのリジッドボディを取得
        mainrb = mainBall.GetComponent<Rigidbody>();
        mainBallDefaultPos = mainBall.transform.localPosition;
        arrow.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!mainBall.activeSelf)
        {
            Debug.Log("main ball is destroied ! Game Over Trigger");
            OnGameOver.Invoke();  // MainBallが破壊されたらGameOverを通知
        }

        if (ballList != null && ballList.Count == 0)
        {
            OnGameClear?.Invoke();  // BallListが空になったらGameClearを通知
        }

        // メインボールがアクティブな時
        if (mainBall != null && mainBall.activeSelf)
        {
            // マウスクリック開始時
            if(Input.GetMouseButtonDown(0))
            {
                // 開始位置の保管
                mousePos = Input.mousePosition;
                // 方向線の表示
                arrow.gameObject.SetActive(true);
                Debug.Log("クリック開始");
            }

            // マウスクリック中
            if(Input.GetMouseButton(0))
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
                arrow.localRotation = qua;
                arrow.transform.position = mainBall.transform.position;

                // マウスの移動距離に応じてImageのスケールを変更
                image.rectTransform.localPosition = mainBall.transform.position;
                float distance = def.magnitude;
                float scaleFactor = distance * 0.005f;
                image.rectTransform.localScale = new Vector3(scaleFactor, 1.5f, 1) ;
            }

            // マウスクリック終了時
            if(Input.GetMouseButtonUp(0))
            {
                // 終了時の位置を保管
                Vector3 upPos = Input.mousePosition;
                // 開始位置と終了位置のベクトル計算から打ち出す方向を算出
                Vector3 def = mousePos - upPos;
                Vector3 add = new Vector3(def.x, 0, def.y);
                // メインボールに力を加える
                mainrb.AddForce(add * power);
                // 方向線を非表示に
                arrow.gameObject.SetActive(false);

                Debug.Log("クリック終了");
            }
        }
        else
        {
            Debug.Log("メインボールがアクティブではありません");
            //OnResetButtonClicked();
        }
    }

    public void OnResetButtonClicked()
    {   
        /*
        mainBall.SetActive(true);
        mainrb.velocity = Vector3.zero;
        mainrb.angularVelocity = Vector3.zero;
        mainBall.transform.localPosition = mainBallDefaultPos;
        

        foreach(ColorBall ball in ballList)
        {
            ball.Reset();
        }
        */

        SceneManager.LoadScene("MainScene");
    }

    public void RemoveBall(ColorBall ball)
    {
        if (ballList.Contains(ball))
        {
            ballList.Remove(ball);

            if (ballList.Count == 0)
            {
                OnGameClear?.Invoke();  // リストが空になったらGameClearを通知
            }
        }
    }
}
