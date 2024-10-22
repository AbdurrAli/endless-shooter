using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kelas Pathfinder mengatur logika pergerakan musuh mengikuti jalur yang telah ditentukan
public class Pathfinder : MonoBehaviour
{
    // Referensi ke spawner musuh yang digunakan untuk mendapatkan konfigurasi gelombang
    EnemySpawner enemySpawner;
    // Konfigurasi gelombang yang berisi data seperti kecepatan gerak dan titik jalur
    WaveConfigSO waveConfig;
    // Daftar titik jalur (waypoints) yang akan dilalui musuh
    List<Transform> waypoints;
    // Indeks saat ini dalam daftar waypoints
    int waypointsIndex = 0;

    // Fungsi Start dipanggil sekali saat objek diinisialisasi
    void Start()
    {
        // Mengambil konfigurasi gelombang saat ini dari EnemySpawner
        waveConfig = enemySpawner.GetCurrentWave();
        // Mengambil daftar titik jalur dari konfigurasi gelombang
        waypoints = waveConfig.GetWaypoint();
        // Menetapkan posisi awal musuh ke titik jalur pertama
        transform.position = waypoints[waypointsIndex].position;
    }

    // Fungsi Awake dipanggil sebelum Start, digunakan untuk menginisialisasi referensi
    void Awake() {
        // Mencari objek EnemySpawner yang ada dalam scene dan menghubungkannya ke variabel enemySpawner
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }

    // Mengatur logika untuk mengikuti jalur (waypoints)
    void FollowPath() {
        // Memastikan bahwa indeks waypoints saat ini masih dalam rentang daftar waypoints
        if (waypointsIndex < waypoints.Count) {
            // Mendapatkan posisi target dari waypoint saat ini
            Vector3 targetPosisiton = waypoints[waypointsIndex].position;
            // Menghitung kecepatan gerak berdasarkan waktu frame dan kecepatan yang ditentukan di konfigurasi gelombang
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            // Memindahkan posisi objek mendekati target menggunakan fungsi MoveTowards
            transform.position = Vector2.MoveTowards(transform.position, targetPosisiton, delta);
            // Jika objek sudah mencapai target, lanjutkan ke waypoint berikutnya
            if (transform.position == targetPosisiton) {
                waypointsIndex++;
            }
        }
        else {
            // Jika semua waypoints telah dilalui, hancurkan objek ini
            Destroy(gameObject);
        }
    }

    // Update dipanggil sekali per frame
    void Update()
    {
        // Memanggil fungsi FollowPath untuk menggerakkan musuh mengikuti jalur
        FollowPath();
    }
}
