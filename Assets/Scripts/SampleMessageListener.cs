using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Globalization;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
/**
 * When creating your message listeners you need to implement these two methods:
 *  - OnMessageArrived
 *  - OnConnectionEvent
 */
public class SampleMessageListener : MonoBehaviour
{
    #region _VARIABLES_

    public bool long_pierna_get;
    public Text t_data_received;

    #endregion _VARIABLES_

    private void Start()
    {
        long_pierna_get = false;
    }
    // Invoked when a line of data is received from the serial device.
    public void OnMessageArrived(string msg)
    {
        UnityEngine.Debug.Log("message arrived: "+ msg);
        string[] msg_split = msg.Split('|');
        if (msg_split.Length < 1)
            return;
        else
        {
            if (long_pierna_get)
            {
                //EMGParams data = JsonUtility.FromJson<EMGParams>("{" + msg_start[1]);
                //t_data_received.text = data.ShowParams();
                StepController.instance.UpdateStride(float.Parse(msg_split[1]), float.Parse(msg_split[0]));

            }
            else
            {

                //EMGParams data = JsonUtility.FromJson<EMGParams>("{" + msg_start[1]);
                StepController.instance.longitud_pierna = float.Parse(msg_split[2]);
                //long_pierna_get = true;
                StepController.instance.SetReferences();
            }
        }
    }






    //private void Update()
    //{
    //    if(DCMRecibido_L && first_time_L)
    //    {
    //        first_time_L = false;
    //        Calibrar(0);
    //    }
    //    if (DCMRecibido_R && first_time_R)
    //    {
    //        first_time_R = false;
    //        Calibrar(1);
    //    }
    //}

    //public bool CalculateMatrix(string[] words2, int lado)
    //{
    //    var clone = (CultureInfo)CultureInfo.InvariantCulture.Clone();
    //    clone.NumberFormat.NumberDecimalSeparator = ".";
    //    clone.NumberFormat.NumberGroupSeparator = ".";

    //    if (lado == 0)
    //    {
    //        Rs_L[0, 0] = Convert.ToSingle(decimal.Parse(words2[0], clone));
    //        Rs_L[0, 1] = Convert.ToSingle(decimal.Parse(words2[1], clone));
    //        Rs_L[0, 2] = Convert.ToSingle(decimal.Parse(words2[2], clone));

    //        Rs_L[1, 0] = Convert.ToSingle(decimal.Parse(words2[3], clone));
    //        Rs_L[1, 1] = Convert.ToSingle(decimal.Parse(words2[4], clone));
    //        Rs_L[1, 2] = Convert.ToSingle(decimal.Parse(words2[5], clone));

    //        Rs_L[2, 0] = Convert.ToSingle(decimal.Parse(words2[6], clone));
    //        Rs_L[2, 1] = Convert.ToSingle(decimal.Parse(words2[7], clone));
    //        Rs_L[2, 2] = Convert.ToSingle(decimal.Parse(words2[8], clone));
    //    }
    //    else
    //    {
    //        Rs_R[0, 0] = Convert.ToSingle(decimal.Parse(words2[0], clone));
    //        Rs_R[0, 1] = Convert.ToSingle(decimal.Parse(words2[1], clone));
    //        Rs_R[0, 2] = Convert.ToSingle(decimal.Parse(words2[2], clone));

    //        Rs_R[1, 0] = Convert.ToSingle(decimal.Parse(words2[3], clone));
    //        Rs_R[1, 1] = Convert.ToSingle(decimal.Parse(words2[4], clone));
    //        Rs_R[1, 2] = Convert.ToSingle(decimal.Parse(words2[5], clone));

    //        Rs_R[2, 0] = Convert.ToSingle(decimal.Parse(words2[6], clone));
    //        Rs_R[2, 1] = Convert.ToSingle(decimal.Parse(words2[7], clone));
    //        Rs_R[2, 2] = Convert.ToSingle(decimal.Parse(words2[8], clone));

    //    }
    //    return true;
    //}
    //private Single[,] GetDCMFromQuaternion(Quaternion quat)
    //{
    //    Single[,] matrizresultante = new Single[3, 3];

