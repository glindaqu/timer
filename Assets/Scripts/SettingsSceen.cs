using UnityEngine;
using UnityEngine.UI;

public class SettingsSceen : MonoBehaviour
{
    [SerializeField] private Text val;
    public void PerfomanceChange()
    {
        switch (val.text)
        {
            case "Medium":
                val.text = "High";
                Application.targetFrameRate = 120;
                break;
            case "High":
                val.text = "Low";
                Application.targetFrameRate = 30;
                break;
            default:
                val.text = "Medium";
                Application.targetFrameRate = 60;
                break;
        }
    }

    public void SetPerfomance(int fps)
    {
        switch (fps)
        {
            case 30:
                val.text = "Low";
                Application.targetFrameRate = 30;
                break;
            case 120:
                val.text = "High";
                Application.targetFrameRate = 120;
                break;
            default:
                val.text = "Medium";
                Application.targetFrameRate = 60;
                break;
        }
    }
}
