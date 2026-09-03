using UnityEngine;

public class Enemigo1 : MonoBehaviour
{
    float Angulo;
    int vidas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Angulo = 90;
        vidas = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Angulo += 150*Time.deltaTime;
        this.transform.Translate(Vector3.up * Mathf.Sin(Angulo*Mathf.Deg2Rad) * Time.deltaTime);
    }

    public void RecibirDano()
    {
        vidas--;
        if (vidas <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
