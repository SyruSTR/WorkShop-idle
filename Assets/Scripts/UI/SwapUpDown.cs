using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwapUpDown : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 3f;
    [SerializeField] private Camera cam;
    [SerializeField] private float coefficientSwapSpeed = 5f;
    [SerializeField] private float minBorder;
    [SerializeField] private float maxBorder;
    [Space]
    [SerializeField] private Text textY;
    
    private Vector3 touchPos;
    private Vector3 lastTouchPos;
    private bool swappingABorderB;
    private float swappingVar;

    // Start is called before the first frame update
    private void Start()
    {
        swappingABorderB = false;
    }
    public IEnumerator BackY()
    {
        yield return new WaitForSeconds(0.3f);
        transform.position = new Vector3(transform.position.x, minBorder, transform.position.z);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            transform.position = new Vector3(0, 0, 0);
        }
        textY.text = "Y: " + transform.position.y;
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            var transformY = transform.position.y;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    lastTouchPos = cam.ScreenToWorldPoint(touch.position);
                    break;
                case TouchPhase.Moved:
                    touchPos = coefficientSwapSpeed * (cam.ScreenToWorldPoint(touch.position) - lastTouchPos);

                    if (Mathf.Abs(touchPos.x) < Mathf.Abs(touchPos.y))
                        GetComponentInParent<SwapMainPanels>().FreezeSwap = true;
                    if (Mathf.Abs(touchPos.y) > Mathf.Abs(touchPos.x) && touch.position.y > 200)
                    {
                        var targetPos = Mathf.Clamp(transformY, minBorder - 3f, maxBorder + 3f);
                        transform.position = new Vector3(transform.position.x, Mathf.Lerp(targetPos, transformY + touchPos.y, lerpSpeed * Time.deltaTime), transform.position.z);

                    }
                    lastTouchPos = cam.ScreenToWorldPoint(touch.position);
                    break;
                case TouchPhase.Ended:
                    
                    
                        StartCoroutine(LerpEnd());
                    GetComponentInParent<SwapMainPanels>().FreezeSwap = false;
                    lastTouchPos = Vector3.zero;
                    break;
                default:
                    break;
            }
        }

        if (swappingABorderB)
        {
            
            SwappingABorder(swappingVar);
        }
    }
    IEnumerator LerpEnd()
    {
        var stopingSpeed = 2f;
        if (Mathf.Abs(touchPos.y) > 1f)
        {
            for (int i = 0; i < 20; i++)
            {
                if (transform.position.y > maxBorder || transform.position.y < minBorder)
                    break;
                var targetPos = Mathf.Clamp(transform.position.y, minBorder, maxBorder);
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(targetPos, transform.position.y + touchPos.y * 3f, Time.deltaTime * lerpSpeed * 1.5f / stopingSpeed), transform.position.z);
                stopingSpeed += 0.7f;
                yield return new WaitForSeconds(0.01f);
            }
        }
        if (transform.position.y > (maxBorder + 0.1f))
        {

            swappingVar = maxBorder;
            swappingABorderB = true;

        }
        if (transform.position.y < (minBorder - 0.1f))
        {

            swappingVar = minBorder;
            swappingABorderB = true;
        }


    }
    private void SwappingABorder(float variable)
    {
        if (transform.position.y < minBorder || transform.position.y > maxBorder)
        {

            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, variable, lerpSpeed * 2.5f * Time.deltaTime), transform.position.z);
        }
        else
            swappingABorderB = false;
    }
}
