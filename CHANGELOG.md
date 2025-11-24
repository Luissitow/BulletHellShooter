# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.0.1] - 24/11/2025

### Added
- Implementación de BossController con 3 patrones de disparo mecánicamente diferentes (24/11/2025).
  - Patrón Circular: 20 balas en círculo de 360°, movimiento recto.
  - Patrón Aleatorio: Balas en direcciones random por todo el mapa.
  - Patrón Espiral: Balas en espiral con movimiento en onda.
- Sistema de contador de balas en UI con TextMeshPro (24/11/2025).
  - Contador de balas activas.
  - Contador de balas creadas totales.
  - Contador de balas destruidas totales.
- Script Bullet.cs con 3 tipos de movimiento (Straight, Homing, Wave) (24/11/2025).
- Sistema de destrucción de balas al salir de pantalla (límites ±100) (24/11/2025).
- PlayerController con movimiento usando Input System (24/11/2025).
- Sistema de disparo del jugador con PlayerBullet (24/11/2025).
- Duración ajustada a 30 segundos totales (10 segundos por patrón) (24/11/2025).
- Documentación README.md con guía de configuración (24/11/2025).
- Documentación README_PROBLEMAS_Y_SOLUCIONES.md con troubleshooting (24/11/2025).

### Fixed
- Corrección de posiciones de FirePoint para evitar spawns fuera de pantalla (24/11/2025).
- Implementación correcta del Input System Package (24/11/2025).
- Ajuste de bounds de destrucción de balas de 50 a 100 (24/11/2025).

## [0.0.0] - 24/11/2025

### Added
- Commit inicial del proyecto Bullet Hell Shooter (24/11/2025).
- Configuración inicial de Unity 6.2 con URP (24/11/2025).
- Estructura de carpetas: Scripts, Prefabs, Scenes, UI (24/11/2025).
