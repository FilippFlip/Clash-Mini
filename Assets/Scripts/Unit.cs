using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isFriendly;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (isFriendly)
        {
          transform.position = transform.position + Vector3.right * Time.deltaTime;

        }
        else
        {
            transform.position = transform.position + Vector3.left * Time.deltaTime;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Unit>(out Unit fight))
        {
            if (isFriendly == fight.isFriendly)
            {
                ///////////////////Анимация атаки будет здесь///////////////////////////////////
            }
        }
    }

}
