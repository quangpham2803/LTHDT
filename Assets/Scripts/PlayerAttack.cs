using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    public Animator animatorAttack;
    public UnityEvent<int> OnActionComple;
    public int water;
    public int beer;
    bool isOldMen = true;
    private void Awake()
    {
        water = 100;
        if (isOldMen)
        {
            OnActionComple.AddListener(DrinkBeer);
        }
        else
        {

            OnActionComple.AddListener(DrinkWater);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
    }
    public void Attack()
    {
        animatorAttack.SetTrigger("Attack");
        StartCoroutine(IEDelayAction());

    }

    IEnumerator IEDelayAction()
    {
        yield return new WaitForSeconds(3);
        if(OnActionComple != null)
        OnActionComple.Invoke(3);
    }

    private void DrinkWater(int amount)
    {
        water -= amount;
        Debug.Log("Uong nc xong sau khi chem'!" + water);
    }

    private void DrinkBeer(int amount)
    {
        beer -= amount;
        Debug.Log("Uong bia xong sau khi chem'!" + beer);
    }
}
