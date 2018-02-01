using UnityEngine;

class CharacterPicker : MonoBehaviour {
    private static char World;

    public void SetWorld (char world) {
        World = world;
    }

    public char GetWorld () {
        return World;
    }
}
