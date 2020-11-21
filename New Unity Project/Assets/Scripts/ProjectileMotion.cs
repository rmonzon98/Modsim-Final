using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine; 
// v 25 a 14
public class ProjectileMotion : MonoBehaviour
{
    private float angulo;
    private float velocidad;

    public float velocidadMinima;
    public float velocidadMaxima;

    public float anguloMinimo;
    public float anguloMaximo;

    public int iteraciones;

    private float gravedad = -9.8f;

    public Transform Projectile;

    public Text result;

    public float vx;
    public float vy;

    float xInicial;
    float yInicial;


    public Vector3 posicionInicial;
    float elapse_time = 0;

    private IEnumerator coroutine;

    private int itersCount = 0;

    private int scores = 0;

    private List<float> velocidades = new List<float>();
    private List<float> angulos = new List<float>();

    void Start()
    {
        if(itersCount < iteraciones) {
            Debug.Log("Iter + " + itersCount);
            velocidad = Random.Range(velocidadMinima, velocidadMaxima);
            angulo = Random.Range(anguloMinimo, anguloMaximo);
            Debug.Log("V + " + velocidad + " A + " + angulo);

            posicionInicial =new Vector3(68, 0, 0);
            vx = velocidad * Mathf.Sin((angulo * Mathf.PI) / 180);
            vy = velocidad * Mathf.Cos((angulo * Mathf.PI) / 180);
            xInicial = posicionInicial.x;
            yInicial = posicionInicial.y;
            coroutine = WaitAndPrint(0.1f);
            StartCoroutine(coroutine);
        } else {
            Debug.Log("total scores: " + scores + " of " + iteraciones + " iteraciones");
            Debug.Log("Velocidad optima entre " + velocidades.Min() + " y " + velocidades.Max());
            Debug.Log("Angulo optimo entre " + angulos.Max() + " y " + angulos.Max());
        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(2);
        float piso = 0f;
        float poste = 105f;
        Vector3 posicionactual = GameObject.Find("Balon").transform.position;
        float posicioneny = posicionactual.y;
        float posicionenx = posicionactual.x;
        Debug.Log("init x: " + posicionenx + " init y: " + posicioneny);
        while (posicioneny + (yInicial + vy * elapse_time + 0.5f * elapse_time * elapse_time * gravedad)*2 >= piso && posicionenx <= poste)
        {
            yield return new WaitForSeconds(waitTime);
            SimulateProjectile();
            elapse_time = elapse_time + 0.05f;
            posicionactual = GameObject.Find("Balon").transform.position;
            posicioneny = posicionactual.y;
            posicionenx = posicionactual.x;
        }

        if(result.text == "1") {
            scores++;
            float tempV = velocidad;
            float tempA = angulo;
            velocidades.Add(tempV);
            angulos.Add(tempA);
        }

        Debug.Log("Completed sim x: "+ posicionenx + " y: " + posicioneny + " Score: " + result.text);
        Projectile.position =  new Vector3(68, 0, 0);
        result.text = "0";
        itersCount++;
        yield return new WaitForSeconds(2);
        Start();
    }

    void SimulateProjectile()
    {
        Vector3 posactual = GameObject.Find("Balon").transform.position;
        float posicionactualx = posactual.x;
        float posicionactualy = posactual.y;
        Projectile.position += new Vector3((xInicial + vx * elapse_time)-posicionactualx, (yInicial + vy * elapse_time + 0.5f * elapse_time * elapse_time * gravedad)-posicionactualy, 0);
    }

}

