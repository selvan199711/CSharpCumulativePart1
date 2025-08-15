# C# Cumulative Part 3 — Teachers (Update)

Small ASP.NET Core app that updates teachers in a MySQL DB. MySQL ADO.NET.

## What’s here
- **API:** `PUT /api/TeacherAPI/{id}` (and `GET /api/TeacherAPI/{id}` for the edit page)
- **MVC page:** `/TeacherPage/Edit/{id}` (AJAX PUT to the API)
- **DB access:** `Models/SchoolDbContext.cs` (reads `ConnectionStrings:SchoolDb`)
- **Model:** `Models/Teacher.cs` (short summaries on props)

## Run it
1. Put your MySQL string under `ConnectionStrings:SchoolDb` in `appsettings.json`.
2. Restore + run:
   - VS: *Ctrl+F5*
   - CLI: `dotnet run`
3. Swagger (dev only): `/swagger`

## API quick notes
- **Method:** `PUT /api/TeacherAPI/{id}`
- **Header:** `Content-Type: application/json`
- **Body (example):**
```json
{
  "teacherId": 2,
  "firstName": "Arun",
  "lastName": "Kumar",
  "employeeNumber": "EMP3435",
  "hireDate": "2024-08-15",
  "salary": 62000
}
```
