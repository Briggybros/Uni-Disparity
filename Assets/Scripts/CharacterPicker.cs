using UnityEngine;

public class CharacterPicker : MonoBehaviour {
    public enum WORLDS {
        CAT = 'A',
        DOG = 'B',
        SPECTATORCAT = 'C',
        SPECATORDOG = 'D',
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
        if (World == WORLDS.SPECATORDOG || World == WORLDS.DOG)
            return WORLDS.CAT;
        else return WORLDS.DOG;
    } 

    public static bool IsSpectator () {
        if (World == WORLDS.SPECTATORCAT || World == WORLDS.SPECATORDOG)
            return true;
        return false;
    }
    
    public static bool IsSpectator (WORLDS world) {
        if (world == WORLDS.SPECTATORCAT || world == WORLDS.SPECATORDOG)
            return true;
        return false;
    }

    // Changes the world the Spectator sees
    public static void ChangeSpectatorFocus (){
        if (World == WORLDS.SPECTATORCAT){
            World = WORLDS.SPECATORDOG;
        }
        else if (World == WORLDS.SPECATORDOG){
            World = WORLDS.SPECTATORCAT;
        }
    }
}
