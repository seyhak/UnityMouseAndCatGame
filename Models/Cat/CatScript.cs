using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatScript : MonoBehaviour {
    [SerializeField]
    GameObject goal1;
    [SerializeField]
    GameObject goal2;
    [SerializeField]
    GameObject goal3;
    [SerializeField]
    GameObject goal4;
    [SerializeField, Range(0, 4)]
    public int whichGoal=0;
    [SerializeField,Range(0,4)]
    public int menu;//0 - Idle, 1 - Sniff, 2 - Walk, 3 - Chase, 4 - Attack  
    float move;
    [SerializeField]
    int speed;
    [SerializeField]
    bool patrolingEnabled;
    [SerializeField]
    GameObject target;
    public bool attacking;
    public bool chasing;
    public bool idling;
    bool sniffing;
    Vector3 pointOfRotation;
    Animator anim;
    Rigidbody body;
    NavMeshAgent agent;
    IEnumerator coroutine;
    List<GameObject> listOfGoals;
    MusicControl mc;
    // Use this for initialization

    //kot wącha
    //kot zmienia punkt patrolu na bliższy poprzedniego miejsca pobytu myszy --> punkt się przybliża
    void Start () {
        idling = false;
        agent = GetComponent<NavMeshAgent>();
        body = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        coroutine = CatDate();
        listOfGoals = new List<GameObject>();
        listOfGoals.Add(goal1);
        listOfGoals.Add(goal2);
        listOfGoals.Add(goal3);
        listOfGoals.Add(goal4);
        GameObject go = GameObject.FindGameObjectWithTag("Music");
        mc = go.GetComponent<MusicControl>();
        StartCoroutine(coroutine);
    }
	
    void Walk()
    {
        GameObject goal = listOfGoals[whichGoal];
        agent.enabled = true;
        if (patrolingEnabled)
        {
            agent.destination = goal.transform.position;
            anim.SetFloat("Move", body.velocity.magnitude);
        }
    }
    IEnumerator Sniff()
    {
        agent.enabled = false;
        sniffing = true;
        body.velocity.Set(0, 0, 0);
        anim.SetBool("Sniff", true);
        anim.SetFloat("Move", 0f);
        yield return new WaitForSeconds(10);
        anim.SetBool("Sniff", false);
        agent.enabled = true;
        menu = 2;
        yield return null;

    }

    void Attack()
    {
        agent.enabled = false;
        anim.SetBool("Attack", true);
    }
    private IEnumerator Idle()
    {
        menu = 10;
        idling = true;
        body.velocity.Set(0, 0, 0);

        Debug.Log("Idle");
        anim.SetBool("Attack", false);
        anim.SetBool("Sniff", false);
        anim.SetFloat("Move", 0f);
        yield return new WaitForSeconds(10);
        idling = false;
        menu = 2;
    }
    private IEnumerator Rotate()
    {
        float rotationSpeed =0.01f;
        Vector3 relativePos = pointOfRotation - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed);
        if (rotation == new Quaternion(0, 0, 0, 0))
        {
            menu = 0;
            yield return null;
        }
    }
    private IEnumerator CatDate() {
        while (true) {
            switch (menu){
                case 0:
                    StartCoroutine(Idle());
                    break;
                case 1:
                    StartCoroutine(Sniff());
                    yield return new WaitForSeconds(11);
                    break;
                case 2:
                    Walk();
                    break;
                case 3:
                    Chase();
                    break;
                case 4:
                    Attack();
                    break;
                case 5:
                    StartCoroutine(Rotate());
                    break;
                default:
                    break;}
            yield return new WaitForSeconds(0);}}

    private void Chase()
    {
        if (chasing)
        {
            mc.danger = true;
            agent.enabled = true;
            agent.destination = target.transform.position;
            anim.SetFloat("Move", body.velocity.magnitude);
        }
        else
        {
            mc.danger = false;
            menu = 0;

        }
    }

    public void RotateTo(Vector3 point)
    {
        pointOfRotation = point;
        menu = 5;
    }
    //void CreatePatrolPointsRandomly()
    //{
    //    List < GameObject > listOfPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Point"));
    //    goal1 = listOfPoints[Random.Range(0, listOfPoints.Count - 1)];
    //    listOfPoints.Remove(goal1);
    //    goal2 = listOfPoints[Random.Range(0, listOfPoints.Count - 2)];
    //    listOfPoints.Remove(goal2);
    //    goal3 = listOfPoints[Random.Range(0, listOfPoints.Count - 3)];
    //    listOfPoints.Remove(goal3);
    //    goal4 = listOfPoints[Random.Range(0, listOfPoints.Count - 4)];
    //    listOfPoints.Remove(goal4);
    //}
    //public void ChangeTargetRandomly()
    //{
    //    GameObject[] points = GameObject.FindGameObjectsWithTag("Point");
    //    GameObject choosen = points[Random.Range(0, points.Length - 1)];
    //    if (choosen == goal1)
    //    {
    //        ChangeTargetRandomly();
    //    }
    //    else
    //        goal1 = choosen;

    //}
    //enum Menu
    //{
    //    Idle, Sniff, Walk, Chase, Attack
    //};

    void ChangePointAfterSniff()
    {
        float distance = Vector3.Distance(listOfGoals[whichGoal].transform.position,GameObject.FindGameObjectWithTag("Player").transform.position);
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            menu = 4;
            chasing = false;
            attacking = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            menu = 3;
            chasing = true;
            attacking = false;

            anim.SetBool("Attack", false);
        }
    }
}
