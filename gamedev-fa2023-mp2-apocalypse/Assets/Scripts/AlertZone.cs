using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertZone : MonoBehaviour
{
    public EnemyControl enemyControl;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && enemyControl.alerted == false)
        {
            enemyControl.alerted = true;
            
        }
    }
}
