﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class ItemController : MonoBehaviour, IDragHandler
{
    private Camera cam;
    private int waitFor = 0;
    private bool isPaused = false;
    private Button btn;
    [SerializeField] private Text time;
    [SerializeField] private Text title;

    private void Awake()
    {
        cam = Camera.main;
        btn = GetComponentInChildren<Button>();
    }

    public void OnDrag(PointerEventData e)
    {
        var pos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(pos.x, pos.y);
        ItemDeleter.ToDelete = this.gameObject;
    }

    public void SetInterval(int interval, string ti)
    {
        this.waitFor = interval;
        StartCoroutine(TickUpdate());
        title.text = ti;
    }

    private IEnumerator Tick()
    {
        if (!this.isPaused )
        {
            yield return new WaitForSeconds(1);
            this.waitFor--;
        }
    }

    private IEnumerator TickUpdate()
    {
        while (waitFor > 0)
        {
            yield return StartCoroutine(Tick());
        }
    }

    private void Update()
    {
        int min = this.waitFor / 60;
        string s = (this.waitFor % 60).ToString().Length == 1 ? "0" + (this.waitFor % 60) : (this.waitFor % 60).ToString();

        time.text = $"{min}:{s}";
    }

    public void PauseClick()
    {
        if (isPaused)
        {
            btn.GetComponentInChildren<Text>().text = "STOP";
        }
        else
        {
            btn.GetComponentInChildren<Text>().text = "CONTINUE";
        }
        this.isPaused = !this.isPaused;
    }
}
