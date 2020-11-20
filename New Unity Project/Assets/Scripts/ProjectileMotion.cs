using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
    public float angulo;
    public float velocidad;
    public float gravedad = 9.8f;

    public Transform Projectile;

    public float vx;
    public float vy;

    float xInicial;
    float yInicial;


    public Vector3 posicionInicial;
    float elapse_time = 0;

    private IEnumerator coroutine;

    void Start()
    {
        posicionInicial = GameObject.Find("Balon").transform.position;
        vx = velocidad * Mathf.Sin((angulo * Mathf.PI) / 180);
        vy = velocidad * Mathf.Cos((angulo * Mathf.PI) / 180);
        xInicial = posicionInicial.x;
        yInicial = posicionInicial.y;
        coroutine = WaitAndPrint(0.1f);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        float piso = 0f;
        Vector3 posicionactual = GameObject.Find("Balon").transform.position;
        float posicioneny = posicionactual.y;
        while (posicioneny + (yInicial + vy * elapse_time + 0.5f * elapse_time * elapse_time * gravedad)*2 >= piso)
        {
            yield return new WaitForSeconds(waitTime);
            SimulateProjectile();
            elapse_time = elapse_time + 0.05f;
            posicionactual = GameObject.Find("Balon").transform.position;
            posicioneny = posicionactual.y;
        }
    }

    void SimulateProjectile()
    {
        Vector3 posactual = GameObject.Find("Balon").transform.position;
        float posicionactualx = posactual.x;
        float posicionactualy = posactual.y;
        Projectile.position += new Vector3((xInicial + vx * elapse_time)-posicionactualx, (yInicial + vy * elapse_time + 0.5f * elapse_time * elapse_time * gravedad)-posicionactualy, 0);
    }

}

