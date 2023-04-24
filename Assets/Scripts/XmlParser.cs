using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XmlParser
{
    public static List<TimerItemModel> GetData()
    {
        var res = new List<TimerItemModel>();
        var xmlDoc = new XmlDocument();

        if (PlayerPrefs.HasKey("save"))
        {
            xmlDoc.LoadXml(PlayerPrefs.GetString("save"));
        }

        else
        {
            xmlDoc.LoadXml("<root> <FPS>60</FPS> <items></items> </root>");
            PlayerPrefs.SetString("save", xmlDoc.InnerXml);
        }

        var items = xmlDoc.GetElementsByTagName("item");

        foreach ( var item in items )
        {
            var cur = new TimerItemModel
            {
                ConstTime = Convert.ToInt32((item as XmlNode).Attributes["ConstTime"].Value),
                CurrentTime = Convert.ToInt32((item as XmlNode).Attributes["CurrentTime"].Value),
                PosX = float.Parse((item as XmlNode).Attributes["PosX"].Value),
                PosY = float.Parse((item as XmlNode).Attributes["PosY"].Value),
                Name = (item as XmlNode).Attributes["Name"].Value
            };

            res.Add(cur);
        }

        return res;
    }

    public static void SaveData(List<TimerItemModel> toSave)
    {
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(PlayerPrefs.GetString("save"));

        var root = xmlDoc.GetElementsByTagName("items")[0];
        root.RemoveAll();
        foreach (var item in toSave)
        {
            var cur = xmlDoc.CreateElement("item", "");
            cur.SetAttribute("Name", item.Name);
            cur.SetAttribute("PosX", item.PosX.ToString());
            cur.SetAttribute("PosY", item.PosY.ToString());
            cur.SetAttribute("CurrentTime", item.CurrentTime.ToString());
            cur.SetAttribute("ConstTime", item.ConstTime.ToString());
            root.AppendChild(cur);
        }

        xmlDoc.GetElementsByTagName("FPS")[0].InnerText = Application.targetFrameRate.ToString();
        PlayerPrefs.SetString("save", xmlDoc.InnerXml);
    }
}
