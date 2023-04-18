using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using UnityEngine;

public class XmlParser
{
    public static List<TimerItemModel> GetData(string xml)
    {
        var res = new List<TimerItemModel>();
        var xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(xml);
        }

        catch (Exception ex)
        {
            Debug.LogException(ex);
            FileStream fileStream = new FileStream(@xml,
                                       FileMode.OpenOrCreate,
                                       FileAccess.ReadWrite,
                                       FileShare.None);
            xmlDoc.LoadXml("<root> <FPS>60</FPS> <items></items> </root>");
            xmlDoc.Save(fileStream);
            Resources.Load(xml);
        }

        var items = xmlDoc.GetElementsByTagName("item");

        foreach ( var item in items )
        {
            var cur = new TimerItemModel();

            cur.ConstTime = Convert.ToInt32((item as XmlNode).Attributes["ConstTime"].Value);
            cur.CurrentTime = Convert.ToInt32((item as XmlNode).Attributes["CurrentTime"].Value);
            cur.PosX = float.Parse((item as XmlNode).Attributes["PosX"].Value);
            cur.PosY = float.Parse((item as XmlNode).Attributes["PosY"].Value);
            cur.Name = (item as XmlNode).Attributes["Name"].Value;

            res.Add(cur);
        }

        return res;
    }

    public static void SaveData(string xml, List<TimerItemModel> toSave)
    {
        var xmlDoc = new XmlDocument();
        xmlDoc.Load(xml);

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

        xmlDoc.Save(Application.persistentDataPath + "\\save.xml");
    }
}
