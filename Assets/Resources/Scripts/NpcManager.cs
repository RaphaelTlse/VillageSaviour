using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public Animator animator;


    private float _speed = 3.0f;
    public bool _isInDialogue = false;
    private float _animationTimeRemining = 0.0f;
    public int _horizontalMovement = 0;
    public int _verticalMovement = 0;
    private int _rnd = 0;
    private bool _isLookingForNextKill = false;
    private float _radius = 15.0f;
    private float _nextMurder = 30.0f;
    public bool _isMurderer = false;
    public bool _isDialogIrrelevant = false;
    public bool _isDialogHonest = false;
    public bool _isDialogLie = false;
    public bool IsMurderer
    {
        set
        {
            _isMurderer = value;
        }
    }
    Transform[] array;

    public void turnToward(Vector3 pos)
    {
        if (Math.Abs(Math.Abs(pos.x) - Math.Abs(transform.position.x)) > Math.Abs(Math.Abs(pos.y) - Math.Abs(transform.position.y)))
        {
            if (pos.x < transform.position.x)
                setState(STATE.IDLE_WEST);
            else
                setState(STATE.IDLE_EAST);
        }
        else
        {
            if (pos.y < transform.position.y)
                setState(STATE.IDLE_SOUTH);
            else
                setState(STATE.IDLE_NORTH);
        }
    }

    private void dropBlood(Vector3 position)
    {
        _rnd = UnityEngine.Random.Range(1, 5);
        if (_rnd == 1)
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Blood1"), position, Quaternion.identity);
        else if (_rnd == 2)
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Blood2"), position, Quaternion.identity);
        else if (_rnd == 3)
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Blood3"), position, Quaternion.identity);
        else
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Blood4"), position, Quaternion.identity);
    }

    void GetVictimInRadius(Vector3 center, float radius)
    {
        bool found = false;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        int i = 0;
        while (i < hitColliders.Length && found == false)
        {
            if (hitColliders[i].gameObject.tag == "Npc" && hitColliders[i].gameObject.name != this.name && hitColliders[i].gameObject.GetComponent<NpcManager>().state != STATE.DEAD && hitColliders[i].GetComponent<Renderer>().isVisible == false && this.GetComponent<Renderer>().isVisible == false)
            {
                Debug.Log("Trouvé : " + hitColliders[i].gameObject.name);
                found = true;
                _isLookingForNextKill = false;
                _radius += 5;
                this.transform.position = hitColliders[i].gameObject.transform.position;
                hitColliders[i].gameObject.GetComponent<NpcManager>().setState(STATE.DEAD);
                hitColliders[i].gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                dropBlood(hitColliders[i].gameObject.transform.position);
            }
            i++;
        }
    }

    public void setMurderer()
    {
        this._isMurderer = true;
        Debug.Log(this.name + " is the murderer.");
    }

    public enum STATE
    {
        MOVE_NORTH,
        MOVE_SOUTH,
        MOVE_EAST,
        MOVE_WEST,
        IDLE_NORTH,
        IDLE_SOUTH,
        IDLE_EAST,
        IDLE_WEST,
        DEAD
    }

    public STATE state = STATE.IDLE_SOUTH;

    public void setState(STATE state)
    {
        this.state = state;
        if (state == STATE.IDLE_NORTH && animator.GetInteger("STATE") != 0)
            animator.SetInteger("STATE", 0);
        else if (state == STATE.IDLE_SOUTH && animator.GetInteger("STATE") != 1)
            animator.SetInteger("STATE", 1);
        else if (state == STATE.IDLE_EAST && animator.GetInteger("STATE") != 2)
            animator.SetInteger("STATE", 2);
        else if (state == STATE.IDLE_WEST && animator.GetInteger("STATE") != 3)
            animator.SetInteger("STATE", 3);
        else if (state == STATE.MOVE_NORTH && animator.GetInteger("STATE") != 4)
            animator.SetInteger("STATE", 4);
        else if (state == STATE.MOVE_SOUTH && animator.GetInteger("STATE") != 5)
            animator.SetInteger("STATE", 5);
        else if (state == STATE.MOVE_EAST && animator.GetInteger("STATE") != 6)
            animator.SetInteger("STATE", 6);
        else if (state == STATE.MOVE_WEST && animator.GetInteger("STATE") != 7)
            animator.SetInteger("STATE", 7);
        else if (state == STATE.DEAD && animator.GetInteger("STATE") != 8 && animator.GetInteger("STATE") != 9)
        {
            _rnd = UnityEngine.Random.Range(1, 3);
            if (_rnd == 1)
                animator.SetInteger("STATE", 8);
            else
                animator.SetInteger("STATE", 9);
        }
    }

    private void transitionToIdle()
    {
        if (state == STATE.MOVE_NORTH)
            setState(STATE.IDLE_NORTH);
        else if (state == STATE.MOVE_SOUTH)
            setState(STATE.IDLE_SOUTH);
        else if (state == STATE.MOVE_EAST)
            setState(STATE.IDLE_EAST);
        else if (state == STATE.MOVE_WEST)
            setState(STATE.IDLE_WEST);
    }

    private void goNorth()
    {
        setState(STATE.MOVE_NORTH);
        _horizontalMovement = 0;
        _verticalMovement = 1;
    }

    private void goSouth()
    {
        setState(STATE.MOVE_SOUTH);
        _horizontalMovement = 0;
        _verticalMovement = -1;
    }

    private void goEast()
    {
        setState(STATE.MOVE_EAST);
        _horizontalMovement = 1;
        _verticalMovement = 0;
    }

    private void goWest()
    {
        setState(STATE.MOVE_WEST);
        _horizontalMovement = -1;
        _verticalMovement = 0;
    }

    void changeRole()
    {
        if (_isDialogIrrelevant == true && _isMurderer == false)
        {
            _rnd = UnityEngine.Random.Range(1, 6);
            if (_rnd == 1)
            {
                _isDialogIrrelevant = false;
                _isDialogLie = true;
            }
            else if (_rnd == 2 || _rnd == 3 )
            {
                _isDialogIrrelevant = false;
                _isDialogHonest = true;
            }
        }
    }

    private void Start()
    {
        InvokeRepeating("changeRole", 60.0f, 60.0f);
    }

    void Update()
    {
        if (state != STATE.DEAD && _isInDialogue == false)
        {
            Vector3 Movement = new Vector3(_horizontalMovement, _verticalMovement, 0);
            _animationTimeRemining -= Time.deltaTime;

            if (_isMurderer == true && _isLookingForNextKill == true)
                GetVictimInRadius(this.transform.position, _radius);
            if (_isMurderer == true && _isLookingForNextKill == false && Time.time > _nextMurder)
            {
                Debug.Log("looking for next kill, Time : " + Time.time + ", cap : " + _nextMurder);
                _nextMurder = Time.time + UnityEngine.Random.Range(15.0f, 45.0f);
                Debug.Log("Next murder : " + _nextMurder);
                _isLookingForNextKill = true;
            }

            if (_animationTimeRemining <= 0)
            {
                if (state == STATE.IDLE_NORTH || state == STATE.IDLE_SOUTH || state == STATE.IDLE_EAST || state == STATE.IDLE_WEST)
                {

                    _rnd = UnityEngine.Random.Range(1, 5);
                    if (_rnd == 1)
                        goNorth();
                    else if (_rnd == 2)
                        goSouth();
                    else if (_rnd == 3)
                        goWest();
                    else
                        goEast();
                }
                else
                {
                    transitionToIdle();
                    _horizontalMovement = 0;
                    _verticalMovement = 0;
                }
                _animationTimeRemining = UnityEngine.Random.Range(0.1f, 7.5f);
            }

            if (Movement.x != 0 || Movement.y != 0)
                transform.position += Movement * _speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == STATE.MOVE_NORTH)
            goSouth();
        else if (state == STATE.MOVE_SOUTH)
            goNorth();
        else if (state == STATE.MOVE_EAST)
            goWest();
        else if (state == STATE.MOVE_WEST)
            goEast();
    }
}
