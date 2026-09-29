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

    [SerializeField] Animator animator;
    [SerializeField] GameObject prefabPincho;

    int estadoAtaque;

    int VIDA;
    int fase;
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

        estadoAtaque = 0;
        Debug.Log(direccionOndas.x);

        VIDA = 30;
        fase = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (estadoAtaque == 0)
        {
            anguloOndas += Time.deltaTime * 200;
            this.transform.Translate(Vector3.up * Mathf.Sin(anguloOndas * Mathf.Deg2Rad) * Time.deltaTime * velocidadOndeo * direccionOndas.y);
            this.transform.Translate(Vector3.right * velocidadOndeo * direccionOndas.x * Time.deltaTime);

            contadorTeletransport += Time.deltaTime;
            if (contadorTeletransport > 2)
            {
                contadorTeletransport = 0;

                direccionOndas.x = -1 + Mathf.RoundToInt(Random.value) * 2;
                direccionOndas.y = -1 + Mathf.RoundToInt(Random.value) * 2;

                this.transform.position = new Vector3(
                    posicionOriginal.x + Random.Range(3, 8) * direccion,
                    posicionOriginal.y + Random.Range(1, 3),
                    0);
                direccion = -direccion;
            }
        }



        if (estadoAtaque == 0)
        {
            contadorAtaque += Time.deltaTime;
            if (contadorAtaque > 2)
            {
                contadorAtaque = 0;
                animator.Play("Jefe1ataque");
                estadoAtaque = 1;
            }
        }
        else
        {
            contadorAtaque += Time.deltaTime;
            if (contadorAtaque > 1)
            {
                contadorAtaque = 0;
                estadoAtaque = 0;
            }
        }
    }
    public void Atacar()
    {
        switch (fase)
        {
            case 1:
                crearPinchos();
                break;
            case 2:
                crearPinchos();
                Invoke("crearPinchos", 0.5f);
                break;
                case 3:
                crearPinchos();
                Invoke("crearPinchos", 0.5f);
                Invoke("crearPinchos", 1);
                break;
        }

    }

    void crearPinchos()
    {
        float aleatoriedad = Random.value * 360;
        for (int i = 0; i < 8; i++)
        {
            Instantiate(prefabPincho, this.transform.position, Quaternion.Euler(0, 0, i * 45 + aleatoriedad));
        }
    }

    public void RecibirDano()
    {
        VIDA--;
        Debug.Log(VIDA);
        if(VIDA < 20)
        {
            fase = 2;
        }
        if (VIDA < 10)
        {
            fase = 3;
        }
    }
}
