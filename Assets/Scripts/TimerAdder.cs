using UnityEngine;
using UnityEngine.UI;

public class TimerAdder : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Animator[] animators;
    [SerializeField] private InputField timeSeconds;
    [SerializeField] private InputField timeMinuts;
    [SerializeField] private InputField title;

    public void OnClick()
    {
        foreach (var animator in animators)
        {
            animator.SetTrigger("Active");
        }
    }

    public void Create()
    {
        var cur = Instantiate(prefab, container.transform);
        cur.transform.position = new Vector2(0, 0);
        int.TryParse(timeSeconds.text, out int s);
        int.TryParse(timeMinuts.text, out int m);
        cur.GetComponent<ItemController>().SetInterval(s + m*60, title.text);
        OnClick();
    }
}
