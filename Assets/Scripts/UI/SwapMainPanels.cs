using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwapMainPanels : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // Start is called before the first frame update
    private Vector2 startPos;
    [SerializeField] private float lerpSpeed = 1f;
    private bool directionChosen;    
    [SerializeField] float choosenScreenX = 0f;
    [SerializeField] float deltaX = 5.6f;
    [SerializeField] private Text textX;
    [SerializeField] private Text textXScreen;
    private int activeScreen;
    private Vector2 touchPos;
    private bool freezeSwap;
    public bool FreezeSwap { set { freezeSwap = value; } }
    void Start()
    {
        activeScreen = 2;
        directionChosen = true;
        Screen.orientation = ScreenOrientation.Portrait;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            transform.position = new Vector3(0, 0, 0);
        }
        if (directionChosen)
        {
            ScreenChosen();
        }
        textX.text = "X: " + transform.position.x;
        textXScreen.text = "ActiveScr: " + choosenScreenX.ToString();
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
                var targetPos = Mathf.Clamp(transform.position.x, choosenScreenX - 3f, choosenScreenX + 3f);
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
                if ((choosenScreenX - transform.position.x) > 1f && activeScreen < 4)
                {
                    choosenScreenX -= deltaX;
                    activeScreen++;
                }
                else if ((choosenScreenX - transform.position.x) < 1f && activeScreen > 0)
                {
                    choosenScreenX += deltaX;
                    activeScreen--;
                }
                ActivateDeactivateSwapUpDown(true);
            }
        }
        directionChosen = true;
    }
    private void ActivateDeactivateSwapUpDown(bool enabled)
    {
        SwapUpDown activeScreenSwapping = transform.GetChild(activeScreen).GetComponent<SwapUpDown>();
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
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, choosenScreenX, lerpSpeed * 3 * Time.deltaTime), transform.position.y, transform.position.z);
    }
    public void ButtonChosenScreen(float screenNumber)
    {
        if (activeScreen != screenNumber && screenNumber >= 0 && screenNumber <= 4)
        {
            ActivateDeactivateSwapUpDown(false);
            choosenScreenX = screenNumber;
            freezeSwap = false;
            choosenScreenX = screenNumber;
            ActivateDeactivateSwapUpDown(true);
        }
    }

}
