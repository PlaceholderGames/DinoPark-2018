using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HerdBehaviour : ScriptableObject {

    public abstract Vector2 CalculateMove(HerdAgent agent, List<Transform> context, Herd herd);
}
