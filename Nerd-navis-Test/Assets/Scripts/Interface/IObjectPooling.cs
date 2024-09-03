using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPooling<T>
{
    public event System.Action<T> OnDestroy;
    public void SetPosition(Vector2 Position);
    public void SetAngle(Vector2 Angle);
}
