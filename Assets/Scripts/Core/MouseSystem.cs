using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public sealed class MouseSystem : MonoBehaviour
{
    static MouseSystem _instance;
    public static MouseSystem Instance { get => _instance; }

    [SerializeField] SpriteContainer _spriteContainer = null;
    SpriteRenderer _spriteRenderer = null;

    [SerializeField] bool _isHolding;
    [SerializeField] bool _isHoldingKnife;
    [SerializeField] Food _holdingFood = new Food();

    public bool IsHolding { get => _isHolding;}

    public bool IsHoldingKnife { get => _isHoldingKnife; }


    private void PickupFood(Food value)
    {
        _isHolding = true;
        _holdingFood = value;
        UpdateSprite();
    }

    private void DropHolding()
    {
        _isHolding = false;
        UpdateSprite();
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    private void Start()
    {
        StartCoroutine(UpdatePosition());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastToMousePos();
        }
    }

    IEnumerator UpdatePosition()
    {
        while (true)
        {
            Vector3 _mousePosNoZ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mousePosNoZ.z = 0;
            transform.position = _mousePosNoZ;
            yield return null;
        }
    }

    public void UpdateSprite()
    {
        // Reset Sprite color
        _spriteRenderer.color = Color.white;
        // player is holding any food and not a knife
        if (_isHolding && !_isHoldingKnife)
        {
            switch (_holdingFood)
            {
                case Food.Bun:
                    _spriteRenderer.sprite = _spriteContainer.Bun;
                    break;
                case Food.rawMeat:
                    _spriteRenderer.sprite = _spriteContainer.Meat;
                    _spriteRenderer.color = new Color32(255, 107, 222, 255);
                    break;
                case Food.Cheese:
                    _spriteRenderer.sprite = _spriteContainer.Cheese;
                    break;
                case Food.Salad:
                    _spriteRenderer.sprite = _spriteContainer.Salad;
                    break;
                case Food.Onion:
                    _spriteRenderer.sprite = _spriteContainer.Onion;
                    break;
                case Food.Tomato:
                    _spriteRenderer.sprite = _spriteContainer.Tomato;
                    break;
                case Food.cookedMeat:
                    _spriteRenderer.sprite = _spriteContainer.Meat;
                    break;
                case Food.burnedMeat:
                    _spriteRenderer.sprite = _spriteContainer.Meat;
                    _spriteRenderer.color = new Color(0.5f, 0.4f, 0.5f, 255);
                    break;
                case Food.cutSalad:
                    _spriteRenderer.sprite = _spriteContainer.CutSalad;
                    break;
                case Food.cutOnion:
                    _spriteRenderer.sprite = _spriteContainer.CutOnion;
                    break;
                case Food.cutTomato:
                    _spriteRenderer.sprite = _spriteContainer.CutTomato;
                    break;
                default:
                    Debug.LogError("Default in ClickedOnFridge called!");
                    _spriteRenderer.sprite = null;
                    break;
            }
        }
        // player holds a knife
        else if (_isHoldingKnife)
        {
            _spriteRenderer.sprite = _spriteContainer.Knife;
        }
        // player holds nothing
        else
        {
            _spriteRenderer.sprite = null;
        }
    }

    void RaycastToMousePos()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.zero);
        if (hit2D)
        {
            Debug.Log(hit2D.transform.gameObject.name);
            RaycastHit2DCheck(hit2D);
        }
    }

    void RaycastHit2DCheck(RaycastHit2D hit2D)
    {
        Transform hit = hit2D.transform;

        FridgeSlot fridge = hit.GetComponent<FridgeSlot>();
        GrillZone grill = hit.transform.GetComponent<GrillZone>();
        CuttingKnife cuttingKnife = hit.transform.GetComponent<CuttingKnife>();
        UncutZone uncutZone = hit.transform.GetComponent<UncutZone>();
        CutZone cutZone = hit.transform.GetComponent<CutZone>();
        BurgerZone burgerZone = hit.transform.GetComponent<BurgerZone>();
        Trashbin trashbin = hit.transform.GetComponent<Trashbin>();
        
        if (fridge)
        {
            ClickedOnFridge(fridge);
        }
        else if (grill)
        {
            ClickedOnGrill(grill);
        }
        else if (uncutZone)
        {
            ClickedOnUncutZone(uncutZone);
        }
        else if (trashbin)
        {
            ClickedOnTrashbin();
        }
        else if (cuttingKnife)
        {
            ClickedOnCuttingKnife(cuttingKnife);
        }
        else if (cutZone)
        {
            ClickedOnCutZone(cutZone);
        }
        else if (burgerZone)
        {
            ClickedOnBurgerZone(burgerZone);
        }
    }



    #region ClickedOn Handlers

    private void ClickedOnCuttingKnife(CuttingKnife cuttingKnife)
    {
        if (!_isHolding && !_isHoldingKnife) // take Knife
        {
            cuttingKnife.ChangeStatus();
            _isHoldingKnife = true;
            UpdateSprite();
        }
        else if (_isHoldingKnife) // give Knife back
        {
            cuttingKnife.ChangeStatus();
            _isHoldingKnife = false;
            UpdateSprite();
        }
        else 
        {
            Debug.Log("Can't pick up Knife.");
        }
    }

    private void ClickedOnTrashbin()
    {
        if (_isHoldingKnife)
        {
            Debug.Log("Cannot trash knife.");
        }
        else
        {
            DropHolding();
            Debug.Log("Food trashed.");
        }
    }

    void ClickedOnGrill(GrillZone grill)
    {
        if (_isHolding && !_isHoldingKnife)
        {
            if (_holdingFood == Food.rawMeat || _holdingFood == Food.cookedMeat || _holdingFood == Food.burnedMeat)
            {
                if (grill.AddMeat(_holdingFood))
                {
                    DropHolding();
                    Debug.Log("Placed meat on grill.");
                }
            }
            else
            {
                Debug.LogError("Could not place food on grill.");
            }
        }
        else if (!_isHoldingKnife)
        {
            if (grill.HasMeat)
            {

                PickupFood(grill.RemoveMeat());
            }
        }
    }

    void ClickedOnFridge(FridgeSlot slot)
    {
        if (_isHolding & !_isHoldingKnife)
        {
            if (slot.RawFoodType == _holdingFood)
            {
                slot.AddAmount(1);
                DropHolding();
                Debug.Log("Placed food back in fridge.");
            }
            else
            {
                Debug.Log("Clicked on wrong fridgeslot or holding wrong food.");
            }
        }
        else
        {
            if (slot.DecAmount())
            {
                PickupFood(slot.RawFoodType);
            }
        }
    }

    void ClickedOnUncutZone(UncutZone zone)
    {
        if (_isHolding && !_isHoldingKnife)
        {
            if (_holdingFood == Food.Salad || _holdingFood == Food.Onion || _holdingFood == Food.Tomato)
            {
                if (zone.AddFood(_holdingFood))
                {
                    DropHolding();
                }
            }
        }
        else if (_isHoldingKnife)
        {
            zone.CutFoodChecker();
        }
    }

    void ClickedOnCutZone(CutZone zone)
    {
        if (!_isHolding && !_isHoldingKnife)
        {
            if (zone.IsHolding)
            {
                PickupFood(zone.GetFood);
            }
        }
    }

    void ClickedOnBurgerZone(BurgerZone burgerZone)
    {
        if (_isHolding)
        {
            switch (_holdingFood)
            {
                case Food.Bun:
                    if (!burgerZone.HasBuns)
                    {
                        burgerZone.AddBuns();
                        DropHolding();
                    }
                    break;
                case Food.rawMeat:

                    break;
                case Food.Cheese:
                    if (!burgerZone.HasCheese)
                    {
                        burgerZone.AddCheese();
                        DropHolding();
                    }
                    break;
                case Food.Salad:

                    break;
                case Food.Onion:

                    break;
                case Food.Tomato:

                    break;
                case Food.cookedMeat:
                    if (!burgerZone.HasMeat)
                    {
                        burgerZone.AddMeat();
                        DropHolding();
                    }
                    break;
                case Food.burnedMeat:

                    break;
                case Food.cutSalad:
                    if (!burgerZone.HasSalad)
                    {
                        burgerZone.AddCutSalad();
                        DropHolding();
                    }
                    break;
                case Food.cutOnion:
                    if (!burgerZone.HasOnion)
                    {
                        burgerZone.AddCutOnion();
                        DropHolding();
                    }
                    break;
                case Food.cutTomato:
                    if (!burgerZone.HasTomato)
                    {
                        burgerZone.AddCutTomato();
                        DropHolding();
                    }
                    break;
                default:
                    break;
            }

        }
    }

    #endregion
}
