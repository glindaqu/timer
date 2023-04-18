using UnityEngine;

public class AboutAnim : MonoBehaviour
{
    [SerializeField] Animator[] animators;
    public void OnClick()
    {
        foreach (var animator in animators)
        {
            animator.SetTrigger("Active");
        }
    }
}
