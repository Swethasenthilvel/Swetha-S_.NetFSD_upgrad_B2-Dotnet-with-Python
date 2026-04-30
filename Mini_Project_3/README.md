# IT Incident Auto-Triage & Tracker

## 📌 Overview

This project is a Python-based CLI tool that automates IT incident classification and tracking.
It reads incident data from a JSON file, classifies each incident using regex-based logic,
creates tickets in multiple platforms (ServiceNow, Jira, Azure Boards), and generates a detailed HTML report.

---

## 🚀 Features

* ✅ Automatic incident classification (Network, Security, App)
* ✅ Severity detection (Critical, High, Medium, Low)
* ✅ Mock API integration (ServiceNow, Jira, Azure Boards)
* ✅ HTML dashboard report generation
* ✅ JSON export support
* ✅ CLI filtering using `--severity`
* ✅ Logging and retry mechanism using decorators
* ✅ Iterator and batch processing support

---

## 🛠️ Tech Stack

* Python 3.x
* Standard Libraries (argparse, json, datetime, logging, functools)
* Regex (`re` module)

---

## 📂 Project Structure

```
incident_tracker/
│
├── main.py
├── config.py
│
├── models/
│   ├── incident.py
│   └── report.py
│
├── services/
│   ├── jira.py
│   ├── servicenow.py
│   └── azure_boards.py
│
├── utils/
│   ├── classifier.py
│   ├── decorators.py
│   └── helpers.py
│
├── data/
│   └── incidents.json
│
├── output/
│   └── report.html
```

---

## ⚙️ Setup Instructions

### 1. Clone or Download Project

```
git clone <your-repo-url>
cd incident_tracker
```

### 2. Install Requirements

(No external libraries required)

---

## ▶️ How to Run

### Run full pipeline

```
python main.py
```

### Run with severity filter

```
python main.py --severity critical
```

---

## 📊 Output

### HTML Report

Generated at:

```
output/report.html
```

### JSON Report

Generated at:

```
output/report.json
```

---

## 🧠 How It Works

1. Load incidents from JSON
2. Validate input data
3. Classify incidents using regex
4. Create tickets (mock API)
5. Sort incidents by severity
6. Generate HTML + JSON reports

---

## 🔁 Key Concepts Used

* OOP (Inheritance, Polymorphism)
* Factory Pattern
* Iterators & Generators
* Decorators (Logging & Retry)
* Regex-based classification
* CLI argument parsing

---

## ⭐ Bonus Features

* JSON schema validation
* Severity-based filtering
* Batch processing generator
* Dynamic ticket ID generation

---

## 📌 Note

* All API integrations are mocked (no real API calls)
* Set `MOCK_API = True` in config.py

---

## 👩‍💻 Author

Swetha

---
