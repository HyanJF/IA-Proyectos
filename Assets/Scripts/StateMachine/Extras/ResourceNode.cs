using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public int cantidad = 10;
    public int maxCantidad = 10;

    public int Extraer(int cantidadSolicitada)
    {
        int cantidadExtraida = Mathf.Min(cantidadSolicitada, cantidad);
        cantidad -= cantidadExtraida;

        if (cantidad <= 0)
        {
            cantidad = 0;
            gameObject.SetActive(false); 
        }

        return cantidadExtraida;
    }

    public bool TieneRecursos()
    {
        return cantidad > 0;
    }
}
