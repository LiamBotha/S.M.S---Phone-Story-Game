using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected StateMachine controller;

    public State(StateMachine controller)
    {
        this.controller = controller;
    }

    public abstract void Excecute();
}
