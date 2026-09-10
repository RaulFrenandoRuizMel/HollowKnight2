using UnityEngine;

public class Herramientas : MonoBehaviour
{
    public static float ObtenerAngulo2D(Vector2 punto1, Vector2 punto2)
    {
        float ca = punto2.x - punto1.x;
        float co = punto2.y - punto1.y;
        float h = Mathf.Sqrt(Mathf.Pow(ca, 2) + Mathf.Pow(co, 2));
        float consAngulo = ca / h;
        float angulo = Mathf.Acos(consAngulo);

        if (punto2.y < punto1.y)
        {
            angulo = -angulo;
        }
        return angulo * Mathf.Rad2Deg;
    }
}
