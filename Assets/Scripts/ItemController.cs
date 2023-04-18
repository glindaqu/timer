using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class ItemController : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    private Camera cam;
    public int waitFor = 0;
    public int waitForConst = 0;
    private bool isPaused = false;
    private Button btn;
    [SerializeField] private Text time;
    [SerializeField] public Text title;
    [SerializeField] private Image BG;

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

    public void OnPointerClick(PointerEventData e)
    {
        ItemDeleter.ToDelete = this.gameObject;
    }

    public void SetInterval(int interval, string ti)
    {
        this.waitFor = interval;
        this.waitForConst = this.waitForConst > 0 ? this.waitForConst : interval;
        StartCoroutine(TickUpdate());
        title.text = ti;
    }

    public void SetIntervalByExist(int interval, int maxIntreval, string ti)
    {
        this.waitFor = interval;
        this.waitForConst = maxIntreval;
        this.isPaused = true;
        StartCoroutine(TickUpdate());
        btn.GetComponentInChildren<Text>().text = "CONT";
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
        GetComponent<AudioSource>().Play();
        BG.color = Color.red;
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
            btn.GetComponentInChildren<Text>().text = "CONT";
        }
        this.isPaused = !this.isPaused;
    }

    public void ResetTimer()
    {
        this.waitFor = this.waitForConst;
        btn.GetComponentInChildren<Text>().text = "CONT";
        this.isPaused = true;
        GetComponent<AudioSource>().Stop();
        BG.color = new Color(255,255,255,166);
    }
}
