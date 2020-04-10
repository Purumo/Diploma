using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.GetInstance().FinishGame();
    }
}
