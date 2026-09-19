using UnityEngine;
using UnityEngine.SceneManagement;

public class manejadorInicio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Juegar()
    {
        triggerTransicion.posicionTransicion = new Vector3(0, 0.5f, 0);
        SceneManager.LoadScene("Cuarto1");
    }
}
