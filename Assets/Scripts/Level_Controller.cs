using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Controller : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;

    public void endGame(string message) {
        pauseMenuUI.transform.Find("Message").GetComponent<TMPro.TextMeshProUGUI>().text = message;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void restartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
