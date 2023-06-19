using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PauseManager _pauseManager;



    private static GameManager _gm;

    public PauseManager PauseManager => _pauseManager;

    public static GameManager Instance => _gm;

    private void Awake()
    {
        _gm = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
