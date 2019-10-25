﻿using System.Collections.Generic;
using UnityEngine;

public class LineCutFeedback : MonoBehaviour
{
    Vector3 startPosition;
    GameObject currentLineObject;
    LineRenderer lineRenderer;
    Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawingLine();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDrawingLine();
        }
        else if (Input.GetMouseButton(0))
        {
            PreviewLine();
        }
    }

    Vector3 GetMousePosition()
    {
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform as RectTransform,
            Input.mousePosition, canvas.worldCamera,
            out movePos);

        Vector3 positionToReturn = transform.TransformPoint(movePos);

        return positionToReturn;
    }

    void StartDrawingLine()
    {
        lineRenderer.positionCount = 20;
        startPosition = GetMousePosition();
    }

    void PreviewLine()
    {
        List<Vector3> points = new List<Vector3>();
        Vector3 endPosition = GetMousePosition();
        float step = (endPosition - startPosition).magnitude / 20;
        points.Add(startPosition);
        for (int i = 1; i < 20; i++)
        {
            points.Add(startPosition + (endPosition - startPosition) * i * step);
        }
        points.Add(endPosition);

        lineRenderer.SetPositions(points.ToArray());
    }

    void EndDrawingLine()
    {
        startPosition = Vector3.zero;
        lineRenderer.positionCount = 0;
    }
}