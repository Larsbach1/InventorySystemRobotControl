using System;

namespace InventorySystemRobotControl;

public static class UrScript
{
    private static string Pose(double x, double y, double z, double rx, double ry, double rz)
        => $"p[{x:0.###}, {y:0.###}, {z:0.###}, {rx:0.###}, {ry:0.###}, {rz:0.###}]";

    // Genererer et simpelt pick & place-program fra en kilde til en destination
    public static string MakePickPlace(double sx, double sy, double sz,
        double tx, double ty, double tz)
    {
        const double approach = 0.05; // 5 cm over emnet
        double rx = 0, ry = Math.PI, rz = 0;

        var pAboveSource = Pose(sx, sy, sz + approach, rx, ry, rz);
        var pAtSource    = Pose(sx, sy, sz,            rx, ry, rz);
        var pAboveTarget = Pose(tx, ty, tz + approach, rx, ry, rz);
        var pAtTarget    = Pose(tx, ty, tz,            rx, ry, rz);

        return
            $@"def f():
              movej(get_inverse_kin({pAboveSource}))
              movej(get_inverse_kin({pAtSource}))
              # (grip close - antaget automatisk)
              movej(get_inverse_kin({pAboveSource}))

              movej(get_inverse_kin({pAboveTarget}))
              movej(get_inverse_kin({pAtTarget}))
              # (grip open - antaget automatisk)
              movej(get_inverse_kin({pAboveTarget}))
            end
            f()
            ";
    }
}