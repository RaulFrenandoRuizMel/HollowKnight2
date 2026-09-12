using UnityEngine;

public class contCamara : MonoBehaviour
{
    Transform jugador;

    [SerializeField] Transform limitesPantalla;

    Vector3 limiteIzquierdo;
    Vector3 limiteDerecho;
    Vector3 limiteInferior;
    Vector3 limiteSuperior;

    Vector3 posicion;

    float JugadorRotacionY;

    Transform camara;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        limiteIzquierdo = limitesPantalla.GetChild(0).position;
        limiteDerecho= limitesPantalla.GetChild(1).position;
        limiteInferior= limitesPantalla.GetChild(2).position;
        limiteSuperior= limitesPantalla.GetChild(3).position;

        this.transform.position = jugador.position;
        posicion = this.transform.position;

        camara = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        JugadorRotacionY = jugador.rotation.eulerAngles.y;
        //posicion = Vector3.MoveTowards(posicion, jugador.position, 4 * Time.deltaTime); ;

        // limites distancia jugador
        float distanciaJugador = Vector3.Distance(jugador.position, this.transform.position);
        //Debug.Log(distanciaJugador);

        if(distanciaJugador > 2)
        {
            posicion = this.transform.position;
            float angulo = Herramientas.ObtenerAngulo2D(this.transform.position, jugador.transform.position);
            posicion.x = jugador.transform.position.x - Mathf.Cos(angulo * Mathf.Deg2Rad) * 2; 
            posicion.y = jugador.transform.position.y - Mathf.Sin(angulo * Mathf.Deg2Rad) * 2;
            Debug.Log("reposicion");

        }

        if(JugadorRotacionY > 90)
        {
            camara.localPosition = Vector3.MoveTowards(camara.localPosition, new Vector3(-1, 0, -10), 3* Time.deltaTime);
        }
        else
        {
            camara.localPosition = Vector3.MoveTowards(camara.localPosition, new Vector3(1, 0, -10), 3 * Time.deltaTime);
        }

        // limites escenario
        /*if (posicion.x < limiteIzquierdo.x)
        {
            posicion.x = limiteIzquierdo.x;
        }

        if (posicion.x > limiteDerecho.x)
        {
            posicion.x = limiteDerecho.x;
        }

        if (posicion.y < limiteInferior.y)
        {
            posicion.y = limiteInferior.y;
        }

        if (posicion.y > limiteSuperior.y)
        {
            posicion.y = limiteSuperior.y;
        }*/
        this.transform.position = posicion;
    }
}
