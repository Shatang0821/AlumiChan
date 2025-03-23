using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject Container;

    private bool isPaused = false;

    private void Start()
    {
        Container.SetActive(false);
        Time.timeScale = 1f; // �O�̂��ߒʏ푬�x�ɖ߂�
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
            Time.timeScale = 0f; // �Q�[����~
            Container.SetActive(true); // UI�\��
        }
        else
        {
            Time.timeScale = 1f; // �Q�[���ĊJ
            Container.SetActive(false); // UI��\��
        }
    }

    public void Retry()
    {
        Time.timeScale = 1f; // �O�̂��ߍĊJ���Ă��烊���[�h
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Title()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
}