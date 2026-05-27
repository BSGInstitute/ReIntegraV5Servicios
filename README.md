# BSI Integra V5 — Servicios API

API REST del sistema Integra V5, desarrollada en ASP.NET Core 6.0.

## Requisitos

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6)
- SQL Server (base de datos principal e interacciones)
- MongoDB _(opcional)_
- Acceso a las credenciales del equipo (solicitarlas al tech lead)

## Configuración inicial

### 1. Clonar el repositorio

```bash
git clone https://github.com/BSGInstitute/IntegraV5Servicios.git
cd IntegraV5Servicios
```

### 2. Crear el archivo de configuración

El archivo `appsettings.json` **no está incluido en el repositorio** por razones de seguridad. Existe un archivo de ejemplo con la estructura completa:

```bash
cp BSI.Integra.Servicios/appsettings.json.example BSI.Integra.Servicios/appsettings.json
```

### 3. Completar los valores en `appsettings.json`

Abrí el archivo recién creado y reemplazá cada placeholder con el valor real correspondiente:

#### TokenKey
Clave secreta para firmar los tokens JWT. Debe tener **mínimo 16 caracteres**.

```json
"TokenKey": "tu_clave_secreta_aqui"
```

#### ConnectionStrings
Cadenas de conexión a las dos bases de datos SQL Server.

```json
"ConnectionStrings": {
  "IntegraDB": "Server=<HOST>;Initial Catalog=integraDBData;User ID=<USUARIO>;password=<PASSWORD>;...",
  "IntegraDB_Interaccion": "Server=<HOST>;Initial Catalog=IntegraDBData_Interaccion;User ID=<USUARIO>;password=<PASSWORD>;..."
}
```

#### MongoDBSettings _(opcional)_
Conexión a la base de datos MongoDB. Solo requerida si trabajás con módulos que la utilizan.

```json
"MongoDBSettings": {
  "ConnectionString": "mongodb://<USUARIO>:<PASSWORD>@<HOST>:27017/?authSource=admin",
  "DatabaseName": "integraDBData"
}
```

#### GoogleAdsApi _(opcional)_
Solo requerido si trabajás con la integración de Google Ads. Credenciales disponibles en [Google Ads API Center](https://ads.google.com/home/tools/manager-accounts/).

```json
"GoogleAdsApi": {
  "DeveloperToken": "<TOKEN>",
  "OAuth2Mode": "APPLICATION",
  "OAuth2ClientId": "<CLIENT_ID>.apps.googleusercontent.com",
  "OAuth2ClientSecret": "<CLIENT_SECRET>",
  "OAuth2RefreshToken": "<REFRESH_TOKEN>",
  "LoginCustomerId": "<CUSTOMER_ID>"
}
```

#### Facebook _(opcional)_
Solo requerido si trabajás con la integración de Facebook/Meta. Access tokens generados desde [Meta for Developers](https://developers.facebook.com/).

```json
"Facebook": {
  "AccessToken": "<ACCESS_TOKEN>",
  "PixelId": "<PIXEL_ID>"
},
"FacebookFaseMaximaConstruccion": {
  "AccessToken": "<ACCESS_TOKEN>",
  "PixelId": "<PIXEL_ID>"
}
```

## Seguridad

- **Nunca** commitear `appsettings.json` con credenciales reales — está en `.gitignore`.
- Para nuevos entornos, siempre partir desde `appsettings.json.example`.
- Las credenciales de producción se gestionan fuera del repositorio.
