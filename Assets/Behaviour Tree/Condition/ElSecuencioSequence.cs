using System;
using Unity.Behavior;
using UnityEngine;
using Composite = Unity.Behavior.Composite;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "El Secuencio", story: "Execute and return Majority", category: "Flow", id: "e4f0aa25bd8257ad2304579bb635d6d5")]
public partial class ElSecuencioSequence : Composite
{
    int currentChild = 0;
    float successCount = 0;
    protected override Status OnStart()
    {
        successCount = 0;
        currentChild = 0;

        return StartChild(currentChild);

    }

    protected override Status OnUpdate()
    {
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }

    protected Status StartChild(int childIndex)
    {
        if (childIndex >= Children.Count)
        {
            return successCount >= (float)Children.Count ? Status.Success : Status.Failure;
        }
        var status = StartNode(Children[childIndex]);
        if (status == Status.Success)
        {
            successCount++;
            if (childIndex + 1 >= Children.Count)
            {
                return successCount >= (float)Children.Count / 2 ? Status.Success : Status.Failure;
            }
            return StartChild(childIndex + 1);
        }
        else if (status == Status.Running)
        {
            return Status.Running;
        }
        else
        {
            if (childIndex + 1 >= Children.Count)
            {
                return successCount >= (float)Children.Count / 2 ? Status.Success : Status.Failure;
            }
            return StartChild(childIndex + 1);
        }
    }
}

