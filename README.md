# PruebaDev_JC - Calculadora de Cuotas

Calculadora de cuotas para prestamos desarrollada como prueba tecnica. Recibe fecha de nacimiento, monto y plazo, y devuelve el valor de la cuota mensual basado en una tasa segun la edad del solicitante.

---

## Stack

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core + SQL Server
- Stored Procedures para el acceso a datos
- Arquitectura en 3 capas: Presentacion, Logica de Negocio y Datos

---

## Logica de validaciones

Una decision importante en este proyecto fue definir que validaciones tienen sentido llegar a la base de datos y cuales no.

**Validaciones que se manejan en el Service (no llegan al SP ni al log):**

- **Monto menor o igual a 0** - Si alguien manda un monto de 0 o negativo, no tiene sentido procesar nada. Es un dato invalido desde el formulario, no un caso de negocio.

- **Monto mayor a RD$ 999,999,999.99** - Un monto por encima de mil millones en una calculadora no tiene sentido operativo. Si alguien necesita evaluar un prestamo de esa escala, el proceso tiene que ser presencial. Se corta en el service y se le indica que visite una sucursal.

- **Fecha de nacimiento futura** - Si la fecha es posterior a hoy, la edad calculada seria negativa o cero. Llamar al backend para evaluar una edad negativa no tiene ningun sentido logico, y menos guardarlo en el log como una consulta valida. Se corta ahi.

**Validaciones que SI llegan al SP y se registran en el log:**

- **Edad menor de 18** - Esta si es una consulta real. Alguien con una edad valida esta solicitando el producto y el sistema le responde que no cumple el requisito minimo. Eso vale la pena registrarlo.

- **Edad mayor de 25** - Igual que el caso anterior, es un cliente potencial que el sistema no puede atender de forma automatica pero que podria ser atendido en sucursal. Se registra y se le da una respuesta amigable diferente al resto de los errores.

La idea es que el log refleje consultas reales con interes genuino, no cosas que se pudieron filtrar antes de llegar a la base de datos.

---

## Estructura del proyecto

```
Controllers/
    HomeController          - Carga la vista principal
    CalculadoraController   - API endpoint para el calculo

Models/
    Data/
        AppDbContext        - Contexto de EF con seed de tasas
    Entities/
        TasaEdad            - Tabla de tasas por edad (18-25)
        ConsultaLog         - Registro de consultas
    DTOs/
        CalcularCuotaRequest
        CalcularCuotaResponse

Repositories/
    Interfaces/
        ICalculadoraRepository
    CalculadoraRepository   - Ejecuta el SP y maneja los OUTPUT

Services/
    Interfaces/
        ICalculadoraService
    CalculadoraService      - Validaciones previas y llamada al repositorio

Database/
    StoredProcedures.sql    - SP_CalcularCuota y SP_ObtenerConsultasLog
```

---

## Base de datos

Las tablas las maneja Entity Framework via migraciones. Los stored procedures se corren manualmente en SSMS una vez que la BD este creada.

El seed de las 8 tasas (edades 18-25) esta incluido en la migracion inicial, no hay que insertarlas a mano.

---

## Como correr el proyecto

1. Configurar la conexion en `appsettings.json`:

```json
"ConnectionStrings": {
  "CalculadoraDB": "Server=.;Database=CalculadoraDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

2. Correr las migraciones:

```
Update-Database
```

3. Ejecutar `StoredProcedures.sql` en SSMS contra la BD `CalculadoraDB`

4. Correr el proyecto
