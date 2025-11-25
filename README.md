# Bullet Hell Shooter - TC2008B

Proyecto de videojuego tipo Bullet Hell desarrollado en Unity 2D para el curso TC2008B.

**Nivel de Dificultad:** Fácil  
**Versión:** 0.0.1  
**Fecha:** 24 de noviembre de 2025

## Descripción

Juego de disparos estilo Bullet Hell donde el jugador debe esquivar oleadas de proyectiles enemigos durante 30 segundos. El jefe ejecuta 3 patrones de disparo mecánicamente diferentes, cada uno con una duración de 10 segundos.

## Características Implementadas

### Requisitos del Nivel "Fácil" ✅

- **3 Patrones de disparo mecánicamente diferentes (30 segundos totales):**
  1. **Patrón Circular** (10s): 20 balas en círculo de 360°, movimiento recto
  2. **Patrón Aleatorio** (10s): Balas en direcciones random cubriendo todo el mapa
  3. **Patrón Espiral** (10s): Balas en espiral con movimiento en onda sinusoidal

- **Sistema de contador de balas en UI:**
  - Balas activas en pantalla
  - Total de balas creadas
  - Total de balas destruidas

- **Optimización con Instantiate/Destroy**

### Sistema de Jugador

- Movimiento con Input System (WASD o flechas)
- Límites de pantalla (±80 unidades)
- Sistema de disparo con cooldown (0.2s)
- Balas con velocidad 15 unidades/segundo

### Tipos de Movimiento de Balas

- **Straight:** Movimiento lineal en dirección inicial
- **Homing:** Persigue al jugador con interpolación
- **Wave:** Movimiento sinusoidal perpendicular a la dirección

## Controles

- **W / Flecha Arriba**: Mover hacia arriba
- **S / Flecha Abajo**: Mover hacia abajo
- **A / Flecha Izquierda**: Mover hacia la izquierda
- **D / Flecha Derecha**: Mover hacia la derecha
- **Barra Espaciadora**: Disparar

## Estructura del Proyecto

```
Assets/
├── Scripts/
│   ├── Boss/
│   │   ├── BossController.cs      # Controlador principal del jefe
│   │   ├── Bullet.cs              # Comportamiento de balas enemigas
│   │   └── BossPatterns.cs        # (Comentado) Patrones alternativos
│   ├── PlayerController.cs        # Controlador del jugador
│   └── PlayerBullet.cs            # Comportamiento de balas del jugador
├── Prefabs/
│   └── Bullets/
│       ├── Bullet                 # Prefab de bala enemiga
│       └── PlayerBullet          # Prefab de bala del jugador
└── Scenes/
    └── SampleScene               # Escena principal del juego
```

## Tecnologías y Requisitos

- **Unity:** 6.2.10f1 (6000.2.10f1)
- **Render Pipeline:** Universal Render Pipeline (URP)
- **Input System Package:** Nuevo sistema de entrada de Unity
- **TextMeshPro:** Para UI de contadores

## Instalación y Configuración

### 1. Clonar el repositorio

```bash
git clone [URL_DE_TU_REPOSITORIO]
cd BulletHellShooter
```

### 2. Abrir en Unity

1. Abre Unity Hub
2. Clic en "Add" → "Add project from disk"
3. Selecciona la carpeta del proyecto
4. Abre con Unity 6.2 o superior

### 3. Configurar Input System

El proyecto usa el **nuevo Input System**. Si Unity pregunta sobre cambiar el sistema de entrada, acepta y reinicia el editor.

### 4. Configurar la escena

**⚠️ IMPORTANTE:** Verifica estas configuraciones en el Inspector:

#### Boss GameObject
- **Position:** X: 0, Y: 4, Z: 0
- **FirePoint (hijo):** X: 0, Y: 0, Z: 0 (posición relativa)
- **BossController Script:**
  - Bullet Prefab: Arrastra el prefab "Bullet" desde Assets/Prefabs
  - Fire Points: Size = 1, Element 0 = Firepoint (arrastra desde Hierarchy)
  - Bullet Counter Text: Arrastra "BulletCounterText" desde Canvas

