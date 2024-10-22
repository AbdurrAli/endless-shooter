using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Menambahkan opsi untuk membuat asset menu di Unity untuk konfigurasi gelombang
[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    // Daftar prefab musuh yang akan dimunculkan dalam gelombang ini
    [SerializeField] List<GameObject> enemyPrefabs;
    // Prefab jalur yang akan digunakan sebagai referensi untuk pergerakan musuh
    [SerializeField] Transform pathPrefab;
    // Kecepatan gerak musuh
    [SerializeField] float moveSpeed = 5f;

    // Mengembalikan jumlah musuh dalam gelombang ini
    public int GetEnemyCount() {
        return enemyPrefabs.Count;
    }

    // Mengembalikan prefab musuh berdasarkan indeks yang diberikan
    public GameObject GetEnemyPrefabs(int index) {
        return enemyPrefabs[index];
    }

    // Mengambil waypoint pertama sebagai posisi awal musuh
    public Transform GetStartingWaypoint() {
        return pathPrefab.GetChild(0);
    }

    // Mengembalikan daftar semua waypoint yang ada pada pathPrefab
    public List<Transform> GetWaypoint() {
        // Membuat daftar untuk menyimpan semua waypoint
        List<Transform> waypoints = new List<Transform>();
        // Menambahkan setiap anak dari pathPrefab ke dalam daftar waypoints
        foreach (Transform child in pathPrefab) {
            waypoints.Add(child);
        }
        return waypoints;
    }

    // Mengembalikan kecepatan gerak musuh
    public float GetMoveSpeed() {
        return moveSpeed;
    }
}
