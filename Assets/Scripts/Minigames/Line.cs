using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public LineGenerator lineGenerator;
    List<Vector2> points;
    public GameObject testLine;
    float tempDistance = 100;
    public Vector2 pastPoint;
    int index;

    public void setRefrence(LineGenerator refrence)
    {
        lineGenerator = refrence;
    }

    public void UpdateLine(Vector2 position)
    {
        if (points == null)
        {
            points = new List<Vector2>();
            SetPoint(position);

            for (int i = 0; i < lineGenerator.listLength; i++)
            {
                //Debug.Log(lineGenerator.DistanceToClosestPoint(position, lineGenerator.startingPos[i], lineGenerator.endingPos[i]));
                if (tempDistance > lineGenerator.DistanceToClosestPoint(position, lineGenerator.startingPos[i], lineGenerator.endingPos[i]))
                {
                    tempDistance = lineGenerator.DistanceToClosestPoint(position, lineGenerator.startingPos[i], lineGenerator.endingPos[i]);
                    index = i;

                }

            }
            //lineGenerator.DistanceToClosestPoint(position, lineGenerator.lineStart, lineGenerator.endOfLine);
            //Debug.Log(index);
            if (tempDistance < .5f)
            {
                lineGenerator.closePoints++;
                pastPoint = position;
            }
            //lineGenerator.totalPercentage = 100 * (lineGenerator.closePoints / lineGenerator.totalPoints);
            //Debug.Log(index);
            //Debug.Log(tempDistance);
            return;
        }

        if (Vector2.Distance(points.Last(), position) > .1f)
        {
            SetPoint(position);
            //Debug.Log(lineGenerator.listLength);
            for (int i = 0; i < lineGenerator.listLength; i++)
            {
                //Debug.Log(lineGenerator.DistanceToClosestPoint(position, lineGenerator.startingPos[i], lineGenerator.endingPos[i]));
                if (tempDistance > lineGenerator.DistanceToClosestPoint(position, lineGenerator.startingPos[i], lineGenerator.endingPos[i]))
                {
                    tempDistance = lineGenerator.DistanceToClosestPoint(position, lineGenerator.startingPos[i], lineGenerator.endingPos[i]);
                    index = i;
                    //Debug.Log(index);
                }

            }

            //Debug.Log(index);
            if (tempDistance < .5f)
            {
                lineGenerator.closePoints++;
                lineGenerator.distanceTravelled += Vector2.Distance(pastPoint, position);
                pastPoint = position;
            }
            if (lineGenerator.distanceNeeded > lineGenerator.distanceTravelled)
            {
                lineGenerator.totalPercentage = 100 * ((lineGenerator.distanceTravelled / lineGenerator.distanceNeeded) * (lineGenerator.closePoints / lineGenerator.totalPoints));
            }
            else
            {
                lineGenerator.totalPercentage = 100 * ((lineGenerator.distanceNeeded / lineGenerator.distanceTravelled) * (lineGenerator.closePoints / lineGenerator.totalPoints));
            }
            lineGenerator.totalPercentage = Mathf.RoundToInt(lineGenerator.totalPercentage);
            //lineGenerator.totalPercentage = 100 * (lineGenerator.closePoints / lineGenerator.totalPoints);
            //Debug.Log(lineGenerator.closePoints);
            //Debug.Log(lineGenerator.closePoints / lineGenerator.totalPoints);
        }
    }

    void SetPoint(Vector2 point)
    {
        points.Add(point);
        tempDistance = 100f;
        lineGenerator.totalPoints++;
        //Debug.Log(Vector2.Distance(point, testLine.transform.position));

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);
    }
}
