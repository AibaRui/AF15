using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour
{
    [SerializeField] private PlayerControl _player;

    [SerializeField] private int addDamage;
    private void Awake()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            _player.GetComponent<IDamageble>().AddDamage(addDamage);
        }
    }


}
