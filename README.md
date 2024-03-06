# Restaurant Feedback Management System

This project aims to provide an efficient and user-friendly solution for managing restaurant feedback. It consists of three APIs: one for restaurant management, one for customer feedback, and one for responding to feedback. The system also utilizes Docker and Docker Compose for containerization and includes PostgreSQL as the database backend.

## Features

### Restaurant Management API

#### Create Restaurant
- Endpoint: `POST /api/restaurants`
- Description: Add a new restaurant to the system.
- Parameters:
  - Name: Name of the restaurant (required)
  - Location: Location of the restaurant (required)

#### Retrieve Restaurant
- Endpoint: `GET /api/restaurants/{id}`
- Description: Retrieve details of a specific restaurant, including its overall rating derived from customer feedback.

### Customer Feedback API

#### Submit Feedback
- Endpoint: `POST /api/feedbacks`
- Description: Submit feedback about a restaurant.
- Parameters:
  - RestaurantId: Unique identifier of the restaurant (required)
  - Comments: Feedback comments (optional)
  - Rating: Rating scale from 1 to 5 (required)

#### List Feedbacks
- Endpoint: `GET /api/feedbacks/{restaurantId}`
- Description: List all feedback entries for a specific restaurant, allowing users to view diverse customer experiences.

### Feedback Response API

#### Respond to Feedback
- Endpoint: `POST /api/feedback-responses`
- Description: Respond to individual feedback entries.
- Parameters:
  - FeedbackId: Unique identifier of the feedback (required)
  - Response: Response to the feedback entry (required)

## Docker Setup

This project utilizes Docker and Docker Compose for easy deployment and management. The Docker Compose file (`docker-compose.yml`) spawns PostgreSQL as the database.

## Development

During development, Swagger UI is available to explore and interact with the APIs. Access Swagger UI via `https://localhost:7065`.

## Getting Started

1. Clone this repository.
2. Ensure you have Docker and Docker Compose installed on your system.
3. Navigate to the project directory.
4. Run `docker-compose up` to start the application and PostgreSQL database.
5. Access the APIs and explore the functionality.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---
© 2024 Restaurant Feedback Management System Team
