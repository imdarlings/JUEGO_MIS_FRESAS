# JUEGO_MIS_FRESAS


**Autora:** Darling Samira Hinestroza Perea
**Materia:** Introducción a la Programación
**Proyecto Final – Noviembre 2025**



## Descripción

*Mis Fresas* es un juego de plataformas 2D donde una princesa debe recolectar fresas para su príncipe.
El jugador acompaña a la princesa a reunir al menos diez fresas, esquivando trampas y enfrentando su miedo a las arañas, mientras aprovecha el poder de las estrellas y las pociones.
Si la princesa decide leer el pergamino mágico, el reto cambia y solo necesitará recolectar cinco fresas para llegar al príncipe.



## Guion técnico

**Escena 1 – Menú Principal**

- Fondo del castillo en el cielo.
- Título del juego.
- Botones: *Jugar (Play)*, *Salir (Exit)*.
- En la parte superior derecha hay un botón con un signo de interrogación (“?”) que despliega el panel “Cómo jugar”.

**Escena 2 – Nivel Principal**

- Jugabilidad: movimiento, salto, recolección de objetos y detección de daño.
- Objetos interactivos:

  - **Fresas:** aumentan el conteo del objetivo.
  - **Estrellas:** otorgan tiempo adicional.
  - **Pociones:** restauran vida.
  - **Arañas:** actúan como trampas que restan vida.
  - **Pergamino:** si se lee, reduce la cantidad de fresas necesarias para ganar.
- Panel de pausa con opciones para reanudar o volver al menú principal.

**Pantallas finales:**

- **Victoria:** muestra el mensaje “¡Ganaste!” y botones para volver al menú o reiniciar.
- **Derrota:** incluye botones de “Reiniciar” y “Volver al menú”. Si no se presiona nada, la escena se reinicia automáticamente tras 10 segundos.



## Lógica de programación

**Variables utilizadas:**

- `vidas`, `fresasRecolectadas`, `tiempoRestante`, `leyoPergamino`, `juegoPausado`, `juegoTerminado`, `panelInstrucciones`, entre otras.

**Condicionales if:**

- Comprobación de si el jugador toca una trampa (`if (vidas <= 0)` → perder).
- Detección de lectura del pergamino (`if (leyoPergamino)` → meta cambia a 5 fresas).
- Comprobación de victoria (`if (fresasRecolectadas >= meta)` → ganar).
- Si el tiempo llega a cero (`if (tiempoRestante <= 0)` → perder).
- Validación de si el jugador está en suelo o no para permitir salto.

**Switch:**

- Control de los estados del juego en el `GameManager` (`Menu`, `Play`, `Pausa`, `Reiniciar`, `Ganar`, `Perder`).

**Arreglos:**
- Arreglo de imágenes de la barra de vida, que representa visualmente la cantidad de vida restante (5 niveles, desde vacía hasta completa).
- Arreglo de objetos interactivos (fresas, trampas, pociones) dentro de scripts de gestión.

**Loops (ciclos):**

- `for` en `UIManager` para actualizar visualmente los segmentos de la barra de vida.
- `while` y `IEnumerator` en corrutinas para la cuenta regresiva antes de reiniciar el nivel tras perder.
- Bucle de actualización en el temporizador del juego.

**Otros aspectos lógicos implementados:**

- Control de animaciones según movimiento, salto y daño.
- Detección de colisiones con distintos objetos mediante `OnTriggerEnter2D`.
- Control de pausas con la tecla **Escape** (Esc) que detiene y reanuda el tiempo del juego.
- Control del flujo de escenas mediante `SceneManager.LoadScene()`.



## **Narrativa e intencionalidad**

La historia busca transmitir ternura y serenidad a través de sus ilustraciones suaves y su música relajante.
La princesa representa la valentía y la dulzura frente a sus miedos.
Su travesía simboliza el esfuerzo, el cariño y la perseverancia.
Las fresas son el lazo afectivo con el príncipe, las arañas los miedos que enfrenta, y las estrellas el tiempo que ilumina su camino.



## **Controles**

- **← / → o A/D** : Mover a la princesa.
- **Espacio** : Saltar.
- **Escape (Esc)** : Pausar / Reanudar el juego.



## **Créditos**

**Diseño, animación, ilustración y programación:** Darling Samira Hinestroza Perea
**Música:**
- *Game Music Loop 13* – XtremeFreddy (Pixabay)
- *Happy Relaxing Loop* – Serge Quadrado (Pixabay)
**Fuentes tipográficas:** descargadas de DaFont (100% gratuitas).
**Motor:** Unity 2D (C#)
**Versión:** 1.0


Muchas Gracias ^^