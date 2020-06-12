using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityToolbag;
using static TestAPI;

public class Auth : MonoBehaviour
{
    [SerializeField] public TMP_InputField loginField;
    [SerializeField] public TMP_InputField passwordField;
    [SerializeField] public TextMeshProUGUI statusText;
    [Space]
    [SerializeField] ControllerGeneralPanels generalPanelController;
    [SerializeField] private Color errorColor;
    [Space]
    [SerializeField] CheckAuth checkAuth;

    public AuthCode IsAuth;
    private void Start()
    {        
        IsAuth = 0;
        if(GetTokenInDB())
        {
            checkAuth.AuthON();
        }
    }
    public void Login()
    {
        statusText.color = Color.white;
        statusText.text = "Login is...";
        ThreadAuth();

    }
    private void ThreadAuth()
    {
        new Thread(new ThreadStart(async () =>
        {
            IsAuth = await PostToken(loginField.text, passwordField.text);

            Dispatcher.Invoke(() =>
            {

                Debug.Log(IsAuth);

                ChangeStatusText();
            });
        })).Start();
    }

    private void ChangeStatusText()
    {
        statusText.color = errorColor;
        switch (IsAuth)
        {
            case AuthCode.NotConnect:
                statusText.text = "Not Server Connection";
                break;
            case AuthCode.ErrorLoginAndPass:
                statusText.text = "Login or Password incorect";
                break;
            case AuthCode.Sucsess:
                generalPanelController.HideActivePanel();
                statusText.text = "";
                checkAuth.AuthON();
                break;
            default:
                break;
        }
    }    
}
