using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMark : Rewards
{
    protected override void HandleRewards()
    {
        meshRenderer.material = empty;
    }
}
