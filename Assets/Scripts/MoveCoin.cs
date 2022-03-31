using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCoin : MonoBehaviour
{
    public float counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - Time.deltaTime*3);
        if (counter > 15f)
        {
            Destroy(this.gameObject);
        }
    }
}
