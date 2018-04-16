using UnityEngine;

public class CharacterPicker : MonoBehaviour {
    public enum WORLDS {
        CAT = 'A',
        DOG = 'B',
        SPECTATOR = 'C',
    }
    private static WORLDS World;

    public static void SetWorld (WORLDS world) {
        World = world;
    }

    public static WORLDS GetWorld () {
        return World;
    }

    // Return the opposite, normal world value (not spectator world)
    public static WORLDS GetOtherWorld() {
        if (World == WORLDS.SPECTATOR)
            return WORLDS.SPECTATOR;
        else if (World == WORLDS.DOG)
            return WORLDS.CAT;
        else return WORLDS.DOG;
    } 

    public static bool IsSpectator () {
        if (World == WORLDS.SPECTATOR)
            return true;
        return false;
    }
    
    public static bool IsSpectator (WORLDS world) {
        if (world == WORLDS.SPECTATOR)
            return true;
        return false;
    }

    // Changes the world the Spectator sees
    public static void ChangeSpectatorFocus (){
        Debug.LogError("Please don't use this anymore");
    }
}
