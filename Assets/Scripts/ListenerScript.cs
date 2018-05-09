using System.Collections;
using UnityEngine;

public class ListenerScript : MonoBehaviour
{
  void EnterFlag()
  {
    base.gameObject.BroadcastMessage("ColliderEnter");
  }

  void ExitFlag()
  {
    base.gameObject.BroadcastMessage("ColliderExit");
  }

  void WithinFlag()
  {
    base.gameObject.BroadcastMessage("ColliderWithin");
  }

  void PulseFlag()
  {
    base.gameObject.BroadcastMessage("PulseReceived");
  }

  void SwitchFlag()
  {
    base.gameObject.BroadcastMessage("SwitchReceived");
  }
}
