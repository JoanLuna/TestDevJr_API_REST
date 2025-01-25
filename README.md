# Proyecto CRUD: API y Frontend

Este proyecto es un sistema CRUD (Crear, Leer, Actualizar y Eliminar) que incluye una **API REST** desarrollada con **ASP.NET Core** y un **Frontend** en **Angular**. Está diseñado para gestionar productos y usuarios con autenticación segura mediante JWT. El repositorio contiene dos carpetas principales: `MyApiRest` (backend) y `frontend-app-examen` (frontend).

---

## **Estructura del proyecto**

```plaintext
/ (Proyecto raíz)
├── MyApiRest                    # API REST
│   ├── Controllers              # Controladores (Auth y CRUD de productos)
│   ├── Data                    # Contexto de la base de datos
│   ├── Models                  # Modelos de entidades (User y Product)
│   ├── Services                # Servicios auxiliares (JWT, Password)
│   ├── Program.cs              # Configuración principal
│   ├── appsettings.json        # Configuración de la base de datos y JWT
│   └── Migrations              # Archivos de migración de Entity Framework
├── frontend-app-examen          # Aplicación Frontend en Angular
│   ├── src/app
│   │   ├── components          # Componentes del frontend
│   │   │   ├── login           # Componente de inicio de sesión
│   │   │   ├── register        # Componente de registro de usuarios
│   │   │   ├── dashboard       # Contenedor principal con barra de navegación
│   │   │   ├── shared          # Componentes compartidos (alertas)
│   │   │   └── modules         # Componentes específicos por módulo
│   │   │       ├── products
│   │   │           ├── product-list    # Lista de productos
│   │   │           ├── product-form    # Crear/editar productos
│   │   │           └── confirm-dialog  # Modal de confirmación
│   ├── services               # Servicios (Auth, Products, Alerts)
│   ├── app-routing.module.ts  # Configuración de rutas
│   └── app.module.ts          # Configuración principal
└── README.md                  # Archivo de documentación del proyecto
```

---

## **Instrucciones para clonar, configurar y ejecutar el proyecto**

### **1. Clonar el repositorio**

```bash
git clone <URL_DEL_REPOSITORIO>
cd <CARPETA_DEL_PROYECTO>
```

---

### **2. Configuración del backend**

#### **Requisitos previos**
- **SDK .NET 9.0.102**
- **SQLite** (opcional, incluido en el proyecto).

#### **Pasos**

1. Navegar a la carpeta del backend:
   ```bash
   cd MyApiRest
   ```

2. Restaurar las dependencias:
   ```bash
   dotnet restore
   ```

3. Crear la base de datos y aplicar las migraciones:
   ```bash
   dotnet ef database update
   ```

4. Ejecutar el proyecto:
   ```bash
   dotnet run
   ```

El backend estará disponible en: `http://localhost:5295`.

---

### **3. Configuración del frontend**

#### **Requisitos previos**
- **Node.js (versión 22.11.0)**
- **npm (versión 10.9.0)**
- **Angular CLI (versión 16.2.16)**

#### **Pasos**

1. Navegar a la carpeta del frontend:
   ```bash
   cd frontend-app-examen
   ```

2. Instalar las dependencias:
   ```bash
   npm install
   ```

3. Ejecutar el servidor de desarrollo:
   ```bash
   ng serve
   ```

El frontend estará disponible en: `http://localhost:4200`.

---

## **Tecnologías utilizadas**

### **Backend (API)**
- **Lenguaje:** C#
- **Framework:** ASP.NET Core 6+
- **ORM:** Entity Framework Core
- **Autenticación:** JSON Web Tokens (JWT)
- **Hashing:** BCrypt
- **Base de Datos:** SQLite
- **Documentación de API:** Swagger

### **Frontend**
- **Framework:** Angular (versión 16.2.16)
- **Lenguaje:** TypeScript (versión 5.1.6)
- **UI:** Angular Material (versión 16.2.14)
- **Programación Reactiva:** RXJS (versión 7.8.1)
- **Formularios:** Reactive Forms
- **Peticiones HTTP:** HttpClientModule

---

## **Decisiones de diseño**

1. **Base de datos SQLite:**
   - Elegida por ser ligera y fácil de configurar durante el desarrollo.
   - Compatible con migraciones y escalable hacia otras bases de datos (por ejemplo, SQL Server o MySQL).

2. **JWT para autenticación:**
   - Se utiliza para manejar sesiones seguras y sin estado.
   - Los tokens se generan en el backend y se verifican en cada solicitud.

3. **Angular Material:**
   - Proporciona componentes predefinidos y estilizados para una experiencia de usuario consistente y moderna.

4. **Arquitectura modular:**
   - El frontend está estructurado en módulos y componentes reutilizables para mantener el código limpio y organizado.

---

## **Ejecución del proyecto**

1. **Iniciar el backend:**
   ```bash
   dotnet run --project MyApiRest
   ```

2. **Iniciar el frontend:**
   ```bash
   ng serve --project frontend-app-examen
   ```

3. Acceder a la aplicación desde el navegador:
   - **Frontend:** `http://localhost:4200`
   - **Backend (API interactiva):** `http://localhost:5295/swagger`

---

¿Dudas o mejoras? Si necesitas ayuda para configurar o entender alguna parte del proyecto, ¡házmelo saber!
