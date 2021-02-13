using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //player states
    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
    const int STATE_ATTACK = 2;
    const int STATE_TAKE_DAMAGE = 3;

    public string _currentDirection = "right";
    public static int _currentAnimationState = STATE_IDLE; 
    public bool _isInvulnerable;

    [Range (0, 10f)]
    public float _moveSpeed = 1f;

    Transform _player;
    Animator _animator;

    [SerializeField]
    GameObject bullet = null;

    [SerializeField]
    HPValue hp = null;

    [SerializeField]
    AudioSource shootSource = null;

    bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        hp._RuntimeValue = 3;
        _player = this.transform;
        _animator = GetComponent<Animator>();
    }

     void Update()
     {
        bool shoot = Input.GetKeyDown("k") || Input.GetKeyDown("space") || Input.GetKeyDown("j");

        MovePlayer(new Vector3(0, Input.GetAxis("Vertical") * _moveSpeed, 0));

        if (shoot)
        {
            if (canShoot)
            {
                canShoot = false;
                StartCoroutine(Shoot());
            }
        }

     }

    IEnumerator Shoot()
    {
        ChangeState(STATE_ATTACK);
        //spawn bullet
        Instantiate(bullet, this.transform.position + new Vector3(0.275f, -0.095f), Quaternion.Euler(0, 0, 0));   // shoot left
        shootSource.Play();
        yield return new WaitForSeconds(1f);
        canShoot = true;

    }

    void MovePlayer(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            Vector2 _objPos = _player.position;
            if (dir.y > 0)
            {
                _objPos = new Vector2(_player.position.x, _player.position.y+0.01f);
            }
            else if (dir.y < 0)
            {
                _objPos = new Vector2(_player.position.x, _player.position.y-0.01f);
            }
            _player.position = Vector2.MoveTowards(_player.position, _objPos, Time.deltaTime);
            ChangeState(STATE_WALK);
        }
        else
        {
            ChangeState(STATE_IDLE);
        }
    }

    public void TakeDamage()
    {
        if (!_isInvulnerable)
        {
            ChangeState(STATE_TAKE_DAMAGE);
            hp._RuntimeValue--;
            //play damage taken sound
            //play damage taken animation
            if (hp._RuntimeValue != 0)
            {
                StartCoroutine(Invulnerability());
            }
        }
    }

    IEnumerator Invulnerability()
    {
        _isInvulnerable = true;
        yield return new WaitForSeconds(1f);
        _isInvulnerable = false;
    }

    /// <summary>
    /// Changes player animation state
    /// </summary>
    /// <param name="state"> animation to play</param>
    void ChangeState(int state)
    {
        //if (_currentAnimationState == state)
          //  return;

        switch (state)
        {
            case STATE_IDLE:
                _animator.SetInteger("stateDIR", 0);//idle
                break;

            case STATE_WALK:
                _animator.SetInteger("stateDIR", 1);//walk 
                break;

            case STATE_ATTACK:
                _animator.SetTrigger("AttackRight");//shoot right
                break;

            case STATE_TAKE_DAMAGE:
                _animator.SetTrigger("TakeDamage");
                break;
        }

        _currentAnimationState = state;
    }
}
