using System;
using System.Xml;
using UnityEngine;

public class FPS : MonoBehaviour
{
    private void Start()
    {
        var xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.LoadXml(PlayerPrefs.GetString("save"));
            Application.targetFrameRate = Convert.ToInt32(xmlDoc.GetElementsByTagName("FPS")[0].InnerText);
        }

        catch
        {
            Application.targetFrameRate = 60;
        }
        FindObjectOfType<SettingsSceen>().SetPerfomance(Application.targetFrameRate);
    }
}