    //    matrizresultante[0, 0] = (float)(1 - 2 * Math.Pow(quat.y, 2) - 2 * Math.Pow(quat.z, 2));
    //    matrizresultante[0, 1] = (float)(2*quat.x*quat.y - 2 *quat.z*quat.w);
    //    matrizresultante[0, 2] = (float)(2 * quat.x * quat.w + 2 * quat.y * quat.w);

    //    matrizresultante[1, 0] = (float)(2 * quat.x * quat.y + 2 * quat.z * quat.w);
    //    matrizresultante[1, 1] = (float)(1 - 2 * Math.Pow(quat.x, 2) - 2 * Math.Pow(quat.z, 2));
    //    matrizresultante[1, 2] = (float)(2 * quat.y * quat.z - 2 * quat.x * quat.w);

    //    matrizresultante[2, 0] = (float)(2 * quat.x * quat.z - 2 * quat.y * quat.w);
    //    matrizresultante[2, 1] = (float)(2 * quat.y * quat.z + 2 * quat.x * quat.w);
    //    matrizresultante[2, 2] = (float)(1 - 2 * Math.Pow(quat.x, 2) - 2 * Math.Pow(quat.y, 2));

    //    return matrizresultante;
    //    //1 - 2y ^ 2 - 2z ^ 2 & 2xy - 2zw & 2xz + 2yw
    //    //2xy + 2zw & 1 - 2x ^ 2 - 2z ^ 2 & 2yz - 2xw
    //    //2xz - 2yw & 2yz + 2xw & 1 - 2x ^ 2 - 2y ^ 2
    //}
    //public void Calibrar(int lado)
    //{
    //    if (lado == 0)
    //    {
    //        Rcal_L = Rs_L;
    //        Rcal_L = traspuesta(Rcal_L);
    //    }
    //    else
    //    {
    //        Rcal_R = Rs_R;
    //        Rcal_R = traspuesta(Rcal_R);
    //    }
    //}
    //void CalculateEuler(string[] words2, int lado)
    //{
    //    if (CalculateMatrix(words2, lado) == false)
    //        return;
    //    if (lado == 0)
    //    {
    //        RT_L = multiplicaMatrices(Rcal_L, Rs_L);

    //        // Rodilla Sólo flexoextensión
    //        alfa_L = Convert.ToSingle(Math.Atan2(RT_L[0, 2], RT_L[0, 0]) * 180 / Math.PI); // Flexoextensión
    //        //beta_L = Convert.ToSingle(Math.Asin(RT_L[0, 1]) * 180 / Math.PI); // Inclinación
    //        gamma_L = Convert.ToSingle(Math.Atan2(-RT_L[2, 1], RT_L[1, 1]) * 180 / Math.PI); // Rotación

    //    }
    //    else
    //    {
    //        RT_R = multiplicaMatrices(Rcal_R, Rs_R);

    //        // Rodilla Sólo flexoextensión
    //        alfa_R = Convert.ToSingle(Math.Atan2(RT_R[0, 2], RT_R[0, 0]) * 180 / Math.PI); // Flexoextensión
    //        //beta_L = Convert.ToSingle(Math.Asin(RT_L[0, 1]) * 180 / Math.PI); // Inclinación
    //        gamma_R = Convert.ToSingle(Math.Atan2(-RT_R[2, 1], RT_R[1, 1]) * 180 / Math.PI); // Rotación
    //    }
    //}
    //void CalculateAceleration()
    //{
    //    moduleAcc = (float)Math.Sqrt((Acc[0, 0] * Acc[0, 0]) + (Acc[0, 1] * Acc[0, 1]) + (Acc[0, 2] * Acc[0, 2]));
    //}
    //private Single[,] multiplicaMatrices(Single[,] matriz1, Single[,] matriz2)
    //{
    //    Single[,] matrizresultante = new Single[3, 3];

    //    matrizresultante[0, 0] = matriz1[0, 0] * matriz2[0, 0] + matriz1[0, 1] * matriz2[1, 0] + matriz1[0, 2] * matriz2[2, 0];
    //    matrizresultante[0, 1] = matriz1[0, 0] * matriz2[0, 1] + matriz1[0, 1] * matriz2[1, 1] + matriz1[0, 2] * matriz2[2, 1];
    //    matrizresultante[0, 2] = matriz1[0, 0] * matriz2[0, 2] + matriz1[0, 1] * matriz2[1, 2] + matriz1[0, 2] * matriz2[2, 2];

