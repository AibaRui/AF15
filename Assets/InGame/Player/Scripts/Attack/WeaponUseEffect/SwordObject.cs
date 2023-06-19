using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordObject : MonoBehaviour, ITrailable
{
    [SerializeField] private List<GameObject> _trails = new List<GameObject>();

    public void UseTrail(bool isUse)
    {
        foreach (var trail in _trails)
        {
            trail.SetActive(isUse);
        }
    }
}
