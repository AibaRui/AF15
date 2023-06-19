using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepInfo : MonoBehaviour
{
    [SerializeField] private bool _isCanStep = true;

    [SerializeField] private int _stepLevel = 1;

    public bool IsCanStep => _isCanStep;

    public int StepLevel => _stepLevel;


    public void StepLevel1()
    {
        _isCanStep = true;
        _stepLevel = 1;
    }
    public void StepLevel2()
    {
        _stepLevel = 20;
    }

}
