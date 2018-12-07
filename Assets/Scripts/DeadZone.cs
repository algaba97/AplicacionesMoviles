using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("holaa");
        BallLogic ball = collision.gameObject.GetComponent<BallLogic>();
        if (ball != null)
        {
            ball.Stop();
        }

    }
}
