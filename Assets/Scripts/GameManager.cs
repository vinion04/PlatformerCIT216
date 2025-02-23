using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //private
    private int lives = 3;
    private GameObject txt;

    //public
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);//handles duplicate GameObjects

        DontDestroyOnLoad(gameObject);  //stays persistent

        txt = GameObject.Find("StoryText");
        StartCoroutine(Start());
    }

    public void DecreaseLives()
    {
        lives--;
    }

    public int GetLives()       //getter
    {
        return lives;
    }

    private IEnumerator Start()
    {
        txt.SetActive(true);
        yield return new WaitForSeconds(2f);
        txt.SetActive(false);
    }
}
