using UnityEngine;

public class CreacionDano : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject,0.1f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemigo")
        {
            Enemigo enemigo = other.gameObject.GetComponent<Enemigo>();
            enemigo.RecibirDano();
        }
        //Debug.Log("Le di en la madre a " + other.gameObject.name);
    }
}
