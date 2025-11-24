# GUÍA DE SOLUCIÓN DE PROBLEMAS - BULLET HELL SHOOTER

## 📍 POSICIONES Y CONFIGURACIÓN CORRECTA

### 1. POSICIÓN DEL BOSS
**Problema:** Las balas se creaban muy arriba (Y: 70) y no se veían en pantalla.

**Solución:**
- **Objeto Boss**: Position Y debe estar entre **0 y 10** (recomendado: Y: 4)
- **Firepoint (hijo del Boss)**: Position Y relativa debe ser **0** (no 35)
- Las posiciones de los objetos hijos se suman a la del padre

**Configuración correcta:**
```
Boss (Transform):
- Position: X: 0, Y: 4, Z: 0

Firepoint (hijo del Boss):
- Position: X: 0, Y: 0, Z: 0  (relativa al Boss)
```

---

### 2. POSICIÓN DEL JUGADOR
**Problema:** El jugador debe estar visible en la parte inferior de la pantalla.

**Solución:**
```
Player (Transform):
- Position: X: 0, Y: -20, Z: 0
```

**Firepoint del jugador (hijo del Player):**
```
FirePoint (hijo del Player):
- Position: X: 0, Y: 1, Z: 0  (justo encima del jugador)
```

---

### 3. LÍMITES DE DESTRUCCIÓN DE BALAS
**Problema:** Las balas se destruían demasiado pronto porque los límites eran muy pequeños.

**Solución en Bullet.cs:**
```csharp
// INCORRECTO (muy pequeño):
if (transform.position.y > 10 || transform.position.y < -10)

// CORRECTO (más grande):
if (transform.position.y > 50 || transform.position.y < -50 ||
    transform.position.x > 50 || transform.position.x < -50)
```

---

### 4. ÁREA DE MOVIMIENTO DEL JUGADOR
**Problema:** El área de movimiento era muy pequeña.

**Solución en PlayerController.cs:**
```csharp
// PEQUEÑO (área de 80x80):
float clampedX = Mathf.Clamp(transform.position.x, -40f, 40f);

// GRANDE (área de 160x160):
float clampedX = Mathf.Clamp(transform.position.x, -80f, 80f);
float clampedY = Mathf.Clamp(transform.position.y, -80f, 80f);
```

**Ajustar según necesites:**
- Valores más grandes = más espacio de movimiento
- Valores más pequeños = menos espacio de movimiento

---

## 🎯 CONFIGURACIÓN DE LA CÁMARA

**Configuración correcta de la Main Camera:**
```
Main Camera (Transform):
- Position: X: 0, Y: 0, Z: -10

Camera Component:
- Projection: Orthographic
- Size: 5 o 10 (ajustar según el tamaño del juego)
```

---

## 🔧 CONFIGURACIÓN DE PREFABS

### Prefab de Bala (Enemigo)
**Componentes necesarios:**
1. Sprite Renderer (con sprite asignado)
2. Script `Bullet`
   - Speed: 5
   - Movement Type: Se asigna desde BossController

**Escala recomendada:**
- Scale: X: 0.3, Y: 0.3, Z: 1 (o ajustar según prefieras)

### Prefab de Bala (Jugador)
**Componentes necesarios:**
1. Sprite Renderer (con sprite de color diferente)
2. Script `PlayerBullet`
   - Speed: 15 (más rápida que las balas enemigas)

**Escala recomendada:**
- Scale: X: 0.2, Y: 0.2, Z: 1

---

## 📊 CONFIGURACIÓN DEL TEXTO (CONTADOR DE BALAS)

**Problema:** El texto no mostraba el número completo.

**Solución:**
```
Text (TMP) - Rect Transform:
- Width: 400 o más (NO 220)
- Height: 80 o 100 (NO 40)

TextMeshPro - Text (UI):
- Font Size: 48
- Overflow: Overflow (NO Truncate)
- Vertex Color: Blanco (255, 255, 255, 255)
```

---

## 🎮 CONFIGURACIÓN DEL INPUT SYSTEM

**Problema:** Errores con Input.GetAxis() o Input.GetKey()

