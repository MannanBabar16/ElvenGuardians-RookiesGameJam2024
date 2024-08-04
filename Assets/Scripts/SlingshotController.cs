using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotController : MonoBehaviour {
    public Transform slingStart;
    public GameObject projectilePrefab; 
    public LineRenderer lineRenderer; 
    public int lineSegmentCount = 20;
    public float launchForceMultiplier = 40f; 
    public float dragSensitivity = 1f;
    public float minDragDistance = 0.1f; 

    private Vector3 dragStartPos; 
    private bool isDragging = false; 
    private Transform activePlayer; 



    void OnEnable() {
        PlayerController.OnPlayerClicked += SetActivePlayer;
    }

    void OnDisable() {
        PlayerController.OnPlayerClicked -= SetActivePlayer;
    }

    void Update() {
        if (activePlayer == null) return; 

        if (Input.GetMouseButtonDown(0)) {
            StartDrag();
        }
        if (Input.GetMouseButton(0) && isDragging) {
            Dragging();
        }
        if (Input.GetMouseButtonUp(0) && isDragging) {
            EndDrag();
        }
    }

    void SetActivePlayer(PlayerController player) {
        activePlayer = player.transform;
        slingStart.position = activePlayer.position; 
    }

    void StartDrag() {
        isDragging = true;
        dragStartPos = Input.mousePosition;
        lineRenderer.positionCount = lineSegmentCount;
        lineRenderer.SetPosition(0, slingStart.position);
    }

    void Dragging() {
        Vector3 currentDragPos = Input.mousePosition;
        Vector3 dragVector = (currentDragPos - dragStartPos) * dragSensitivity;
        Vector3 dragWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(currentDragPos.x, currentDragPos.y, Camera.main.WorldToScreenPoint(slingStart.position).z));
        Vector3 aimingPoint = slingStart.position - (dragWorldPos - slingStart.position);
        UpdateTrajectory(slingStart.position, (aimingPoint - slingStart.position) * launchForceMultiplier);
    }

    void EndDrag() {
        isDragging = false;
        lineRenderer.positionCount = 0;

        Vector3 currentDragPos = Input.mousePosition;
        Vector3 dragVector = (currentDragPos - dragStartPos) * dragSensitivity;

       
        if (dragVector.magnitude >= minDragDistance) {
            Vector3 dragWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(currentDragPos.x, currentDragPos.y, Camera.main.WorldToScreenPoint(slingStart.position).z));
            Vector3 aimingPoint = slingStart.position - (dragWorldPos - slingStart.position);
            Vector3 shootingDirection = (aimingPoint - slingStart.position).normalized;

            GameObject projectile = Instantiate(projectilePrefab, slingStart.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = shootingDirection * launchForceMultiplier;


        }


    }

    void UpdateTrajectory(Vector3 start, Vector3 velocity) {
        Vector3[] points = new Vector3[lineSegmentCount];
        points[0] = start;

        for (int i = 1; i < lineSegmentCount; i++) {
            float time = i * 0.1f; 
            points[i] = start + velocity * time + 0.5f * Physics.gravity * time * time;
        }

        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }
}