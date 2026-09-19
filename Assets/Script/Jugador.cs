using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Jugador : MonoBehaviour
{
    CharacterController characterController;
    PlayerInput playerInput;
    Vector3 velocidad;
    Vector3 rotacion;
    Animator animator;
    int saltos_restantes;
    float is_graunded;

    //Estados
    float contadorDash;
    float cooldawnDash;
    //Ataque
    float contadorAtaque;
    [SerializeField] GameObject prefabCreacionDano;
    [SerializeField] Transform CreacionDaño;
    [SerializeField] JugadorDestello destello;
    [SerializeField] GameObject prefabEspiritu;

    // ------------ VIDA
    int vida;
    public float timepoInv;
    float DanoMovX;
    JugadorVibracion jugadorVibracion;
    public bool PoderMoverse;

    float contadoVengativo;

    //pared
    int estadoPared;
    float velocidadParedX;
    bool tocarParedDerecha;

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        characterController = this.GetComponent<CharacterController>();
        playerInput = this.GetComponent<PlayerInput>();
        velocidad = Vector3.zero;
        rotacion = Vector3.zero;
        animator = this.transform.GetChild(0).GetComponent<Animator>();
       // Application.targetFrameRate = 1;
        saltos_restantes = 1;
        is_graunded = 0;

        //Estados
        contadorDash = 0;
        cooldawnDash = 0;
        contadorAtaque = 0;

        //Pared
        estadoPared = 0;
        velocidadParedX = 0;
        tocarParedDerecha =  true;

        vida = 5;
        timepoInv = 0;
        DanoMovX = 0;
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0, 0);
        }
        jugadorVibracion = this.GetComponent<JugadorVibracion>();
        
        PoderMoverse = true;

        contadoVengativo = 0;

        characterController.enabled = false;
        this.transform.position = triggerTransicion.posicionTransicion;
        characterController.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        //GRAVEDAD
        if(estadoPared == 0)
        {
            velocidad.y -= 60 * Time.deltaTime;
        }

        //playerInput.actions["Move"].ReadValue<Vector2>();
        if (contadorDash <= 0)
        {
            //velocidad.x = 16;
            if (DanoMovX == 0)
            {
                velocidad.x = Mathf.RoundToInt (playerInput.actions["Move"].ReadValue<Vector2>().x) * 4;
                if (!PoderMoverse) velocidad.x = 0;
            }
        }
        else
        {
            velocidad.y = 0;
        }
        /*if(contadorDash>0)
        {
            velocidad.x = 16;
        }
        //1;*/
        if (velocidad.x > 0)
        {
            rotacion.y = 0;
        }
        if (velocidad.x < 0)
        {
            rotacion.y = 180;
        }

        is_graunded -= Time.deltaTime;
        
        if(characterController.isGrounded)
        {
            is_graunded = 0.3f;
        }

        if (is_graunded > 0)
        {
            saltos_restantes = 1;
            if(contadorDash <= 0 && contadorAtaque <=0)
            {
                if (velocidad.x == 0)
                {
                    animator.Play("JugadorIddle");
                }
                else
                {
                    animator.Play("Jugador_Caminando");
                }
            }
            velocidad.y = -1;
            if (playerInput.actions["Jump"].WasPressedThisFrame() )
            {
                if(PoderMoverse && estadoPared == 0)
                {
                    velocidad.y = 10;
                    is_graunded = 0;
                    animator.Play("Jugador_Saltar");
                }
            }
        }
        else
        {
            if(estadoPared == 0)
            {
                if (playerInput.actions["Jump"].IsPressed())
                {
                    velocidad.y += 40 * Time.deltaTime;
                }
            }

            
            if(contadorAtaque <= 0 && estadoPared == 0)
            {
                if (velocidad.y < -1)
                {
                    animator.Play("Jugador_Caida");
                }
                else if (velocidad.y < 0)
                {
                    animator.Play("Jugador_Empezar_Caida");
                }
            }
            
            if (playerInput.actions["Jump"].WasPressedThisFrame())
            {
                if (saltos_restantes > 0 && estadoPared == 0)
                {
                    velocidad.y = 14;
                    saltos_restantes--;
                    animator.Play("Jugador_DobleSalto");
                }
            }
        }

        //--------------------- DASH -----------------------//
        cooldawnDash -= Time.deltaTime;
        if (playerInput.actions["Sprint"].WasPressedThisFrame() && PoderMoverse && estadoPared == 0)
        {
            animator.Play("Jugador_dash");
            if (cooldawnDash <= 0)
            {
                contadorDash = .2f;
                if (rotacion.y == 0)
                {
                    velocidad.x = 20;
                }
                else
                {
                    velocidad.x = -20;
                }
                cooldawnDash = 0.3f;

                velocidadParedX = 0;
                estadoPared = 0;
            }

        }
        contadorDash -= Time.deltaTime;

        //--------------Ataque---------------//
        if (playerInput.actions["Attack"].WasPressedThisFrame() && PoderMoverse)
        {
            if(contadorAtaque<=0)
            { 
                animator.Play("JugadorAtaque");
                contadorAtaque = 0.2f;
                Instantiate(prefabCreacionDano, CreacionDaño.position, Quaternion.identity);
                //Debug.Log("Espadazo");
            }
        }
        contadorAtaque -= Time.deltaTime;
        timepoInv -= Time.deltaTime;

        //Empujondano

        if(DanoMovX > 0)
        {
            DanoMovX -= Time.deltaTime * 10;
            rotacion.y = 180;
            if(DanoMovX < 0)
            {
                DanoMovX = 0;
            }
        }
        if (DanoMovX < 0)
        {
            DanoMovX -= Time.deltaTime * 10;
            rotacion.y = 0;
            if (DanoMovX < 0)
            {
                DanoMovX = 0;
            }
        }

        if (playerInput.actions["hechizo"].WasPressedThisFrame())
        {
            contadoVengativo = 0.7f;
            velocidad.y = 0;
            Instantiate(prefabEspiritu, this.transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }

        if (contadoVengativo > 0)
        {
            contadoVengativo = 0.7f;
            contadoVengativo -= Time.deltaTime;
        }

        //WAllJUMP
        if (Physics.Raycast(this.transform.position + Vector3.up * 0.5f, Vector3.left, 0.35f) && is_graunded == 0 && is_graunded <= 0)
        {
            if (estadoPared == 0)
            {
                if (velocidad.y < 0)
                {
                    velocidad.y = 0;

                }
                saltos_restantes = 1;
            }
            estadoPared = 1;
            tocarParedDerecha = false;
        }
        else if (Physics.Raycast(this.transform.position + Vector3.up * 0.5f, Vector3.right, 0.35f) && is_graunded == 0 && is_graunded <= 0)
        {
            if (estadoPared == 0)
            {
                if (velocidad.y < 0)
                {
                    velocidad.y = 0;

                }
                saltos_restantes = 1;
            }
            estadoPared = 1;
            tocarParedDerecha = true;

        }
        else
                {
            estadoPared = 0;
        }

        switch (estadoPared)
        {
            case 1: //Pared
                animator.Play("jugador_Pared");
                velocidad.y -= 20 * Time.deltaTime;

                if (playerInput.actions["Jump"].WasPressedThisFrame() && PoderMoverse)
                {
                    velocidad.y = 10;
                    if (tocarParedDerecha)
                    {
                        velocidadParedX = -10;
                    }
                    else
                    {
                        velocidadParedX = 10;
                    }
                    characterController.Move(Vector3.right * 0.5f);
                    animator.Play("Jugador_Saltar");
                }

                if (playerInput.actions["Sprint"].WasPressedThisFrame() && PoderMoverse)
                {
                    animator.Play("Jugador_dash");
                    if (cooldawnDash <= 0)
                    {
                        contadorDash = .2f;
                        if(tocarParedDerecha)
                        {
                            velocidad.x = -20;
                        }
                        else
                        {
                            velocidad.x = 20;
                        }

                        velocidad.x = -20;
                        cooldawnDash = 0.3f;
                        velocidadParedX = 0;
                        estadoPared = 0;
                    }
                }
                    break;
        }

        if (velocidadParedX > 0)
        {
            velocidad.x = velocidadParedX;
            velocidadParedX -= 60 * Time.deltaTime;
            if (velocidadParedX < 0)
            {
                velocidadParedX = 0;
            }
            if (velocidad.x > 0)
            {
                rotacion.y = 0;
            }
            if (velocidad.x < 0)
            {
                rotacion.y = 180;
            }
        }

        if (velocidadParedX < 0)
        {
            velocidad.x = velocidadParedX;
            velocidadParedX += 60 * Time.deltaTime;
            if(velocidadParedX > 0)
            { 
                velocidadParedX = 0;
            }
            if (velocidad.x > 0)
            {
                rotacion.y = 0;
            }
            if (velocidad.x < 0)
            {
                rotacion.y = 180;
            }
        }



        characterController.Move(velocidad * Time.deltaTime);
        this.transform.rotation = Quaternion.Euler(rotacion);
        /*characterController.Move(Vector3.down);
        //Que son lasl corrutinas*/

        //Colicionador Techo
        //Debug.DrawRay(this.transform.position + Vector3.up * 0.5f, Vector3.up* 0.7f, Color.purple);
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position + Vector3.up * 0.5f, Vector3.up, out hit, 0.8f))
        {
            velocidad.y = -4;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Dano":
                if (timepoInv <= 0)
                {
                    vida--;
                    timepoInv = 1.5f;
                    contadorDash = 0;
                    //velocidad.x = -10; esto afectaa el movimiento del golpe para el empujon

                    //Saltito
                    if (other.gameObject.transform.position.x > this.transform.position.x)
                    {
                        DanoMovX = -5;
                    }
                    else
                    {
                        DanoMovX = 5;
                    }
                    velocidad.x = DanoMovX;

                    velocidad.y = 7;
                    is_graunded = 0;
                    characterController.Move(Vector3.up * 0.2f);

                    jugadorVibracion.RecibirDano();

                    if (vida <= 0)
                    {
                        Destroy(this.gameObject);
                    }
                    destello.ActvarDestello();
                    //Destroy(this.gameObject);
                }
                break;
            case "transicionadorEscena":
                TransicionEscenas transicionEscenas = GameObject.Find("PantallaNegra").GetComponent<TransicionEscenas>();

                triggerTransicion triggerTransicion = other.gameObject.GetComponent<triggerTransicion>();
                triggerTransicion.posicionTransicion = triggerTransicion.posicionInicio;

                transicionEscenas.transicionarEscena(triggerTransicion.NombreEscena);
                break;
        }

    }
}
