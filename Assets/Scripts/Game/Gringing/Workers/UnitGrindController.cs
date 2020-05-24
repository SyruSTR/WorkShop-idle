using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitGrindController : MonoBehaviour
{
    [SerializeField] private int _grindResourceCount;
    public int unitID;



    public int GrindResource { get { return _grindResourceCount; } }

    bool _startWork;
    private bool _boost;
    [SerializeField] private float _animationSpeed;
    public float _AnimationSpeed { get { return _animationSpeed; } set { _animationSpeed = value; } }
    [SerializeField] private float speedTranzition;
    public float SpeedTranzition
    {
        set
        {
            speedTranzition = value;
            if (speedTranzition > 0)
            {
                _startWork = true;
                StartCoroutine(StartStopWorking());
            }
        }
    }
    [SerializeField] private int effectivity = 2;
    public int Effectivity { get { return effectivity; } set { effectivity = value; } }
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


        //_animationSpeed = 1f;
    }

    private Coroutine speedBoost;
    private void OnMouseDown()
    {
        //Debug.Log("ggwp");
        if (!_startWork)
        {
            _startWork = true;
            SQLiteBD.ExecuteQueryWithoutAnswer($"UPDATE Units SET timeToEnd = '{System.DateTime.UtcNow.ToString("u")}' WHERE unitID = {unitID}");
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
        if (speedTranzition > 1f)
            yield return new WaitForSeconds(speedTranzition);
        else
            //seconds * minutes * hours
            yield return new WaitForSeconds(60 * 60 * 1);
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
        _workerAnim.SetSpeedAnimation(_animationSpeed * 2);
        if (!_boost)
            SetResourcesCount(_grindResourceCount * 2);
        _boost = true;
        yield return new WaitForSeconds(1f);
        _workerAnim.SetSpeedAnimation(_animationSpeed / 2);
        SetResourcesCount(_grindResourceCount / 2);
        _boost = false;
    }
}
