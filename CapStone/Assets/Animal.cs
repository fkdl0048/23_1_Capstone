using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float time;
    [SerializeField] private float randomRange;
    
    private Animator _animator;
    private float _x;
    private float _y;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

        StartCoroutine(GetRandomDirection());
    }

    void Update()
    {
        transform.Translate(new Vector3(_y, _x, 0) * speed * Time.deltaTime);
    }
    
    private IEnumerator GetRandomDirection()
    {
        while (true)
        {
            int random = Random.Range(0, 5);

            switch (random)
            {
                case 0:
                    _x = 0;
                    _y = 0;
                    break;
                case 1:
                    _x = 1;
                    _y = 0;
                    break;
                case 2:
                    _x = -1;
                    _y = 0;
                    break;
                case 3:
                    _x = 0;
                    _y = 1;
                    break;
                case 4:
                    _x = 0;
                    _y = -1;
                    break;
            }
            
            _animator.SetFloat("X", _x);
            _animator.SetFloat("Y", _y);
            
            if (_x != 0 || _y != 0)
                _animator.SetBool("IsMoving", true);
            else
                _animator.SetBool("IsMoving", false);

            yield return new WaitForSeconds(Random.Range(time - randomRange, time + randomRange));
        }
    } 
    
}
