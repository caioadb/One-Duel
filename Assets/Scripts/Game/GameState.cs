using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField]
    HPValue playerHP = null;
    [SerializeField]
    HPValue enemyHP = null;
    [SerializeField]
    AudioSource hitSource = null;
    [SerializeField]
    AudioSource enemyHitSource = null;
    [SerializeField]
    AudioSource deathSource = null;    
    [SerializeField]
    AudioSource winSource = null;
    [SerializeField]
    GameObject gameoverUI = null;    
    [SerializeField]
    GameObject loseUI = null;    
    [SerializeField]
    GameObject winUI = null;    
    [SerializeField]
    GameObject gameUI = null;

    private void Start()
    {
        Time.timeScale = 1f;
        gameoverUI.SetActive(false);
        gameUI.SetActive(true);
        playerHP.OnVariableChange += playerHPChangeHandler;
        enemyHP.OnVariableChange += enemyHPChangeHandler;
        Cursor.visible = false;
    }

    private void playerHPChangeHandler(int hpVal)
    {
        if (hpVal == 0) // if player is dead
        {
            //play death  sound
            deathSource.Play();

            Time.timeScale = 0f; // stop time       
            gameUI.SetActive(false);
            gameoverUI.SetActive(true);
            winUI.SetActive(false);
            //youwin

            Cursor.visible = true;
        }
        else
        {
            //play hit sound
            hitSource.Play();
        }
    }

    private void enemyHPChangeHandler(int hpVal)
    {
        if (hpVal == 0) // if enemy is dead
        {
            //play death  sound
            winSource.Play();
            Time.timeScale = 0f; // stop time
            gameUI.SetActive(false);
            gameoverUI.SetActive(true);
            loseUI.SetActive(false);
            //you lose

            Cursor.visible = true;            
        }
        else
        {
            //play hit sound
            enemyHitSource.Play();
        }
    }

    public void MenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

}
