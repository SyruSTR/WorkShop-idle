using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    Animator _animator;
    [SerializeField] bool _startWork;
    [SerializeField] bool _boost;
    [SerializeField] float speedTranzition;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        GetComponent<Image>().sprite = null;
    }
    void Start()
    {
        _startWork = false;
        _boost = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("ggwp");
        if (!_startWork)
        {
            _animator.SetBool("startWork", true);
            _startWork = true;
            StartCoroutine(StopWorking());
        }
        else
        {
            if (!_boost)
                StartCoroutine(SpeedBoost());
        }

    }
    private IEnumerator StopWorking()
    {
        yield return new WaitForSeconds(speedTranzition);
        _animator.SetBool("startWork", false);
        if (_boost)
        {
            StopCoroutine(SpeedBoost());
            _boost = false;
        }
        yield return new WaitForSeconds(1f);
        _startWork = false;
    }
    private IEnumerator SpeedBoost()
    {
        _boost = true;
        _animator.speed *= 2;
        yield return new WaitForSeconds(1f);
        _boost = false;
        _animator.speed /= 2;

    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
}
