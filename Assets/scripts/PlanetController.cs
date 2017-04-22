﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

    PlanetPivotManager planetPivotManager;
    float planetRadius;

    Dictionary<GameObject, GameObject> entityPivots;

    public float PlanetRadius
    {
        get
        {
            return planetRadius;
        }
    }

    void Awake () {
        // Assuming perfectly round planets
        planetRadius = transform.localScale.x / 2;
        planetPivotManager =
            transform.GetComponentInChildren<PlanetPivotManager>();
        planetPivotManager.Init(this);

        entityPivots = new Dictionary<GameObject, GameObject>();
    }

    public GameObject PlaceEntity(GameObject entity, float angle)
    {
        GameObject entityPivot =
            planetPivotManager.CreatePivot(angle);
        entityPivot.name = entity.name + "'s pivot";

        entity.transform.parent = entityPivot.transform;
        float entityHeight = entity.transform.localScale.y;
        entity.transform.localPosition = 
            new Vector3(0, planetRadius + entityHeight / 2, 0);

        entityPivots.Add(entity, entityPivot);

        return entityPivot;
    }

    public void FreeEntity(GameObject entity)
    {
        entity.transform.parent = null;
        GameObject entityPivot = entityPivots[entity];
        planetPivotManager.DeletePivot(entityPivot);
        // Delete the pair from the dict
        entityPivots.Remove(entity);
    }
}