using UnityEngine;

public class Tiempoinbulnerale : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Color color;
    float contadorParpadeo;
    Jugador scriptJugador;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        color= Color.white;
        contadorParpadeo = 0;
        scriptJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Jugador>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptJugador.timepoInv > 0)
        {
            contadorParpadeo += Time.deltaTime;
            if (contadorParpadeo > 1)
            {
                color.a = 0.5f;
            }
            else
            {
                color.a = 1;
            }
            spriteRenderer.color = color;
        }
        else
        {
            color.a = 1;
        }
        spriteRenderer.color = color;
    }
}
