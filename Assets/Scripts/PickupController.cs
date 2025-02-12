using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{

    private int distance = 5;
    private int movement = 1;
    void Start()
    {
        StartCoroutine("MoveObject");
    }
    IEnumerator MoveObject()
    {
        while (true)
        {
            transform.Translate(new Vector2(0,.2f) * movement);
            distance -= 1;
            if(distance <= 0)
            {
                distance = 5;
                movement *= -1;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
