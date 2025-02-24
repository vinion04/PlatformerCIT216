using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //private
    private int lives = 3;

    //public
    public static GameManager instance;

    void Awake()
    {
        Debug.Log("Awake");
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);//handles duplicate GameObjects

        DontDestroyOnLoad(gameObject);  //stays persistent
    }

    public void DecreaseLives()
    {
        if(lives >= 2)
            lives--;
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            lives = 3;
        }
    }

    public int GetLives()       //getter
    {
        return lives;
    }
}
