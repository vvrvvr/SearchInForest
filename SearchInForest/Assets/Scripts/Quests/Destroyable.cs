using UnityEngine;

public class Destroyable : Quest, IExplodable
{
    [SerializeField] private GameObject effect;
    public void BombExplode()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        Debug.Log("взорвал коробку");
        ConditionComplete();
        Destroy(gameObject);
    }

}
