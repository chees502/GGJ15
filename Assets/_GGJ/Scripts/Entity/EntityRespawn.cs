using UnityEngine;
using System.Collections;

public class EntityRespawn : MonoBehaviour {
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    public bool respawnable = true;
    public float respawnTime = 0f;
    private float _respawnCounter;

    private bool _isRespawning = false;
    public bool IsRespawning {
        get { return _isRespawning; }
    }

    public delegate void RespawnEvent();
    public event RespawnEvent OnEntityRespawned;
    public event RespawnEvent OnEntityDestroyed;

    void Awake() {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    void Update() {
        if (IsRespawning) {
            _respawnCounter -= Time.deltaTime;
            if (_respawnCounter < 0f) {
                Respawn();
            }
        }
    }

    public void Destroy() {
        Destroy(0f, respawnTime);
    }

    public void Destroy(float destroyTimer) {
        Destroy(destroyTimer, respawnTime);
    }

    public void Destroy(float destroyTimer, float respawnTimer) {
        StartCoroutine(DestroyCoroutine(destroyTimer, respawnTimer));
    }

    public IEnumerator DestroyCoroutine(float destroyTimer, float respawnTimer) {
        Debug.Log("Start Destroy Coroutine");
        yield return new WaitForSeconds(destroyTimer);
        Debug.Log("Start DestroyCoroutine Hiding Object");

        TriggerEntityDestroyed();
        SetAllColliders(false);
        SetAllMeshRenderer(false);
        _isRespawning = (respawnTimer >= 0f);
        _respawnCounter = respawnTime;
    }

    public void Respawn() {
        SetAllColliders(true);
        SetAllMeshRenderer(true);
        ResetPosition();
        ResetRigidbodys();
        _isRespawning = false;
        TriggerEntityRespawned();
    }

    public void ResetPosition() {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
    }

    public void ResetRigidbodys() {
        foreach (Rigidbody r in GetComponents<Rigidbody>()) {
            r.velocity = Vector3.zero;
        }

        foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>()) {
            r.velocity = Vector3.zero;
        }
    }

    void TriggerEntityDestroyed() {
        if (OnEntityDestroyed != null) {
            OnEntityDestroyed();
        }
    }

    void TriggerEntityRespawned() {
        if (OnEntityRespawned != null) {
            OnEntityRespawned();
        }
    }

    void SetAllMeshRenderer(bool value) {
        foreach (MeshRenderer mr in GetComponents<MeshRenderer>()) {
            mr.enabled = value;
        }

        foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>()) {
            mr.enabled = value;
        }
    }

    void SetAllColliders(bool value) {
        foreach (Collider c in GetComponents<Collider>()) {
            c.enabled = value;
        }

        foreach (Collider c in GetComponentsInChildren<Collider>()) {
            c.enabled = value;
        }
    }
}