    //    matrizresultante[1, 0] = matriz1[1, 0] * matriz2[0, 0] + matriz1[1, 1] * matriz2[1, 0] + matriz1[1, 2] * matriz2[2, 0];
    //    matrizresultante[1, 1] = matriz1[1, 0] * matriz2[0, 1] + matriz1[1, 1] * matriz2[1, 1] + matriz1[1, 2] * matriz2[2, 1];
    //    matrizresultante[1, 2] = matriz1[1, 0] * matriz2[0, 2] + matriz1[1, 1] * matriz2[1, 2] + matriz1[1, 2] * matriz2[2, 2];

    //    matrizresultante[2, 0] = matriz1[2, 0] * matriz2[0, 0] + matriz1[2, 1] * matriz2[1, 0] + matriz1[2, 2] * matriz2[2, 0];
    //    matrizresultante[2, 1] = matriz1[2, 0] * matriz2[0, 1] + matriz1[2, 1] * matriz2[1, 1] + matriz1[2, 2] * matriz2[2, 1];
    //    matrizresultante[2, 2] = matriz1[2, 0] * matriz2[0, 2] + matriz1[2, 1] * matriz2[1, 2] + matriz1[2, 2] * matriz2[2, 2];

    //    matrizresultante = normalizaMatriz(matrizresultante);

    //    return matrizresultante;
    //}
    //private Single[,] normalizaMatriz(Single[,] matriz)
    //{
    //    Single[,] matrizresultante = new Single[3, 3];

    //    Single[] fila0 = new Single[3];
    //    Single[] fila1 = new Single[3];
    //    Single[] fila2 = new Single[3];

    //    Single modulo0;
    //    Single modulo1;
    //    Single modulo2;

    //    fila0[0] = matriz[0, 0];
    //    fila0[1] = matriz[0, 1];
    //    fila0[2] = matriz[0, 2];
    //    fila1[0] = matriz[1, 0];
    //    fila1[1] = matriz[1, 1];
    //    fila1[2] = matriz[1, 2];
    //    fila2[0] = matriz[2, 0];
    //    fila2[1] = matriz[2, 1];
    //    fila2[2] = matriz[2, 2];

    //    modulo0 = modulo(fila0);
    //    modulo1 = modulo(fila1);
    //    modulo2 = modulo(fila2);

    //    matrizresultante[0, 0] = matriz[0, 0] / modulo0;
    //    matrizresultante[0, 1] = matriz[0, 1] / modulo0;
    //    matrizresultante[0, 2] = matriz[0, 2] / modulo0;

    //    matrizresultante[1, 0] = matriz[1, 0] / modulo1;
    //    matrizresultante[1, 1] = matriz[1, 1] / modulo1;
    //    matrizresultante[1, 2] = matriz[1, 2] / modulo1;

    //    matrizresultante[2, 0] = matriz[2, 0] / modulo2;
    //    matrizresultante[2, 1] = matriz[2, 1] / modulo2;
    //    matrizresultante[2, 2] = matriz[2, 2] / modulo2;

    //    return matrizresultante;
    //}
    //private Single[,] traspuesta(Single[,] matriz)
    //{
    //    Single[,] matrizresultante = new Single[3, 3];

    //    matrizresultante[0, 0] = matriz[0, 0];
    //    matrizresultante[0, 1] = matriz[1, 0];
    //    matrizresultante[0, 2] = matriz[2, 0];

    //    matrizresultante[1, 0] = matriz[0, 1];
    //    matrizresultante[1, 1] = matriz[1, 1];
    //    matrizresultante[1, 2] = matriz[2, 1];

    //    matrizresultante[2, 0] = matriz[0, 2];
    //    matrizresultante[2, 1] = matriz[1, 2];
    //    matrizresultante[2, 2] = matriz[2, 2];

    //    return matrizresultante;
    //}
    //private Single modulo(Single[] vector)
    //{
    //    Single modulo;
    //    //modulo = 0;
    //    modulo = Convert.ToSingle(Math.Sqrt((vector[0] * vector[0]) + (vector[1] * vector[1]) + (vector[2] * vector[2])));
    //    return modulo;
    //}
}
