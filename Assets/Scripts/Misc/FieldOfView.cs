using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    private float fov;
    private float viewDistance;
    private Vector3 origin;
    private float startingAngle;

    private float gameOverTimer = 1f;
    public float gameOverCounter = 0f;

    public bool playerDetected = false;
    [SerializeField] private EnemyMovementDK enemyMovement;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerDetectionDK playerDetection;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
        if (enemyMovement == null)
            enemyMovement = GetComponentInParent<EnemyMovementDK>();

        if (playerDetection == null)
            playerDetection = FindObjectOfType<PlayerDetectionDK>();

        if (player == null && enemyMovement != null)
            player = enemyMovement.player;
    }

    private void LateUpdate()
    {
        if (enemyMovement == null) return;

        int rayCount = 50;
        float angle;
        if (enemyMovement != null && enemyMovement.anchor1Distance <= enemyMovement.anchorRange)
        {
            angle = (180 + (fov * 0.5f));
        }
        else
        {
            angle = startingAngle + (fov * 0.5f);
        }
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        bool _playerDetected = false;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                //Debug.Log("FOV hit nothing");
                vertex = origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                //Debug.Log("FOV hit something
                if (raycastHit2D.collider.CompareTag("Player"))
                {
                    vertex = origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;//Esto se puede quitar para que el jugador tape el FOV y arrelgar el problema que tiene de que deja que el FOV 
                                                                                            //continue a donde no deberia
                    Debug.Log("Enemy detected Player");
                    _playerDetected = true;
                }
                else
                {
                    vertex = raycastHit2D.point;
                }
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;
        }
        playerDetected = _playerDetected;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        PlayerDetected();
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) - fov / 20f;
    }

    public float SetFOV(float newFoV)
    {
        return fov = newFoV;
    }

    public float SetViewDistance(float newVD)
    {
        return viewDistance = newVD;
    }

    float TimeIncrement()
    {
        if (player != null)
        {
            return 1f;
        }
        return 0f;
    }

    void PlayerDetected()
    {
        if (playerDetected)
        {
            gameOverCounter += TimeIncrement() * Time.deltaTime;
            if (gameOverCounter > gameOverTimer)
            {
                if (playerDetection != null)
                {
                    playerDetection.GameOver();
                }
                gameOverCounter = 0;
            }
        }
        else
        {
            gameOverCounter = 0;
        }
    }
}