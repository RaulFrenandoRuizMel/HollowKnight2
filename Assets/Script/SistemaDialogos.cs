using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class SistemaDialogos : MonoBehaviour
{
    [SerializeField] GameObject recuadroTexto;
    [SerializeField] TMP_Text texto_dialogo;
    [SerializeField] GameObject BotonSiguiente;
    [SerializeField] TMP_Text NombreNPC;

    PlayerInput playerInput;
    int EstadoDialogo;

    string texto;

    int i;

    float contador;

    float tiempoLetra = 0.05f;

    ActivarDialogo _activarDialogo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        recuadroTexto.SetActive(false);
        EstadoDialogo = 0;
        texto = "";

        i = 0;

        contador = 0;

        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();

        _activarDialogo = null;
    }

    // Update is called once per frame
    void Update()
    {
        switch(EstadoDialogo)
        {
            case 0:
                break;
            case 1://escribir Dialogo
                contador += Time.deltaTime;
                if(contador > tiempoLetra)
                {
                    if (i < texto.Length - 1)
                    {
                        texto_dialogo.text += texto[i].ToString();
                        i++;
                    }
                    else
                    {
                        BotonSiguiente.SetActive(true);
                        EstadoDialogo = 2;
                    }
                }
                if (playerInput.actions["Jump"].WasPressedThisFrame())
                {
                    texto_dialogo.text = texto;
                    BotonSiguiente.SetActive(true);
                    EstadoDialogo = 2;
                    i = 0;
                }
                break;
            case 2: //esperar InputJugador
            if (playerInput.actions["Jump"].WasPressedThisFrame())
            {
                EstadoDialogo = 0;
                texto_dialogo.text = "";
                BotonSiguiente.SetActive(false);
                recuadroTexto.SetActive(false);
                _activarDialogo.TerminoDialogoActual();
                i = 0;
            }
                break;
        }
    }

    public void MostrarDialogos(string dialogo, string nombre, ActivarDialogo activarDialogo)
    {
        if (EstadoDialogo == 0)
        {
            NombreNPC.text = nombre;
            recuadroTexto.SetActive(true);

            texto = dialogo;
            texto_dialogo.text = "";
            EstadoDialogo = 1;
            _activarDialogo = activarDialogo;
        }
    }
}
