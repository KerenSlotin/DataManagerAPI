# DataRetriever API

This project is a multi-layered data retrieval service using .NET, MongoDB, Redis, and Docker.

## 🚀 Getting Started

### Prerequisites

- [Docker](https://www.docker.com/)
- [.NET SDK 9.0](https://dotnet.microsoft.com/en-us/download)
- (Optional) [Postman](https://www.postman.com/downloads/) for testing API endpoints

---

## 📦 Running the Service
1. **Navigate to the project folder**:

   ```bash
   cd DataRetriever
   ```

2. **Start all services using Docker Compose**:

   ```bash
   docker compose up --build
   ```

   This will build the application and start the API along with Redis and MongoDB.

3. **Access the API documentation via Swagger**:

   Open your browser and go to:

   ```
   http://localhost:8080/swagger/index.html
   ```

---

## 🧪 Testing the API with Postman

You can also test the endpoints using Postman:

1. Import the following files into Postman (they're inside the main folder)
   - `DataApiCollection.postman_collection.json`
   - `DataAPIEnv.postman_environment.json`

2. Set the environment to `DataAPIEnv` and run requests from the collection.

---

## 🛠️ Tech Stack

- .NET 9.0
- MongoDB
- Redis
- Docker & Docker Compose

---

## 🧹 Cleaning Up

To stop and remove all containers:

```bash
docker compose down
```

---

## 🔐 Notes

Make sure port `8080` is not already in use on your machine before running the service.

