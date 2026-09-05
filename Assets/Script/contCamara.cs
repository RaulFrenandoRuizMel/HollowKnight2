using UnityEngine;

public class contCamara : MonoBehaviour
{
    Transform jugador;

    [SerializeField] Transform limitesPantalla;

    Vector3 limiteIzquierdo;

    Vector3 posicion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        limiteIzquierdo = limitesPantalla.GetChild(0).position;
        posicion = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        posicion = jugador.position;

        if(posicion.x < limiteIzquierdo.x)
        {
            posicion.x = limiteIzquierdo.x;
        }

        this.transform.position = jugador.position;
    }
}
