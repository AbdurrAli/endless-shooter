using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kelas EnemySpawner bertanggung jawab untuk menghasilkan musuh sesuai dengan konfigurasi gelombang
public class EnemySpawner : MonoBehaviour
{
    // Daftar konfigurasi gelombang yang akan digunakan untuk pemunculan musuh
    [SerializeField] List<WaveConfigSO> waveConfigs;
    // Waktu tunggu antara setiap gelombang musuh
    [SerializeField] float timeBetweenWaves = 2f;
    // Gelombang saat ini yang akan digunakan untuk menentukan pengaturan musuh

    [SerializeField] bool isLooping;


    WaveConfigSO currentWaves;

    // Fungsi Start dipanggil sekali saat objek diinisialisasi
    void Start()
    {
        // Memanggil fungsi untuk memulai proses pemunculan musuh
        StartCoroutine(SpawnEnemyWaves());
    }

    // Mengembalikan konfigurasi gelombang yang sedang aktif
    public WaveConfigSO GetCurrentWave() {
        return currentWaves;
    }

    // Fungsi untuk memunculkan musuh berdasarkan konfigurasi gelombang
    IEnumerator SpawnEnemyWaves() {

        do
        {
            // Melakukan iterasi pada setiap gelombang yang ada dalam waveConfigs
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWaves = wave; // Mengatur gelombang saat ini

                // Memunculkan musuh berdasarkan jumlah yang ditentukan dalam gelombang saat ini
                for (int i = 0; i < currentWaves.GetEnemyCount(); i++)
                {
                    // Membuat instance musuh pada posisi awal yang ditentukan, tanpa rotasi (Quaternion.identity), dan menetapkan spawner sebagai parent
                    Instantiate(
                        currentWaves.GetEnemyPrefabs(i), // Prefab musuh yang akan dibuat
                        currentWaves.GetStartingWaypoint().position, // Posisi awal musuh
                        Quaternion.Euler(0,0,180), // Rotasi musuh (identity berarti tidak ada rotasi)
                        transform // Menetapkan spawner sebagai parent dari musuh yang dibuat
                    );

                    // Menunggu waktu spawn yang ditentukan sebelum memunculkan musuh berikutnya
                    yield return new WaitForSeconds(currentWaves.GetRandomSpawnTime());
                }
                // Menunggu waktu antara gelombang sebelum memunculkan gelombang berikutnya
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        while(isLooping);
    }

    // Update dipanggil sekali per frame
    // Fungsi Update di sini dikomentari karena tidak diperlukan
    // void Update() {
    // }
}
