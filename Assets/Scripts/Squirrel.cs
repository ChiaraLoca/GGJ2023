using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Level1
{
    public class Squirrel : MonoBehaviour
{
    [SerializeField] Transform _playerPosition;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] private List<Transform> _path;
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private float arcHeight = 2f;
    [SerializeField] private float rotationSpeed = 10f;

    [SerializeField] private int _pathStep = 0;

    [SerializeField] private bool _grounded = true;

    [SerializeField] private bool _canJump = true;
    [SerializeField] private bool _canMove = false;

    [SerializeField] private AudioSource _audioSouce;
    private void Start()
    {

            StartCoroutine(MoveAlongPath());

    }

        private IEnumerator MoveAlongPath()
        {
            yield return new WaitForSeconds(5);

            for (int i = 0; i < _path.Count - 1; i++)
            {
                Vector3 startPoint = _path[i].position;
                Vector3 endPoint = _path[i + 1].position;

                float elapsed = 0f;

                while (elapsed < moveDuration)
                {
                    float t = elapsed / moveDuration;

                    // Linear interpolation between start and end
                    Vector3 horizontalPosition = Vector3.Lerp(startPoint, endPoint, t);

                    // Add arc (parabolic curve)
                    float height = Mathf.Sin(Mathf.PI * t) * arcHeight;
                    Vector3 arcPosition = new Vector3(horizontalPosition.x, horizontalPosition.y + height, horizontalPosition.z);

                    transform.position = arcPosition;

                    // Face movement direction
                    Vector3 direction = (endPoint - startPoint).normalized;
                    if (direction != Vector3.zero)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                    }

                    elapsed += Time.deltaTime;
                    yield return null;
                }

                // Snap to exact final position
                transform.position = endPoint;

                yield return new WaitForSeconds(1);
            }

            transform.position = _path[_path.Count - 1].position;
        }

        /*void Update()
        {
            if(_canMove)
            {
                IsGrounded();
                if ((transform.position - _path[_pathStep].position).magnitude>5)
                {
                    Vector3 direction = (_path[_pathStep].position - transform.position).normalized;
                    _rigidbody.MovePosition(transform.position + direction * 15 * Time.deltaTime);
                }
                else
                {
                    _pathStep++;
                    if ((int)Random.Range(0f, 4f) % 2 == 0)
                        _audioSouce.Play();
                    else
                        _audioSouce.Stop();
                }
            }
            if (_pathStep == _path.Count)
                Destroy(gameObject);

           // if (!_canMove )
            //{
               // if ((transform.position - _playerPosition.position).magnitude < 20)
               // {
                    Debug.Log((transform.position - _playerPosition.position).magnitude);
                    nextStep(); 
               // }
            //}


        }*/

        public IEnumerator WaitAndStart()
        {
            yield return new WaitForSeconds(5);
            _canMove = true;
        }

   /* public void nextStep()
    {
        _pathStep++;
        _canMove = true;
        
    }*/
    private bool IsGrounded()
    {

        if (Physics.Raycast(transform.position, Vector3.down, 1.5f))
        {
            /*_grounded = true;
            _canJump = true;*/
            _rigidbody.useGravity = true;
            return true;
        }
        else
        {
            /*_grounded = false;
            if(_canJump)
            {
                _rigidbody.AddForce(transform.forward + new Vector3(0, 2, 0), ForceMode.Impulse) ;*
                _canJump = false;
            }*/
            _rigidbody.useGravity = false;
            
            return false;
        }
    }



}}


