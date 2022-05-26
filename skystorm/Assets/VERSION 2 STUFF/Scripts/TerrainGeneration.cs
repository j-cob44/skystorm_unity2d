using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour {

    public GameObject[] terrainPieces;
    public GameObject[] beachPieces;
    public GameObject[] beachCornerPieces;
    public GameObject[] oceanPieces;

    void Start() {
        //Instantiate(terrainPieces[(int)Random.Range(0,terrainPieces.Length - 1)], new Vector3(0,0,0), Quaternion.identity);
        createOcean();
        createBeachCorners();
        createBeaches();
        createTerrain();
    }

    void createOcean() {
        int lengthX = 30;
        while(lengthX != -35)
        {
            Instantiate(oceanPieces[(int)Random.Range(0, oceanPieces.Length - 1)], new Vector3(lengthX, 0, 30), Quaternion.identity);
            lengthX = lengthX - 5;
        }
        int heightZ = 25;
        while (heightZ != -30) {
            Instantiate(oceanPieces[(int)Random.Range(0, oceanPieces.Length - 1)], new Vector3(30, 0, heightZ), Quaternion.identity);
            heightZ = heightZ - 5;
        }
        heightZ = 25;
        while (heightZ != -30)
        {
            Instantiate(oceanPieces[(int)Random.Range(0, oceanPieces.Length - 1)], new Vector3(-30, 0, heightZ), Quaternion.identity);
            heightZ = heightZ - 5;
        }
        lengthX = 30;
        while (lengthX != -35)
        {
            Instantiate(oceanPieces[(int)Random.Range(0, oceanPieces.Length - 1)], new Vector3(lengthX, 0, -30), Quaternion.identity);
            lengthX = lengthX - 5;
        }

    }

    void createBeachCorners() {
        Instantiate(beachCornerPieces[(int)Random.Range(0, beachCornerPieces.Length - 1)], new Vector3(25, 0, 25), Quaternion.Euler(0, 90, 0));
        Instantiate(beachCornerPieces[(int)Random.Range(0, beachCornerPieces.Length - 1)], new Vector3(25, 0, -25), Quaternion.Euler(0,180,0));
        Instantiate(beachCornerPieces[(int)Random.Range(0, beachCornerPieces.Length - 1)], new Vector3(-25, 0, 25), Quaternion.identity);
        Instantiate(beachCornerPieces[(int)Random.Range(0, beachCornerPieces.Length - 1)], new Vector3(-25, 0, -25), Quaternion.Euler(0, 270, 0));
    }

    void createBeaches() {
        int lengthX = 20;
        while (lengthX != -25) {
            Instantiate(beachPieces[(int)Random.Range(0, beachPieces.Length - 1)], new Vector3(lengthX, 0, 25), Quaternion.Euler(0, 90, 0));
            lengthX = lengthX - 5;
        }
        int heightZ = 20;
        while (heightZ != -25)
        {
            Instantiate(beachPieces[(int)Random.Range(0, beachPieces.Length - 1)], new Vector3(25, 0, heightZ), Quaternion.Euler(0, 180, 0));
            heightZ = heightZ - 5;
        }
        heightZ = 20;
        while (heightZ != -25)
        {
            Instantiate(beachPieces[(int)Random.Range(0, beachPieces.Length - 1)], new Vector3(-25, 0, heightZ), Quaternion.identity);
            heightZ = heightZ - 5;
        }
        lengthX = 20;
        while (lengthX != -25)
        {
            Instantiate(beachPieces[(int)Random.Range(0, beachPieces.Length - 1)], new Vector3(lengthX, 0, -25), Quaternion.Euler(0, 270, 0));
            lengthX = lengthX - 5;
        }
    }

    void createTerrain() {
        int lengthX = 20;
        int heightZ = 20;
        while (lengthX != -25 && heightZ != -25)
        {
            Instantiate(terrainPieces[(int)Random.Range(0, terrainPieces.Length - 1)], new Vector3(lengthX, 0, heightZ), Quaternion.identity);
            lengthX = lengthX - 5;
            if(lengthX == -25 && heightZ != -25) {
                lengthX = 20;
                heightZ = heightZ - 5;
            }
        }

    }
}
