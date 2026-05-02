# 🥐 Belle Croissant Lyonnais — Sistema de Gestión de Productos

> Proyecto de escritorio en C# Windows Forms para gestionar el catálogo de productos de una panadería francesa.

---

## 📋 Descripción

Aplicación de escritorio desarrollada en **C# (.NET) con Windows Forms** que permite gestionar el catálogo de productos de Belle Croissant Lyonnais. Soporta las operaciones CRUD (Crear, Leer, Actualizar, Eliminar) con persistencia local mediante un archivo **JSON**, sin uso de bases de datos ni APIs externas.

---

## 🛠️ Tecnologías

- **Lenguaje:** C#
- **Framework:** .NET — Windows Forms
- **Persistencia:** Archivo JSON local (`products.json`)
- **Librería de serialización:** `System.Text.Json` o `Newtonsoft.Json`

---

## 🗂️ Estructura del Proyecto

```
BelleCroissantLyonnais/
├── Models/
│   └── Product.cs                 # Clase modelo de datos
├── Repository/
│   └── ProductRepository.cs       # Acceso y persistencia en JSON
├── Services/
│   └── ProductService.cs          # Lógica de negocio
├── Forms/
│   ├── frmProductList.cs          # Pantalla principal — listado
│   └── frmAddEditProduct.cs       # Formulario agregar/editar
├── products.json                  # Archivo de datos
└── README.md
```

### Flujo de comunicación

```
Formulario (UI) ➡ ProductService ➡ ProductRepository ➡ products.json
```

> ⚠️ Los formularios **nunca** acceden directamente al archivo JSON ni al Repository. Siempre pasan por el Service.

---

## 🧩 Modelo de Datos — `Product`

| Campo            | Tipo      | Obligatorio | Restricciones                        |
|------------------|-----------|:-----------:|--------------------------------------|
| `ProductName`    | string    | ✅          | Máximo 100 caracteres                |
| `Category`       | string    | ✅          | Seleccionar de lista predefinida     |
| `Price`          | decimal   | ✅          | Número positivo > 0                  |
| `Cost`           | decimal   | ✅          | Positivo y menor que `Price`         |
| `Description`    | string    | ❌          | Opcional                             |
| `Seasonal`       | bool      | ❌          | `true` / `false`                     |
| `Active`         | bool      | ❌          | `true` / `false`                     |
| `IntroducedDate` | DateTime  | ✅          | Fecha válida                         |

### Ejemplo de `products.json`

```json
[
  {
    "productName": "Croissant",
    "category": "Pastries",
    "price": 4.32,
    "cost": 3.30,
    "description": "Classic French croissant",
    "seasonal": false,
    "active": true,
    "introducedDate": "2024-01-15"
  }
]
```

---

## 🖥️ Funcionalidades

### Pantalla Principal — Listado de Productos (`frmProductList`)
- Tabla con columnas: `Active`, `ProductName`, `Category`, `Price`, `Cost`, `Action`
- Barra de búsqueda con filtrado en tiempo real por nombre o categoría
- Botón **"Add new product"** para abrir el formulario de creación
- Acciones por fila: **Edit** y **Delete**
- Ordenamiento por columna al hacer clic en el encabezado

### Formulario Agregar/Editar (`frmAddEditProduct`)
- Campos pre-cargados al editar un producto existente
- Validación de todos los campos con mensajes de error descriptivos
- Botón **Save**: guarda cambios y refresca el listado
- Botón **Cancel**: cierra sin guardar

### Eliminar Producto
- Diálogo de confirmación antes de eliminar
- Actualiza el JSON y refresca la tabla al confirmar

---

## ⚙️ Patrones de Diseño Utilizados

### Repository Pattern
Clase `ProductRepository` responsable exclusivamente de leer y escribir el archivo JSON.

```csharp
List<Product> GetAll()           // Lee el JSON y retorna todos los productos
void Save(List<Product> products) // Serializa la lista y escribe el JSON
```

### Service Layer
Clase `ProductService` que contiene la lógica de negocio entre la UI y el Repository.

```csharp
List<Product> GetAllProducts()
void AddProduct(Product p)
void DeleteProduct(int id)
List<Product> Search(string query)
```

---

## ✅ Criterios de Evaluación

| Criterio                                              | Puntos | %   |
|-------------------------------------------------------|:------:|:---:|
| Persistencia JSON correcta (carga y guardado)         | 25     | 25% |
| DataGridView: visualización, columnas y filtro        | 25     | 25% |
| Formulario Agregar/Editar con validaciones            | 20     | 20% |
| Funcionalidad Eliminar con confirmación               | 10     | 10% |
| Uso correcto de colecciones (`List<T>`, LINQ)         | 10     | 10% |
| Calidad del código y organización del proyecto        | 10     | 10% |
| **Total**                                             | **100**| **100%** |

---

## 📚 Temas de Estudio Requeridos

Antes de comenzar, investiga los siguientes temas:

- **JSON en C#:** `JsonSerializer.Serialize/Deserialize`, `File.ReadAllText/WriteAllText`, manejo de excepciones
- **DataGridView:** data binding, columnas personalizadas, eventos, ordenamiento
- **Colecciones:** `List<T>`, `Dictionary`, LINQ (`Where`, `OrderBy`, `Find`)
- **Controles WinForms:** `TextBox`, `ComboBox`, `NumericUpDown`, `DateTimePicker`, `CheckBox`, `MessageBox`
- **Validación:** campos obligatorios, rangos numéricos, longitud máxima
- **Patrones de diseño:** Repository, Service Layer, principio SOLID (SRP)

---

## 🚀 Orden de Implementación Sugerido

1. Crear la clase `Product` con todas sus propiedades
2. Implementar `ProductRepository` (lectura/escritura JSON)
3. Implementar `ProductService` (lógica de negocio)
4. Construir `frmProductList` con el `DataGridView`
5. Agregar barra de búsqueda con filtrado en tiempo real
6. Construir `frmAddEditProduct` con validaciones completas
7. Implementar la funcionalidad de eliminar con confirmación
8. Probar todos los flujos y verificar que el JSON persiste al reiniciar

---

## 📝 Notas

- No se permite el uso de bases de datos (SQL, SQLite, etc.)
- No se permite consumir APIs externas
- Toda la persistencia se realiza **únicamente** a través del archivo `products.json` local
