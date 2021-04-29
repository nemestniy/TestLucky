using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int Count = 3;

    public GameObject StartButton;
    public GameObject RestartButton;

    public Text HPText;
    public Text ScoreText;
    public Text CountText;
    public Text WinText;

    public UIJoystick Joystick;

    private bool isInitialized = false;

    public void Initialize()
    {
        HPText.text = "HP: " + GameManager.Player.HP;
        ScoreText.text = "Score: " + Player.Score;

        StartButton.SetActive(true);
        RestartButton.SetActive(false);
        CountText.gameObject.SetActive(false);
        Joystick.Initialize();
        WinText.gameObject.SetActive(false);

        isInitialized = true;
    }

    public void Finish()
    {
        RestartButton.SetActive(true);
        Joystick.Hide();
        WinText.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(isInitialized)
        {
            HPText.text = "HP: " + GameManager.Player.HP;
            ScoreText.text = "Score: " + Player.Score;
        }
    }

    public void ShowRestartButton()
    {
        RestartButton.SetActive(true);
        Joystick.Hide();
    }

    public void OnClickStartButton()
    {
        StartButton.SetActive(false);
        StartCoroutine(Counting(Count));
    }

    public void OnClickRestartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    private IEnumerator Counting(int count)
    {
        var floatCount = (float)count;
        CountText.text = floatCount.ToString();
        CountText.gameObject.SetActive(true);

        while(floatCount > 0)
        {
            floatCount -= Time.deltaTime;
            CountText.text = floatCount.ToString();
            yield return new WaitForEndOfFrame();
        }

        CountText.gameObject.SetActive(false);

        GameManager.Instance.StartGame();
        Joystick.Show();
    }
}
