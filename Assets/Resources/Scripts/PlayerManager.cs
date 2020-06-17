using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    [SerializeField]
    AIPath _aipath;

    private Vector2 worldPoint;
    private RaycastHit2D hit;
    public GameObject ClickPoint;
    public Animator animator;
    public String CurrentInterlocutor;
    public UnityEngine.Experimental.Rendering.LWRP.Light2D sceneLight;
    public Camera mainCamera;

    private float _speed = 4.5f;
    private bool _isInDialogue = false;
    private bool _gameEnded = false;
    private int _remainingLives = 14;
    private Vector3 Movement;

    private enum STATE
    {
        MOVE_NORTH,
        MOVE_SOUTH,
        MOVE_EAST,
        MOVE_WEST,
        IDLE_NORTH,
        IDLE_SOUTH,
        IDLE_EAST,
        IDLE_WEST
    }

    private STATE state = STATE.IDLE_SOUTH;

    private void setState(STATE state)
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
    }

    private void turnToward(Vector3 pos)
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

    private void tryInitiateTalk()
    {
        bool found = false;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1);
        int i = 0;
        while (i < hitColliders.Length && found == false)
        {
            //LogsManager.instance.NewText.text += " // " + hitColliders[i].gameObject.name;
            //LogsManager.instance.NewText.text += " ce qui en fait un " + hitColliders[i].gameObject.tag + " // ";
            if (hitColliders[i].gameObject.tag == "Npc" && hitColliders[i].gameObject.GetComponent<NpcManager>().state != NpcManager.STATE.DEAD)
            {
                found = true;
                _isInDialogue = true;
                CurrentInterlocutor = hitColliders[i].gameObject.name;
                Debug.Log("Parle avec : " + hitColliders[i].gameObject.name);
                hitColliders[i].gameObject.GetComponent<NpcManager>()._isInDialogue = true;
                hitColliders[i].gameObject.GetComponent<NpcManager>()._horizontalMovement = 0;
                hitColliders[i].gameObject.GetComponent<NpcManager>()._verticalMovement = 0;
                turnToward(hitColliders[i].gameObject.transform.position);
                hitColliders[i].gameObject.GetComponent<NpcManager>().turnToward(this.transform.position);
                hitColliders[i].gameObject.GetComponent<DialogManager>().Talk();
                Debug.Log("Is the murderer : " + hitColliders[i].gameObject.GetComponent<NpcManager>()._isMurderer + ", is honnest : " + hitColliders[i].gameObject.GetComponent<NpcManager>()._isDialogHonest + ", is lier : " + hitColliders[i].gameObject.GetComponent<NpcManager>()._isDialogLie + ", is irrelevant : " + hitColliders[i].gameObject.GetComponent<NpcManager>()._isDialogIrrelevant);
            }
            i++;
        }
    }

    private void setNewMoveState(Vector3 pos)
    {
       if (Math.Abs(Math.Abs(pos.x) - Math.Abs(transform.position.x)) > Math.Abs(Math.Abs(pos.y) - Math.Abs(transform.position.y)))
        {
            if (pos.x < transform.position.x)
                setState(STATE.MOVE_WEST);
            else
                setState(STATE.MOVE_EAST);
        }
        else
        {
            if (pos.y < transform.position.y)
                setState(STATE.MOVE_SOUTH);
            else
                setState(STATE.MOVE_NORTH);
        }
    }

    private bool isDistanceRelevant(Vector3 pos, float max)
    {
        if ((transform.position.x > pos.x - max) && (transform.position.x < pos.x + max) && (transform.position.y > pos.y - max) && (transform.position.y < pos.y + max))
            return (false);
        return (true);
    }

    private bool isMovementRelevant(Vector3 mov, float max)
    {
        if ((Math.Abs(mov.x) - max <= 0) && (Math.Abs(mov.y) - max <= 0))
            return (false);
        return (true);
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

    void getAlive()
    {
        if (_gameEnded == false)
        {
            int cpt = 0;

            if (GameObject.Find("Npc1(Clone)").GetComponent<NpcManager>()._isMurderer == false && GameObject.Find("Npc1(Clone)").GetComponent<NpcManager>().state != NpcManager.STATE.DEAD)
                cpt++;
            if (GameObject.Find("Npc2(Clone)").GetComponent<NpcManager>()._isMurderer == false && GameObject.Find("Npc2(Clone)").GetComponent<NpcManager>().state != NpcManager.STATE.DEAD)
                cpt++;
            if (GameObject.Find("Npc3(Clone)").GetComponent<NpcManager>()._isMurderer == false && GameObject.Find("Npc3(Clone)").GetComponent<NpcManager>().state != NpcManager.STATE.DEAD)
                cpt++;
            if (GameObject.Find("Npc4(Clone)").GetComponent<NpcManager>()._isMurderer == false && GameObject.Find("Npc4(Clone)").GetComponent<NpcManager>().state != NpcManager.STATE.DEAD)
                cpt++;
            if (GameObject.Find("Npc5(Clone)").GetComponent<NpcManager>()._isMurderer == false && GameObject.Find("Npc5(Clone)").GetComponent<NpcManager>().state != NpcManager.STATE.DEAD)
                cpt++;
            if (GameObject.Find("Npc6(Clone)").GetComponent<NpcManager>()._isMurderer == false && GameObject.Find("Npc6(Clone)").GetComponent<NpcManager>().state != NpcManager.STATE.DEAD)
                cpt++;
            if (GameObject.Find("Npc7(Clone)").GetComponent<NpcManager>()._isMurderer == false && GameObject.Find("Npc7(Clone)").GetComponent<NpcManager>().state != NpcManager.STATE.DEAD)
                cpt++;
            if (GameObject.Find("Npc8(Clone)").GetComponent<NpcManager>()._isMurderer == false && GameObject.Find("Npc8(Clone)").GetComponent<NpcManager>().state != NpcManager.STATE.DEAD)
                cpt++;
            if (GameObject.Find("Npc9(Clone)").GetComponent<NpcManager>()._isMurderer == false && GameObject.Find("Npc9(Clone)").GetComponent<NpcManager>().state != NpcManager.STATE.DEAD)
                cpt++;
            if (GameObject.Find("Npc10(Clone)").GetComponent<NpcManager>()._isMurderer == false && GameObject.Find("Npc10(Clone)").GetComponent<NpcManager>().state != NpcManager.STATE.DEAD)
                cpt++;
            if (GameObject.Find("Npc11(Clone)").GetComponent<NpcManager>()._isMurderer == false && GameObject.Find("Npc11(Clone)").GetComponent<NpcManager>().state != NpcManager.STATE.DEAD)
                cpt++;
            if (GameObject.Find("Npc12(Clone)").GetComponent<NpcManager>()._isMurderer == false && GameObject.Find("Npc12(Clone)").GetComponent<NpcManager>().state != NpcManager.STATE.DEAD)
                cpt++;
            if (GameObject.Find("Npc13(Clone)").GetComponent<NpcManager>()._isMurderer == false && GameObject.Find("Npc13(Clone)").GetComponent<NpcManager>().state != NpcManager.STATE.DEAD)
                cpt++;
            if (GameObject.Find("Npc14(Clone)").GetComponent<NpcManager>()._isMurderer == false && GameObject.Find("Npc14(Clone)").GetComponent<NpcManager>().state != NpcManager.STATE.DEAD)
                cpt++;
            if (GameObject.Find("Npc15(Clone)").GetComponent<NpcManager>()._isMurderer == false && GameObject.Find("Npc15(Clone)").GetComponent<NpcManager>().state != NpcManager.STATE.DEAD)
                cpt++;

            _remainingLives = cpt;
        }
    }

    public void displayMurderer()
    {
        if (GameObject.Find("Npc1(Clone)").GetComponent<NpcManager>()._isMurderer == true)
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Npc1End"), this.transform.position, Quaternion.identity);
        else if (GameObject.Find("Npc2(Clone)").GetComponent<NpcManager>()._isMurderer == true)
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Npc2End"), this.transform.position, Quaternion.identity);
        else if (GameObject.Find("Npc3(Clone)").GetComponent<NpcManager>()._isMurderer == true)
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Npc3End"), this.transform.position, Quaternion.identity);
        else if (GameObject.Find("Npc4(Clone)").GetComponent<NpcManager>()._isMurderer == true)
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Npc4End"), this.transform.position, Quaternion.identity);
        else if (GameObject.Find("Npc5(Clone)").GetComponent<NpcManager>()._isMurderer == true)
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Npc5End"), this.transform.position, Quaternion.identity);
        else if (GameObject.Find("Npc6(Clone)").GetComponent<NpcManager>()._isMurderer == true)
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Npc6End"), this.transform.position, Quaternion.identity);
        else if (GameObject.Find("Npc7(Clone)").GetComponent<NpcManager>()._isMurderer == true)
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Npc7End"), this.transform.position, Quaternion.identity);
        else if (GameObject.Find("Npc8(Clone)").GetComponent<NpcManager>()._isMurderer == true)
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Npc8End"), this.transform.position, Quaternion.identity);
        else if (GameObject.Find("Npc9(Clone)").GetComponent<NpcManager>()._isMurderer == true)
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Npc9End"), this.transform.position, Quaternion.identity);
        else if (GameObject.Find("Npc10(Clone)").GetComponent<NpcManager>()._isMurderer == true)
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Npc10End"), this.transform.position, Quaternion.identity);
        else if (GameObject.Find("Npc11(Clone)").GetComponent<NpcManager>()._isMurderer == true)
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Npc11End"), this.transform.position, Quaternion.identity);
        else if (GameObject.Find("Npc12(Clone)").GetComponent<NpcManager>()._isMurderer == true)
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Npc12End"), this.transform.position, Quaternion.identity);
        else if (GameObject.Find("Npc13(Clone)").GetComponent<NpcManager>()._isMurderer == true)
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Npc13End"), this.transform.position, Quaternion.identity);
        else if (GameObject.Find("Npc14(Clone)").GetComponent<NpcManager>()._isMurderer == true)
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Npc14End"), this.transform.position, Quaternion.identity);
        else
            GameObject.Instantiate(Resources.Load<GameObject>("Utilities/Npc15End"), this.transform.position, Quaternion.identity);
    }

    public void endScreen(string result)
    {
        if (result == "Game over")
        {
            mainCamera.GetComponent<AudioSource>().pitch = 0.15f;
            sceneLight.intensity = 0.15f;
            if (_remainingLives > 0)
                result += ", you arrested the wrong person...";
            else
                result += ", everyone is dead...";
            _remainingLives = 0;
            displayMurderer();
        }
        else
        {
            mainCamera.GetComponent<AudioSource>().pitch = 1.0f;
            sceneLight.intensity = 1.0f;
            getAlive();
        }

        UIManager.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        UIEndScreenManager.instance.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        _gameEnded = true;


        UIEndScreenManager.instance.Result.text = result;
        if (result == "Perfect !")
            UIEndScreenManager.instance.SavedLives.text = "You saved everyone, congratulations !";
        else
        {
            UIEndScreenManager.instance.SavedLives.text = "You saved " + _remainingLives + " lives.";
            UIEndScreenManager.instance.WastedLives.text = "You wasted " + (14 - _remainingLives) + " lives.";
        }
    }

    public void arrest()
    {
        if (_isInDialogue == true)
        {
            if (GameObject.Find(CurrentInterlocutor).GetComponent<NpcManager>()._isMurderer == true)
            {
                getAlive();
                if (_remainingLives == 14)
                    endScreen("Perfect !");
                else
                    endScreen("Success !");
            }
            else
                endScreen("Game over");
        }
    }

    public void dismissDialog()
    {
        _isInDialogue = false;
        UIManager.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        GameObject.Find(CurrentInterlocutor).GetComponent<NpcManager>()._isInDialogue = false;
    }

    private void makeDarker()
    {
        if (_gameEnded == false)
        {
            if (mainCamera.GetComponent<AudioSource>().pitch > 0.15f)
                mainCamera.GetComponent<AudioSource>().pitch -= 0.0005f;
            if (sceneLight.intensity > 0.15f)
                sceneLight.intensity -= 0.0005f;
        }
    }

    private void Start()
    {
        InvokeRepeating("getAlive", 0.0f, 5.0f);
        InvokeRepeating("makeDarker", 30.0f, 0.1f);
    }

    void Update()
    {


        if (_isInDialogue == false && _gameEnded == false)
        {
            if (_remainingLives == 0)
                endScreen("Game over");
            if (hit.collider != null && state != STATE.IDLE_NORTH && state != STATE.IDLE_SOUTH && state != STATE.IDLE_EAST && state != STATE.IDLE_WEST)
            {
                if (isDistanceRelevant(hit.point, 0.25f) == false)
                    transitionToIdle();
            }

            Movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

            if (Movement.x != 0 || Movement.y != 0)
            {
                _aipath.maxSpeed = 0.0f;
                ClickPoint.SetActive(false);
                if (Math.Abs(Movement.x) >= Math.Abs(Movement.y))
                {
                    if (Movement.x < 0)
                        setState(STATE.MOVE_WEST);
                    else if (Movement.x > 0)
                        setState(STATE.MOVE_EAST);
                }
                else
                {
                    if (Movement.y < 0)
                        setState(STATE.MOVE_SOUTH);
                    else if (Movement.y > 0)
                        setState(STATE.MOVE_NORTH);
                }
                transform.position += Movement * _speed * Time.deltaTime;
            }

            if (Input.GetKeyDown("space") || Input.GetKeyDown("enter") || Input.GetKeyDown("joystick button 0") || Input.GetMouseButtonDown(1))
            {
                tryInitiateTalk();
            }

            if (Input.GetMouseButtonDown(0))
            {
                _aipath.maxSpeed = 5.0f;
                ClickPoint.SetActive(true);
                worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                ClickPoint.transform.position = hit.point;
            }

            if (ClickPoint.activeSelf == true && isDistanceRelevant(_aipath.steeringTarget, 0.3f) == true)
                setNewMoveState(_aipath.steeringTarget);
            else if (ClickPoint.activeSelf == false && state != STATE.IDLE_NORTH && state != STATE.IDLE_SOUTH && state != STATE.IDLE_EAST && state != STATE.IDLE_WEST && isMovementRelevant(Movement, 0.01f) == false)
                transitionToIdle();
        }
        else if (Input.GetKeyDown("space") || Input.GetKeyDown("enter") || Input.GetKeyDown("b") || Input.GetKeyDown("joystick button 1"))
            dismissDialog();
        else if ((Input.GetKeyDown("joystick button 2") || Input.GetKeyDown("x")) && _gameEnded == false)
            arrest();
        if (Input.GetKeyDown("joystick button 3") || Input.GetKeyDown("escape"))
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger detection : " + col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detection : " + collision.gameObject.name + " : " + gameObject.name + " : " + Time.time);
    }
}
