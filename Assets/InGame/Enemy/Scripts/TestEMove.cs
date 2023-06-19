using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEMove : MonoBehaviour
{
    [SerializeField] private float _time = 3;

    private float _timeCount = 0;

    [SerializeField] private Rigidbody _rb;

    Vector3 startPos;
    Vector3 dir = Vector2.zero;

    private bool isMoveEnd = false;

    void Start()
    {

    }


    void Update()
    {
        _timeCount += Time.deltaTime;

        if (_timeCount > _time && !isMoveEnd)
        {
            int x = Random.Range(-10, 10);
            int z = Random.Range(-10, 10);

            dir = new Vector3(x, 0, z).normalized;
            startPos = transform.position;
            isMoveEnd = true;
            _timeCount = 0;
            _rb.velocity = dir * 3;
        }

        if (Vector3.Distance(startPos, transform.position) > 7 && isMoveEnd)
        {
            isMoveEnd = false;
            _rb.velocity = Vector3.zero;
        }


    }
}
