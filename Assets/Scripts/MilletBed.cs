using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilletBed : MonoBehaviour
{
    [SerializeField] private GameObject[] _beds;
    [SerializeField] private List<Millet> _typeOfMillet;

    private GameObject _millet;
    private float secundomer;
    private GameObject[] _millets;

    private void Start()
    {
        _millet = _typeOfMillet[0].TypeMillet;
        _millets = new GameObject[_beds.Length];
        StartCreateMillet(_millet);
    }

    private void StartCreateMillet(GameObject millet)
    {
        for(int i = 0; i < _beds.Length ;i++)
        {
            GameObject NewMillet = Instantiate(millet, _beds[i].transform);
            _millets[i] = NewMillet;
        }
    }

    private int MowedDown(GameObject millet)
    {
        DisableMillet(millet);
        return _typeOfMillet[0].Size;
    }

    private void DisableMillet(GameObject millet)
    {
        SwitchingStates(millet, false, false);

        StartCoroutine(StartOfGrowth(millet));
    }

    IEnumerator StartOfGrowth(GameObject millet)
    {
        yield return new WaitForSeconds(5);
        SwitchingStates(millet, true, false);
        yield return new WaitForSeconds(5);
        SwitchingStates(millet, true, true);
    }

    private void SwitchingStates(GameObject millet, bool stateLittle, bool stateBig)
    {
        //переключение littleMillet
        foreach (Transform child in millet.transform)
        {
            child.gameObject.SetActive(stateLittle);
        }
        //Псевдопереключение BigMillet
        millet.GetComponent<MeshRenderer>().enabled = stateBig;
        millet.GetComponent<BoxCollider>().enabled = stateBig;
    }

    private void OnEnable()
    {
        PlayerController.OnMowedDown += MowedDown;
    }
    private void OnDisable()
    {
        PlayerController.OnMowedDown -= MowedDown;
    }
}
