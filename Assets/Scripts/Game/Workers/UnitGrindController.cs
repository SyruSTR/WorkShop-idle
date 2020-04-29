using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitGrindController : MonoBehaviour
{
    [SerializeField] private int _grindResourceCount;
    private float _animationSpeed;


    public int GrindResource { get { return _grindResourceCount; } }

    bool _startWork;
    bool _boost;
    [SerializeField] float speedTranzition;
    [SerializeField] int effectivity = 2;
    WorkerAnimation _workerAnim;
    AllChoppersController _mainChoppersController;

    private void Awake()
    {
        _mainChoppersController = GetComponentInParent<AllChoppersController>();
        _workerAnim = GetComponent<WorkerAnimation>();
        GetComponent<Image>().sprite = null;
    }
    void Start()
    {
        _startWork = false;
        _boost = false;
        _animationSpeed = 1f;
    }

    private Coroutine speedBoost;
    private void OnMouseDown()
    {
        //Debug.Log("ggwp");
        if (!_startWork)
        {
            _startWork = true;
            
            StartCoroutine(StartStopWorking());
        }
        else
        {
            if (!_boost)
                speedBoost = StartCoroutine(SpeedBoost());
            else
            {
                StopCoroutine(speedBoost);
                speedBoost = StartCoroutine(SpeedBoost());
            }
        }

    }
    private IEnumerator StartStopWorking()
    {
        SetResourcesCount(effectivity);
        _workerAnim.SetSpeedAnimation(_animationSpeed);
        _workerAnim.SetAnimVarible(AnimVarible.AnimatorVarible.animBool, "startWork", true);
        yield return new WaitForSeconds(speedTranzition);
        if (_boost)
        {
            StopCoroutine(speedBoost);
            _boost = false;
        }
        _workerAnim.SetAnimVarible(AnimVarible.AnimatorVarible.animBool, "startWork", false);
        SetResourcesCount(0);
        
        yield return new WaitForSeconds(1f);
        _startWork = false;
    }
    private void SetResourcesCount(int count)
    {
        _mainChoppersController.Allgrindresources = -_grindResourceCount;
        _grindResourceCount = count;
        _mainChoppersController.Allgrindresources = _grindResourceCount;
    }
    private IEnumerator SpeedBoost()
    {        
            _workerAnim.SetSpeedAnimation(_animationSpeed*2);
        if(!_boost)
            SetResourcesCount(_grindResourceCount * 2);
            _boost = true;
        yield return new WaitForSeconds(1f);
            _workerAnim.SetSpeedAnimation(_animationSpeed/2);
            SetResourcesCount(_grindResourceCount / 2);
        _boost = false;
    }
}
