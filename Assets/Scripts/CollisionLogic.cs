using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLogic : MonoBehaviour
{
    private GameController _gameController;

    [SerializeField] private GameObject PhoneGO;

    private void Start()
    {
        _gameController = FindObjectOfType<GameController>();
        PhoneGO = GameObject.FindGameObjectWithTag("Board");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Boundary")
        {
            _gameController.InitGame();
            Destroy(gameObject);
        }
        if (other.name == "PlusCapsule")
        {
            _gameController.PlusScore();
            MoveToNewSpawn(other.transform);
        }
        if (other.name == "MinusCapsule")
        {
            _gameController.MinusScore();
            MoveToNewSpawn(other.transform);
        }
    }

    private void MoveToNewSpawn(Transform SpawnableObject)
    {
        SpawnableObject.localPosition = new Vector3(Random.Range(-1.0f, 1.0f) / PhoneGO.transform.localScale.x, 
            Random.Range(-1.0f, 1.0f) / PhoneGO.transform.localScale.y, 
            0.2f / PhoneGO.transform.localScale.z);
    }
}
