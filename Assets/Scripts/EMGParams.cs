using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EMGParams 
{
    public List<float> rom = new List<float>(2);
    //public List<float> emg = new List<float>(8);
    public int leg;

    public string ShowParams()
    {
        string info = "R_Rom: " + rom[0].ToString() + "; L_Rom: " + rom[1].ToString();
        //info += "\n C1: " + emg[0].ToString() + " C2: " + emg[1].ToString()
        //    + " C3: " + emg[2].ToString() + " C4: " + emg[3].ToString()
        //    + " C5: " + emg[4].ToString() + " C6: " + emg[5].ToString()
        //    + " C7: " + emg[6].ToString() + " C8: " + emg[7].ToString();

        Debug.Log("Show Params Info: " + info);
        return info;
    }
}
