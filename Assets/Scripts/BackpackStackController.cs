using UnityEngine;
using DG.Tweening;

public class BackpackStackController : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;
    [SerializeField] private GameObject[] _stacks;

    private void Start()
    {
        Vector3 Rotator = transform.position + new Vector3(-2,-2,-10); 
        transform.DOLocalRotate(Rotator, 0.75f).SetLoops(-1, LoopType.Yoyo);
    }

    public void StacksEnable()
    {
        if (_playerController.FullnessOfBackpack >= 10) _stacks[0].SetActive(true);
        else _stacks[0].SetActive(false);
        if (_playerController.FullnessOfBackpack >= 20) _stacks[1].SetActive(true);
        else _stacks[1].SetActive(false);
        if (_playerController.FullnessOfBackpack >= 30) _stacks[2].SetActive(true);
        else _stacks[2].SetActive(false);
    }

}