**Solución:**
1. Ve a **Edit > Project Settings > Player**
2. En **Other Settings > Active Input Handling**
3. Cambia a **"Input Manager (Old)"** o **"Both"**
4. Reinicia Unity

---

## 🏷️ TAGS IMPORTANTES

### Tag "Player"
**Para qué:** Las balas homing necesitan encontrar al jugador.

**Cómo asignar:**
1. Selecciona el objeto `Player`
2. En el Inspector, arriba, cambia `Tag` a `Player`
3. Si no existe, ve a `Tag > Add Tag...` y crea "Player"

### Tag "Boss" (Opcional)
Para que las balas del jugador detecten al jefe.

---

## 📐 RELACIÓN PADRE-HIJO (MUY IMPORTANTE)

**Concepto clave:** Las posiciones de los objetos hijos son **RELATIVAS** al padre.

**Ejemplo:**
```
Boss (Position: X: 0, Y: 4, Z: 0)
└── Firepoint (Position: X: 0, Y: 0, Z: 0)
    Posición REAL en el mundo: X: 0, Y: 4, Z: 0

Boss (Position: X: 0, Y: 4, Z: 0)
└── Firepoint (Position: X: 0, Y: 35, Z: 0)
    Posición REAL en el mundo: X: 0, Y: 39, Z: 0  ❌ MUY ARRIBA
```

**Regla de oro:** Los objetos vacíos (FirePoints) deben tener posiciones relativas pequeñas (0 o cercanas a 0).

---

## 🔄 ORDEN DE CONFIGURACIÓN RECOMENDADO

1. Crear y posicionar los objetos principales (Boss, Player, Cámara)
2. Crear los objetos hijos vacíos (FirePoints)
3. Crear y configurar los prefabs (Balas)
4. Asignar los scripts a los objetos
5. Asignar las referencias en el Inspector (prefabs, firepoints, texto)
6. Ajustar tags (Player)
7. Probar y ajustar posiciones/límites

---

## 🐛 ERRORES COMUNES Y SOLUCIONES

### "BulletPrefab no está asignado"
**Causa:** El campo está vacío en el Inspector.
**Solución:** Arrastra el prefab de la bala al campo correspondiente.

### "FirePoints está vacío"
**Causa:** El array no tiene elementos o está en tamaño 0.
**Solución:** Cambia el tamaño a 1 y arrastra el objeto Firepoint.

### "Las balas no se ven"
**Causa:** Están muy lejos de la cámara o fuera de los límites.
**Solución:** Verifica las posiciones del Boss y Firepoint (ver sección 1).

### "El texto no muestra el número"
**Causa:** El Width del Rect Transform es muy pequeño.
**Solución:** Aumenta el Width a 400 o más (ver sección de texto).

### "Error con Input.GetKey()"
**Causa:** Unity está usando el nuevo Input System.
**Solución:** Cambia a Input Manager (Old) en Project Settings.

---

## 📝 VALORES DE REFERENCIA RÁPIDA

```
POSICIONES:
- Boss: Y: 4
- Player: Y: -20
- Cámara: Y: 0, Z: -10

LÍMITES:
- Destrucción de balas: ±50
- Movimiento jugador: ±80

VELOCIDADES:
- Balas enemigas: 5
- Balas jugador: 15
- Jugador: 10

ESCALAS:
- Balas enemigas: 0.3
- Balas jugador: 0.2

TEXTO UI:
- Width: 400+
- Font Size: 48
```

---

## 🎯 CHECKLIST DE VERIFICACIÓN

Antes de probar el juego, verifica:

- [ ] Boss tiene Position Y entre 0 y 10
- [ ] Firepoint del Boss tiene Position Y cercana a 0
- [ ] Player tiene Tag "Player"
- [ ] Prefabs de balas tienen scripts asignados
- [ ] BossController tiene todos los campos asignados
- [ ] PlayerController tiene todos los campos asignados
- [ ] Texto UI tiene Width suficiente (400+)
- [ ] Input System configurado correctamente

---

**Fecha de creación:** 24 de noviembre de 2025
**Versión:** 1.0
