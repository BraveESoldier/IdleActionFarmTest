using UnityEngine;

[CreateAssetMenu(fileName = "New Millet", menuName = "Millet/Create new Millet", order = 51)]
public class Millet : ScriptableObject
{
    [SerializeField] private int _size;
    [SerializeField] private float _growthRate;
    [SerializeField] private int _price;
    [SerializeField] private GameObject _typeMillet;

    public int Size => _size;
    public float GrowthRate => _growthRate;
    public int Price => _price;
    public GameObject TypeMillet => _typeMillet;
}
