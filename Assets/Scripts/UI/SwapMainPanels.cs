using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwapMainPanels : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // Start is called before the first frame update
    private Vector2 startPos;
    [SerializeField] private float border;
    [SerializeField] private float lerpSpeed = 1f;
    private bool directionChosen;
    [SerializeField] float choosenScreenX = 0f;
    [SerializeField] const float deltaX = 5.625f;
    //[SerializeField] private Text textX;
    [SerializeField] private Text textXScreen;
    [SerializeField] private int activeScreen;
    private Vector2 touchPos;
    private bool freezeSwap;
    public bool FreezeSwap { set { freezeSwap = value; } }
    private void Awake()
    {
        activeScreen = 0;
        GameController.activeSceen = activeScreen;
    }
    void Start()
    {
        directionChosen = true;
        Screen.orientation = ScreenOrientation.Portrait;
    }
    private void Update()
    {
        if (directionChosen)
        {
            ScreenChosen();
        }
        //textX.text = "X: " + transform.position.x;
        //textXScreen.text = "ActiveScr: " + choosenScreenX.ToString();
        //Debug.Log(choosenScreenX);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //startPos = eventData.delta;
        directionChosen = false;
    }
    public void OnDrag(PointerEventData eventData)
    {

        if (!freezeSwap)
        {
            touchPos = eventData.delta;
            if (Mathf.Abs(touchPos.x) > Mathf.Abs(touchPos.y))
            {
                var targetPos = Mathf.Clamp(transform.position.x, choosenScreenX - border, choosenScreenX + border);
                transform.position = new Vector3(Mathf.Lerp(targetPos, transform.position.x + touchPos.x / 5, lerpSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            }
        }
        else
            directionChosen = true;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!freezeSwap)
        {
            if (Mathf.Abs(touchPos.x) > Mathf.Abs(touchPos.y))
            {
                ActivateDeactivateSwapUpDown(false);

                if ((choosenScreenX - transform.position.x) > 1f && activeScreen < 2)
                {
                    activeScreen++;
                    choosenScreenX = deltaX * activeScreen * -1;
                }
                else if ((choosenScreenX - transform.position.x) < 1f && activeScreen > -2)
                {
                    activeScreen--;
                    choosenScreenX = deltaX * activeScreen * -1;
                }
                ActivateDeactivateSwapUpDown(true);
            }
        }
        GameController.activeSceen = activeScreen;
        directionChosen = true;
    }
    private void ActivateDeactivateSwapUpDown(bool enabled)
    {
        SwapUpDown activeScreenSwapping = transform.GetChild(activeScreen + 2).GetComponent<SwapUpDown>();
        if (activeScreenSwapping != null)
        {
            StopCoroutine(activeScreenSwapping.BackY());
            if (!enabled)
            {
                StartCoroutine(activeScreenSwapping.BackY());
            }
            activeScreenSwapping.enabled = enabled;
        }
    }
    private void ScreenChosen()
    {
        if (!Mathf.Approximately(transform.position.x, choosenScreenX))
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, choosenScreenX, lerpSpeed * 3 * Time.deltaTime), transform.position.y, transform.position.z);
    }
    public void ButtonChosenScreen(int screenNumber)
    {
        if (activeScreen != screenNumber && screenNumber >= -2 && screenNumber < 3)
        {
            ActivateDeactivateSwapUpDown(false);
            activeScreen = screenNumber;
            choosenScreenX = deltaX * activeScreen * -1;
            freezeSwap = false;
            ActivateDeactivateSwapUpDown(true);
        }
    }

}

