using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AForge;
using AForge.Fuzzy;
using System;
using UnityEngine.AI;

public class FuzzyBot : MonoBehaviour
{
    FuzzySet Near, Med, Far;
    LinguisticVariable lv_Distance;
    NavMeshAgent agent;
    public float speed, distance;
    public Transform player;
    Database db;
    FuzzySet Slow, Medium, Fast;
    LinguisticVariable lvSpeed;
    InferenceSystem ISys;

    void Start()
    {
        Initializate();
    }

    private void Initializate()
    {
        SetDistanceFuzzySets();
        SetSpeedFuzzySets();
        AddRulesTODataBase();
    }

    private void SetDistanceFuzzySets()
    {
        Near = new FuzzySet("Near", new TrapezoidalFunction(5, 6, TrapezoidalFunction.EdgeType.Right));
        Med = new FuzzySet("Med", new TrapezoidalFunction(6, 7, 8, 10));
        Far = new FuzzySet("Far", new TrapezoidalFunction(13, 50, TrapezoidalFunction.EdgeType.Left));

        lv_Distance = new LinguisticVariable("Distance", 0, 50);
        lv_Distance.AddLabel(Near);
        lv_Distance.AddLabel(Med);
        lv_Distance.AddLabel(Far);

    }

    private void SetSpeedFuzzySets()
    {
        Slow = new FuzzySet("Slow", new TrapezoidalFunction(4, 5, TrapezoidalFunction.EdgeType.Right));
        Medium = new FuzzySet("Medium", new TrapezoidalFunction(4, 5, 8, 10));
        Fast = new FuzzySet("Fast", new TrapezoidalFunction(8, 10, TrapezoidalFunction.EdgeType.Left));
        lvSpeed = new LinguisticVariable("Speed", 0, 10);

        lvSpeed.AddLabel(Slow);
        lvSpeed.AddLabel(Medium);
        lvSpeed.AddLabel(Fast);
    }


    private void AddRulesTODataBase()
    {
        db = new Database();
        db.AddVariable(lv_Distance);
        db.AddVariable(lvSpeed);
        SetRules();
    }

    private void SetRules()
    {
        ISys = new InferenceSystem(db, new CentroidDefuzzifier(120));
        ISys.NewRule("Rule 1", "IF Distance IS Near THEN Speed IS Slow");
        ISys.NewRule("Rule 2", "IF Distance IS Med THEN Speed IS Medium");
        ISys.NewRule("Rule 3", "IF Distance IS Far THEN Speed IS Fast");
    }


    void Update()
    {
        Evalueate();
    }

    private void Evalueate()
    {
        distance = Vector3.Distance(player.position, transform.position);
        ISys.SetInput("Distance", distance);
        speed = ISys.Evaluate("Speed");

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        Debug.Log(speed);
    }
}
