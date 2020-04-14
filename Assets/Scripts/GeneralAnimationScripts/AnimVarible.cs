using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimVarible : MonoBehaviour
{

    #region animation varible logic)
    protected Animator _animator;
    public enum AnimatorVarible
    {
        animInt,
        animFloat,
        animBool,
        animTrigger
    }
    public void SpeedAnimationBoost(float speed)
    {
        _animator.speed *= speed;
    }
    public void SetSpeedAnimation(float speed)
    {
        _animator.speed = speed;
    }

    public void SetAnimVarible(AnimatorVarible varible, string nameAnimVarible)
    {
        if (varible == AnimatorVarible.animTrigger)
        {
            _animator.SetTrigger(nameAnimVarible);
        }
        else
            Debug.LogError("Anim varible of the wrong tipe");
    }
    public void SetAnimVarible(AnimatorVarible varible, string nameAnimVarible, int value)
    {
        if (varible == AnimatorVarible.animInt)
        {
            _animator.SetInteger(nameAnimVarible, value);
        }
        else
            Debug.LogError("Anim varible of the wrong tipe");
    }
    public void SetAnimVarible(AnimatorVarible varible, string nameAnimVarible, float value)
    {
        if (varible == AnimatorVarible.animFloat)
        {
            _animator.SetFloat(nameAnimVarible, value);
        }
        else
            Debug.LogError("Anim varible of the wrong tipe");
    }
    public void SetAnimVarible(AnimatorVarible varible, string nameAnimVarible, bool value)
    {
        if (varible == AnimatorVarible.animBool)
        {
            _animator.SetBool(nameAnimVarible, value);
        }
        else
            Debug.LogError("Anim varible of the wrong tipe");
    }
    #endregion
}
