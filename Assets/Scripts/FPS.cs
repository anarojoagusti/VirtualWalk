using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    public TMP_Text fps_text;
    // Update is called once per frame
    void Update()
    {
        fps_text.text = (1.0f / Time.deltaTime).ToString() + " FPS";
    }
}
