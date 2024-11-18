using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // float x = Random.Range(-11,12);
        // float y = Random.Range(-5,6);
        // transform.Translate(0.1f,-0.1f,0);

        objectDestroy();
    }
    
    void objectDestroy(){
        if (transform.position.x > 13.0f || transform.position.x < -13.0f || transform.position.y > 7.0f ||
        transform.position.y < -7.0f ){
            Destroy(gameObject);
        }
    }
}
