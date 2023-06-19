using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineActiveFalser : MonoBehaviour
{
    public void End()
    {
        this.gameObject.SetActive(false);
    }
}
