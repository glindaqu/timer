using UnityEngine;
using UnityEngine.UI;

public class ItemDeleter : MonoBehaviour
{
    public static GameObject ToDelete = null;
    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
    }

    public void DeleteObject()
    {
        if (ToDelete != null)
        {
            Destroy(ToDelete);
            ToDelete = null;
        }
    }

    private void Update()
    {
        if (ToDelete == null)
        {
            btn.interactable = false;
        }
        else
            btn.interactable = true;
    }
}
