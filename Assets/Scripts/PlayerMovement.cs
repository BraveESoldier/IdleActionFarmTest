using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private Joystick _joystick;

    private Animator _anim;
    private Rigidbody _rbPlayer;
    private bool _taking;

    float Angle360(Vector3 from, Vector3 to, Vector3 right)
    {
        float angle = Vector3.Angle(from, to);
        return (Vector3.Angle(right, to) > 90f) ? 360f - angle : angle;
    }

    private Vector3 _movementVector
    {
        get
        {
            var horizontal = _joystick.Horizontal;
            var vertical = _joystick.Vertical;

            return new Vector3(horizontal, 0.0f, vertical);
        }
    }

    private void Start()
    {
        _rbPlayer = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    private void MovementLogic()
    {
        if (_joystick.Horizontal != 0 && _taking == false)
        {
            _anim.Play("RunAnimation");
            _rbPlayer.AddForce(_movementVector * _speed, ForceMode.Impulse);

            transform.Rotate(Vector3.up, Angle360(transform.forward, _movementVector, transform.right));
        }
        else if (_taking == false) _anim.Play("Idle");
    }
    private void Taking(int crutch)
    {
        _taking = true;
        _anim.Play("Taking");
        StartCoroutine(TakeAnimation());
    }

    IEnumerator TakeAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        _taking = false;
    }

    private void OnEnable()
    {
        PlayerController.OnFullnessChanched += Taking;
    }

    private void OnDisable()
    {
        PlayerController.OnFullnessChanched -= Taking;
    }

    private void FixedUpdate()
    {
        MovementLogic();
    }
}
