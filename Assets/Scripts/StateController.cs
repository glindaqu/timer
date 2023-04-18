using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform parent;
    private void Start()
    {
        List<TimerItemModel> items = XmlParser.GetData(Application.persistentDataPath + "\\save.xml");

        foreach (var item in items)
        {
            var cur = Instantiate(prefab, parent);
            cur.transform.position = new Vector2(item.PosX, item.PosY);
            cur.GetComponent<ItemController>().waitForConst = item.ConstTime;
            cur.GetComponent<ItemController>().SetIntervalByExist(item.CurrentTime, item.ConstTime, item.Name);
        }
    }

    private void OnApplicationQuit()
    {
        var items = new List<TimerItemModel>();

        foreach (var item in FindObjectsOfType<ItemController>())
        {
            var cur = new TimerItemModel();
            cur.PosX = item.transform.position.x;
            cur.PosY = item.transform.position.y;
            cur.ConstTime = item.waitForConst;
            cur.CurrentTime = item.waitFor;
            cur.Name = item.title.text;

            items.Add(cur); 
        }

        XmlParser.SaveData(Application.persistentDataPath + "\\save.xml", items);
    }
}
