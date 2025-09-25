# AGENTS

## Project Description

This project is a personal tool designed for image cleanup.  
The purpose is **not to categorize or tag images**, but to quickly go through collections and **remove unnecessary files**.  
The remaining images become easier to manage as a result.

### Architectural Principles (MVVM)

The application follows the **MVVM (Model-View-ViewModel) pattern**:

- **Model**

  - Represents image entities and file system operations.
  - Responsible for providing access to metadata, thumbnails, and file deletion logic.
  - Independent from UI concerns.

- **View**

  - Displays images and user controls for marking files as "keep" or "delete."
  - Keeps UI responsive and focuses solely on presentation.
  - Has no business logic.

- **ViewModel**
  - Acts as a mediator between the View and the Model.
  - Holds the current image collection state and deletion candidates.
  - Exposes commands (e.g., `MarkForDeletion`, `ConfirmDeletion`) to the View.
  - Ensures that the View is testable and decoupled from the file system.

This separation makes it easier to **maintain, extend, and test** the application over time.

## Guidelines

- Always add documentation comments to classes, methods, and properties, even without explicit instructions.
- Do not add other comments unless explicitly instructed.
- Follow the `.editorconfig` settings.
