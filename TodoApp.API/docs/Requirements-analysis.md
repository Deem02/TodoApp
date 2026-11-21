# Phase 0: Requirements Analysis | To-Do List API

## 1. Actors and Their Responsibilities

### User
A registered and authenticated user who can:
- Register and log in.
- Create, view, update, and manage their own tasks.
- View their deleted tasks.
- Restore their deleted tasks.
- View the history of their own tasks.
- View statistics about their tasks, including completion rate.

### Admin
An authenticated user with the Admin role who can:
- Log in to the system.
- View all registered users.
- View and search request logs.

---

## 2. User Stories and Acceptance Criteria

### 2.1 Accounts and Login

**Story 1: User Registration**

As a visitor, I want to register an account so that I can access the system and manage my tasks.


**Checks:**
- Registering with a valid, unused email and a valid password creates an account.
- Registering with an existing email is rejected.
- A password that does not meet the requirements (at least 8 characters, one letter, and one number) is rejected.

**Story 2: User Login**

As a registered user, I want to log in so that I can access my account and personal tasks.

**Checks:**
- Logging in with valid credentials returns a JWT token.
- Logging in with invalid credentials is rejected.
- The token expires after 60 minutes, based on the configured expiration setting.

### 2.2 Access Rules

**Story 3: Task Ownership**

As an authenticated user, I want to access only my own tasks so that my task data remains private.

**Checks:**
- A user can view and manage their own tasks.
- Attempting to access another user's task returns Not Found (404).
- Unauthenticated users cannot access protected task endpoints.

**Story 4: Admin Access**

As an Admin, I want to view all registered users so that I can monitor the system's users.

**Checks:**
- An Admin can retrieve the list of all users.
- A regular User cannot access the Admin users endpoint.

### 2.3 Task Management

**Story 5: Create a Task**

As a user, I want to create a task so that I can organize my work.

**Checks:**
- A task must have a title.
- A task can include a description, status, priority, and optional due date.
- Creating a task with a due date in the past is rejected.

**Story 6: View and Manage Tasks**

As a user, I want to view and update my tasks so that I can keep their information up to date.

**Checks:**
- A user can retrieve their own tasks.
- A user can update their task details.
- A user cannot update another user's task.

**Story 7: Search, Filter, Sort, and Paginate Tasks**

As a user, I want to search, filter, sort, and paginate my tasks so that I can find and organize them easily.

**Checks:**
- Tasks can be filtered by status, priority, and due date range.
- Tasks can be searched by title and sorted.
- Results are paginated, and the page size cannot exceed 50.

### 2.4 Soft Delete and Restore

**Story 8: Delete and Restore Tasks**

As a user, I want to soft-delete and restore my tasks so that I can recover tasks deleted by mistake.

**Checks:**
- Deleting a task hides it from the normal task list without permanently removing its data.
- A user can view their deleted tasks and restore them.
- A deleted task cannot be updated until it is restored.



### 2.5 Task History

**Story 9: View Task History**

As a user, I want to view the history of my tasks so that I can track changes made to them.

**Checks:**
- The history records task creation, updates, status changes, deletion, and restoration.
- Each history record includes what changed, the old and new values, who made the change, and when.
- History is displayed from newest to oldest and cannot be edited or deleted through the API.

### 2.6 Request Log

**Story 10: View and Search Request Logs**

As an Admin, I want to view and search request logs so that I can monitor API activity.

**Checks:**
- Each HTTP request is logged with its method, path, status code, duration, user (if authenticated), IP address, and timestamp.
- Admin can search logs by date range, status code, user, and path, with pagination.
- Passwords and authentication tokens are never recorded in logs.
- A regular User cannot access the Admin request logs endpoint.

### 2.7 Completion Rate and Stats

**Story 11: View Task Statistics**

As a user, I want to view statistics about my tasks so that I can track my progress.

**Checks:**
- Statistics include the count of tasks in each status, completion rate, and overdue task count.
- Statistics include the number of tasks completed per week for the last four weeks.
- Statistics can be limited to a selected date range.

**Completion rate formula:**

Completion Rate = (Number of Done Tasks / Total Non-Deleted Tasks) × 100

The result is rounded to two decimal places. If there's no tasks, the result is 0%.

---

## 3. General System Requirements

The following requirements apply across the system:

- All API responses follow a consistent response structure.
- API endpoints return appropriate HTTP status codes and clear error messages.
- Stack traces are never exposed to API clients.
- All dates are stored in UTC.
- Passwords are stored securely using password hashing, never as readable text.
- A default Admin account is created when the application starts for the first time.

---


## 4. Assumptions and Proposed Decisions

1. The default page size will initially be 10 tasks, with a maximum of 50 per page. 
2. User registration is assumed to be available through a public registration endpoint, while Admin account is created through initial database seeding.
3. Admin is assumed to have access only to the users list and request logs, with no task management permissions unless explicitly confirmed.
4. The required fields for the Admin users list are not explicitly specified. I propose returning User ID, Full Name, Email, Role, and Created At.
5. Soft-deleted tasks are excluded from normal task lists and completion statistics.

---

## 5. Open Questions

1. Can Admin create new user accounts, or is registration available only through the public registration endpoint?
2. Can Admin view or manage users' tasks, or is Admin access limited to viewing users and request logs?
3. Should task status and priority be required when creating a task, or should the system apply default values?
4. Should the task history display the actor's name and email, or only their User ID?
5. Can user roles be changed after an account is created?
6. Should DueDate store only a calendar date (DateOnly) or a specific date and time (DateTime)?
7. How should the selected date range be applied to statistics?
Should all statistics (Status Counts, Completion Rate, Overdue Count, and Weekly Completed Tasks) include only tasks created within the selected date range (CreatedAt)?

---

## 6. Edge Cases

| Scenario | Expected Behavior |
|---|---|
| A user tries to access another user's task. | Return 404 Not Found without revealing whether the task exists. |
| A user tries to restore a task that is not deleted. | Reject the request with an appropriate error response. |
| A user tries to update a deleted task. | Reject the update until the task is restored. |
| Saving task history fails after a task update. | Prevent inconsistent data; the task update and its history should be saved atomically. |
| A request log fails to save. | Do not fail or interrupt the original user request because of the logging failure. |
| A user in Riyadh creates a task due today. | The task should not be marked as overdue before the end of the due date in Riyadh time. |