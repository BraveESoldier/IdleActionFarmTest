using System;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    //[SerializeField] private int _money;
    [SerializeField] private int _fullnessOfBackpack;
    [SerializeField] private BackpackStackController _backpackStackController;


    private int _maxBackpack = 40; //40 ъръ т ђч
    private int _milletPrice = 15; //15 ъръ т ђч

    public int MaxBackpack => _maxBackpack;
    public int FullnessOfBackpack => _fullnessOfBackpack;

    public static Func<GameObject, int> OnMowedDown;
    public static Action<int> OnFullnessChanched;
    public static Func<int,int> OnMoneyChanched;

    private void OnCollisionEnter(Collision collision)
    {
        //it is necessary to bring in a new method
        if (collision.gameObject.tag == "Millet")
        {
            if(_fullnessOfBackpack < _maxBackpack)
            {
                _fullnessOfBackpack += Convert.ToInt32(OnMowedDown?.Invoke(collision.gameObject));
                OnFullnessChanched?.Invoke(_fullnessOfBackpack);
                _backpackStackController.StacksEnable();
            }
        }

        if(collision.gameObject.tag == "Shop")
        {
            if(_fullnessOfBackpack > 0)
            {
                _fullnessOfBackpack = Convert.ToInt32(OnMoneyChanched?.Invoke(_fullnessOfBackpack*_milletPrice));
                _backpackStackController.StacksEnable();
            }
        }
    }


}
