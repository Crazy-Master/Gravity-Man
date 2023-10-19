using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test
{
    public Test(IUpdatable updatable)
    {
        updatable.OnUpdate += Up;
        Debug.Log("test");
    }

    public void Up()
    {
        
    }
}
