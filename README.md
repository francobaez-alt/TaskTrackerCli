# Task Tracker CLI

A simple command-line task tracker built with **C#** and **.NET 10**.

This project is my solution to the **Task Tracker** challenge from roadmap.sh.

> **Project URL:** https://roadmap.sh/projects/task-tracker

---

## 📖 Overview

Task Tracker CLI is a console application that allows users to manage their daily tasks directly from the terminal.

Tasks are stored in a local JSON file, making the application lightweight and easy to use without requiring a database.

---

## ✨ Features

- ✅ Add new tasks
- ✏️ Update task descriptions
- 🗑️ Delete tasks
- 🚧 Mark tasks as **In Progress**
- ✔️ Mark tasks as **Done**
- 📋 List all tasks
- 🔍 Filter tasks by status:
  - Todo
  - In Progress
  - Done
- 💾 Automatic persistence using JSON
- 🆔 Auto-generated IDs
- 🕒 Automatic creation and update timestamps

---

## 🛠️ Technologies

- C#
- .NET 10
- System.Text.Json

---

## 📁 Project Structure

```text
TaskTrackerCli
│
├── Commands
│   └── CommandHandler.cs
│
├── Models
│   ├── ServiceResponse.cs
│   ├── TaskItem.cs
│   └── TaskStatus.cs
│
├── Services
│   └── TaskService.cs
│
├── Storage
│   └── JsonStorage.cs
│
├── Program.cs
├── README.md
├── .gitignore
└── Tasks.json
```

---

## 🚀 Getting Started

Clone the repository:

```bash
git clone https://github.com/francobaez-alt/TaskTrackerCli.git
```

Navigate to the project:

```bash
cd TaskTrackerCli
```

Build the project:

```bash
dotnet build
```

Run the application:

```bash
dotnet run
```

Or publish it as a standalone executable:

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

---

## 💻 Usage

### Add a task

```bash
task-cli add "Buy groceries"
```

### Update a task

```bash
task-cli update 1 "Buy groceries and milk"
```

### Delete a task

```bash
task-cli delete 1
```

### Mark a task as In Progress

```bash
task-cli markinprogress 1
```

### Mark a task as Done

```bash
task-cli markdone 1
```

### List all tasks

```bash
task-cli list
```

### List completed tasks

```bash
task-cli list done
```

### List pending tasks

```bash
task-cli list todo
```

### List tasks in progress

```bash
task-cli list inprogress
```

---

## 📄 Example

```text
> task-cli add "Learn C#"

Task added successfully (ID: 1)

> task-cli list

1 / Learn C# / Todo / CreatAt: 25/7/2026 22:32:39 / LastUpdate: 25/7/2026 22:32:39 
```

---

## 📦 Version

Current version:

```
v1.0.0
```

---

## 🎯 Learning Objectives

This project helped reinforce concepts such as:

- Command-line application development
- Object-Oriented Programming (OOP)
- File persistence with JSON
- Separation of concerns
- Git & GitHub workflow
- Semantic Versioning
- Building and publishing .NET applications

---

## 📜 License

This project was created for educational purposes as part of the roadmap.sh Backend Roadmap.
