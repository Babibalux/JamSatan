using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MacroManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject scoreMenu;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI note;
    public GameManager gameManager;

    public static MacroManager instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    [ContextMenu("StartGame")]
    public void StartGame()
    {
        mainMenu.SetActive(false);

        gameManager.enabled = true;
        gameManager.StartGame();
    }

    public void ScoreMenu(int score)
    {
        gameManager.enabled =false;

        scoreMenu.SetActive(true);
        scoreText.text = score.ToString();
        //Note
    }

    public void OpenMainMenu()
    {
        scoreMenu.SetActive(false);

        mainMenu.SetActive(true);
    }
}
