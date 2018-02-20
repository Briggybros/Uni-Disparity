using UnityEngine;

class CharacterPicker : MonoBehaviour {
    public const char CAT = 'A';
    public const char DOG = 'B';
    public const char SPECTATOR = 'C';
    private static char World;

    public static void SetWorld (char world) {
        World = world;
    }

    public static char GetWorld () {
        return World;
    }
}
