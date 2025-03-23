using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject Container;

    private bool isPaused = false;

    private void Start()
    {
        Container.SetActive(false);
        Time.timeScale = 1f; // 念のため通常速度に戻す
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // ゲーム停止
            Container.SetActive(true); // UI表示
        }
        else
        {
            Time.timeScale = 1f; // ゲーム再開
            Container.SetActive(false); // UI非表示
        }
    }

    public void Retry()
    {
        Time.timeScale = 1f; // 念のため再開してからリロード
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Title()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
}