using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geometry: MonoBehaviour 
{
    public enum Geometry_Type
    {
        circle = 0,
        triangle = 1,
        square = 2,
        diamond = 3,
        rectangle = 4,
        pentagon = 5,
        hexagon = 6,
        star = 7
    }

    public Geometry_Type _geometryType;
    public List<AudioClip> _geometryAudio;

    /// <summary>
    /// Returns the geometry name in spanish
    /// </summary>
    /// <returns>Gemometry name</returns>
    public string getGeometryString()
    {
        switch (_geometryType)
        {
            case Geometry_Type.circle:
                return "círculo";

            case Geometry_Type.triangle:
                return "triángulo";

            case Geometry_Type.square:
                return "cuadrado";

            case Geometry_Type.diamond:
                return "diamante";

            case Geometry_Type.rectangle:
                return "rectángulo";

            case Geometry_Type.pentagon:
                return "pentágono";

            case Geometry_Type.hexagon:
                return "hexágono";

            case Geometry_Type.star:
                return "estrella";

            default:
                return "error";
        }
    }

    /// <summary>
    /// Returns the geometry name in spanish
    /// by the geometry given
    /// </summary>
    /// <param name="geometry">Given geometry</param>
    /// <returns>Gemometry name</returns>
    public string getGeometryString(Geometry_Type geometry)
    {
        switch (geometry)
        {
            case Geometry_Type.circle:
                return "círculo";

            case Geometry_Type.triangle:
                return "triángulo";

            case Geometry_Type.square:
                return "cuadrado";

            case Geometry_Type.diamond:
                return "diamante";

            case Geometry_Type.rectangle:
                return "rectángulo";

            case Geometry_Type.pentagon:
                return "pentágono";

            case Geometry_Type.hexagon:
                return "hexágono";

            case Geometry_Type.star:
                return "estrella";

            default:
                return "error";
        }
    }

    public void GenerateSound()
    {
        switch (_geometryType)
        {
            case Geometry_Type.circle:
                break;
            case Geometry_Type.triangle:
                break;
            case Geometry_Type.square:
                break;
            case Geometry_Type.diamond:
                break;
            case Geometry_Type.rectangle:
                break;
            case Geometry_Type.pentagon:
                break;
            case Geometry_Type.hexagon:
                break;
            case Geometry_Type.star:
                break;
            default:
                break;
        }
    }
}
