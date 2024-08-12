

namespace Integra7AuralAlchemist.Models.Services;

public class Mapping
{
    public static double clip(double value, double min, double max)
    {
        if (min > max)
        {
            (min, max) = (max, min);
        }
        if (value < min)
        {
            return min;
        }
        if (value > max)
        {
            return max;
        }
        return value;
    }

    public static double linlin(double value, double imin, double imax, double omin, double omax, bool autoclip = false)
    {
        if (imin == imax)
        {
            if (value == imin && omin == omax)
            {
                return omin;
            }
            return 0; // or throw exception?
        }

        double output = ((omin + omax) + (omax - omin) * ((2 * value - (imin + imax)) / (imax - imin))) / 2.0;

        if (autoclip)
        {
            output = Mapping.clip(output, omin, omax);
        }

        return output;
    }

}