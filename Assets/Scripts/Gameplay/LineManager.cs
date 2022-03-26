using UnityEngine;
using System.Collections.Generic;
using System;

namespace HappyGarden.Gameplay {
    internal class LineManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private GameObject brush;

        internal static event Action OnCompleted;

        private List<Vector2> points;

        private LineRenderer lineRenderer;
        private EdgeCollider2D currentCollider;

        [SerializeField] private float lineEdgeRadius;

        private Vector2 lastPos;

        private void Start()
        {
            OnCompleted += DestroyLine;
        }

        private void Update()
        {
            DrawLine();
        }

        private void DrawLine() {
            if (Input.GetKeyDown(KeyCode.Mouse0)) 
            {
                CreateBrush();
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

                if (mousePos != lastPos)
                {
                    AddPoint(mousePos);
                    lastPos = mousePos;
                }   
            }
            else
            {
                OnCompleted?.Invoke();   
            }
        }

        private void DestroyLine()
        {
            if (lineRenderer != null)
            {
                Destroy(lineRenderer.gameObject);
            }
        }

        private void CreateBrush() 
        {
            points = new List<Vector2>();

            GameObject brushInstance = Instantiate(brush);
            lineRenderer = brushInstance.GetComponent<LineRenderer>();

            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            lineRenderer.SetPosition(0, mousePos);
            lineRenderer.SetPosition(1, mousePos);

            currentCollider = lineRenderer.gameObject.AddComponent<EdgeCollider2D>();
            currentCollider.edgeRadius = lineEdgeRadius;
            currentCollider.isTrigger = true;
        }

        private void AddPoint(Vector2 point) 
        {
            points.Add(point);

            lineRenderer.positionCount++;

            int positionIndex = lineRenderer.positionCount-1;
            lineRenderer.SetPosition(positionIndex, point);

            currentCollider.SetPoints(points);
        }
    }
}
