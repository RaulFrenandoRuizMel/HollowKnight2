using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActivarDialogo : MonoBehaviour
{
    [SerializeField] AudioClip[] Audios;

    [SerializeField] GameObject letreroHablar;
    PlayerInput playerInput;

    SistemaDialogos sistemaDialogos;
    Jugador scriptJugador;

    [SerializeField] string[] dialogos;

    int dialogoActual;
    bool leyendoDialogo;
    [Tooltip("nombre del NPC en Canvas")]
    [SerializeField] string nombre;

    public UnityEvent OnDialogoTerminado;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        letreroHablar.SetActive(false);
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();

        sistemaDialogos = GameObject.Find("SistemaDialogos").GetComponent<SistemaDialogos>();
        scriptJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Jugador>();

        dialogoActual =0;
        leyendoDialogo = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        letreroHablar.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        letreroHablar.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (leyendoDialogo == false)
        {
            if (playerInput.actions["Move"].ReadValue<Vector2>().y > 0.5f)
            {
                scriptJugador.PoderMoverse = false;
                sistemaDialogos.MostrarDialogos(dialogos[dialogoActual], nombre, this, Audios[dialogoActual]);
                leyendoDialogo = true;
            }
        }
    }
    public void TerminoDialogoActual()
    {
        if (dialogoActual < dialogos.Length - 1)
        {
            dialogoActual++;
            sistemaDialogos.MostrarDialogos(dialogos[dialogoActual], nombre, this, Audios[dialogoActual]);
        }
        else
        {
            leyendoDialogo = false;
            letreroHablar.SetActive (false);
            scriptJugador.PoderMoverse = true;
            dialogoActual = 0;

            OnDialogoTerminado.Invoke();
        }
    }
}
