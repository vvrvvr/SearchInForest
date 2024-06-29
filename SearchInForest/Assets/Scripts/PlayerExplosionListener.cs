using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosionListener : MonoBehaviour, IExplodable
{
    [HideInInspector] public bool delayAfterExplosionAffect = false;
    [HideInInspector] public float delayTime = 0.3f;
    private bool isDelay = false;

    public void BombExplode()
    {
        if (delayAfterExplosionAffect)
        {
            if (isDelay)
                return;
            isDelay = true;
            GameManager.Instance.ReduceLives(1, 1);
            if (gameObject.activeInHierarchy)
                StartCoroutine(ResetDelay());
        }
        else
        {
            GameManager.Instance.ReduceLives(1, 1);
        }
    }

    private IEnumerator ResetDelay()
    {
        yield return new WaitForSeconds(delayTime);
        isDelay = false;
    }
}