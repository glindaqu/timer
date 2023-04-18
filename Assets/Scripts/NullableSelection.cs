using UnityEngine;
using UnityEngine.EventSystems;

public class NullableSelection : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData e)
    {
        ItemDeleter.ToDelete = null;
    }
}
