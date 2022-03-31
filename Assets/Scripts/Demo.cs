using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    public Image icono;
    public Image pisada_izquierda;
    public Image pisada_derecha;
    public Color pisadas_activas;
    public GameObject derecha;
    public GameObject izquierda;

    float counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        pisada_derecha.material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.realtimeSinceStartup > 10f && Time.realtimeSinceStartup < 11f)
        {
            icono.color = Color.green;
            pisada_derecha.color = pisadas_activas;
            pisada_izquierda.color = pisadas_activas;
        }

        counter += Time.deltaTime;
        switch ((int)counter)
        {
            case 15:
                derecha.transform.localPosition += new Vector3(0, 0.03f, 0);
                break;
            case 18:
                izquierda.transform.localPosition += new Vector3(0, 0.03f,0);
                break;
            case 30:
                derecha.transform.localPosition += new Vector3(0, 0.03f, 0);
                break;
            case 33:
                izquierda.transform.localPosition += new Vector3(0, 0.03f, 0);
                break;
        }

    }
}
