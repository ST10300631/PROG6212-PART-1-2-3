# PROG6212-PART-1-2-3
 ## RaceDay🏃‍♂️🚴‍♀️

RaceDay is a full-stack, cloud-aware, API-driven event management platform built specifically for the South African road running, walking, and cycling community. It replaces paper-based registrations and fragmented communication channels with a unified system where event organisers can manage events and categories, and participants can browse races, enrol, view live weather/route info, and track their performance history.

## 🛠  Features

**For Organisers:** Create and manage events, set race categories (distances), process registrations, and upload participant results.

**For Participants:** Discover upcoming local races, enter events securely, track personal race history, and view integrated weather and route data.

**Modern Architecture:** Fully containerized (Docker), API-driven backend, and prepared for cloud deployment.

## 🗺 API Endpoint Plan

### RaceDay Event Management System — API Endpoint Plan

| HTTP Method | Route | Description | Role Required | Request Body (if any) | Expected Response |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **POST** | `/api/auth/register` | Register a new participant or organiser account | Public | `{name, email, password, role ('Participant'\|'Organiser'), phone}` | `201 Created: {token, user}` |
| **POST** | `/api/auth/login` | Authenticate user and issue JWT token | Public | `{email, password}` | `200 OK: {token, user}` |
| **POST** | `/api/auth/logout` | Invalidate current session / token blacklist | Authenticated | None | `200 OK: {message}` |
| **GET** | `/api/users/profile` | Retrieve current authenticated user profile | Authenticated | None | `200 OK: {id, name, email, role, phone, createdAt}` |
| **PUT** | `/api/users/profile` | Update user profile information | Authenticated | `{name, phone, emergencyContact}` | `200 OK: {updatedUser}` |
| **GET** | `/api/events` | Browse and filter upcoming road events (search, location) | Public | Query params: `search, startDate, location` | `200 OK: [{id, title, date, location, organiserId}]` |
| **POST** | `/api/events` | Create a new road running/cycling event | Organiser | `{title, description, date, location, routeInfo, weatherData}` | `201 Created: {event}` |
| **GET** | `/api/events/{id}` | Get detailed info for a specific event including weather/route | Public | None | `200 OK: {event, categories, weatherForecast}` |
| **PUT** | `/api/events/{id}` | Update event details | Organiser (Owner) | `{title, description, date, location, routeInfo}` | `200 OK: {updatedEvent}` |
| **DELETE** | `/api/events/{id}` | Cancel or delete an event | Organiser (Owner) | None | `200 OK: {message}` |
| **GET** | `/api/events/{eventId}/categories` | List categories for an event (e.g. 10km, 21km, 50km Cycle) | Public | None | `200 OK: [{id, eventId, name, distanceKm, entryFee}]` |
| **POST** | `/api/events/{eventId}/categories` | Add a category to an event | Organiser (Owner) | `{name, distanceKm, entryFee, maxParticipants}` | `201 Created: {category}` |
| **PUT** | `/api/categories/{id}` | Update event category details | Organiser | `{name, distanceKm, entryFee, maxParticipants}` | `200 OK: {updatedCategory}` |
| **DELETE** | `/api/categories/{id}` | Remove a category from an event | Organiser | None | `200 OK: {message}` |
| **POST** | `/api/enrolments` | Enter / enrol in an event category | Participant | `{eventId, categoryId, emergencyDetails}` | `201 Created: {enrolmentId, status, paymentRef}` |
| **GET** | `/api/enrolments/my-history` | Track personal performance & entry history | Participant | None | `200 OK: [{enrolmentId, eventTitle, category, date, status}]` |
| **GET** | `/api/events/{eventId}/enrolments` | List all participant entries for an event | Organiser (Owner) | None | `200 OK: [{enrolmentId, participantName, category, status}]` |
| **DELETE** | `/api/enrolments/{id}` | Cancel event enrolment / entry | Participant / Owner | None | `200 OK: {message}` |
| **GET** | `/api/events/{eventId}/results` | Browse public results for an event category | Public | Query params: `categoryId` | `200 OK: [{rank, participantName, finishTime, category}]` |
| **POST** | `/api/results` | Upload participant finish results or times | Organiser | `{enrolmentId, finishTime, splitTimes, position}` | `201 Created: {result}` |
| **PUT** | `/api/results/{id}` | Correct or update recorded result | Organiser | `{finishTime, position, status}` | `200 OK: {updatedResult}` |


Below is the structured RESTful API design mapping out the system requirements for the RaceDay platform.

## 🏗 Tech Stack

**Backend:** C# (.NET Core) (Choose yours)

**Database:**  MySQL

**🚀 Getting Started**


 ```
 bash

 git clone https://github.com/ST10300631/PROG6212-PART-1-2-3.git


```

## YouTube Link


