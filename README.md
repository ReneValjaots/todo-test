# Test Assignment

## Thought process and reasoning

### Backend
For the backend, I used .NET and implemented some patterns:
- **Repository Pattern**: The repository pattern was used to abstract data access logic, keeping it separate from the business logic. This makes the application more testable and maintainable.
- **DTOs (Data Transfer Objects)**: DTOs allow us to manage and control what data is exposed to the client. For instance, we shouldn't expose internal fields like `Id` and `CreationDate` during create/update operations.

The backend structure was designed with the following folders:
- **Controllers**: Contains the endpoints for interacting with the API.
- **Data**: Database context and initializers.
- **DTOs**: As explained before.
- **Models**: Represents database entities, like `Todo`.
- **Repositories & Interfaces**: Implements the repository pattern, separating data access logic from the rest of the application.

### Frontend
For the frontend, I chose **Nuxt 3** because I’ve worked with Nuxt before and am more comfortable using it than React. 

### Application Design
The application supports basic CRUD functionality with filtering for todos:
- Users can filter by `done`, `due date`, and `description text`.
- Filtering is optimized to handle 1 000 000 todos with ease.

Users can get sub Todos of a parent using a dedicated endpoint. This functionality is also implemented on the frontend using the "details" button on the "My Todos" page. 

### Docker & Deployment
The stack can be fully brought up using **Docker Compose**:

## Step-by-step guide to run
1) **Clone the repository**  Run the following command in your terminal:
    ```bash
   git clone https://github.com/ReneValjaots/todo-test.git

2) **Move to directory**  Run the following command in your terminal:
    ```bash
   cd todo-test

3) **Docker compose build**  Run the following command in your terminal:
    ```bash
   docker-compose build

4) **Docker compose up**  Run the following command in your terminal:
    ```bash
   docker-compose up

5) **Frontend url** After running these commands the frontend page should be accessible on URL: http://localhost:3333/
6) **Backend swagger** After running these commands the backend swagger page should be accessible on URL: http://localhost:5017/swagger/index.html
