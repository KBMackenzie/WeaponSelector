namespace WeaponSelector.Utils;

public static class MathUtils
{
    /**
     * Clamp a number by 'wrapping':
     * - If value < min, max is returned.
     * - If value > max, min is returned. 
     * Similar to the overflow/underflow behavior of unsigned integers in C. */
    public static int WrapAround(int value, int min, int max)
    {
        if (value < min) return max;
        if (value > max) return min;
        return value;
    }
}
