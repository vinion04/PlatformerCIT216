using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesController : MonoBehaviour
{
    public TMP_Text livesTxt;

    void Start()
    {
        livesTxt.SetText("Lives: " + GameManager.instance.GetLives());
    }
}
