using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    [Header("Attack_çUåÇóÕ")]
    [SerializeField] private int damage1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<IDamageble>()?.AddDamage(damage1);
            this.gameObject.SetActive(false);
        }
    }

}
