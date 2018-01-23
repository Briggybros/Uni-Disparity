using System;
using System.Collections.Generic;

public static class TouchManager {
    private static Dictionary<string, bool> buttons = new Dictionary<string, bool>();

    public static void Update(string label, bool touch) {
        buttons[label] = touch;
    }

    public static bool Test(string label) {
        return buttons[label];
    }
}