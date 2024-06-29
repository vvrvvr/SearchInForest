using System;
using UnityEngine;
using System.Collections;

public class ExplodeListener : MonoBehaviour, IExplodable
{
  [SerializeField] private Bomb bomb;
  public int reduceTimeAtExplosion = 2;
  [Space(10)]
  [SerializeField] private bool delayAfterExplosionAffect = false;
  [SerializeField] private float delayTime = 0.3f;
  [Space(10)]
  public float bombCameraShakePower = 0.1f;
  public int bombTimer = 10; 
  
  
  private bool isDelay = false;

  private void Start()
  {
    bomb.bombShakePower = bombCameraShakePower;
    bomb.bombTimer = bombTimer;
  }

  public void BombExplode()
  {
    if (delayAfterExplosionAffect)
    {
      if (isDelay)
        return;
      isDelay = true;
      bomb.ReduceTime(reduceTimeAtExplosion);
      StartCoroutine(ResetDelay());
    }
    else
    {
      bomb.ReduceTime(reduceTimeAtExplosion);
    }
  }

  public void RestartExplode()
  {
    bomb.ReduceTime(20);
  }
  
  private IEnumerator ResetDelay()
  {
    yield return new WaitForSeconds(delayTime);
    isDelay = false;
  }
}

