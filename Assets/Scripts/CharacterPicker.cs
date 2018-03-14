using UnityEngine;

public class CharacterPicker : MonoBehaviour {
    public const char CAT = 'A';
    public const char DOG = 'B';
    public const char SPECTATORCAT = 'C';
    public const char SPECATORDOG = 'D';
    private static char World;

    public static void SetWorld (char world) {
        World = world;
    }

    public static char GetWorld () {
        return World;
    }

    // Return the opposite, normal world value (not spectator world)
    public static char GetOtherWorld() {
        if (World == SPECATORDOG || World == DOG)
            return CAT;
        else return DOG;
    } 

    public static bool IsSpectator (char world = ' ') {
        if (world == ' ') world = World;
        if (world == SPECTATORCAT || world == SPECATORDOG)
            return true;
        return false;
    }

    // Changes the world the Spectator sees
    public static void ChangeSpectatorFocus (){
        if (World == SPECTATORCAT){
            World = SPECATORDOG;
        }
        if (World == SPECATORDOG){
            World = SPECTATORCAT;
        }
    }
}
