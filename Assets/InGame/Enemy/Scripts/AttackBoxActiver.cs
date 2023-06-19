using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoxActiver : MonoBehaviour
{

    [SerializeField] private GameObject _box;

    public void OnBox()
    {
        _box.SetActive(true);
    }

    public void OffBox()
    {
        _box.SetActive(false);
    }
}
