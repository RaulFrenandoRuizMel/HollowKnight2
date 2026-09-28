using UnityEngine;

public class Jefe1EventosAnimaciones : MonoBehaviour
{
    jefe1 scriptJefe1;
   public void Atacar()
    {
        scriptJefe1 = this.transform.parent.GetComponent<jefe1>();
        scriptJefe1.Atacar();
    }
}
