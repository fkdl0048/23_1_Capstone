using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonNPC : MonoBehaviour
{
    private bool _isFlip = false;
    private float _count = 0;
    private float _speed = 4.0f;
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFlip)
        {
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
            _count += Time.deltaTime;
            if (_count > 2)
            {
                _isFlip = false;
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                _count = 0;
            }
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
            _count += Time.deltaTime;
            if (_count > 2)
            {
                _isFlip = true;
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                _count = 0;
            }
        }
    }
}
