from utils.decorators import log_call, retry
from config import MOCK_API
import random


# -------------------------------
# AZURE BOARDS TICKET CREATION
# -------------------------------
@log_call
@retry(times=3, delay=1)
def create_azure_ticket(incident):
    """
    Creates an Azure DevOps work item (mock or real).
    Returns work item ID (int)
    """

    # -------------------------------
    # PRIORITY MAPPING
    # 1 = Critical, 2 = High, 3 = Medium, 4 = Low
    # -------------------------------
    priority_map = {
        "critical": 1,
        "high": 2,
        "medium": 3,
        "low": 4
    }

    payload = [
        {"op": "add", "path": "/fields/System.Title", "value": incident.title},
        {"op": "add", "path": "/fields/System.AssignedTo", "value": incident.assigned_team},
        {
            "op": "add",
            "path": "/fields/Microsoft.VSTS.Common.Priority",
            "value": priority_map.get(incident.severity, 4)
        }
    ]

    # -------------------------------
    # MOCK MODE
    # -------------------------------
    if MOCK_API:
        print("AZURE PAYLOAD:", payload)
        return f"AZURE:{random.randint(1000, 9999)}"

    # -------------------------------
    # REAL API (placeholder)
    # -------------------------------
    # Example:
    # response = requests.patch(url, json=payload, headers=headers)
    # return response.json()["id"]

    return 12345