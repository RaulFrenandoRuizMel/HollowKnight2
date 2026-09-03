using UnityEngine;

public class JugadorDestello : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer jugadorspriteRenderer;

    Color color;

    [SerializeField] GameObject prefabDestello;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        color = Color.white;
        color.a = 0;
        spriteRenderer.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        color.a -= Time.deltaTime * 2f;
        spriteRenderer.color = color;
        spriteRenderer.sprite = jugadorspriteRenderer.sprite;
    }

    public void ActvarDestello()
    {
        Instantiate(prefabDestello, this.transform.position, Quaternion.identity);
        color.a = 1;
    }
}
