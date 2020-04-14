using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPanel : MonoBehaviour
{
    [SerializeField] float lerpSpeed;
    [SerializeField] bool show;

    public void Show()
    {
        show = true;
    }
    public void Hide()
    {
        show = false;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (show)
            if (transform.position.y < Screen.height / 2)
                ShowPanel();
    }


    private void ShowPanel()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, Screen.height / 2, lerpSpeed * Time.deltaTime));
    }
}
