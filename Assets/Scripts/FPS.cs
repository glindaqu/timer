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
            xmlDoc.Load(Application.persistentDataPath + "\\save.xml");
        }

        catch (Exception ex)
        {
            xmlDoc.LoadXml("<root> <FPS>60</FPS> <items></items> </root>");
        }
        Application.targetFrameRate = Convert.ToInt32(xmlDoc.GetElementsByTagName("FPS")[0].InnerText);
        FindObjectOfType<SettingsSceen>().SetPerfomance(Application.targetFrameRate);
    }
}
