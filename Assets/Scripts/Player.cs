using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 1f;
    [SerializeField]
    private float _jumpHeight = 15.0f;
    private float _yVelocity;
    private bool _canDoubleJump = false;
    [SerializeField]
    private int _coins;
    private UIManager _uiManager;
    [SerializeField]
    private int _lives = 3;
    private Vector3 _direction, _velocity;
    private bool _canWalljump;
    private Vector3 _wallSurfaceNormal;
    [SerializeField]
    private float _pushPower = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }

        _uiManager.UpdateLivesDisplay(_lives);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (_controller.isGrounded)
        {
            _canDoubleJump = true;
            _canWalljump = false; // Reset wall jump flag when grounded
            _yVelocity = -_gravity; // Reset the vertical velocity when grounded

            _direction = new Vector3(horizontalInput, 0, 0);
            _velocity = _direction * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
            }
        }
        else
        {
            // Check for double jump
            if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump)
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = false;
            }

            // Check for wall jump
            if (Input.GetKeyDown(KeyCode.Space) && _canWalljump)
            {
                _yVelocity = _jumpHeight;
                _velocity = _wallSurfaceNormal * _speed;
                _canWalljump = false;
            }

            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;

        _controller.Move(_velocity * Time.deltaTime);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //check for the moving box
        //confirm it has a rigidbody
        //push power -- declare variable on top
        //push! (using movingBox velocity)
        if (hit.gameObject.CompareTag("MovingBox"))
        {
            Rigidbody boxRigidbody = hit.collider.attachedRigidbody;

            // Confirm the box has a Rigidbody component
            if (boxRigidbody != null && !boxRigidbody.isKinematic)
            {
                // Apply pushing force using the box's velocity
                Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z);
                boxRigidbody.velocity = pushDirection * _pushPower;
            }
        }
        //if not grounded && touching a wall

        if (_controller.isGrounded == false && hit.transform.tag == "Wall")
        {
            Debug.DrawRay(hit.point, hit.normal, Color.red);
            _wallSurfaceNormal = hit.normal;
            _canWalljump = true;
        }        
    }
    public void AddCoins()
    {
        _coins++;
        _uiManager.UpdateCoinDisplay(_coins);
    }

    public void Damage()
    {
        _lives--;

        _uiManager.UpdateLivesDisplay(_lives);

        if (_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }

    public int CoinCoint()
    {
        return _coins;
    }
}




