using UnityEngine;
using UnityEngine.Events;

public class Enemigo : MonoBehaviour
{
    [SerializeField] UnityEvent eventoRecibirDano;
    public void RecibirDano()
    {
        //----------  ----------//
        eventoRecibirDano.Invoke();

    }
}
