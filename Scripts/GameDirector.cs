
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [SerializeField] private GameObject gameClear;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private BallController ballController; // BallController を直接参照する
    

    public void Initialize()
    {
        gameClear.SetActive(false);
        gameOver.SetActive(false);

        // BallControllerコンポーネントを取得
        //ballController = GetComponent<BallController>();

        if (ballController != null)
        {
            // イベントを登録
            ballController.OnGameOver += GameOver;
            ballController.OnGameClear += GameClear;
            Debug.Log("Start: GameDirector");
        }
        else
        {
            Debug.LogError("BallController component is not attached to the GameObject.");
        }
    }

    void Start()
    {
        Initialize();
    }

    public void GameClear()
    {
        gameClear.SetActive(true);
        Debug.Log("Game Clear!");
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Debug.Log("Game Over!");
    }

    private void OnDestroy()
    {
        // イベントの解除（メモリリークを防ぐため）
        if (ballController != null)
        {
            ballController.OnGameOver -= GameOver;
            ballController.OnGameClear -= GameClear;
        }
    }
}
