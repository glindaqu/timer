using UnityEngine;

public class SettingsAnim : MonoBehaviour
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
