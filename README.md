# CSharpCumulativePart1 & CSharpCumulativePart2

This project was built as part of a course with Professor Christine Bittle at Humber College.  
It uses ASP.NET Core and connects to a MySQL database to manage teacher data. The app supports both Web API and MVC.  

This is Part 2 of the cumulative assignment and builds on Part 1 by adding features to create and delete teacher records.

---

## Part 1 – Read Features

In the first part, the focus was on reading teacher data from the database using both API and MVC.

**API Endpoints:**
- `GET /api/TeacherAPI` – Get a list of all teachers
- `GET /api/TeacherAPI/{id}` – Get details of one teacher

**MVC Pages:**
- `/TeacherPage/List` – Displays all teachers in a table
- `/TeacherPage/Show/{id}` – Shows one teacher and their assigned courses

Swagger was added to help test the API directly in the browser.  
The app uses Entity Framework to talk to the MySQL database.

---

## Part 2 – Add and Delete Features

This section added the ability to create new teachers and delete existing ones through both API and Razor views.

**API Endpoints:**
- `POST /api/TeacherAPI` – Add a new teacher
- `DELETE /api/TeacherAPI/{id}` – Delete a teacher by ID

**MVC Pages:**
- `/TeacherPage/New` – Simple form to add a teacher
- `/TeacherPage/DeleteConfirm/{id}` – Confirmation page before deleting a teacher

**Validations Added:**
- First and last name must not be empty
- Hire date must not be in the future
- Employee number must start with “T” followed by digits
- Employee number must be unique
- Teachers assigned to courses can’t be deleted

---

## Bonus Features

To go beyond the minimum requirements, a few extras were added:
- A search feature to filter teachers by hire date
- A ViewModel that shows a teacher’s details along with their courses
- A new `TeacherWorkPhone` field in the form and database
- Error messages when adding or deleting fails due to rules
- Basic layout and styling using Bootstrap for a cleaner UI

---

## Tech Stack

- ASP.NET Core (.NET 8)
- Entity Framework Core (Pomelo)
- MySQL
- Razor Views (MVC)
- Swagger (Swashbuckle for API testing)
- Bootstrap (for light styling)

---

## Running the App

1. Clone this repo
2. Open the solution in Visual Studio
3. Update the MySQL connection string in `appsettings.json`
4. Make sure your MySQL server is running and the `school` database is created
5. Hit F5 or click Run

**App Pages:**
- Go to `/swagger` to test the API  
- Go to `/TeacherPage/List` to see all teachers in the browser

---

This was a hands-on project to practice CRUD operations in ASP.NET Core, combining Web API and MVC with real database interaction.
