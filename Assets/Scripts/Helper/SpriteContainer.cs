using UnityEngine;

public class SpriteContainer : ScriptableObject
{
    [SerializeField] Sprite bun = null;
    [SerializeField] Sprite meat = null;
    [SerializeField] Sprite cheese = null;
    [SerializeField] Sprite salad = null;
    [SerializeField] Sprite cutSalad = null;
    [SerializeField] Sprite onion = null;
    [SerializeField] Sprite cutOnion = null;
    [SerializeField] Sprite tomato = null;
    [SerializeField] Sprite cutTomato = null;
    [SerializeField] Sprite knife = null;

    [SerializeField] Sprite redCross = null;
    [SerializeField] Sprite greenCheck = null;
    [SerializeField] Sprite area = null;

    public Sprite Bun { get => bun; }
    public Sprite Meat { get => meat; }
    public Sprite Cheese { get => cheese; }
    public Sprite Salad { get => salad; }
    public Sprite Onion { get => onion; }
    public Sprite Tomato { get => tomato; }
    public Sprite CutSalad { get => cutSalad; }
    public Sprite CutOnion { get => cutOnion; }
    public Sprite CutTomato { get => cutTomato; }

    public Sprite Knife { get => knife; }
    public Sprite RedCross { get => redCross;}
    public Sprite GreenCheck { get => greenCheck; }
    public Sprite Area { get => area; }
}
