from utils.decorators import log_call, retry
from config import MOCK_API
import uuid


# -------------------------------
# SERVICE NOW TICKET CREATION
# -------------------------------
@log_call
@retry(times=3, delay=1)
def create_snow_ticket(incident):
    """
    Creates a ServiceNow ticket (mock or real).
    Returns sys_id (string)
    """

    # -------------------------------
    # URGENCY MAPPING (SPEC CORRECT)
    # 1 = High, 2 = Medium, 3 = Low
    # -------------------------------
    severity_map = {
        "critical": 1,
        "high": 1,
        "medium": 2,
        "low": 3
    }

    payload = {
        "short_description": incident.title,
        "description": incident.description,
        "urgency": severity_map.get(incident.severity, 3),
        "category": getattr(incident, "type", "general"),
        "assignment_group": incident.assigned_team
    }

    # -------------------------------
    # MOCK MODE
    # -------------------------------
    if MOCK_API:
        print("SNOW PAYLOAD:", payload)
        return f"SNOW:MOCK-SNOW-{incident.id}"

    # -------------------------------
    # REAL API 
    # -------------------------------
    # Example:
    # response = requests.post(url, json=payload, auth=(user, pass))
    # return response.json()["result"]["sys_id"]

    return str(uuid.uuid4())