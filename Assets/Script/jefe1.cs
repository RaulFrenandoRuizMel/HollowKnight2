using UnityEngine;

public class jefe1 : MonoBehaviour
{
    float contadorTeletransport;
    float contadorAtaque;
    Vector3 posicionOriginal;

    int direccion;
    float anguloOndas;
    float velocidadOndeo;
    Vector2 direccionOndas;

     [SerializeField]Animator animator;
     [SerializeField]GameObject prefabPincho;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        contadorTeletransport = 0;
        contadorAtaque = 0;
        posicionOriginal = Vector3.zero;
        direccion = 1;
        anguloOndas = 0;
        direccionOndas = Vector2.zero;

        velocidadOndeo = 2;
        direccionOndas.x = -1 + Mathf.RoundToInt(Random.value) * 2;
        direccionOndas.y = -1 + Mathf.RoundToInt(Random.value) * 2;
        Debug.Log(direccionOndas.x);
    }

    // Update is called once per frame
    void Update()
    { /*
        anguloOndas += Time.deltaTime * 200;
        this.transform.Translate(Vector3.up * Mathf.Sin(anguloOndas * Mathf.Deg2Rad) * Time.deltaTime * velocidadOndeo * direccionOndas.y);
        this.transform.Translate(Vector3.right * velocidadOndeo * direccionOndas.x * Time.deltaTime);


        contadorTeletransport += Time.deltaTime;
        if(contadorTeletransport > 2)
        {
            contadorTeletransport = 0;

            direccionOndas.x = -1 + Mathf.RoundToInt(Random.value) * 2;
            direccionOndas.y = -1 + Mathf.RoundToInt(Random.value) * 2;

                this.transform.position = new Vector3(
                    posicionOriginal.x + Random.Range(3, 8) * direccion,
                    posicionOriginal.y + Random.Range(1, 3),
                    0);
            direccion = -direccion;
        }*/

        contadorAtaque += Time.deltaTime;
        if(contadorAtaque > 5)
        {
            contadorAtaque = 0;
            animator.Play("Jefe1ataque");

            
        }
    }
    public void Atacar()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(prefabPincho, this.transform.position, Quaternion.Euler(0, 0, i * 45));

        }
    }
}
