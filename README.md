# Test Assignment

## Thought process and reasoning

### Backend
For the backend, I used .NET Core and implemented some design patterns:
- **Repository Pattern**: The repository pattern was used to abstract data access logic and separate it from business logic. While this pattern may not have a significant impact on a small project like this, it ensures scalability and simplifies adding new models as the project grows.
- **DTOs (Data Transfer Objects)**: DTOs allow us to manage and control what data is exposed to the client. For instance, we shouldn't expose sensitive fields like `Id` and `CreationDate` during create/update operations.

The backend structure was designed with the following folders:
- **Controllers**: Contains the endpoints for interacting with the API.
- **Data**: Database context and initializers.
- **DTOs**: As explained before.
- **Models**: Represents database entities, like `Todo`.
- **Repositories & Interfaces**: Implements data logic with repository pattern.

Database
The application uses Microsoft SQL Server as the database.

The application initializes test data using a custom implementation of database initialization, which leverages generic base classes for creating tables and populating sample data. 
The database initialization is implemented with the following:
1) BaseDbContext: Used for defining database tables and setting column types.
2) DbInitializer: Handles test data creation.

**Modifying Test Data Count**
To change the number of test data entries generated in the database, modify the value in the Program.cs file:
```c#
await new TodoDbInitializer(todoDb, todoDb.Todos).Initialize(10_000, batchSize: 5000);
```

Replace 10_000 with the desired number of test entries and 500 with the desired batch size (default value is 1000).


### Frontend
For the frontend, I chose Nuxt 3 because I am familiar with it and find it more comfortable to use compared to other JavaScript frameworks. 

### Application Design
The application supports basic CRUD functionality with filtering for todos:
- Users can filter by `done / not done`, `due date`, and `description text`.

Users can retrieve subtodos of a parent todo using a dedicated endpoint. This functionality is also available on the frontend through the "Details" button on the "My Todos" page.

### API Endpoints

| Method | Endpoint                | Description                                   |
|--------|-------------------------|-----------------------------------------------|
| GET    | /api/todos              | Fetch all todos.                             |
| GET    | /api/todos/filter       | Filter todos by completion, due date, or text.|
| GET    | /api/todo/{id}          | Fetch a specific todo by its ID.             |
| PUT    | /api/todo/{id}          | Update a todo by its ID.                     |
| DELETE | /api/todo/{id}          | Delete a specific todo.                      |
| GET    | /api/todo/{id}/subtodos | Fetch subtodos of a specific parent todo.    |
| POST   | /api/todo               | Create a new todo.                           |

---

## Step-by-step guide to run
1) **Clone the repository**  Run the following command in your terminal:
    ```bash
   git clone https://github.com/ReneValjaots/todo-test.git
    ```

2) **Move to directory**  Run the following command in your terminal:
    ```bash
   cd todo-test
    ```

3) **Docker compose build**  Run the following command in your terminal:
    ```bash
   docker-compose build
    ```

4) **Docker compose up**  Run the following command in your terminal:
    ```bash
   docker-compose up
    ```

5) **Frontend url** After running these commands the frontend page should be accessible on URL: http://localhost:3333/
6) **Backend swagger** After running these commands the backend swagger page should be accessible on URL: http://localhost:5017/swagger/index.html
