using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Main Menù
    public GameObject titleScreen;

    //References
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    //In game Settings
    public bool gameOver = false; //Giusto per essere sicuri
    public float spawnRate = 2f; //Default spawnRate

    //In game state
    private int score;


    public void StartGame(int difficulty)
    {
        //Imposto la UI
        titleScreen.SetActive(false);
        scoreText.gameObject.SetActive(true);
        
        //Imposto la difficoltà e imposto i parametri vitali del gioco
        spawnRate /= difficulty;
        gameOver = false;
        score = 0;

        //Faccio partire il gioco
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
    }

    public void GameOver()
    {
        //Imposto che il gioco è finito
        gameOver = true;

        //Mostro la UI di Game Over
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

        //Tolgo lo score
        scoreText.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        //Fa la ricarica della scena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = $"Score: {score}";
    }

    private IEnumerator SpawnTarget()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            var target = targets.ElementAt(index);
            Instantiate(target);
        }
    }

}
