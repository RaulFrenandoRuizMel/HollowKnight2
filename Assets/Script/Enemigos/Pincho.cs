using UnityEngine;

public class Pincho : MonoBehaviour
{
    float velocidad;

    float contador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        velocidad = 5;
        contador = 0;
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(contador > 0.4f)
        {
            velocidad = 16;
        }
        this.transform.Translate(Vector3.right * 5 * Time.deltaTime);

        if(contador > 10)
        {
            Destroy(this.gameObject);
        }
    }
}
