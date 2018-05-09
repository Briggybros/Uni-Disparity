using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBehaviour : Receiver
{
  public GameObject Anchor1, Anchor2;
  public int x, y, z;

  private bool Move, Switch;
  private int Forward, Stage;
  private Vector3 Target;

  protected override void Start()
  {
    Move = false;
    Forward = -1;
    Stage = 1;
  }

  protected virtual float BoolToFloat(bool Input)
  {
    if (Input)
    {
      return 1f;
    }
    else
    {
      return 0f;
    }
  }

  protected override void Update()
  {
    if (Move)
    {
      if (Forward == 1)
      {
        Target = Anchor2.transform.position;
      }
      else
      {
        Target = Anchor1.transform.position;
      }
      switch (Stage)
      {
        case 0:
          Stage += 1;
          if (Switch)
          {
            ToggleForward();
          }
          else
          {
            Move = false;
          }
          break;
        case 1:
          Target = Target - transform.position;
          Target.x *= BoolToFloat(x == 1);
          Target.y *= BoolToFloat(y == 1);
          Target.z *= BoolToFloat(z == 1);
          Target = Target + transform.position;

          transform.position = Vector3.MoveTowards(transform.position, Target, 4 * Time.deltaTime);
          if (transform.position == Target) { Stage += Forward; }
          break;
        case 2:
          Target = Target - transform.position;
          Target.x *= BoolToFloat(x == 2);
          Target.y *= BoolToFloat(y == 2);
          Target.z *= BoolToFloat(z == 2);
          Target = Target + transform.position;

          transform.position = Vector3.MoveTowards(transform.position, Target, 4 * Time.deltaTime);
          if (transform.position == Target) { Stage += Forward; }
          break;
        case 3:
          Target = Target - transform.position;
          Target.x *= BoolToFloat(x == 3);
          Target.y *= BoolToFloat(y == 3);
          Target.z *= BoolToFloat(z == 3);
          Target = Target + transform.position;

          transform.position = Vector3.MoveTowards(transform.position, Target, 4 * Time.deltaTime);
          if (transform.position == Target) { Stage += Forward; }
          break;
        case 4:
          Stage -= 1;
          if (Switch)
          {
            ToggleForward();
          }
          else
          {
            Move = false;
          }
          break;
      }
    }
  }

  protected virtual void ToggleForward()
  {
    if (Forward == 1)
    {
      Forward = -1;
    }
    else
    {
      Forward = 1;
    }
  }

  protected override void ColliderEnter()
  {
    ToggleForward();
    if (!Move) { Move = true; }
  }

  protected override void PulseReceived()
  {
    ToggleForward();
    if (!Move) { Move = true; }
  }

  protected override void SwitchReceived()
  {
    ToggleForward();
    if (!Move) { Move = true; }
    Switch = !Switch;
  }
}
