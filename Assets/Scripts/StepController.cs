using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StepController : Singleton<StepController>
{
    public Image pisada_izquierda;
    public Image pisada_derecha;
    public GameObject referencia_izquierda;
    public GameObject referencia_derecha;
    public Color pisadas_activas;
    public GameObject feedback_derecha;
    public GameObject feedback_izquierda;
    public TMP_Text debug;
    public float longitud_pierna;

    public void ActivateSteps()
    {
        pisada_derecha.color = pisadas_activas;
        pisada_izquierda.color = pisadas_activas;
    }

    public void SetReferences()
    {
        referencia_derecha.transform.localPosition += new Vector3(0, (longitud_pierna*2.0f*0.413f/100.0f), 0);
        referencia_izquierda.transform.localPosition += new Vector3(0, (longitud_pierna*4.0f*0.413f/100.0f), 0);
        debug.text = "Seteando referencias: "+ longitud_pierna.ToString();
    }

    public void UpdateStride(float LRom, float RRom)
    {
        if (LRom > 0)
        {
            float avance = longitud_pierna/100.0f * Mathf.Sin(LRom);
            //float desv_izquierda = longitud_pierna/100.0f * Mathf.Sin(angulo_rotacion);
            feedback_izquierda.transform.localPosition += new Vector3(0, avance / 10, 0);
            debug.text = "Angulo: " + LRom.ToString() + ", avance: " + avance.ToString();
        }

        if (RRom > 0)
        {
            float avance = longitud_pierna / 100.0f * Mathf.Sin(RRom);
            //float desv_derecha = longitud_pierna / 100.0f * Mathf.Sin(angulo_rotacion);
            feedback_derecha.transform.localPosition += new Vector3(0, avance / 10, 0);
            debug.text = "Angulo: " + RRom.ToString() + ", avance: " + avance.ToString();
        }    
    }

    private void Update()
    {
        if (Vector3.Distance(feedback_izquierda.transform.localPosition, referencia_izquierda.transform.localPosition) < 0.05f)
            SetReferences();
    }
}
