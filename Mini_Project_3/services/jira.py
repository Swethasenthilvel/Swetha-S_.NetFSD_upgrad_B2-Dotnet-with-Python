from utils.decorators import log_call, retry
from config import MOCK_API
import random


# -------------------------------
# JIRA TICKET CREATION
# -------------------------------
@log_call
@retry(times=3, delay=1)
def create_jira_ticket(incident):
    """
    Creates a Jira ticket (mock or real).
    Returns ticket key (e.g., PROJ-123)
    """

    payload = {
        "summary": incident.title,
        "description": incident.description,
        "issuetype": {"name": "Bug"},
        "priority": {"name": incident.severity.upper()},
        "project": {"key": "IT"},
        "labels": [getattr(incident, "type", "general")]
    }

    # -------------------------------
    # MOCK MODE
    # -------------------------------
    if MOCK_API:
        print("JIRA PAYLOAD:", payload)
        return f"JIRA: IT-{random.randint(100, 999)}"

    # -------------------------------
    # REAL API (placeholder)
    # -------------------------------
    # Example:
    # response = requests.post(url, json=payload, headers=headers)
    # return response.json()["key"]

    return "REAL-JIRA-ID"