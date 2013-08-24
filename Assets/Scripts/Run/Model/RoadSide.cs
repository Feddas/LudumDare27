using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Int driven enum class by JohnLBevan http://stackoverflow.com/questions/943398/get-int-value-from-enum
/// </summary>
public class RoadSide
{
	//attributes
    protected int index;
    //go with a dictionary to enforce unique index
    //protected static readonly ICollection<Question> values = new Collection<Question>();
    protected static readonly IDictionary<int,RoadSide> values = new Dictionary<int,RoadSide>();

	//define the "enum" values
	public static readonly RoadSide SideLeft = new RoadSide(-1);
	public static readonly RoadSide MiddleOfRoad = new RoadSide(0);
	public static readonly RoadSide SideRight = new RoadSide(1);
	
	//constructors
	public RoadSide(RoadSide defaultLocation)
	{
		this.index = defaultLocation.index;
	}
    protected RoadSide(int index)
    {
        this.index = index;
        values.Add(index, this);
    }

    //easy int conversion
    public static implicit operator int(RoadSide location)
    {
		if (location == null)
		{
			return 0;
		}
        return location.index;
    }
    public static implicit operator RoadSide(int index)
    {
        RoadSide location;
        values.TryGetValue(index, out location);
        return new RoadSide(location); //return a copy, not a reference
    }
	
	//operators
	public static RoadSide operator ++(RoadSide location) 
	{
		location.index++;
		return location;
	}
	public static RoadSide operator --(RoadSide location) 
	{
		location.index--;
		return location;
	}
	
	public override string ToString()
    {
        return this.index.ToString();
    }
}