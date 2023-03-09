using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //Little script for UI <<<really little>>>

    [Header("This can be customized")]
    [SerializeField] private TMP_Text _milletStackCounter;
    [SerializeField] private TMP_Text _moneyConter;

    [SerializeField] private GameObject _moneyImage;
    [SerializeField] private GameObject[] _pickUpsMillet;
    [SerializeField] private GameObject _drop;
    [SerializeField] private GameObject _takeMoney;

    private Vector3 _playerPosition;
    private Vector3 _ambarPosition;
    private int _maxBackpack = 40;
    private int _fullnessBackpack;
    private int _money;
    private int _counter;

    private void Start()
    {
        //Start with zero
        _milletStackCounter.text = Convert.ToString(0) + "/" + Convert.ToString(_maxBackpack);
        _moneyConter.text = Convert.ToString(_money);
        //init position
        _playerPosition = _pickUpsMillet[0].transform.position;
        _ambarPosition = _takeMoney.transform.position;
    }

    IEnumerator MilletRiseAnimation()
    {
        if (_counter == 5) _counter = 0;
        _pickUpsMillet[_counter].SetActive(true);
        _pickUpsMillet[_counter].transform.DOMove(_milletStackCounter.transform.position, 0.4f);

        yield return new WaitForSeconds(0.45f);
        _pickUpsMillet[_counter].SetActive(false);
        _pickUpsMillet[_counter].transform.DOMove(_playerPosition, 0.01f);


        _milletStackCounter.text = Convert.ToString(_fullnessBackpack) + "/" + Convert.ToString(_maxBackpack);
        _counter++;
    }

    IEnumerator MilletSellAnimation()
    {
        _drop.SetActive(true);
        _drop.transform.DOMove(_ambarPosition, 0.4f);
        yield return new WaitForSeconds(0.45f);
        _milletStackCounter.text = Convert.ToString(_fullnessBackpack) + "/" + Convert.ToString(_maxBackpack);
        _drop.SetActive(false);
        _takeMoney.SetActive(true);
        _drop.transform.DOMove(_milletStackCounter.transform.position, 0.4f);
        _takeMoney.transform.DOMove(_moneyConter.transform.position, 0.4f);
        yield return new WaitForSeconds(0.45f);
        _takeMoney.SetActive(false);
        _takeMoney.transform.DOMove(_ambarPosition, 0.1f);
        _milletStackCounter.text = Convert.ToString(0) + "/" + Convert.ToString(_maxBackpack);
        _moneyConter.text = Convert.ToString(_money-25);
        _moneyImage.transform.DOScale(2, 1);
        yield return new WaitForSeconds(0.3f);
        _moneyConter.text = Convert.ToString(_money);
        _moneyImage.transform.DOScale(1, 1);
    }

    private void FullnessChanched(int fullness)
    {
        _fullnessBackpack = fullness;
        StartCoroutine(MilletRiseAnimation());
    }

    private int MoneyChanched(int money)
    {
        _money += money;
        StartCoroutine(MilletSellAnimation());
        return 0;
    }

    private void OnEnable()
    {
        PlayerController.OnFullnessChanched += FullnessChanched;
        PlayerController.OnMoneyChanched += MoneyChanched;
    }

    private void OnDisable()
    {
        PlayerController.OnFullnessChanched -= FullnessChanched;
        PlayerController.OnMoneyChanched -= MoneyChanched;

    }
}
