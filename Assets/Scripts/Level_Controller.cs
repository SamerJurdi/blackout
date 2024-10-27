using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Controller : MonoBehaviour
{
    [SerializeField] GameObject victoryUI;
    [SerializeField] GameObject deathUI;

    public void endGame(string message, bool isAlive) {
        Time.timeScale = 0f;
        if (isAlive)
        {
            victoryUI.transform.Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = message;
            victoryUI.SetActive(true);
        } else {
            deathUI.transform.Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = message;
            deathUI.SetActive(true);
        }
    }
    
    public void restartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
