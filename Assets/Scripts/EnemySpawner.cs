using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kelas EnemySpawner bertanggung jawab untuk menghasilkan musuh sesuai dengan konfigurasi gelombang
public class EnemySpawner : MonoBehaviour
{
    // Gelombang saat ini yang akan digunakan untuk menentukan pengaturan musuh
    [SerializeField] WaveConfigSO currentWaves;

    // Fungsi Start dipanggil sekali saat objek diinisialisasi
    void Start()
    {
        // Memanggil fungsi untuk memulai proses pemunculan musuh
        SpawnEnemies();
    }

    // Mengembalikan konfigurasi gelombang yang sedang aktif
    public WaveConfigSO GetCurrentWave() {
        return currentWaves;
    }

    // Fungsi untuk memunculkan musuh berdasarkan konfigurasi gelombang
    void SpawnEnemies() {
        // Mengulangi sebanyak jumlah musuh yang ditentukan dalam konfigurasi gelombang
        for (int i = 0; i < currentWaves.GetEnemyCount(); i++) {
            // Membuat instance musuh pada posisi awal yang ditentukan, tanpa rotasi (Quaternion.identity), dan menetapkan spawner sebagai parent
            Instantiate(
                currentWaves.GetEnemyPrefabs(i), // Prefab musuh yang akan dibuat
                currentWaves.GetStartingWaypoint().position, // Posisi awal musuh
                Quaternion.identity, // Rotasi musuh (identity berarti tidak ada rotasi)
                transform // Menetapkan spawner sebagai parent dari musuh yang dibuat
            );
        }
    }

    // Update dipanggil sekali per frame
    // Fungsi Update di sini dikomentari karena tidak diperlukan
    // void Update() {
    // }
}
