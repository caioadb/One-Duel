using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //player states
    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
    const int STATE_ATTACK = 2;
    const int STATE_TAKE_DAMAGE = 3;

    public string _currentDirection = "left";
    public static int _currentAnimationState = STATE_IDLE;
    public bool _isInvulnerable;
    bool isMoving;
    bool canShoot;

    [Range(0, 10f)]
    public float _moveSpeed = 1f;

    Transform _enemy;
    Animator _animator;

    [SerializeField]
    GameObject bullet = null;
    [SerializeField]
    Transform player = null;

    [SerializeField]
    HPValue hp = null;

    [SerializeField]
    AudioSource shootSource = null;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        hp._RuntimeValue = 3;
        _enemy = this.transform;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool shoot = Input.GetKeyDown("k") || Input.GetKeyDown("space") || Input.GetKeyDown("j");

        if (!isMoving)
        {
            StartCoroutine(MovePlayer());
        }

        if (shoot)
        {
            Shoot();
        }
    }

    IEnumerator Shoot()
    {
        ChangeState(STATE_ATTACK);
        //spawn bullet
        Instantiate(bullet, this.transform.position + new Vector3(-0.2804f, -0.095f), Quaternion.Euler(0, 0, 180));   // shoot left
        shootSource.Play();
        yield return new WaitForSeconds(1.5f);
        canShoot = true;

    }

    IEnumerator MovePlayer()
    {
        isMoving = true;
        Vector2 _objPos = player.position;
        if (_objPos.y > _enemy.position.y + 0.1f || _objPos.y < _enemy.position.y - 0.1f)
        {
            _objPos.x = _enemy.position.x;        
            _enemy.position = Vector2.MoveTowards(_enemy.position, _objPos, Time.deltaTime);
            yield return new WaitForSeconds(0.0025f);
            ChangeState(STATE_WALK);
        }
        if (_objPos.y < _enemy.position.y + 0.1f && _objPos.y > _enemy.position.y - 0.1f)
        {
            if (canShoot)
            {
                canShoot = false;
                StartCoroutine(Shoot());
            }

        }
        ChangeState(STATE_IDLE);
        isMoving = false;
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
