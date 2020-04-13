using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrderZone : MonoBehaviour
{
    Order[] _orders;
    bool _hasActiveOrders;
    int _nextOrderID = 0;

    IEnumerator _orderCreator;
    IEnumerator _orderActiveChecker;

    UnityEventInt _changeNextOrderIDEvent = new UnityEventInt();

    public UnityEventInt ChangeNextOrderIDEvent { get => _changeNextOrderIDEvent;}

    private void Awake()
    {
        _orders = new Order[transform.childCount];
        
        _orderCreator = OrderCreator();
        _orderActiveChecker = OrderActiveChecker();
    }

    private void Start()
    {
        AssignOrders();
        BellZone.Instance.BellPressedEvent.AddListener(OnBellPressed);

        StartCoroutine(_orderCreator);
        StartCoroutine(_orderActiveChecker);
    }

    IEnumerator OrderCreator()
    {
        yield return new WaitForSeconds(2);
        while (true)
        {
            if (!_hasActiveOrders)
            {
                CreateNewOrder();
                yield return new WaitForEndOfFrame();
                NextOrder();
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(1, 3));
                CreateNewOrder();
            }
        }
    }

    IEnumerator OrderActiveChecker()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (!_orders[_nextOrderID].IsActive)
            {
                _nextOrderID = 0;
                _changeNextOrderIDEvent.Invoke(0);
            }

            foreach (var order in _orders)
            {
                if (order.IsActive)
                {
                    _hasActiveOrders = true;
                    break;
                }
                else
                {
                    _hasActiveOrders = false;
                }
            }
        }
    }

    void AssignOrders()
    {
        for (int i = 0; i < _orders.Length; i++)
        {
            _orders[i] = transform.GetChild(i).GetComponent<Order>();
        }
    }

    void OnBellPressed()
    {
        if (_hasActiveOrders)
        {
            Player.Instance.AddRating(CheckOrderRequirements());
            NextOrder();
        }
    }

    void NextOrder()
    {
        if (_hasActiveOrders)
        {
            for (int i = _orders.Length - 1; i >= 0; i--)
            {
                if (_orders[i].IsActive)
                {
                    _nextOrderID = i;
                    _changeNextOrderIDEvent.Invoke(_nextOrderID);
                    break;
                }
            }
        }
    }

    void CreateNewOrder()
    {
        foreach (var order in _orders)
        {
            if (!order.IsActive)
            {
                order.Activate();
                break;
            }
        }
    }

    Rating CheckOrderRequirements()
    {
        int rating = 0;

        if (_orders[_nextOrderID].WantMeat == BurgerZone.Instance.HasMeat)
        {
            rating += 1;
        }

        Debug.LogWarning(rating);

        if (_orders[_nextOrderID].WantCheese == BurgerZone.Instance.HasCheese)
        {
            rating += 1;
        }

        Debug.LogWarning((int)rating);

        if (_orders[_nextOrderID].WantSalad == BurgerZone.Instance.HasSalad)
        {
            rating += 1;
        }

        Debug.LogWarning(rating);

        if (_orders[_nextOrderID].WantOnion == BurgerZone.Instance.HasOnion)
        {
            rating += 1;
        }

        Debug.LogWarning(rating);

        if (_orders[_nextOrderID].WantTomato == BurgerZone.Instance.HasTomato)
        {
            rating += 1;
        }

        Debug.LogWarning(rating);

        if (!BurgerZone.Instance.HasBuns)
        {
            rating -= 3;
        }

        Debug.LogWarning(rating);

        if (rating < 0)
        {
            rating = 0;
        }

        _orders[_nextOrderID].Deactivate();

        Rating finalRating = (Rating)rating;

        return finalRating;
    }

}
