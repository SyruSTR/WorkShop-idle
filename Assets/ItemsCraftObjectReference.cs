using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsCraftObjectReference : MonoBehaviour
{
    [SerializeField] private ControllerGeneralPanels additionalCanvas;
    public ControllerGeneralPanels AdditionalCanvas { get { return additionalCanvas; } }
    [SerializeField] private AddRecipeOnScript craftPanel;
    public AddRecipeOnScript CraftPanel { get { return craftPanel; } }
}
