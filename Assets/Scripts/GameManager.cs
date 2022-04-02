using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    public Text puntosText;
    int puntos;

    public Text smashedText;
    AudioSource sableRojoAudio;
    AudioSource sableAzulAudio;

    float t;
    int segundos;
    public TMP_Text cuentaAtras;
    public Image cuentaAtrasBackground;
    public  bool gameStarted = false;

    public GameObject sableRojo;
    public GameObject sableAzul;

    public List<GameObject> veggies;

    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        sableRojoAudio = sableRojo.GetComponent<AudioSource>();
        sableAzulAudio = sableAzul.GetComponent<AudioSource>();

        t = 0f;
        segundos = 5;
        cuentaAtras.text = segundos.ToString();

        //sableRojo.SetActive(false);
        //sableAzul.SetActive(false);
        cuentaAtras.gameObject.SetActive(false);
        foreach (GameObject veg in veggies)
        {
            PoolManager.instance.Load("Veggies", veg, 1);
        }
    }

    IEnumerator SpawnVeggies ()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 6));
            int rndVeggie = Random.Range(0, 2);
            Instantiate(veggies[rndVeggie], 
                new Vector3(playerTransform.position.x + Random.Range(-0.5f, 0.5f), playerTransform.position.y + Random.Range(-0.5f, 0.5f), playerTransform.position.z + 10f), Quaternion.Euler(0f, 90f, 90f));
            //PoolManager.instance.Spawn("Veggies", veggies[rndVeggie], rndVeggie,
            //    new Vector3(playerTransform.position.x + Random.Range(-1, 1), playerTransform.position.y + Random.Range(-1, 1), playerTransform.position.z + 10f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        if (t >= 1 && segundos > 0 && gameStarted)
        {
            t = 0f;
            segundos -= 1;
            cuentaAtras.text = segundos.ToString();
        }else if(segundos <= 0 && gameStarted)
        {
            gameStarted = false;
            StartCoroutine(SpawnVeggies());
            cuentaAtras.gameObject.SetActive(false);
        }

        if (gameStarted)
        {
            cuentaAtras.gameObject.SetActive(true);
            //cuentaAtrasBackground.gameObject.SetActive(true);
            //sableRojo.SetActive(true);
            //sableAzul.SetActive(true);
        }
        if(Time.realtimeSinceStartup > 180)
        {
            StopAllCoroutines();
            cuentaAtras.gameObject.SetActive(true);
            cuentaAtras.text =  "Game Over!";

        }
    }

    void SetPuntos ()
    {
        puntosText.text = "SCORE: " + puntos;
    }

    public void AddPuntos ()
    {
        puntos += 1;
        SetPuntos();
    }

    public void SetSmashedText (bool isActiv)
    {
        smashedText.color = new Color(smashedText.color.r, smashedText.color.g, smashedText.color.b, 1f);
        smashedText.gameObject.SetActive(isActiv);

        if (isActiv)
        {
            sableAzulAudio.Play();
            sableRojoAudio.Play();
        }
    }
}