#### Player GameObject
- **Position:** X: 0, Y: -20, Z: 0
- **FirePoint (hijo):** X: 0, Y: 1, Z: 0 (posición relativa)
- **PlayerController Script:**
  - Player Bullet Prefab: Arrastra "PlayerBullet" desde Assets/Prefabs

### 5. Ejecutar el juego

Presiona **Play** en Unity. El juego durará 30 segundos con 3 patrones de 10 segundos cada uno.

---

## ⚠️ PUNTOS IMPORTANTES A RECORDAR

### 🎯 Concepto clave: Objetos hijos (heredados)

**El Firepoint es un objeto hijo del Boss/Player, esto significa que su posición es RELATIVA al objeto padre.**

#### ¿Qué es un objeto hijo o heredado?

En Unity, cuando un objeto es hijo de otro (aparece indentado debajo en el Hierarchy):
```
Boss (padre)
└── Firepoint (hijo/heredado)
```

**La posición del hijo se SUMA a la posición del padre:**

```
Ejemplo 1:
Boss Position:      X: 0,  Y: 4,  Z: 0
Firepoint Position: X: 0,  Y: 0,  Z: 0  (relativa al Boss)
Posición REAL:      X: 0,  Y: 4,  Z: 0  ✅ CORRECTO

Ejemplo 2:
Boss Position:      X: 0,  Y: 4,   Z: 0
Firepoint Position: X: 0,  Y: 35,  Z: 0  (relativa al Boss)
Posición REAL:      X: 0,  Y: 39,  Z: 0  ❌ MUY ARRIBA, FUERA DE CÁMARA
```

#### ⚡ El problema que tuvimos:

1. **Boss** estaba en **Y: 35** (muy arriba)
2. **Firepoint** (hijo del Boss) estaba en **Y: 35** (posición relativa)
3. **Resultado:** Las balas aparecían en **Y: 70** (completamente fuera de la cámara)

#### ✅ La solución:

1. **Boss** debe estar en **Y: 4** (visible en cámara)
2. **Firepoint** debe estar en **Y: 0** (posición relativa cerca del centro)
3. **Resultado:** Las balas aparecen en **Y: 4** (perfectamente visible)

### 🔑 Regla de oro para objetos hijos:

**Los objetos vacíos que son hijos (como FirePoints) deben tener posiciones relativas PEQUEÑAS:**
- Valores cercanos a **0** son ideales
- Valores como **±1** o **±2** están bien para ajustes finos
- Valores grandes como **35** causan que los objetos aparezcan muy lejos

### 📍 Posiciones correctas de referencia:

```
JEFE:
Boss (padre):           Y: 4
└── Firepoint (hijo):   Y: 0  (relativa)
    = Posición real:    Y: 4  ✅

JUGADOR:
Player (padre):         Y: -20
└── FirePoint (hijo):   Y: 1   (relativa, justo encima)
    = Posición real:    Y: -19 ✅
```

### 🎮 Por qué esto afecta las balas:

Cuando el script crea una bala en `firePoint.position`, usa la **posición real en el mundo** (la suma de padre + hijo). Si el hijo tiene valores grandes, las balas aparecen muy lejos de donde esperamos.

### 💡 Cómo verificar si tu configuración es correcta:

1. Selecciona el objeto hijo (Firepoint) en el Hierarchy
2. Mira el Inspector, en Transform
3. Si los valores de Position son grandes (>10), probablemente están mal
4. Ajusta a valores cercanos a 0
5. Prueba el juego y verifica que las balas aparezcan donde debe estar el objeto padre

---

## Créditos

Desarrollado como parte del curso TC2008B.
Fecha: Noviembre 2025
