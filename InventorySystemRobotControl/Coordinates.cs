namespace InventorySystemRobotControl;

public static class Coordinates
{
    // Base-offset (justér så det passer til din URSim-scene)
    public const double Ox = 0.40; // meter
    public const double Oy = -0.20;
    public const double Oz = 0.10; // højde over bord

    public const double Step = 0.10; // 10 cm mellem felter

    public static (double x,double y,double z) A() => (Ox + 1*Step, Oy + 1*Step, Oz);
    public static (double x,double y,double z) B() => (Ox + 2*Step, Oy + 1*Step, Oz);
    public static (double x,double y,double z) C() => (Ox + 3*Step, Oy + 1*Step, Oz);
    public static (double x,double y,double z) S() => (Ox + 3*Step, Oy + 3*Step, Oz);

    public static (double x,double y,double z) SourceByItemId(uint id) => id switch
    {
        1 => A(),
        2 => B(),
        3 => C(),
        _ => A()
    };
}