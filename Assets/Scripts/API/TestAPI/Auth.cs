using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Auth : MonoBehaviour
{
    [SerializeField] public TMP_InputField loginField;
    [SerializeField] public TMP_InputField passwordField;
    [SerializeField] public Text statusText;

    [SerializeField] ControllerGeneralPanels generalPanelController;
    [SerializeField] Color errorColor;

    public AuthCode IsAuth;
    private void Start()
    {
        IsAuth = 0;
    }
    public void Login()
    {
        statusText.color = Color.white;
        statusText.text = "Login is...";
        new Thread(new ThreadStart(async () =>
        {
            IsAuth = await TestAPI.PostToken(loginField.text, passwordField.text);
            //statusText.color = Color.red;
            Debug.Log(IsAuth);

            ChangeStatusText();
        })).Start();
    }
    private void ChangeStatusText()
    {
        switch (IsAuth)
        {
            case AuthCode.NotConnect:
                statusText.text = "Not Server Connection";
                break;
            case AuthCode.ErrorLoginAndPass:
                statusText.text = "Login or Password incorect";
                break;
            case AuthCode.Sucsess:
                Debug.Log("Work");
                generalPanelController.HideActivePanel();
                Debug.Log("Work2");
                statusText.text = "";
                break;
            default:
                break;
        }
    }
    public enum AuthCode
    {
        NotConnect,
        ErrorLoginAndPass,
        Sucsess
    }
}
