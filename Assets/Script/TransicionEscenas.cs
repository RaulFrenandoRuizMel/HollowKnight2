using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TransicionEscenas : MonoBehaviour
{
    Image image;
    Color color;
    int estadoTransicion;
    string nombreEscena;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = this.GetComponent<Image>();
        color = image.color;
        color.a = 1f;
        image.color = color;
        estadoTransicion = 0;
        image.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch(estadoTransicion)
        {
            case 0:
                color.a -= Time.deltaTime;
                if(color.a < 0)
                {
                    color.a = 0;
                }
                image.color = color;
                break;
            case 1:
                color.a += Time.deltaTime;
                if (color.a > 1)
                {
                    color.a = 1;
                    SceneManager.LoadScene("Cuarto2");

                }
                image.color = color;
                break;
        }
    }

    public void transicioanrEscena()
    {
        estadoTransicion = 1;
    }

    public void transicionarEscena(string nombre)
    {
        nombreEscena = nombre;
        estadoTransicion = 1;
    }
}
